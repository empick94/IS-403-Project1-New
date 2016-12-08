using MissionSite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MissionSite.Models;
using System.Web.Security;

namespace MissionSite.Controllers
{
    public class HomeController : Controller
    {
        private Project1Context db = new Project1Context();//link to the database

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Missions()//page with dropdown for the mission you want to view
        {
            /*
            List<SelectListItem> mission = new List<SelectListItem>();
            mission.Add(new SelectListItem { Text = "Korea, Busan Mission", Value = "0" });
            mission.Add(new SelectListItem { Text = "Brazil, Rio De Janeiro Mission", Value = "1" });
            mission.Add(new SelectListItem { Text = "Czech/Slovak Mission", Value = "2" });
             * */
            IEnumerable<MissionsDropDown> ieMissions = db.Database.SqlQuery<MissionsDropDown>("SELECT MissionID, MissionName FROM Missions");

            List<SelectListItem> lMissions = new List<SelectListItem>();

            foreach (MissionsDropDown mission in ieMissions)
            {
                lMissions.Add(new SelectListItem { Text = mission.MissionName, Value = mission.MissionID.ToString() });
            }

            ViewBag.Mission = lMissions;

            return View();
        }

        [HttpPost]
        public ActionResult Missions(String Mission)
        {
            this.Session["Parameter"] = Mission;

            return RedirectToAction("missionFAQs", "Home", new { Mission = Mission });
        }

        public ActionResult Contact()//page for generic contact form
        {
            List<SelectListItem> subject = new List<SelectListItem>();
            subject.Add(new SelectListItem { Text = "Korea Busan Mission", Value = "0" });
            subject.Add(new SelectListItem { Text = "Brazil Rio De Janeiro Mission", Value = "1" });
            subject.Add(new SelectListItem { Text = "Czech/Slovak Mission", Value = "2" });
            subject.Add(new SelectListItem { Text = "Other", Value = "3" });
            ViewBag.Subject = subject;

            return View();
        }

        [Authorize]
        public ViewResult missionFAQs(string Mission)//loads facts for the selcted mission. Also has form for new question.
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            //get the user id from the cookie
            int userID = int.Parse(ticket.Name);


            //go to the mission given in the parameter
            Missions mission = db.Missions.Find(int.Parse(Mission));

            //this object is the one we'll pass to the view. it has user, misison, questions
            MissionMissionQuestions mmq = new MissionMissionQuestions();
            mmq.missions = mission;//set model mission = url mission

            //assing user to question
            mmq.user = new Users();
            mmq.user.UserID = userID;

            //get questions for this mission
            IEnumerable<MissionQuestions> questions = db.Database.SqlQuery<MissionQuestions>("select * from MissionQuestions where MissionID = " + mmq.missions.MissionID);
            //add questions to model
            mmq.missionQuestions = questions;

            return View(mmq);
        }

        //for question submit button on the mission questions page, returns back to mission faq
        [HttpPost]
        public ActionResult questionEdit(MissionMissionQuestions mmq)
        {
            if (mmq.question.Question != null)//as long as there is a question to add
            {
                MissionQuestions mq = new MissionQuestions();//the new/updated question we will add

                mq.MissionID = mmq.missions.MissionID;//mission parameter from url
                mq.UserID = 1;//hard coded for now. but it needs to be the logged in user.
                mq.Question = mmq.question.Question;//question from the form
                mq.UserID = mmq.user.UserID;//set the current user as the asker

                db.MissionQuestions.Add(mq);//add the new question
                db.SaveChanges();//save to DB

                return RedirectToAction("missionFAQs", new { Mission = mmq.missions.MissionID.ToString() });
            }
            return View(mmq);
        }

        //handles submit for answer edit button, returns back to mission faq
        [HttpPost]
        public ActionResult answerEdit(MissionMissionQuestions mmq)
        {
            /*if (mmq.missionQuestions.Any != null)//as long as there is a question to add
            {
                MissionQuestions mq = new MissionQuestions();//the new/updated question we will add

                mq.MissionID = mmq.missions.MissionID;//mission parameter from url
                mq.UserID = 1;//hard coded for now. but it needs to be the logged in user.
                mq.Question = mmq.question.Question;//question from the form
                mq.UserID = mmq.user.UserID;//set the current user as the asker

                db.MissionQuestions.Add(mq);//add the new question
                db.SaveChanges();//save to DB

                return RedirectToAction("missionFAQs", new { Mission = mmq.missions.MissionID.ToString() });
                return null;
            }*/
            return View(mmq);
        }

        public ViewResult Questions(string Question)
        {
            ViewBag.userQuestion = Question;
            return View("missionFAQs");
        }

        public ActionResult Location()
        {
            List<SelectListItem> location = new List<SelectListItem>();
            location.Add(new SelectListItem { Text = "Korea, Busan Mission", Value = "0" });
            location.Add(new SelectListItem { Text = "Brazil, Rio De Janeiro Mission", Value = "1" });
            location.Add(new SelectListItem { Text = "Czech/Slovak Mission", Value = "2" });
            ViewBag.Locations = location;

            return View();
        }

        public ActionResult Map(string Locations)
        {
            if (Locations.Equals("0"))
            {
                ViewBag.messageString = "Korea, Busan Mission";
                ViewBag.latitude = 35.1646501;
                ViewBag.longitude = 128.9317147;
            }
            else if (Locations.Equals("1"))
            {
                ViewBag.messageString = "Brazil, Rio De Janeiro Mission";
                ViewBag.latitude = -22.9109878;
                ViewBag.longitude = -43.7285266;
            }
            else
            {
                ViewBag.messageString = "Czech/Slovak Mission";
                ViewBag.latitude = 49.866006;
                ViewBag.longitude = 15.031587;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login(string Mission)
        {
            ViewBag.Mission = Mission;

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String email = form["UserEmail"].ToString();
            String password = form["Password"].ToString();

            IEnumerable<Users> ieUsers = db.Database.SqlQuery<Users>("SELECT UserID, UserEmail, Password, FirstName, LastName FROM Users;");

            foreach (Users user in ieUsers)
            {
                if (String.Equals(email, user.UserEmail) && String.Equals(password, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserID.ToString(), rememberMe);

                    return RedirectToAction("missionFAQs", "Home", new { Mission = this.Session["Parameter"] });
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserEmail,Password,FirstName,LastName")] Users users, bool rememberMe = false)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();

                String email = users.UserEmail;
                String password = users.Password;

                IEnumerable<Users> ieUsers = db.Database.SqlQuery<Users>("SELECT UserID, UserEmail, Password, FirstName, LastName FROM Users;");

                foreach (Users user in ieUsers)
                {
                    if (String.Equals(email, user.UserEmail) && String.Equals(password, user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.UserID.ToString(), rememberMe);

                        return RedirectToAction("missionFAQs", "Home", new { Mission = this.Session["Parameter"] });
                    }
                }

                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();//signs out of your account
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ajax()
        {
            return View();
        }

        public ActionResult DisplayUser()
        {
            IEnumerable<Users> ieUsers = db.Database.SqlQuery<Users>("SELECT UserID, UserEmail, Password, FirstName, LastName FROM Users;");
            foreach (Users users in ieUsers)
            {
                return Json(ieUsers, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
    }
}