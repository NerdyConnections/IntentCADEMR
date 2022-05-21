using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.Models
{
   
        public class ForgotPasswordModel
        {
            [Required(ErrorMessage = "Required")]
            [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
            public string UserName { get; set; }
        }
    
}