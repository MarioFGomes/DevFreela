using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public ProjectRepository(DevFreelaDbContext devFreelaDbContext, IConfiguration configuration)
    {
        _devFreelaDbContext=devFreelaDbContext;
        _ConnectionString = configuration.GetConnectionString("DevFreelaConnection");
    }

    public async Task AddAsync(Project project)
    {
        await _devFreelaDbContext.Projects.AddAsync(project);

        await _devFreelaDbContext.SaveChangesAsync();
    }

    public async Task CreateComentAsync(ProjectComment comment)
    {
        await _devFreelaDbContext.ProjectComents.AddAsync(comment);

        await _devFreelaDbContext.SaveChangesAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        // Usando o Dapper

        using (var sqlConetion = new SqlConnection(_ConnectionString))
        {
            sqlConetion.Open();

            var script = "SELECT Id,Title,Description,CreatedAt,StartedAt,FinishedAt,Status FROM Projects";

            var skill = await sqlConetion.QueryAsync<Project>(script);

            return skill.ToList();
        };
    }

    public async Task<Project> GetByIdAsync(int id)
    {
       var project = await _devFreelaDbContext.Projects.Include(i=>i.Cliente).Include(f=>f.Freelancer).SingleAsync(i=>i.Id==id);
        return project;
    }

    public async Task SaveChangeAsync()
    {
        await _devFreelaDbContext.SaveChangesAsync();
    }
}
