using System;

namespace TestNg.Controllers.Contacts.Models
{
    public class ContactsServiceModel
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string Phonenumber { get; set; }
        public string numbertype { get; set; }
        public string city { get; set; }
        public string? adress { get; set; }
        public string? Other { get; set; }
        public bool isfavorite { get; set; }

    }
}
