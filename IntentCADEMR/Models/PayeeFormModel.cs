using IntentCADEMR.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.Models
{
    public class PayeeFormModel
    {
        public int UserID { get; set; }
      
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ChequePayableTo { get; set; }
       
        public string InternalRefNum { get; set; }
        [Required(ErrorMessage = "Required")]
        public string MailingAddr1 { get; set; }
        public string MailingAddr2 { get; set; }

        public string AttentionTo { get; set; }
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PostalCode { get; set; }
        [ValidateApplicableTax]
        public string ApplicableTax { get; set; }
        [ValidateTaxNumber]
        public string TaxNumber { get; set; }
        public string AdditionalInformation { get; set; }
    }
}