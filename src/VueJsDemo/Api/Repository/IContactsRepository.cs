using System.Collections.Generic;
using System.Threading.Tasks;
using VueJsDemo.Api.Models;

namespace VueJsDemo.Api.Repository
{
    public interface IContactsRepository
    {
        Task AddAsync(Contact item);
        Task<IEnumerable<Contact>> ListAsync();
        Task<Contact> GetByIdAsync(string key);
        Task UpdateAsync(Contact item);
        Task DeleteAsync(string Id);
    }
}