using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

using BaseDataAccess.Dac;
using BaseUtility;

namespace BaseFramework.Controllers
{
    public class UserController : Controller
    {
        #region Login

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public JsonResult LoginProc(string id = "", string password = "")
        {
            string result = "SUCCESS";
            return Json(result);
        }

        #endregion

        #region LogOut

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        #endregion

        #region SignUp

        public ActionResult SignUp()
        {
            return View();
        }

        public JsonResult SignUpProc(string userid = "", string password = "")
        {
            string flag = "";
            if (password.Length > 0)
                password = Security.ShaEncryption(password);

            int result = new Dac_UserInfo().SaveUserInfo(userid, password);
            if (result > 0)
                flag = "SUCCESS";

            return Json(new string[] { flag, userid });
        }

        #endregion

        #region SignMailConfirm

        public ActionResult SignMailConfirm()
        {
            return View();
        }

        #endregion

    }
}