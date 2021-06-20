using ModelFE;
using ModelFE.DAO;
using ModelFE.FE;
using NguyenVanHai.Areas.Admin.Model;
using NguyenVanHai.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanHai.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var Dao = new UserDao();
                var Result = Dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));

                if (Result == 0)
                    ModelState.AddModelError("", "Invalid username!! ");
                else if(Result == -1)
                    ModelState.AddModelError("", "Incorrect Password!");
                else
                {
                    // Lưu User Name vào session
                    Session["use"] = model.UserName;
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Fail!");
                return View("Index");
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Login");
        }

        
    }
}