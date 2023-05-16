﻿using Dapper;
using DevFreela.Core.DTO;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    private readonly ICachingService _cachingService;
    public SkillRepository(IConfiguration configuration, DevFreelaDbContext devFreelaDbContext, ICachingService cachingService)
    {
        _connectionString = configuration.GetConnectionString("DevFreelaConnection");
        _devFreelaDbContext = devFreelaDbContext;
        _cachingService = cachingService;
    }

    public async Task AddAsync(Skill skill)
    {
        await _devFreelaDbContext.Skills.AddAsync(skill);

        await _devFreelaDbContext.SaveChangesAsync();
        await _cachingService.SetAsync(skill.Id.ToString(), JsonConvert.SerializeObject(skill));

    }

    public async Task<List<SkillDTO>> GetAllAsync()
    {
        var key = "GetAllSkill";

        var cache = await _cachingService.GetAsync(key);

        if (!string.IsNullOrWhiteSpace(cache))
        {
            return JsonConvert.DeserializeObject<List<SkillDTO>>(cache);
        }

        using (var sqlConetion = new SqlConnection(_connectionString))
        {
            sqlConetion.Open();

            var script = "SELECT [Id],[Description] FROM Skills";

            var skill = await sqlConetion.QueryAsync<SkillDTO>(script);

            await _cachingService.SetAsync(key, JsonConvert.SerializeObject(skill));

            return skill.ToList();
        }
    }
}
