using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.CustomValidation
{
    public class ValidateTaxNumber : ValidationAttribute
    {
        //payment method no longer needed for this project

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var pfm = (PayeeFormModel)validationContext.ObjectInstance;
            //applicable tax is only required if payment method is to a company name
            if (pfm.PaymentMethod == "To a Company Name")
            {
                if (string.IsNullOrEmpty(pfm.TaxNumber))
                {
                    return new ValidationResult("Please enter tax number");
                }

            }
            return ValidationResult.Success;

        }
    }
}