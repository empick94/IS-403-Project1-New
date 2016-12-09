﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    //represents a logged in user
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
   
}