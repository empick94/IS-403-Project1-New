using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MissionSite.Models;

namespace MissionSite.Models
{
    public class MissionMissionQuestions
    {
        //for a veiw to have a mission and all its questions
        public Missions missions { get; set; }//stores the mission info
        public IEnumerable<MissionQuestions> missionQuestions { get; set; }//stores all the questions
        public MissionQuestions question { get; set; }//for one new question from the page
    }
}