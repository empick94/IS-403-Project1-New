using MissionSite.DAL;
using MissionSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MissionSite.Controllers
{
    public class ResponseController : Controller
    {
        private Project1Context db = new Project1Context();

        // GET: Response
        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Insert(String FirstName, String LastName, String Email, String Subject, String Message)
        {
            db.Database.ExecuteSqlCommand("INSERT INTO Response (FirstName, LastName, Email, Subject, Message) Values ('" + FirstName + "', '" + LastName + "', '" + Email + "', '" + Subject + "', '" + Message + "')");

            return RedirectToAction("Contact", "Home");
        }
    }
}