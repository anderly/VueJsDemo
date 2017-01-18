using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueJsDemo.Api.Filters;
using VueJsDemo.Api.Models;
using VueJsDemo.Api.Repository;

namespace VueJsDemo.Api.Controllers
{
    /// <summary>
    /// ContactsController
    /// </summary>
    [Authorize]
    [ValidatesModel]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _contactsRepository;

        /// <summary>
        /// ContactsController Constructor
        /// </summary>
        /// <param name="contactsRepository"></param>
        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        /// <summary>
        /// Get a list of all contacts
        /// </summary>
        /// <returns>List of Contacts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Contact>), 200)]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactsRepository.ListAsync();
        }

        /// <summary>
        /// Get (read) a single contact
        /// </summary>
        /// <param name="id">The Mobile Phone of the contact</param>
        /// <remarks>
        /// Note that the id is a string (Mobile Phone) and not an integer.
        ///  
        ///     GET /contacts/{id}
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetContacts")]
        [ValidateContactExists]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _contactsRepository.GetByIdAsync(id));
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="contact">The contact to create</param>
        /// <returns>New Created Contact</returns>
        /// <response code="201">Returns the newly created contact</response>
        /// <response code="400">If the contact is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(Contact), 201)]
        [ProducesResponseType(typeof(Contact), 400)]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            await _contactsRepository.AddAsync(contact);
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = contact.MobilePhone }, contact);
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="id">The Mobile Phone of the contact</param>
        /// <param name="contact">The contact</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateContactExists]
        public async Task<IActionResult> Put(string id, [FromBody] Contact contact)
        {
            await _contactsRepository.UpdateAsync(contact);
            return Ok(contact);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="id">The Mobile Phone of the contact</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateContactExists]
        public async Task<IActionResult> Delete(string id)
        {
            await _contactsRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}