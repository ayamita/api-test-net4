using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public HttpStatusCodeResult BadGateway()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway, "There is something wrong. please contact admin.");
        }

        public HttpStatusCodeResult Unauthorized()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You are not authorized to access this controller action.");
        }

        public HttpStatusCodeResult NotFound()
        {
            return HttpNotFound("Your request did not find.");
        }
    }
}
