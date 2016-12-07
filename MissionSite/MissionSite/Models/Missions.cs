﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    [Table("Missions")]
    public class Missions
    {
        [Key]
        public  int MissionID { get; set; }
        public string MissionName { get; set; }
        public string MissionPresName { get; set; }
        public string MissionAddress { get; set; }
        public string Language { get; set; }
        public string Climate { get; set; }
        public string DominantReligion { get; set; }
        public string FlagOrSymbol { get; set; }
    }
}