using IntentCADEMR.DAL;
using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IntentCADEMR.Util
{
    public class UserHelper
    {
        private static void LoadDataIntoSession()
        {
            // PhysicianController usrControler = new PhysicianController();
            UserRepository UserRepo = new UserRepository();

            Models.UserModel user = UserRepo.GetUserDetails(System.Web.HttpContext.Current.User.Identity.Name);

            if (user != null)
            {
                HttpContext.Current.Session[Constants.USER] = user;
            }

        }
        public static UserModel GetLoggedInUser()
        {
            if (HttpContext.Current.Session[Constants.USER] == null)

                LoadDataIntoSession();

            return HttpContext.Current.Session[Constants.USER] as UserModel;

        }
        public static string GetRoleByUserID(int UserID)
        {
            UserRepository UserRepo = new UserRepository();
            return UserRepo.GetRoleByUserID(UserID);

        }
        public static void ReloadUser()
        {
            LoadDataIntoSession();
        }
        //pass in a comma delimited string of roles and determine if current user is in any one of them
        public static bool IsInRole(string roles)
        {

            String[] ArRoles = roles.Split(',');
            var user = HttpContext.Current.User;
            foreach (string role in ArRoles)
            {
                if (user.IsInRole(role))
                    return true;
            }

            return false;

        }
        public static void SetLoggedInUser(UserModel user, System.Web.SessionState.HttpSessionState session)
        {

            session[Constants.USER] = user;

        }

        public static bool SendEmailToAdmin(string emailTo, string emailBody, string emailSubject)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(emailTo));
                //testing  mailMessage.To.Add(new System.Net.Mail.MailAddress("amanullaha@chrc.net"));
                mailMessage.Subject = emailSubject;

                mailMessage.IsBodyHtml = true;
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetRegistrationEmailBody(string.Empty, string.Empty, string.Empty, string.Empty), null, "text/html");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");


                //LinkedResource imagelink = new LinkedResource(Server.MapPath("~/images/regEmailImage.jpg"), "image/jpg");

                //imagelink.ContentId = "imageId";

                //imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                //htmlView.LinkedResources.Add(imagelink);

                mailMessage.AlternateViews.Add(htmlView);
                //  mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/pdf/CHOLESTABETES Needs Assessment.pdf")));

                SendMail(mailMessage);
                return true;


            }

            catch (Exception e)
            {
                // Response.Write("fail in sendEmailNotification+++++" + e.Message.ToString());

                return false;
            }
        }


        public static void SendEmailAfterRegistration(RegistrationInfoModel model)
        {
            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(model.UserName);
                mailMessage.Subject = "Intent-CAD EMR Program – Registration Confirmation and Next Steps";
                mailMessage.IsBodyHtml = true;

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Text", "RegistrationEmail.html");
                string emailBody = System.IO.File.ReadAllText(path);
                emailBody = emailBody.Replace("{LastName}", model.LastName)
                    .Replace("{BaseURL}", ConfigurationManager.AppSettings["BaseURL"])
                    .Replace("{UserName}", model.UserName)
                    .Replace("{Password}", model.Password)
                    .Replace("{ContactEmail}", ConfigurationManager.AppSettings["ContactEmail"]);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);
                UserHelper.SendMail(mailMessage);
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

        public static void SendForgotPasswordEmail(UserModel model)
        {
            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["ContactEmail"]);
                mailMessage.To.Add(model.Username);
                mailMessage.Subject = "FORGOT PASSWORD";
                mailMessage.IsBodyHtml = true;

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Text", "ForgotPasswordEmail.html");
                string emailBody = System.IO.File.ReadAllText(path);
                emailBody = emailBody.Replace("{UserName}", model.Username)
                    .Replace("{Password}", model.Password);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);
                UserHelper.SendMail(mailMessage);
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

        public static HttpCookie GetAuthorizationCookie(string userName, string userData)
        {
            HttpCookie httpCookie = FormsAuthentication.GetAuthCookie(userName, true);
            FormsAuthenticationTicket currentTicket = FormsAuthentication.Decrypt(httpCookie.Value);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(currentTicket.Version, currentTicket.Name, currentTicket.IssueDate, currentTicket.Expiration, currentTicket.IsPersistent, userData);
            httpCookie.Value = FormsAuthentication.Encrypt(ticket);
            return httpCookie;
        }

        public static string GetProvinceFullName(string ProvinceCode)
        {
            string ProvinceFullName = string.Empty;
            switch (ProvinceCode.ToUpper())
            {
                case "AB":
                    ProvinceFullName = "Alberta";
                    break;
                case "BC":
                    ProvinceFullName = "British Columbia";
                    break;
                case "NB":
                    ProvinceFullName = "New Brunswick";
                    break;
                case "NL":
                    ProvinceFullName = "Newfoundland";
                    break;
                case "NS":
                    ProvinceFullName = "Nova Scotia";
                    break;
                case "NT":
                    ProvinceFullName = "Northwest Territories";
                    break;
                case "NU":
                    ProvinceFullName = "Nunavut";
                    break;
                case "ON":
                    ProvinceFullName = "Ontario";
                    break;
                case "PEI":
                    ProvinceFullName = "Prince Edward Island";
                    break;
                case "QC":
                    ProvinceFullName = "Quebec";
                    break;
                case "SK":
                    ProvinceFullName = "Saskatchewan";
                    break;
                case "YT":
                    ProvinceFullName = "Yukon";
                    break;
            }
            return ProvinceFullName;
        }


        public static void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            try
            {

                client.Host = ConfigurationManager.AppSettings["smtpServer"];

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                // NetworkCred.UserName = "webmaster@questionaf.ca";
                //NetworkCred.Password = "xkc232v";
                NetworkCred.UserName = ConfigurationManager.AppSettings["smtpUser"];
                NetworkCred.Password = ConfigurationManager.AppSettings["smtpPassword"];
                client.UseDefaultCredentials = false;
                client.Credentials = NetworkCred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // client.Port = 25;
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                client.EnableSsl= Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                client.Timeout = 20000;
                client.Send(Message);

            }
            catch (Exception e)
            {
                client = null;
                String Error = e.Message.ToString();
                //Utility.WriteToLog("SendMail Error: " + Error);
            }

        }


        public static IEnumerable<SelectListItem> GetProvinces()
        {
            List<SelectListItem> provinces = new List<SelectListItem>
            {

                      new SelectListItem {Text = "AB", Value = "Alberta"},
                      new SelectListItem {Text = "BC", Value = "British Coloumbia"},
                      new SelectListItem {Text = "MB", Value = "Manitoba"},
                      new SelectListItem {Text = "NS", Value = "Nova Scotia"},
                      new SelectListItem {Text = "NB", Value = "New Brunswick"},
                      new SelectListItem {Text = "NL", Value = "Newfoundland and Labrador"},
                      new SelectListItem {Text = "ON", Value = "Ontario"},
                      new SelectListItem {Text = "PE", Value = "Prince Edward Island"},
                      new SelectListItem {Text = "QC", Value = "Quebec"},
                      new SelectListItem {Text = "SK", Value = "Saskatchewan"},


            };
            return provinces;

        }


        public static IEnumerable<SelectListItem> GetStatin()
        {
            return new List<SelectListItem>
            {
                      new SelectListItem {Text = "Atorvastatin", Value = "Atorvastatin"},
                      new SelectListItem {Text = "Rosuvastatin", Value = "Rosuvastatin"},
                      new SelectListItem {Text = "Simvastatin", Value = "Simvastatin"},
                      new SelectListItem {Text = "Fluvastatin", Value = "Fluvastatin"},
                      new SelectListItem {Text = "Lovastatin", Value = "Lovastatin"},
                      new SelectListItem {Text = "Pravastatin", Value = "Pravastatin"}
            };
        }

        public static IEnumerable<SelectListItem> GetStatinDose()
        {
            return new List<SelectListItem>
            {
                      new SelectListItem {Text = "5 mg", Value = "5 mg"},
                      new SelectListItem {Text = "10 mg", Value = "10 mg"},
                      new SelectListItem {Text = "20 mg", Value = "20 mg"},
                      new SelectListItem {Text = "40 mg", Value = "40 mg"},
                      new SelectListItem {Text = "80 mg", Value = "80 mg"},

            };
        }

    }
}