using IntentCADEMR.DAL;
using IntentCADEMR.Models;
using IntentCADEMR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IntentCADEMR.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View(); 
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {

                return View();
            }

            var userRepo = new UserRepository();
            bool IsAuthenticated, IsActivated;
            IsAuthenticated = userRepo.Authenticate(model.Email, Encryptor.Encrypt(model.Password));
            if (IsAuthenticated)
            {
                //the database has the correct credentials but is the account activated yet?
                IsActivated = userRepo.IsActivated(model.Email, Encryptor.Encrypt(model.Password));
                if (IsActivated)
                {
                    HttpCookie AuthorizationCookie = UserHelper.GetAuthorizationCookie(model.Email, userRepo.GetRoles(model.Email)); //roles are pipe delimited
                    Response.Cookies.Add(AuthorizationCookie);
                    string[] userRoles = userRepo.GetRolesAsArray((model.Email));
                    System.Web.HttpContext.Current.User = new GenericPrincipal(System.Web.HttpContext.Current.User.Identity, userRoles);  //set the roles of Current.User.Identity

                    // FormsAuthentication.SetAuthCookie(model.Email, false);


                    //bool result1 = User.IsInRole("SPECIALIST");
                    //bool result2 = User.IsInRole("PCP");

                    UserModel CurrentUser = userRepo.GetUserDetails(model.Email);//cannot user identity.name because it will set only when the auth cookie is passed in in the next request.

                    //making sure nothing in the temppaf table from previous session
                    // Session["LogoUrl"] = UserHelper.GetCPDLogo(Request);

                    UserHelper.SetLoggedInUser(CurrentUser, System.Web.HttpContext.Current.Session);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") & !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (User.IsInRole(Util.Constants.Admin))
                            return RedirectToAction("Index", "Admin");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }//not yet activate, redirect to activate account screen
                else
                {

                    return RedirectToAction("Activate", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Invalid Username or Password");
                //  TempData["ImageUrl"] = UserHelper.GetCPDLogo(Request);
                return View();
            }

;
        }
        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegistrationInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
            else
            {

                UserRepository repo = new UserRepository();
                ViewBag.IsSuccessful = repo.AddUser(model);
                ViewBag.IsHttpPost = true;
                if (ViewBag.IsSuccessful)
                {
                    Task.Factory.StartNew(() =>
                    {
                        UserHelper.SendEmailAfterRegistration(model);
                    });
                }

            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {


            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ForgotPassword", model);
            }
            else
            {
                UserRepository repo = new UserRepository();
                UserModel user = repo.GetUserDetails(model.UserName);
                if (user != null)
                {
                    Task.Factory.StartNew(() =>
                    {
                        UserHelper.SendForgotPasswordEmail(user);
                    });
                    ViewBag.IsSuccessful = true;
                }
                else
                {
                    ViewBag.IsSuccessful = false;
                }

                ViewBag.IsHttpPost = true;
                return View();
            }
        }


    }
}
