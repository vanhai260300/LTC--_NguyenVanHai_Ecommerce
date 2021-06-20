using ModelFE.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanHai.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new ProductDao();
            ViewBag.TopEight = model.TopEightProduct(8);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }
    }
}