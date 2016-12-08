using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    public class MissionsDropDown
    {
        [Key]
        public int MissionID { get; set; }
        public String MissionName { get; set; }
    }
}