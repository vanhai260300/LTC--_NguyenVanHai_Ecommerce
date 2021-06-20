using ModelFE.FE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;

namespace ModelFE.DAO
{
    public class UserDao
    {
        //Khởi tạo Db
        NguyenVanHaiContext Db = null;
        //Khởi tạo phương thức
        public UserDao()
        {
            Db = new NguyenVanHaiContext();
        }
        //Insert
        public long Insert(UserAccount Entity)
        {
            Db.UserAccounts.Add(Entity);
            Db.SaveChanges();
            return Entity.ID;
        }
        //Update
        public bool Update(UserAccount Entity)
        {
            try
            {
                var User = Db.UserAccounts.Find(Entity.ID);

                if (!string.IsNullOrEmpty(Entity.PassWord))
                {
                    User.PassWord = Entity.PassWord;
                }

                User.Status = Entity.Status;
                User.UserName = Entity.UserName;

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
                var User = Db.UserAccounts.Find(ID);
                string status = "";
                if (!string.IsNullOrEmpty(User.Status))
                {
                    status = User.Status.Replace(" ", "");//Xóa khoảng trắng
                }
                if (status == "BLOCKED")
                {
                    Db.UserAccounts.Remove(User);
                    Db.SaveChanges();
                    return true; // Xóa thành công
                }
                else
                {
                    return false; // Không được xóa
                }
            }
            catch (Exception)
            {
               return false; // Xóa thất bại
            }
        }
        //Get Page
        public IEnumerable<UserAccount> ListAllPaging(string searchString, int Page, int PageSize)
        {
            IQueryable<UserAccount> model = Db.UserAccounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(Page, PageSize);
        }

        //View Detail
        public UserAccount ViewDetail(int ID)
        {
            return Db.UserAccounts.Find(ID);
        }

        //Check UserName
        public bool CheckUserName(string UserName)
        {
            return Db.UserAccounts.Count(x => x.UserName == UserName) > 0;
        }


        //Login
        public int Login(string UserName, string PassWord)
        {
            var Result = Db.UserAccounts.SingleOrDefault(x => x.UserName.Equals(UserName));

            if (Result == null)
                return 0; //Không tồn tại user

            else if (Db.UserAccounts.SingleOrDefault(x => x.UserName.Equals(UserName) && x.PassWord.Equals(PassWord)) == null)
                return -1; //Sai mật khẩu
            else
                return 1; // Đăng nhập hợp lệ
        }

    }
}
