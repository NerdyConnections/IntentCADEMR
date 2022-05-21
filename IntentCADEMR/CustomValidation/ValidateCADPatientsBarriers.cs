using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntentCADEMR.CustomValidation
{
    public class ValidateCADPatientsBarriers: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (RegistrationInfoModel)validationContext.ObjectInstance;

            if ((gModel.LackEvidenceBasedGuidelines == false) && (gModel.LackApplicabilityGuidelines == false) && (gModel.LackTime == false) && (gModel.OrganizationalInstitutional == false) && (gModel.ReimbursementFinancial == false) && (gModel.PatientAdherence == false) && (gModel.TreatmentAdverseEvents == false) && (gModel.NoneAbove == false))
            {
                return new ValidationResult("Please select at least one checkbox to continue");
            }

            else
                return ValidationResult.Success;
        }
    }
}