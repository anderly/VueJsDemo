using System.Collections.Generic;
using System.Threading.Tasks;
using VueJsDemo.Api.Models;

namespace VueJsDemo.Api.Repository
{
    public interface IContactsRepository
    {
        Task AddAsync(Contact item);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> FindAsync(string key);
        Task RemoveAsync(string Id);
        Task UpdateAsync(Contact item);

        bool CheckValidUserKey(string reqkey);
    }
}