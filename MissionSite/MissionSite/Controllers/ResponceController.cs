using MissionSite.DAL;
using MissionSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MissionSite.Controllers
{
    public class ResponceController : Controller
    {
        private Project1Context db = new Project1Context();

        // GET: Responce
        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Insert(String FirstName, String LastName, String Email, String Subject, String Message)
        {
            db.Database.ExecuteSqlCommand("INSERT INTO Responce (FirstName, LastName, Email, Subject, Message) Values ('" + FirstName + "', '" + LastName + "', '" + Email + "', '" + Subject + "', '" + Message + "')");

            return RedirectToAction("Contact", "Home");
        }
    }
}