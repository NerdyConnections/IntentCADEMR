using IntentCADEMR.DAL;
using IntentCADEMR.Models;
using IntentCADEMR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntentCADEMR.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult IntroVideo()
        {
            ViewBag.Title = "Introductory Video";

            return View();
        }
        public ActionResult MOU()
        {
            MOURepository repo = new MOURepository();
            MOUModel model = new MOUModel();
            var CurrentUser = UserHelper.GetLoggedInUser();


            if (CurrentUser != null)
            {
                Initialize(CurrentUser.UserID);
                int UserId = CurrentUser.UserID;
                bool IsSubmitted = repo.IsSubmitted(UserId);
                model.MOU = repo.GetMOU(UserId);
                model.IsSubmitted = IsSubmitted;
                model.UserId = UserId;
                return View(model);
            }


            return RedirectToAction("Login", "Account");
           
        }
        private void Initialize(int UserID)
        {

            MOURepository regRepo = new MOURepository();
            ViewBag.IsMOUComplete = regRepo.IsMOUComplete(UserID);
            ViewBag.IsPayeeFormComplete = regRepo.IsPayeeFormComplete(UserID);

        }
        public ActionResult PayeeForm()
        {
            UserModel currentUser = UserHelper.GetLoggedInUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                PayeeFormModel model = null;
                if (currentUser.Payee)
                {
                    UserRepository repo = new UserRepository();
                    model = repo.GetPayeeForm(currentUser);
                    ViewBag.IsSubmitted = true;
                }
                else
                {
                    model = new PayeeFormModel();
                    model.UserID = currentUser.UserID;
                    ViewBag.IsSubmitted = false;
                }


                return View(model);
            }
        }
        [HttpPost]
        public ActionResult PayeeForm(PayeeFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                UserRepository repo = new UserRepository();
                repo.AddPayeeForm(model);

                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult SetMOU(bool isChecked)
        {
            bool errored = false;
            string error;
            MOURepository repo = new MOURepository();

            try
            {

                UserModel um = UserHelper.GetLoggedInUser();
                if (um != null)
                {
                    repo.SetMOU(um);

                    UserHelper.ReloadUser();
                    // Initialize(um.UserID);
                    string redirectUrl = "/MOU/Index";
                    var success = new { msg = "MOU Updated Successfully", returnURL = redirectUrl };
                    return Json(new { success = success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    error = "User is not Logged In";
                    return Json(new { Error = error }, JsonRequestBehavior.AllowGet);

                }
                //  return  RedirectToAction("Index", "Home");

            }
            catch (Exception exc)
            {
                //return RedirectToAction("Index", "Home");
                error = "Database is not available";
                return Json(new { Error = error });
            }



        }
    }
}