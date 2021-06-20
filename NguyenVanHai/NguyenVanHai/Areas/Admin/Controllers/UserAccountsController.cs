using ModelFE.FE;
using ModelFE.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenVanHai.Common;

namespace NguyenVanHai.Areas.Admin.Controllers
{
    public class UserAccountsController : BaseController
    {
        // GET: Admin/UserAccouts
        public ActionResult Index(String searchString, int p = 1, int PageSize = 5)
        {
            var Dao = new UserDao();
            var Model = Dao.ListAllPaging(searchString, p,PageSize);
            ViewBag.Search = searchString;
            return View(Model);
        }
        //Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //Create
        public ActionResult Create(UserAccount Entity)
        {
            if (ModelState.IsValid)
            {
                var Dao = new UserDao();

                if (Dao.CheckUserName(Entity.UserName))
                {
                    ModelState.AddModelError("", "Account already exists!");
                }

                else
                {

                    Entity.PassWord = Encryptor.MD5Hash(Entity.PassWord);

                    long Result = Dao.Insert(Entity);

                    if (Result > 0)
                    {
                        SetAlert("Add user successfully!", "success");
                        return RedirectToAction("Index", "UserAccounts");
                    }

                    else
                        ModelState.AddModelError("", "Can't add this user! Please check again!");
                }
            }
            return View(Entity);
        }
        //Edit
        [HttpGet]
        public ActionResult Detail(int ID)
        {
            return View(new UserDao().ViewDetail(ID));
        }

        //Edit
        [HttpGet]
        public ActionResult Update(int ID)
        {
            return View(new UserDao().ViewDetail(ID));
        }
        [HttpPost]
        //Edit
        public ActionResult Update(UserAccount Entity)
        {
            if (ModelState.IsValid)
            {
                Entity.PassWord = Encryptor.MD5Hash(Entity.PassWord);
                Entity.Status = Entity.Status;
                if (new UserDao().Update(Entity))
                {
                    return RedirectToAction("Index", "UserAccounts");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update information! Please check again!");
                }
            }
            return View(Entity);
        }
        //Delete
        [HttpPost]
        public ActionResult DeleteAJax2(int ID)
        {
            var dao = new UserDao();
            var res = dao.Delete(ID);
            if (res != true)
                return Content("OK"); 
            else
                return Content("NOT");
        }
    }
}