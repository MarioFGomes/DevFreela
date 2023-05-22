using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _devFreelaDbContext;
    private readonly string? _ConnectionString;
    private readonly ICachingService _cachingService;
    public ProjectRepository(DevFreelaDbContext devFreelaDbContext, IConfiguration configuration, ICachingService cachingService)
    {
        _devFreelaDbContext=devFreelaDbContext;
        _ConnectionString = configuration.GetConnectionString("DevFreelaConnection");
        _cachingService=cachingService;
    }

    public async Task AddAsync(Project project)
    {
        try
        {
            Log.Information("Criando um novo Projeto");

            await _devFreelaDbContext.Projects.AddAsync(project);

            await _devFreelaDbContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Criando um novo Projeto:{project}");
        }
    }

    public async Task CreateComentAsync(ProjectComment comment)
    {
        try
        {
            Log.Information("Criando um novo comentario");

            await _devFreelaDbContext.ProjectComents.AddAsync(comment);

            await _devFreelaDbContext.SaveChangesAsync();

        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Erro Criando um novo comentario:{comment}");
        }
    }

    public async Task<List<Project>> GetAllAsync()
    {
        try
        {
            Log.Information("Pesquisando todos os projetos");

            var key = "GetAllProject";

            var cache = await _cachingService.GetAsync(key);

            if (!string.IsNullOrWhiteSpace(cache) && cache != "null")
            {
                return JsonConvert.DeserializeObject<List<Project>>(cache);
            }

            // Usando o Dapper

            using (var sqlConetion = new SqlConnection(_ConnectionString))
            {
                sqlConetion.Open();

                var script = "SELECT Id,Title,Description,CreatedAt,StartedAt,FinishedAt,Status FROM Projects";

                var skill = await sqlConetion.QueryAsync<Project>(script);

                await _cachingService.SetAsync(key, JsonConvert.SerializeObject(skill));

                return skill.ToList();
            };

        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Erro pesquisando todos os projetos");

            return new List<Project>();
        }
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        try
        {
            Log.Information("Pesquisando Projeto por Id");

            var key = "ProjectId"+id;
            var cache = await _cachingService.GetAsync($"{key}");

            if (!string.IsNullOrWhiteSpace(cache) && cache != "null")
            {
                var data= JsonConvert.DeserializeObject<Project>(cache, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                return data;
            }

            var project = await _devFreelaDbContext.Projects.Include(i => i.Cliente).Include(f => f.Freelancer).SingleAsync(i => i.Id == id);

            var projectJson = JsonConvert.SerializeObject(project, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                            NullValueHandling=NullValueHandling.Ignore
                        });

            await _cachingService.SetAsync($"{key}", projectJson);
            return project;

        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Erro pesquisando Projeto por Id:{id}");

            return new Project();
        }
    }

    public async Task SaveChangeAsync()
    {
        
        try
        {
            Log.Information("Salvando Alterações no Banco");

            await _devFreelaDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro salvando alterações no banco de dados");
        }
    }

    public async Task UpdateChangesAsync(Project project)
    {
        try
        {
            Log.Information("Atualizando Projeto");

          _devFreelaDbContext.Entry(project).State = EntityState.Modified;
            var key = "ProjectId" + project.Id;

             await _cachingService.RemoveAsync($"{key}");

            var projectJson = JsonConvert.SerializeObject(project, Formatting.Indented,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                           NullValueHandling = NullValueHandling.Ignore
                       });

            await _cachingService.SetAsync(key, projectJson);

            await _devFreelaDbContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Erro ao Atualizar Projeto:{project}");
        }
    }
}
