using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public string UserType { get; set; }
        public int UserTypeID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ClinicName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Specialty { get; set; }


        public int SponsorID { get; set; }
        public string SponsorName { get; set; }

        public string SubmittedDate { get; set; }
        public string Comment { get; set; }
        public bool? Approved { get; set; }
        public bool? Activated { get; set; }
        public bool? Deleted { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
        public bool MOU { get; set; }
        public bool Payee { get; set; }
        public bool IsReportAdmin { get; set; }
    }
}