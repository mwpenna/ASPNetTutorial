﻿using System.Collections.Generic;
using ASPNetCoreWebApiTutorial.Models;
using ASPNetCoreWebApiTutorial.Repository;
using System.Net;
using System.Web.Http;

namespace ASPNetCoreWebApiTutorial.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactController : ApiController
    {
        public ContactsRepository ContactsRepo = new ContactsRepository();

        public ContactController()
        {

        }

        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Contacts> GetAll()
        {
            return ContactsRepo.GetAll();
        }

        [Route("id/{id}", Name = "GetById")]
        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            var item = ContactsRepo.Find(id);
            if(item == null)
            {
                return NotFound();
            }

            return Json(item);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] Contacts item)
        {
            if(item == null)
            {
                return BadRequest("Contacts cannot be null");
            }
            ContactsRepo.Add(item);
            return CreatedAtRoute("GetById", new { id = item.MobilePhone }, item);
        }

        [Route("id/{id}")]
        [HttpPut]
        public IHttpActionResult Update(string id, [FromBody] Contacts item)
        {
            if(item == null)
            {
                return BadRequest("Contacts cannot be null");
            }

            var contactObj = ContactsRepo.Find(id);
            if(contactObj == null)
            {
                return NotFound();
            }
            ContactsRepo.Update(item);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("id/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            ContactsRepo.Remove(id);
        }
    }
}