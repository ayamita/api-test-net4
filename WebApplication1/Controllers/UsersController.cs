using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "https://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
    public class UsersController : Controller
    {
        [HttpGet]
        public async Task<JsonResult> GetUsersAsync()
        {
            JsonResult jsonResult;
            var response = await GetUsers();
            jsonResult = Json(response, JsonRequestBehavior.AllowGet);
           
            return jsonResult;
        }

        private async Task<List<User>> GetUsers()
        {
            List<User> lstUsers = new List<User>();

            using (var db = new dbTest())
            {
                await Task.Run(() => {
                    lstUsers = db.Users.ToList();
                });
                
                return lstUsers;
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> GetUsersAsyncById(int id)
        {
            JsonResult jsonResult;
            var response = await GetUsersById(id);
            jsonResult = Json(response, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        private async Task<User> GetUsersById(int id)
        {
            User users = new User();
            using (var db = new dbTest())
            {
                await Task.Run(() =>
                {
                    users = db.Users
                       .Where(x => x.id_user == id)
                       .FirstOrDefault();
                   
                });                                               
                
            }
            return users;

        }

        static HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<ActionResult> Login(string user, string password)
        {
            JsonResult jsonResult;
            var response = await Authentification(user, password);
            if (response.id_user == 0)
            {
                var myData = new
                {
                    status = 0,
                    msm = "Autentificación fallida"
                };

                jsonResult = Json(myData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var myData = new
                {
                    status = 1,
                    msm = "Autentificación exitosa"
                };

                jsonResult = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return (jsonResult);
        }

        private async Task<User> Authentification(string user, string password)
        {
            User usuarios = new User();
            using (var db = new dbTest())
            {
                await Task.Run(() =>
                {
                    var users = db.Users
                       .Where(x => x.user1 == user && x.password == password)
                       .Select(x => x.id_user)
                       .FirstOrDefault();

                    usuarios.id_user = Convert.ToInt32(users);
                });               
            }
            return usuarios;

        }







    }
}