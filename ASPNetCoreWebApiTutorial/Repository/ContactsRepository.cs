using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNetCoreWebApiTutorial.Models;
using Raven.Client;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;

namespace ASPNetCoreWebApiTutorial.Repository
{
    public class ContactsRepository : IContactRepository
    {

        static List<Contacts> ContactList = new List<Contacts>();

        public void Add(Contacts item)
        {
            // ContactList.Add(item);
            this.RavenSession.SaveChanges();
        }

        public Contacts Find(string key)
        {
            return ContactList.Where(e => e.MobilePhone.Equals(key)).SingleOrDefault();
        }

        public IEnumerable<Contacts> GetAll()
        {
            return ContactList;
        }

        public void Remove(string Id)
        {
            var itemToRemove = ContactList.SingleOrDefault(r => r.MobilePhone == Id);
            if(itemToRemove != null)
            {
                ContactList.Remove(itemToRemove);
            }
        }

        public void Update(Contacts item)
        {
            var itemToUpdate = ContactList.SingleOrDefault(r => r.MobilePhone == item.MobilePhone);
            if(itemToUpdate != null)
            {
                itemToUpdate.FirstName = item.FirstName;
                itemToUpdate.LastName = item.LastName;
                itemToUpdate.IsFamilyMember = item.IsFamilyMember;
                itemToUpdate.Company = item.Company;
                itemToUpdate.JobTitle = item.JobTitle;
                itemToUpdate.Email = item.Email;
                itemToUpdate.MobilePhone = item.MobilePhone;
                itemToUpdate.DateOfBirth = item.DateOfBirth;
                itemToUpdate.AnniversaryDate = item.AnniversaryDate;
            }
        }

        private IDocumentSession RavenSession
        {
            get { return HttpContext.Current.GetOwinContext().Get<IDocumentSession>(); }
        }
    }
}