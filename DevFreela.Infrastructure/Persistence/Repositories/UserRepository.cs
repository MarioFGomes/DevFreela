using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.Caching;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        _devFreelaDbContext.Users.Add(user);  
        await _devFreelaDbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var cache = await _cachingService.GetAsync(email);

        if (!string.IsNullOrWhiteSpace(cache))
        {
            return JsonConvert.DeserializeObject<User>(cache);
        }

        var user=await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Email == email);
        await _cachingService.SetAsync(email, JsonConvert.SerializeObject(user));
        return user;
    }

    public async Task<User> GetByEmailPasswordAsync(string email, string PasswordHash)
    {
        var cache = await _cachingService.GetAsync(email);

        if (!string.IsNullOrWhiteSpace(cache))
        {
            return JsonConvert.DeserializeObject<User>(cache);
        }

        var user=await _devFreelaDbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == PasswordHash);
        await _cachingService.SetAsync(email, JsonConvert.SerializeObject(user));
        return user;
    }

    public async Task<User> GetByIdAsync(int Id)
    {
        var key = "UserId";
        var cache=await _cachingService.GetAsync($"{key}{Id.ToString()}");

        if (!string.IsNullOrWhiteSpace(cache))
        {
            return JsonConvert.DeserializeObject<User>(cache);
        }
        var user= await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Id == Id);
        await _cachingService.SetAsync($"{key}{Id.ToString()}", JsonConvert.SerializeObject(user));
        return user; 
    }

    public async Task SaveChangeAsync()
    {
      await _devFreelaDbContext.SaveChangesAsync();
      
    }
}
