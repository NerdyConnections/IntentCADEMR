using IntentCADEMR.Data;
using IntentCADEMR.Models;
using IntentCADEMR.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace IntentCADEMR.DAL
{
    public class UserRepository : BaseRepository
    {
        public string GetRoleByUserID(int UserID)
        {
            var userRole = Entities.UserInfoes.Where(x => x.UserID == UserID).Select(x => x.UserTypeLookUp.Description).SingleOrDefault();
            return userRole;


        }
        public bool Authenticate(string userName, string password)
        {


            // return Entities.Users.Count(i => i.Username == userName && i.Password == password) == 1;
            var user = Entities.Users.Where(x => x.Username == userName && x.Password == password).SingleOrDefault();
            if (user != null)
            {//if username and password exist but the delete flag is turned on, then the user is no longer active and should not be authenticated
                if (user.Deleted ?? false)
                {
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
                return false;

        }
        public string[] GetRolesAsArray(string userName)
        {
            return Entities.Users.First(u => u.Username == userName).Roles.Select(r => r.RoleName).ToArray();

        }
        public string GetRoles(string userName)
        {
            string retStr = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (string str in Entities.Users.First(u => u.Username == userName).Roles.Select(r => r.RoleName).ToList())
            {
                sb.Append(str).Append("|");

            }

            if (sb.Length > 0)
                retStr = sb.ToString().TrimEnd("|".ToCharArray());

            return retStr;
        }
        public bool IsActivated(string userName, string password)
        {


            // return Entities.Users.Count(i => i.Username == userName && i.Password == password) == 1;
            var user = Entities.Users.Where(x => x.Username == userName && x.Password == password).SingleOrDefault();
            if (user != null)
            {//if username and password exist but the delete flag is turned on, then the user is no longer active and should not be authenticated

                return true;

            }
            return false;

        }
        public UserModel GetUserDetails(string username)
        {
            var user = Entities.Users.Where(x => x.Username == username).SingleOrDefault();
            int userID = 0;
            bool MOU = false;
            bool Payee = false;
            bool isReportAdmin = false;
            if (user != null)
                userID = user.UserID;
            var userInfo = Entities.UserInfoes.Where(x => x.UserID == userID).SingleOrDefault();

            var userReg = Entities.UserRegistrations.Where(x => x.UserID == userID).SingleOrDefault();
            if (userReg != null)
            {
                MOU = userReg.MOU ?? false;
                Payee = userReg.PayeeForm ?? false;
            }
            string[] reportAdmins = ConfigurationManager.AppSettings["ReportAdmins"].Split(',');
            if (Array.IndexOf(reportAdmins, username) >= 0)
            {
                isReportAdmin = true;
            }

            if (userInfo != null)
            {


                return new UserModel()
                {
                    UserID = userInfo.UserID ?? 0,
                    // TerritoryID = userInfo.TerritoryID,
                    Username = username,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    EmailAddress = userInfo.EmailAddress,
                    ClinicName = userInfo.ClinicName,
                    Address = userInfo.Address,
                    Address2 = userInfo.Address2,
                    City = userInfo.City,
                    // Province = userInfo.Province,
                    PostalCode = userInfo.PostalCode,
                    Phone = userInfo.Phone,
                    Fax = userInfo.Fax,
                    Specialty = userInfo.Specialty,
                    MOU = MOU,
                    Payee = Payee,
                    IsReportAdmin = isReportAdmin,
                    SponsorID = userInfo.SponsorID ?? 0,
                    UserType = userInfo.UserType.ToString(),
                    Password = Util.Encryptor.Decrypt(user.Password)




                };//return usermodel object
            }
            else
                return null;
        }

        public bool IsUserExisted(string userName)
        {
            return Entities.Users.Where(u => u.Username.Equals(userName)).Count() > 0;
        }

        public bool AddUser(RegistrationInfoModel model)
        {
            if (IsUserExisted(model.UserName))
            {
                return false;
            }

            User user = new User();
            user.Username = model.UserName;
            user.Password = Encryptor.Encrypt(model.Password);
            // user.Activated = true;
            user.Deleted = false;
            user.ActivationDate = DateTime.Now;

            Entities.Users.Add(user);
            Entities.SaveChanges();

            RegistrationInfo info = new RegistrationInfo();
            info.UserID = user.UserID;
            info.FirstName = model.FirstName;
            info.LastName = model.LastName;
            info.ClinicName = model.ClinicName;
            info.MailingAddress = model.MailingAddress;
            info.City = model.City;
            info.Province = model.Province;
            info.PhoneNumber = model.PhoneNumber;
            info.FaxNumber = model.FaxNumber;
            info.Gender = model.Gender;
            info.MedicalSpecialty = model.MedicalSpecialty;
            info.MedicalSpecialtyOther = model.OtherMedicalSpecialty;
            info.Practice = model.PracticeSetting;
            info.PracticeOther = model.OtherPracticeSetting;
            info.PracticeYears = model.PracticeYears;
            info.PatientNumbers = model.PatientNumbers;
            info.ProportionPAD = model.ProportionPAD;
            info.ProportionCD = model.ProportionCD;
            info.ProportionDM = model.ProportionDM;
            info.ProportionRD = model.ProportionRD;
            info.ProportionHF = model.ProportionHF;
            info.ProportionCurrentSmoker = model.ProportionCurrentSmoker;

            info.FamiliarCOMPASSTrial = model.FamiliarCOMPASSTrial;

            info.SomeCAD_PAD = model.SomeCAD_PAD;
            info.SomeCAD_2RiskFactors = model.SomeCAD_2RiskFactors;
            info.MostCAD_PAD = model.MostCAD_PAD;
            info.MostCAD_2RiskFactors = model.MostCAD_2RiskFactors;
            info.NoImpact = model.NoImpact;

            info.ASA = model.ASA;
            info.Statin = model.Statin;
            info.ACEI = model.ACEI;
            info.OAA = model.OAA;
            info.Rivaroxaban = model.Rivaroxaban;

            info.LackEvidenceBasedGuidelines = model.LackEvidenceBasedGuidelines;
            info.LackApplicabilityGuidelines = model.LackApplicabilityGuidelines;
            info.LackTime = model.LackTime;
            info.OrganizationalInstitutional = model.OrganizationalInstitutional;
            info.ReimbursementFinancial = model.ReimbursementFinancial;
            info.PatientAdherence = model.PatientAdherence;
            info.TreatmentAdverseEvents = model.TreatmentAdverseEvents;
            info.NoneAbove = model.NoneAbove;
            info.EducationalPracticalSolutions = model.EducationalPracticalSolutions;
            info.SubmittedDate = DateTime.Now;


            Entities.RegistrationInfoes.Add(info);
            Entities.SaveChanges();


            UserInfo userInfo = new UserInfo();
            userInfo.UserID = user.UserID;
            userInfo.UserType = 1;
            userInfo.FirstName = model.FirstName;
            userInfo.LastName = model.LastName;
            userInfo.EmailAddress = model.UserName;
            userInfo.ClinicName = model.ClinicName;
            userInfo.Address = model.MailingAddress;
            userInfo.City = model.City;
            userInfo.Province = model.Province;
            userInfo.Phone = model.PhoneNumber;
            userInfo.Fax = model.FaxNumber;
            userInfo.SubmittedDate = DateTime.Now;


            Entities.UserInfoes.Add(userInfo);
            Entities.SaveChanges();

            return true;
        }
        public bool AddEmailCorrespondence(string EmailAddress)
        {
            if (Entities.EmailCorrespondences.Where(x => x.EmailAddress == EmailAddress).Count() == 0)
            {
                EmailCorrespondence ec = new EmailCorrespondence();
                ec.EmailAddress = EmailAddress;

                ec.OptIn = true;
                Entities.EmailCorrespondences.Add(ec);
                Entities.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }


        }
        public void AddPayeeForm(PayeeFormModel model)
        {

            PayeeInfo info = new PayeeInfo();
            info.UserID = model.UserID;
            info.PaymentMethod = model.PaymentMethod;
            info.ChequePayableTo = model.ChequePayableTo;
            info.InternalRefNum = model.InternalRefNum;
            info.MailingAddr1 = model.MailingAddr1;
            info.MailingAddr2 = model.MailingAddr2;
            info.AttentionTo = model.AttentionTo;
            info.City = model.City;
            info.Province = model.Province;
            info.PostalCode = model.PostalCode;
            info.ApplicableTax = model.ApplicableTax;
            info.TaxNumber = model.TaxNumber;
            info.AdditionalInformation = model.AdditionalInformation;
            info.LastUpdated = DateTime.Now;

            Entities.PayeeInfoes.Add(info);
            Entities.SaveChanges();

            UserRegistration userRegistration = Entities.UserRegistrations.Where(u => u.UserID == model.UserID).FirstOrDefault();
            if (userRegistration != null)
            {
                userRegistration.PayeeForm = true;
            }
            else
            {
                userRegistration = new UserRegistration();
                userRegistration.UserID = model.UserID;
                userRegistration.PayeeForm = true;
                Entities.UserRegistrations.Add(userRegistration);
            }
            Entities.SaveChanges();
            UserHelper.ReloadUser();
        }

        public PayeeFormModel GetPayeeForm(UserModel user)
        {
            PayeeFormModel model = new PayeeFormModel();
            PayeeInfo info = Entities.PayeeInfoes.Where(p => p.UserID == user.UserID).FirstOrDefault();

            model.UserID = info.UserID;
            model.PaymentMethod = info.PaymentMethod;
            model.ChequePayableTo = info.ChequePayableTo;
            model.InternalRefNum = info.InternalRefNum;
            model.MailingAddr1 = info.MailingAddr1;
            model.MailingAddr2 = info.MailingAddr2;
            model.AttentionTo = info.AttentionTo;
            model.City = info.City;
            model.Province = info.Province;
            model.PostalCode = info.PostalCode;
            model.ApplicableTax = info.ApplicableTax;
            model.TaxNumber = info.TaxNumber;
            model.AdditionalInformation = info.AdditionalInformation;

            return model;
        }

    }
}