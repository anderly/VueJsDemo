using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueJsDemo.Api.Infrastructure;
using VueJsDemo.Api.Models;
using VueJsDemo.Api.Repository;
using WebApi2.Infrastructure;

namespace VueJsDemo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        public IContactsRepository ContactsRepository { get; set; }

        public ContactsController(IContactsRepository contactsRepository)
        {
            ContactsRepository = contactsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await ContactsRepository.GetAllAsync();
        }

        //[HttpGet]
        //public async Task<IActionResult> List()
        //{
        //    return Ok(Repository.GetAllAsync());
        //}

        [HttpGet("{id}", Name = "GetContacts")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await ContactsRepository.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.FailedValidation.ToString());
                }
                await ContactsRepository.AddAsync(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new Error
                {
                    Code = ErrorCode.CouldNotCreateItem.ToString(),
                    Message = ex.InnerException.Message
                });
            }
            //return Ok(item);
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = item.MobilePhone }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Contact item)
        {
            //if (item == null)
            //{
            //    return BadRequest();
            //}
            //var contactObj = await ContactsRepository.FindAsync(id);
            //if (contactObj == null)
            //{
            //    return NotFound();
            //}
            //await ContactsRepository.UpdateAsync(item);
            //return new NoContentResult();
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.FailedValidation.ToString());
                }
                var existingItem = await ContactsRepository.FindAsync(id);
                if (existingItem == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                await ContactsRepository.UpdateAsync(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var item = ContactsRepository.FindAsync(id);
                if (item == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                await ContactsRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
            return NoContent();

        }
    }
}