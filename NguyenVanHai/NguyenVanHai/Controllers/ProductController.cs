using ModelFE.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanHai.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return RedirectToAction("ByCategory/0", "Product");
            
        }
        //Category Product
        public ActionResult ByCategory(int ID, int Page = 1, int PageSize = 6)
        {

            var model = new ProductDao();
            ViewBag.Category = new CategoryDao().ListAll();
            ViewBag.CategoryName = "";
            if (ID == 0)
            {
                ViewBag.CategoryName = "All Product";
                ViewBag.Product = model.ListAll(Page, PageSize);
            }
            else
            {
                var category = new CategoryDao().ViewDetail(ID);
                ViewBag.CategoryName = category.Name;
                ViewBag.Product = model.ByCategoryID(ID, Page, PageSize);
            }
            return View();
            
        }
        public ActionResult Detail(int ID)
        {
            var dao = new ProductDao();
            var product = dao.ViewDetail(ID);
            return View(product);
        }
        
        
    }
}