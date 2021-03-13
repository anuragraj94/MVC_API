using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using web_Api.Models;

namespace web_Api.Controllers
{
    public class UserController : ApiController
    {
        //http://localhost:2331/api/user?name=Anurag
        [HttpGet]
        public string Greet(string name)
        {
            return "Welcome " + name;
        }

        DatabaseContext db = new DatabaseContext();

        //api/user
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        //api/user/2
        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }

        //api/user
        [HttpPost]
        public HttpResponseMessage AddUser(User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //api/user
        [HttpPut]
        public HttpResponseMessage UpdateUser(User model,int id)
        {
            try
            {
                if (id==model.UserId)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                   // db.Users.Add(model);
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
               
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //api/user
        public HttpResponseMessage DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user!=null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }


    }
}
