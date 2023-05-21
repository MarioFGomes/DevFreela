using DevFreela.API.Filters;
using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DevFreela.Infrastructure.Persistence.Caching;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ElmahCore.Sql;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using DevFreela.Application.AutoMapper;
using ElmahCore;
using Elmah.Io.AspNetCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using DevFreela.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configurando  Authentication no Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Configurando a Injecão de Dependencia

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddStackExchangeRedisCache(r =>
{
    r.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddControllers(options=>options.Filters.Add(typeof(ValidationFilter))).AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());
builder.Services.AddHttpClient();



// Configurando a autenticação pegando BearerToken

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer= true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,

        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience= builder.Configuration["jwt:Audience"],
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
    };
});




//Configurando o MediatR
builder.Services.AddMediatR(s=>s.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly));

builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));

var ConnectionString = builder.Configuration.GetConnectionString("DevFreelaConnection");

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(ConnectionString));


// Adicionando o Health Check

//Aqui adicionamos o HealthChecks
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DevFreelaConnection"), name: "sqlserver", tags: new string[] { "db", "data", "sql" })
                .AddRedis(builder.Configuration.GetConnectionString("Redis"), name: "redis", tags: new string[] { "db", "data", "nosql" });

//Configurando a interface gráfica e o armazenamento do histórico
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(5);
    options.MaximumHistoryEntriesPerEndpoint(10);
    options.AddHealthCheckEndpoint("API com Health Checks", "/health");
}).AddInMemoryStorage(); //Aqui adicionamos o banco em memória


// Configurando o ElmahCore

builder.Services.Configure<ElmahIoOptions>(builder.Configuration.GetSection("ElmahIo"));
if (!string.IsNullOrWhiteSpace(builder.Configuration["ElmahIo:HeartbeatId"]))
{
    builder.Services
    .AddHealthChecks()
    .AddElmahIoPublisher(options =>
    {
        options.ApiKey = "d6f140e44abc45b7b34ed64990fd8027";
        options.LogId = new Guid("3f3d7951-bce0-4776-b577-0856c1ae27ef");
        options.HeartbeatId = "12edb67bca8647cca3abace7d73c7a84";
    });
}
builder.Services.AddElmahIo();


builder.Services.AddElmah<SqlErrorLog>(options =>
{
    options.ConnectionString = ConnectionString;
    options.Path = "/elmah";
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger= (Serilog.ILogger)new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(ConnectionString,
    sinkOptions:new MSSqlServerSinkOptions()
    {
        AutoCreateSqlTable = true,
        TableName="Logs"
    }).WriteTo.Console().CreateLogger();
}).UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = p => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options => { options.UIPath = "/dashboard"; }); //Aqui ativamos o serviço e o caminho da chamada

app.UseWhen(context=> context.Request.Path.StartsWithSegments("/elmah", StringComparison.OrdinalIgnoreCase),appBuilder=>
{
    appBuilder.Use(next =>
    {
        return async ctx =>
        {
            ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;
            await next(ctx);
        };
    });
});

app.UseElmahIo();

app.UseElmah();

app.Run();
