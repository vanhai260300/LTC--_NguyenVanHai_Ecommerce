using ModelFE;
using ModelFE.DAO;
using ModelFE.FE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanHai.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        NguyenVanHaiContext db = new NguyenVanHaiContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //Create
        public ActionResult Create(Category Entity)
        {
            if (ModelState.IsValid)
            {
                CategoryDao categoryDao = new CategoryDao();


                categoryDao.Insert(Entity);
                return RedirectToAction("Index", "Category");

            }
            return View(Entity);
        }

        //Edit
        [HttpGet]
        public ActionResult Update(int ID)
        {
            return View(new CategoryDao().ViewDetail(ID));
        }
        [HttpPost]
        //Edit
        public ActionResult Update(Category Entity)
        {
            if (ModelState.IsValid)
            {
                

                if (new CategoryDao().Update(Entity))
                {

                    
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật thông tin! Vui lòng kiểm tra lại!");
                }
            }
            return View(Entity);
        }

        //Delete
        
        public ActionResult Delete(int ID)
        {
           
                new CategoryDao().Delete(ID);
                return RedirectToAction("Index", "Category");
            
        }
        [HttpGet]
        public ActionResult DetailCategory(int ID)
        {
            return View(new CategoryDao().ViewDetail(ID));
        }
        //Delete
        [HttpDelete]
        public ActionResult DeleteAJax(int ID)
        {
            new CategoryDao().Delete(ID);
            return RedirectToAction("Category");
        }
        //Delete
        [HttpPost]
        public ActionResult DeleteAJax2(int ID)
        {
            var dao = new CategoryDao();
            var res =  dao.Delete(ID);
            if (res == false)

                return Content("NOT");
            else
                return Content("OK");
        }
    }
}