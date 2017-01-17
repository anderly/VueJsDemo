using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueJsDemo.Api.Filters;
using VueJsDemo.Api.Models;
using VueJsDemo.Api.Repository;

namespace VueJsDemo.Api.Controllers
{
    //[Authorize]
    [ValidatesModel]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactsRepository.ListAsync();
        }

        [HttpGet("{id}", Name = "GetContacts")]
        [ValidateContactExists]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _contactsRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            await _contactsRepository.AddAsync(contact);
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = contact.MobilePhone }, contact);
        }

        [HttpPut("{id}")]
        [ValidateContactExists]
        public async Task<IActionResult> Put(string id, [FromBody] Contact contact)
        {
            await _contactsRepository.UpdateAsync(contact);
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        [ValidateContactExists]
        public async Task<IActionResult> Delete(string id)
        {
            await _contactsRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}