using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services
{
    public interface ICachingService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
        string Get(string key);
        void Set(string key, string value);
        void Refresh(string key);
        Task RefreshAsync(string key);
        void Remove(string key);
        Task RemoveAsync(string key);
    }
}
