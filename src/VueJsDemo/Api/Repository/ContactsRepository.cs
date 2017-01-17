using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VueJsDemo.Api.Contexts;
using VueJsDemo.Api.Models;

namespace VueJsDemo.Api.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AppDbContext _dbContext;

        public ContactsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Contact> GetByIdAsync(string key)
        {
            // ToDo - Integrate with EF Core, DbSet.Find not available yet
            return await _dbContext.Contacts
                .SingleOrDefaultAsync(c => c.MobilePhone.Equals(key));
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _dbContext.Contacts.AsNoTracking().ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var contact = _dbContext.Contacts.SingleOrDefault(c => c.MobilePhone.Equals(id));
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            _dbContext.Entry(contact).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}