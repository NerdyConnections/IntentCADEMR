using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.CustomValidation
{
    public class ValidateCOMPASSTrialImpact:ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (RegistrationInfoModel)validationContext.ObjectInstance;

            if ((gModel.SomeCAD_PAD == false) && (gModel.SomeCAD_2RiskFactors == false) && (gModel.MostCAD_PAD == false) && (gModel.MostCAD_2RiskFactors == false) && (gModel.NoImpact == false))
            {
                return new ValidationResult("Please select at least one checkbox to continue");
            }

            else
                return ValidationResult.Success;
        }
    }
}