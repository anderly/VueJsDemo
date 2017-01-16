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
        ContactsContext _context;
        public ContactsRepository(ContactsContext context)
        {
            _context = context;
        }
        static List<Contact> ContactsList = new List<Contact>();

        public async Task AddAsync(Contact item)
        {
            //ContactsList.Add(item);
            _context.Contacts.Add(item);
            await _context.SaveChangesAsync();
        }

        public bool CheckValidUserKey(string reqkey)
        {
            var userkeyList = new List<string>();
            userkeyList.Add("28236d8ec201df516d0f6472d516d72d");
            userkeyList.Add("38236d8ec201df516d0f6472d516d72c");
            userkeyList.Add("48236d8ec201df516d0f6472d516d72b");

            if (userkeyList.Contains(reqkey))
            {
                return true;
            }
            return false;
        }

        public async Task<Contact> FindAsync(string key)
        {
            // ToDo - Integrate with EF Core, DbSet.Find not available yet
            //return ContactsList
            //    .Where(e => e.MobilePhone.Equals(key))
            //    .SingleOrDefault();
            return await _context.Contacts
                .SingleOrDefaultAsync(c => c.MobilePhone.Equals(key));
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            //ContactsList.Add(new Contacts() {FirstName ="Mithun", MobilePhone = "2345" });

            return await _context.Contacts.ToListAsync();
        }

        public async Task RemoveAsync(string Id)
        {
            // ToDo - Integrate with EF Core
            //var itemToRemove = ContactsList.SingleOrDefault(r => r.MobilePhone == Id);
            //if (itemToRemove != null)
                //ContactsList.Remove(itemToRemove);
            var itemToRemove = _context.Contacts.SingleOrDefault(c => c.MobilePhone.Equals(Id));
            if (itemToRemove != null)
            {
                _context.Contacts.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Contact item)
        {
            var itemToUpdate = _context.Contacts.SingleOrDefault(c => c.MobilePhone.Equals(item.MobilePhone));
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