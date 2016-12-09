using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    [Table("Response")]
    public class Response
    {
        [Key]
        public int ResponseID { get; set; }

        [Required(ErrorMessage="Please enter your first name.")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please select a subject.")]
        public String Subject { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        public String Message { get; set; }
    }
}