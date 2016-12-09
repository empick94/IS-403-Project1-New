using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MissionSite.Models;
using MissionSite.DAL;

namespace MissionSite.Controllers
{
    public class APIController : ApiController
    {
        public IEnumerable<Users> Get()
        {
            using (Project1Context entities = new Project1Context())
            {
                return entities.Users.ToList();
            }
        }

        public Users Get(int id)
        {
            using (Project1Context entities = new Project1Context())
            {
                return entities.Users.FirstOrDefault(u => u.UserID == id);
            }
        }
    }
}
