using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _devFreelaDbContext;
    public UserRepository(DevFreelaDbContext devFreelaDbContext)
    {
        _devFreelaDbContext = devFreelaDbContext;
    }

    public async Task AddAsync(User user)
    {
        _devFreelaDbContext.Users.Add(user);
        _devFreelaDbContext.SaveChanges();

    }

    public async Task<User> GetByEmail(string email)
    {
        return await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Email == email);
    }

    public async Task<User> GetByEmailPasswordAsync(string email, string PasswordHash)
    {
        return await _devFreelaDbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == PasswordHash);
    }

    public async Task<User> GetByIdAsync(int Id)
    {
        return await _devFreelaDbContext.Users.SingleOrDefaultAsync(i => i.Id == Id);
    }

    public async Task SaveChangeAsync()
    {
      await _devFreelaDbContext.SaveChangesAsync();
    }
}
