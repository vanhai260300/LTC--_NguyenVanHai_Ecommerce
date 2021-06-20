using ModelFE.DAO;
using ModelFE.FE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NguyenVanHai.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        NguyenVanHaiContext db = new NguyenVanHaiContext();
        public ActionResult Index(String searchString, int page = 1, int PageSize = 10)
        {
            var Dao = new ProductDao();
            var Model = Dao.ListAllPaging(searchString, page, PageSize);
            ViewBag.Search = searchString;
            return View(Model);
        }
        public void SetViewBag(long? SelectID = null)
        {
            var Dao = new CategoryDao();
            ViewBag.Category = new SelectList(Dao.ListAll(), "ID", "Name", SelectID);
        }

        public ActionResult CreateProduct()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        //Create
        public ActionResult CreateProduct(Product Entity, HttpPostedFileBase upLoadImage)
        {
            if (ModelState.IsValid)
            {
                if (Entity.UnitCost == 0 || string.IsNullOrEmpty(Entity.UnitCost.ToString()))
                {
                    Entity.UnitCost = Entity.UnitCost;
                }
                Entity.Name = Entity.Name;
                Entity.Quantity = Entity.Quantity;
                Entity.Image = Entity.Image;
                Entity.Status = Entity.Status;
                Entity.Description = Entity.Description;

                //Thêm Sản Phẩm
                if (new ProductDao().Insert(Entity) > 0)
                {
                    //UpLoad Hình ảnh
                    if (upLoadImage != null && upLoadImage.ContentLength > 0)
                    {
                        int id = int.Parse(db.Products.ToList().Last().ID.ToString());
                        string _FileName = "";
                        int index = upLoadImage.FileName.IndexOf('.');
                        // Đặt tên mới cho ảnh theo ID Sản phẩm
                        _FileName = "product" + (id).ToString() + "." + upLoadImage.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Assets/Image"), _FileName);
                        upLoadImage.SaveAs(_path);
                        //Update ảnh vào Data
                        Product unv = db.Products.FirstOrDefault(x => x.ID == id);
                        unv.Image = _FileName;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Product");
                }
                else
                    ModelState.AddModelError("", "Can't Add! Check Again!");
            }
            SetViewBag();
            return View(Entity);
        }
        
        [HttpGet]
        public ActionResult DetailProduct (int ID)
        {
            
            return View(new ProductDao().ViewDetail(ID));
        }

        public ActionResult Update(int ID)
        {
            Product product = db.Products.FirstOrDefault(x => x.ID == ID);
            SetViewBag();
            return View(new ProductDao().ViewDetail(ID));
        }
        [HttpPost]
        //Edit
        public ActionResult Update(Product Entity, HttpPostedFileBase upLoadImage)
        {
            if (ModelState.IsValid)
            {
                if (new ProductDao().Update(Entity))
                {
                    Product product = db.Products.FirstOrDefault(x => x.ID == Entity.ID);
                    if (upLoadImage != null && upLoadImage.ContentLength > 0)
                    {
                        int id = Entity.ID;
                        string _FileName = "";
                        int index = upLoadImage.FileName.IndexOf('.');
                        _FileName = "product" + (id).ToString() + "." + upLoadImage.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Assets/Image"), _FileName);
                        upLoadImage.SaveAs(_path);

                        Product unv = db.Products.FirstOrDefault(x => x.ID == id);
                        unv.Image = _FileName;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Fail! !");
                }
            }
            SetViewBag();
            return View(Entity);
        }
        [HttpDelete]
        public ActionResult DeleteAJax(int ID)
        {
            new ProductDao().Delete(ID);
            return RedirectToAction("Product");
        }
    }
}