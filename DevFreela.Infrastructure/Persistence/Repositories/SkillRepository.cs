using Dapper;
using DevFreela.Core.DTO;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly string _connectionString;
    private readonly DevFreelaDbContext _devFreelaDbContext;
    public SkillRepository(IConfiguration configuration, DevFreelaDbContext devFreelaDbContext)
    {
        _connectionString = configuration.GetConnectionString("DevFreelaConnection");
        _devFreelaDbContext = devFreelaDbContext;
    }

    public async Task AddAsync(Skill skill)
    {
        await _devFreelaDbContext.Skills.AddAsync(skill);

        await _devFreelaDbContext.SaveChangesAsync();

    }

    public async Task<List<SkillDTO>> GetAllAsync()
    {

        using (var sqlConetion = new SqlConnection(_connectionString))
        {
            sqlConetion.Open();

            var script = "SELECT [Id],[Description] FROM Skills";

            var skill = await sqlConetion.QueryAsync<SkillDTO>(script);

            return skill.ToList();
        }
    }
}
