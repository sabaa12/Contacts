using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestNg.Controllers.Contacts.Models;


namespace TestNg.Controllers.Contacts
{
    [Authorize]
    [Route("contacts")]
    public class ContactsController : ApiController
    {

        private readonly IcontactsService _service;
        public ContactsController(IcontactsService service)
        {

            _service = service;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IEnumerable<ContactsServiceModel>> listContacts()
        {
            var userID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _service.ListContacts(userID); ;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(ContactsServiceModel model)
        {
            var id = await _service.CreateContact(model, this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Created(nameof(this.Create), id);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ContactsServiceModel> Details(int ID)
        {
            var userID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _service.ContactDetails(ID, userID);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task Delete(int ID)
        {
            await _service.DeleteContact(ID, this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task Edit(ContactsServiceModel model, int id)
        {
            await _service.EditContact(this.User.FindFirst(ClaimTypes.NameIdentifier).Value, id, model);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IQueryable<ContactsServiceModel>> Favorites()
            => await _service.Favorites(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        [Route("Search")]
        public async Task<IQueryable<ContactsServiceModel>> search(string name)
            =>
            await _service.Search(User.FindFirst(ClaimTypes.NameIdentifier).Value, name);

        [HttpPost]
        [Route("DefaultSearch")]
        public async Task<List<ContactsServiceModel>> DetalsSearch(string FirstName = null, string LastName = null, string Phonenumber = null, string city = null)
            =>
            await _service.DefaultSearch(User.FindFirst(ClaimTypes.NameIdentifier).Value, FirstName, LastName, Phonenumber, city);


    }
}
