﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MissionSite.Models;
using System.Data.Entity;

namespace MissionSite.DAL
{
    public class Project1Context : DbContext
    {
        public Project1Context() : base("Project1Context")
        {

        }

        public DbSet<Responce> Responces { get; set; }
    }
}