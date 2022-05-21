using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IntentCADEMR.CustomValidation;

namespace IntentCADEMR.Models
{
    public class RegistrationInfoModel
    {
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        public string ClinicName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string MailingAddress { get; set; }
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PostalCode { get; set; }
       
        public string FaxNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Required")]
        public string MedicalSpecialty { get; set; }
        public string OtherMedicalSpecialty { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PracticeSetting { get; set; }
        public string OtherPracticeSetting { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PracticeYears { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Number of Patients")]
        public int PatientNumbers { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionPAD { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionCD { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionDM { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionRD { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionHF { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProportionCurrentSmoker { get; set; }



        [Required(ErrorMessage = "Required")]
        public string FamiliarCOMPASSTrial { get; set; }

        [ValidateCOMPASSTrialImpact]
        public string COMPASSTrialImpactValid { get; set; }
        public bool SomeCAD_PAD { get; set; }
        public bool SomeCAD_2RiskFactors { get; set; }
        public bool MostCAD_PAD { get; set; }
        public bool MostCAD_2RiskFactors { get; set; }
        public bool NoImpact { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ASA { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Statin { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ACEI { get; set; }
        [Required(ErrorMessage = "Required")]
        public string OAA { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Rivaroxaban { get; set; }


        [ValidateCADPatientsBarriers]
        public string BarriersCADPatientsValid { get; set; }
        public bool LackEvidenceBasedGuidelines { get; set; }
        public bool LackApplicabilityGuidelines { get; set; }
        public bool LackTime { get; set; }
        public bool OrganizationalInstitutional { get; set; }
        public bool ReimbursementFinancial { get; set; }
        public bool PatientAdherence { get; set; }
        public bool TreatmentAdverseEvents { get; set; }
        public bool NoneAbove { get; set; }

        [Required(ErrorMessage = "Required")]
        public string EducationalPracticalSolutions { get; set; }
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string RepeatPassword { get; set; }

    }
}