using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.Caching;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _devFreelaDbContext;
    private readonly ICachingService _cachingService;
    public UserRepository(DevFreelaDbContext devFreelaDbContext, ICachingService cachingService)
    {
        _devFreelaDbContext = devFreelaDbContext;
        _cachingService = cachingService;
    }

    public async Task AddAsync(User user)
    {
        try
        {
            Log.Information("adicionando novo usuario");

            _devFreelaDbContext.Users.Add(user);
            await _devFreelaDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var data=JsonConvert.SerializeObject(user);

            Log.Error(ex, $"Erro ao adicionar novo usuário:{data}");
        }
    }

    public async Task<User> GetByEmail(string email)
    {
        try
        {
            Log.Information($"Pesquisando usuario por email:{email}");

            var cache = await _cachingService.GetAsync(email);

            if (!string.IsNullOrWhiteSpace(cache))
            {
                return JsonConvert.DeserializeObject<User>(cache);
            }

            var user = await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Email == email);
            await _cachingService.SetAsync(email, JsonConvert.SerializeObject(user));
            return user;

        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Erro pesquisando user por email:{email}");

            return new User();
        }
    }

    public async Task<User> GetByEmailPasswordAsync(string email, string PasswordHash)
    {
        try
        {
            Log.Information("Pesquisando usuario por email e password");

            var cache = await _cachingService.GetAsync(email);

            if (!string.IsNullOrWhiteSpace(cache))
            {
                return JsonConvert.DeserializeObject<User>(cache);
            }

            var user = await _devFreelaDbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == PasswordHash);
            await _cachingService.SetAsync(email, JsonConvert.SerializeObject(user));
            return user;

        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Erro pesquisando usuario por email:{email} e password:{PasswordHash}");
            return new User();
        }
    }

    public async Task<User> GetByIdAsync(int Id)
    {
        try
        {
            Log.Information("Pesquisando user por Id");

            var key = "UserId";
            var cache = await _cachingService.GetAsync($"{key}{Id.ToString()}");

            if (!string.IsNullOrWhiteSpace(cache))
            {
                return JsonConvert.DeserializeObject<User>(cache);
            }
            var user = await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Id == Id);
            await _cachingService.SetAsync($"{key}{Id.ToString()}", JsonConvert.SerializeObject(user));
            return user;

        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Pesquisando user por Id:{Id}");

            return new User();
        }
    }

    public async Task SaveChangeAsync()
    {
      await _devFreelaDbContext.SaveChangesAsync();
      
    }
}
