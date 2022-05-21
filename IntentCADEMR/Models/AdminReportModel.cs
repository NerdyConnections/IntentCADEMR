using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntentCADEMR.Models
{
    public class AdminReportModel
    {
        public int? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ClinicName { get; set; } //null/incomplete/complete/losttofollowup
        public string MailingAddress { get; set; }

        public string City { get; set; }

        public string Province { get; set; }
        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Gender { get; set; }
        public string MedicalSpecialty { get; set; }
        public string MedicalSpecialtyOther { get; set; }

        public string Practice { get; set; }

        public string PracticeOther { get; set; }
        public string PracticeYears { get; set; }

        public int? PatientNumbers { get; set; }

        public string ProportionPAD { get; set; }
        public string ProportionCD { get; set; }
        public string ProportionDM { get; set; }
        public string ProportionRD { get; set; }
        public string ProportionHF { get; set; }
        public string ProportionCurrentSmoker { get; set; }
        public string FamiliarCOMPASSTrial { get; set; }

        public bool? SomeCAD_PAD { get; set; }
        public bool? SomeCAD_2RiskFactors { get; set; }
        public bool? MostCAD_PAD { get; set; }
        public bool? MostCAD_2RiskFactors { get; set; }
        public bool? NoImpact { get; set; }

        public string ASA { get; set; }
        public string Statin { get; set; }
        public string ACEI { get; set; }
        public string OAA { get; set; }
        public string Rivaroxaban { get; set; }

        public bool? LackEvidenceBasedGuidelines { get; set; }

        public bool? LackApplicabilityGuidelines { get; set; }
        public bool? LackTime { get; set; }
        public bool? OrganizationalInstitutional { get; set; }
        public bool? ReimbursementFinancial { get; set; }
        public bool? PatientAdherence { get; set; }
        public bool? TreatmentAdverseEvents { get; set; }
        public bool? NoneAbove { get; set; }
        public string EducationalPracticalSolutions { get; set; }
        public DateTime? SubmittedDate { get; set; }

    }
}