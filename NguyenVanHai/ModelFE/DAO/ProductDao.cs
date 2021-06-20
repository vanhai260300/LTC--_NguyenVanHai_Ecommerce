using ModelFE.FE;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFE.DAO
{
    public class ProductDao
    {
        NguyenVanHaiContext Db = null;
        public ProductDao()
        {
            Db = new NguyenVanHaiContext();

        }
        //Get Page
        public IEnumerable<Product> ListAllPaging(string searchString, int Page, int PageSize)
        {
            IQueryable<Product> model = Db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            // Sắp xếp số lượng tăng dần, đơn giá giảm dần
            return model.OrderBy(x => x.Quantity).ThenByDescending(n => n.UnitCost).ToPagedList(Page, PageSize);
        }
        //View Details
        public List<Product> TopEightProduct(int top)
        {
            List<Product> model = Db.Products.OrderBy(x => x.UnitCost).Take(top).ToList();
            return model;
        }

        //Get All Product
        public IEnumerable<Product> ListAll(int Page, int PageSize)
        {
            IQueryable<Product> model = Db.Products;

            return model.OrderBy(x => x.Quantity).ThenByDescending(n => n.UnitCost).ToPagedList(Page, PageSize);
        }
        //Get Products by Category
        public IEnumerable<Product> ByCategoryID(long CategoryID, int Page, int PageSize)
        {
            var model = Db.Products.Where(x => x.IDCategory == CategoryID).OrderBy(x => x.UnitCost).ToPagedList(Page, PageSize);
            return model;
        }
        //Get Products by Category
        public List<Product> ByCategory(long CategoryID, ref int TotalRecord, int p = 1, int PageSize = 6)
        {
            TotalRecord = Db.Products.Where(x => x.IDCategory == CategoryID).Count();
            var model = Db.Products.Where(x => x.IDCategory == CategoryID).OrderBy(x => x.UnitCost).Skip((p - 1) * PageSize).Take(PageSize).ToList();
            return model;
        }
        //View Details
        public Product ViewDetail(long ID)
        {
            return Db.Products.Find(ID);
        }
        //Insert
        public long Insert(Product Entity)
        {
            Db.Products.Add(Entity);
            Db.SaveChanges();
            return Entity.ID;
        }

        //Update
        public bool Update(Product Entity)
        {
            try
            {
                var Product = Db.Products.Find(Entity.ID);

                Product.Name = Entity.Name;
                Product.IDCategory = Entity.IDCategory;
                Product.Description = Entity.Description;
                Product.UnitCost = Entity.UnitCost;
                Product.Image = Entity.Image;
                Product.Status = Entity.Status;
                Product.Quantity = Entity.Quantity;

                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Delete
        public bool Delete(int ID)
        {
            try
            {
                var Product = Db.Products.Find(ID);
                Db.Products.Remove(Product);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<string> ListName(string s)
        {
            return Db.Products.Where(x => x.Name.Contains(s)).Select(x => x.Name).ToList();
        }
    }
}
