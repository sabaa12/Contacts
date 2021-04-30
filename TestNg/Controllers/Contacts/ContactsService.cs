using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestNg.Controllers.Contacts.Models;
using TestNg.Data;
using TestNg.Data.Models;

namespace TestNg.Controllers.Contacts
{
    public class ContactsService : IcontactsService
    {
        private readonly ApplicationDbContext _context;

        public ContactsService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<int> CreateContact(ContactsServiceModel model, string userID)
        {
            contact addModel = new()
            {
                userID = userID,
                firstName = model.firstName,
                lastName = model.lastName,
                Gender = model.gender,
                Phonenumber = model.Phonenumber,
                numbertype = model.numbertype,
                City = model.city,
                Adress = model.adress,
                Other = model.Other,
                isfavorite = model.isfavorite,
            };
            _context.contacts.Add(addModel);
            await _context.SaveChangesAsync();


            return addModel.ID;
        }

        public async Task<IQueryable<ContactsServiceModel>> ListContacts(string userID)
            =>
             _context
            .contacts
            .Where(x => x.userID == userID)
            .Select(z => new ContactsServiceModel
            {
                ID = z.ID,
                firstName = z.firstName,
                lastName = z.lastName,
                gender = z.Gender,
                age = ((DateTime.Now - z.DateofBirth) / 365).ToString().Substring(0, 2),
                Phonenumber = z.Phonenumber,
                numbertype = z.numbertype,
                city = z.City,
                adress = z.Adress,
                Other = z.Other,
                isfavorite = z.isfavorite
            });


        public async Task<ContactsServiceModel> ContactDetails(int contactID, string userID)
            => _context
                .contacts
                .Where(x => x.userID == userID && x.ID == contactID)
                .Select(z => new ContactsServiceModel
                {
                    ID = z.ID,
                    firstName = z.firstName,
                    lastName = z.lastName,
                    gender = z.Gender,
                    age = ((DateTime.Now - z.DateofBirth) / 365).ToString().Substring(0, 2),
                    Phonenumber = z.Phonenumber,
                    numbertype = z.numbertype,
                    city = z.City,
                    adress = z.Adress,
                    Other = z.Other,
                    isfavorite = z.isfavorite
                }).FirstOrDefault();


        public async Task DeleteContact(int contactID, string userID)
        {
            var Contact = _context.contacts.Where(x => x.userID == userID && x.ID == contactID).FirstOrDefault();
            _context.Remove(Contact);
            await _context.SaveChangesAsync();
        }

        public async Task EditContact(string userID, int contactID, ContactsServiceModel model)
        {
            var contact = _context.contacts.Where(c => c.ID == contactID && c.userID == userID).FirstOrDefault();
            contact.firstName = model.firstName;
            contact.lastName = model.lastName;
            contact.numbertype = model.numbertype;
            contact.Other = model.Other;
            contact.Phonenumber = model.Phonenumber;
            contact.isfavorite = model.isfavorite;
            contact.Adress = model.adress;
            contact.City = model.city;
            contact.Gender = model.gender;
            await _context.SaveChangesAsync();

        }

        public async Task<IQueryable<ContactsServiceModel>> Favorites(string userID)
            => _context
                .contacts
                .Where(x => x.userID == userID && x.isfavorite)
                .Select(z => new ContactsServiceModel
                {
                    ID = z.ID,
                    firstName = z.firstName,
                    lastName = z.lastName,
                    gender = z.Gender,
                    age = ((DateTime.Now - z.DateofBirth) / 365).ToString().Substring(0, 2),
                    Phonenumber = z.Phonenumber,
                    numbertype = z.numbertype,
                    city = z.City,
                    adress = z.Adress,
                    Other = z.Other,
                    isfavorite = z.isfavorite
                });

        public async Task<IQueryable<ContactsServiceModel>> Search(string userID, string name)
         => _context
                .contacts
                .Where(x => x.userID == userID && x.firstName.Contains(name))
                .Select(z => new ContactsServiceModel
                {
                    ID = z.ID,
                    firstName = z.firstName,
                    lastName = z.lastName,
                    gender = z.Gender,
                    age = ((DateTime.Now - z.DateofBirth) / 365).ToString().Substring(0, 2),
                    Phonenumber = z.Phonenumber,
                    numbertype = z.numbertype,
                    city = z.City,
                    adress = z.Adress,
                    Other = z.Other,
                    isfavorite = z.isfavorite
                });
        public async Task<List<ContactsServiceModel>> DefaultSearch(string userID, string FirstName = null, string LastName = null, string Phonenumber = null, string city = null)
        {
            var expression = GetExpression(FirstName,LastName,Phonenumber,city);
            var querabel = _context
                .contacts
                .Where(x => x.userID == userID)
                .Select(z => new ContactsServiceModel
                {
                    ID = z.ID,
                    firstName = z.firstName,
                    lastName = z.lastName,
                    gender = z.Gender,
                    age = ((DateTime.Now - z.DateofBirth) / 365).ToString().Substring(0, 2),
                    Phonenumber = z.Phonenumber,
                    numbertype = z.numbertype,
                    city = z.City,
                    adress = z.Adress,
                    Other = z.Other,
                    isfavorite = z.isfavorite
                });

            var compExpressio = expression.Compile();
            var ret = new List<ContactsServiceModel>();
            foreach (var item in querabel)
            {
                if (compExpressio(item))
                {
                    ret.Add(item);
                }
            }

            return ret;
        }




        public Expression<Func<ContactsServiceModel, bool>> GetExpression(string FirstName = null, string LastName = null, string Phonenumber = null, string city = null)
        {
            ParameterExpression pe = Expression.Parameter(typeof(ContactsServiceModel), "Contact");
            Expression left;
            Expression right;
            Expression body1, body2, body3 = null;

            var list = new List<Expression>();

            if (FirstName != null)
            {
                left = Expression.Property(pe, typeof(ContactsServiceModel).GetProperty("firstName"));
                right = Expression.Constant(FirstName, typeof(string));
                list.Add(Expression.Equal(left, right));
            }

            if (LastName != null)
            {
                left = Expression.Property(pe, typeof(ContactsServiceModel).GetProperty("lastName"));
                right = Expression.Constant(LastName, typeof(string));
                list.Add(Expression.Equal(left, right));
            }

            if (Phonenumber != null)
            {
                left = Expression.Property(pe, typeof(ContactsServiceModel).GetProperty("Phonenumber"));
                right = Expression.Constant(Phonenumber, typeof(string));
                list.Add(Expression.Equal(left, right));
            }

            if (city != null)
            {
                left = Expression.Property(pe, typeof(ContactsServiceModel).GetProperty("city"));
                right = Expression.Constant(city, typeof(string));
                list.Add(Expression.Equal(left, right));
            }

            if (list.Count == 4)
            {
                body1 = Expression.AndAlso(list[0], list[1]);
                body2 = Expression.AndAlso(body1, list[2]);
                body3 = Expression.AndAlso(body2, list[3]);
            }
            if (list.Count == 3)
            {
                body1 = Expression.AndAlso(list[0], list[1]);
                body3 = Expression.AndAlso(body1, list[2]);
            }
            else if (list.Count == 2)
            {
                body3 = Expression.AndAlso(list[0], list[1]);
            }
            else if (list.Count == 1)
            {
                body3 = list[0];
            }

            Expression<Func<ContactsServiceModel, bool>> expression = Expression.Lambda<Func<ContactsServiceModel, bool>>(body3, new ParameterExpression[] { pe });

            //   Func<ContactsServiceModel, bool> compExpression = expression.Compile();



            return expression;
        }


    }
}
