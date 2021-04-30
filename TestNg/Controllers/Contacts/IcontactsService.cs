using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNg.Controllers.Contacts.Models;

namespace TestNg.Controllers.Contacts
{
    public interface IcontactsService
    {
        Task<IQueryable<ContactsServiceModel>> ListContacts(string userID);
        Task<int> CreateContact(ContactsServiceModel model,string userID);
        Task<ContactsServiceModel> ContactDetails(int contactID, string userID);
        Task DeleteContact(int contactID, string userID);
        Task EditContact(string userID, int contactID, ContactsServiceModel model);
        Task<IQueryable<ContactsServiceModel>> Favorites(string userID);
        Task<IQueryable<ContactsServiceModel>> Search(string userID,string name);
        Task<List<ContactsServiceModel>> DefaultSearch(string userID, string FirstName = null, string LastName = null, string Phonenumber = null, string city = null);
    }
}
