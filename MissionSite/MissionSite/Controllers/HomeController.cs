using MissionSite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MissionSite.Models;

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
            List<SelectListItem> mission = new List<SelectListItem>();
            mission.Add(new SelectListItem { Text = "Korea, Busan Mission", Value = "0" });
            mission.Add(new SelectListItem { Text = "Brazil, Rio De Janeiro Mission", Value = "1" });
            mission.Add(new SelectListItem { Text = "Czech/Slovak Mission", Value = "2" });
            ViewBag.Mission = mission;

            return View();
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

        public ViewResult missionFAQs(string Mission)//loads facts for the selcted mission. Also has form for new question.
        {
            if (Mission.Equals("0"))
            {
                ViewBag.messageString = "Korea Busan Mission";
                ViewBag.president = "Kenneth S. Barrow";
                ViewBag.address1 = "Korea Busan Mission";
                ViewBag.address2 = "Dongnae PO Box 73";
                ViewBag.address3 = "Busan-si";
                ViewBag.address4 = "Busan-gwangyeoksi 607-600";
                ViewBag.address5 = "SOUTH KOREA";
                ViewBag.language = "Korean";
                ViewBag.climate = "Moderate";
                ViewBag.religion = "Korean Buddhism";
                ViewBag.flag = "korea.png";
                ViewBag.missionID = 0;
            }

            else if (Mission.Equals("1"))
            {
                ViewBag.messageString = "Brazil Rio De Janeiro Mission";
                ViewBag.president = "Antônio Marcos Cabral de Sousa";
                ViewBag.address1 = "Brazil Rio De Janeiro Mission";
                ViewBag.address2 = "Rua Dois de Dezembro 78 salas 703/704";
                ViewBag.address3 = "Flamengo";
                ViewBag.address4 = "22220-040 Rio de Janeiro-RJ";
                ViewBag.address5 = "Brazil";
                ViewBag.language = "Portuguese";
                ViewBag.climate = "Hot and humid";
                ViewBag.religion = "Roman Catholicism";
                ViewBag.flag = "brazil.png";
            }
            else if (Mission.Equals("2"))
            {
                ViewBag.messageString = "Czech/Slovak Mission";
                ViewBag.president = "Jan Pohořelický";
                ViewBag.address1 = "Czech/Slovak Mission";
                ViewBag.address2 = "Badeniho 1";
                ViewBag.address3 = "160 00 Praha 6";
                ViewBag.address4 = "Czech Republic";
                ViewBag.language = "Czech and Slovak";
                ViewBag.climate = "Like Utah";
                ViewBag.religion = "Roman Catholicism";
                ViewBag.flag = "czech-slovak.jpg";
            }


            else
                ViewBag.messageString = "Other";



            return View();
        }

        //for question submit button on the mission questions page
        [HttpPost]
        public ActionResult missionFAQs(FormCollection form, string Mission)
        {
            MissionQuestions mq = new MissionQuestions();//the new/updated question we will add

            mq.MissionID = int.Parse(Mission);//mission parameter from url
            mq.UserID = 1;//hard coded for now. but it needs to be the logged in user.
            mq.Question = form["Question"].ToString();//question from the form

            db.MissionQuestions.Add(mq);//add the new question
            db.SaveChanges();//save to DB

            return View();
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
    }
}