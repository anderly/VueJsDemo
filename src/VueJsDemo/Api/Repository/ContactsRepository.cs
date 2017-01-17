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
        AppDbContext _dbContext;

        public ContactsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        static List<Contact> ContactsList = new List<Contact>();

        public async Task AddAsync(Contact item)
        {
            //ContactsList.Add(item);
            _dbContext.Contacts.Add(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Contact> GetByIdAsync(string key)
        {
            // ToDo - Integrate with EF Core, DbSet.Find not available yet
            //return ContactsList
            //    .Where(e => e.MobilePhone.Equals(key))
            //    .SingleOrDefault();
            return await _dbContext.Contacts
                .SingleOrDefaultAsync(c => c.MobilePhone.Equals(key));
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            //ContactsList.Add(new Contacts() {FirstName ="Mithun", MobilePhone = "2345" });

            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task DeleteAsync(string Id)
        {
            // ToDo - Integrate with EF Core
            //var itemToRemove = ContactsList.SingleOrDefault(r => r.MobilePhone == Id);
            //if (itemToRemove != null)
                //ContactsList.Remove(itemToRemove);
            var itemToRemove = _dbContext.Contacts.SingleOrDefault(c => c.MobilePhone.Equals(Id));
            if (itemToRemove != null)
            {
                _dbContext.Contacts.Remove(itemToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Contact item)
        {
            var itemToUpdate = _dbContext.Contacts.SingleOrDefault(c => c.MobilePhone.Equals(item.MobilePhone));
            //await TryUpdate
            if (itemToUpdate != null)
            {
                itemToUpdate.FirstName = item.FirstName;
                itemToUpdate.LastName = item.LastName;
                itemToUpdate.Company = item.Company;
                itemToUpdate.JobTitle = item.JobTitle;
                itemToUpdate.Email = item.Email;
                itemToUpdate.MobilePhone = item.MobilePhone;
                itemToUpdate.DateOfBirth = item.DateOfBirth;
                _context.Entry(itemToUpdate).State = EntityState.Modified;
                _context.Contacts.Update(itemToUpdate);
                await _context.SaveChangesAsync();
            }
        }
    }
}