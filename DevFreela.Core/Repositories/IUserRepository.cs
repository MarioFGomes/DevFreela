using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int Id);

    Task AddAsync(User user);

    Task<User> GetByEmail(string email);
    Task SaveChangeAsync();

    Task<User> GetByEmailPasswordAsync(string email, string PasswordHash);
}
