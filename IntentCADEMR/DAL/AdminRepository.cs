using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntentCADEMR.DAL
{
    public class AdminRepository : BaseRepository
    {
        public List<Models.AdminReportModel> GetAdminReport()
        {
            string[] TestUserIDs = new string[100];

            TestUserIDs = System.Configuration.ConfigurationManager.AppSettings["TestUserIDs"].Split(',');
            List<string> liTestUserIDs = new List<string>(TestUserIDs);
            var result = from p in Entities.RegistrationInfoes

                             //join pvd2 in Entities.PatientVisitDetailsV2P1 on p.id equals pvd2.PatientID
                             // join pvd3 in Entities.PatientVisitDetailsV3P1 on p.id equals pvd3.PatientID
                             // join pvd4 in Entities.PatientVisitDetailsV4P1 on p.id equals pvd4.PatientID
                         where !liTestUserIDs.Contains(p.UserID.ToString())
                         select new AdminReportModel
                         {
                             UserID = p.UserID,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             ClinicName = p.ClinicName,

                             MailingAddress = p.MailingAddress,
                             City = p.City,
                             //   Visit1CompletionDate = pv.GIntV1CompletionDate,
                             Province = p.Province,
                             // Visit2VisitDate = pvd2.VisitDate,
                             PhoneNumber = p.PhoneNumber,
                             //  Visit2CompletionDate = pv.GIntV2CompletionDate,
                             FaxNumber = p.FaxNumber,
                             // Visit3VisitDate = pvd3.VisitDate,
                             Gender = p.Gender,
                             //    Visit3CompletionDate = pv.GIntV3CompletionDate,
                             MedicalSpecialty = p.MedicalSpecialty,
                             // Visit4VisitDate = pvd4.VisitDate,
                             MedicalSpecialtyOther = p.MedicalSpecialtyOther,
                             // Visit4CompletionDate = pv.GIntV4CompletionDate,
                             Practice = p.Practice,
                             PracticeOther = p.PracticeOther,
                             PracticeYears = p.PracticeYears,
                             PatientNumbers = p.PatientNumbers,
                             ProportionPAD = p.ProportionPAD,
                             ProportionCD = p.ProportionCD,
                             ProportionDM = p.ProportionDM,
                             ProportionRD = p.ProportionRD,
                             ProportionHF = p.ProportionHF,
                             ProportionCurrentSmoker = p.ProportionCurrentSmoker,
                             FamiliarCOMPASSTrial = p.FamiliarCOMPASSTrial,
                             SomeCAD_PAD = p.SomeCAD_PAD,
                             SomeCAD_2RiskFactors = p.SomeCAD_2RiskFactors,
                             MostCAD_PAD = p.MostCAD_PAD,
                             MostCAD_2RiskFactors = p.MostCAD_2RiskFactors,
                             NoImpact = p.NoImpact,


                             ASA = p.ASA,
                             Statin = p.Statin,
                             ACEI = p.ACEI,
                             OAA = p.OAA,
                             Rivaroxaban = p.Rivaroxaban,

                             LackEvidenceBasedGuidelines = p.LackEvidenceBasedGuidelines,
                             LackApplicabilityGuidelines = p.LackApplicabilityGuidelines,
                             LackTime = p.LackTime,
                             OrganizationalInstitutional = p.OrganizationalInstitutional,
                             ReimbursementFinancial = p.ReimbursementFinancial,
                             PatientAdherence = p.PatientAdherence,
                             TreatmentAdverseEvents = p.TreatmentAdverseEvents,
                             NoneAbove = p.NoneAbove,

                             EducationalPracticalSolutions = p.EducationalPracticalSolutions,


                             SubmittedDate = p.SubmittedDate
                         };

            List<AdminReportModel> list = result.ToList();



            return list;
        }

    }
}