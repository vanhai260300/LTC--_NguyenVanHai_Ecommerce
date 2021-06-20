using ModelFE.FE;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFE.DAO
{
    public class CategoryDao
    {
        NguyenVanHaiContext Db = null;
        public CategoryDao()
        {
            Db = new NguyenVanHaiContext();
        }

        //Get List
        public List<Category> ListAll()
        {
            return Db.Categories.ToList();
        }


        //View Detail
        public Category ViewDetail(long ID)
        {
            return Db.Categories.Find(ID);
        }


        //Update
        public bool Update(Category Entity)
        {
            try
            {
                var Cate = Db.Categories.Find(Entity.ID);
                Cate.Name = Entity.Name;
                Cate.Description = Entity.Description;

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
                var Cate = Db.Categories.Find(ID);
                Db.Categories.Remove(Cate);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Insert(Category entity)
        {
            Db.Categories.Add(entity);
            Db.SaveChanges();
            return entity.ID;
        }
    }
}
