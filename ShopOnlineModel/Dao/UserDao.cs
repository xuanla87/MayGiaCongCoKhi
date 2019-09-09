using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class UserDao
    {
        ShopModelDbContext db = null;
        public UserDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Users entity)
        {
            db.tb_Users.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Users entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public tb_Users Detail(long? Id)
        {
            return db.tb_Users.Find(Id);
        }

        public tb_Users DetailByUser(string UserName)
        {
            return db.tb_Users.SingleOrDefault(x => x.UserName == UserName || x.UserEmail == UserName);
        }

        public bool CheckUserEmailExits(string Email, long? Id)
        {
            return db.tb_Users.Any(x => x.UserEmail == Email && x.Id != Id);
        }

        public bool CheckUserNameExits(string UserName, long? Id)
        {
            return db.tb_Users.Any(x => x.UserName == UserName && x.Id != Id);
        }

        public bool CheckOldPass(string UserPassword, long? Id)
        {
            return db.tb_Users.Any(x => x.UserPassword == UserPassword && x.Id == Id);
        }

        public bool Login(string UserName, string Password)
        {
            try
            {
                var user = db.tb_Users.SingleOrDefault(x => (x.UserName == UserName || x.UserEmail == UserName) && x.UserPassword == Password);
                if (user == null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<tb_Users> ListAll()
        {
            return db.tb_Users.ToList();
        }

        public bool Delete(long? Id)
        {
            try
            {
                var list = db.tb_Users.ToList();
                if (list.Count > 1)
                {
                    var user = db.tb_Users.Find(Id);
                    db.tb_Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;

            }
            catch
            {
                return false;
            }

        }

        public bool ChangePass(long? Id, string OldPass, string NewPass)
        {
            var User = db.tb_Users.SingleOrDefault(x => x.Id == Id && x.UserPassword == OldPass);
            if (User != null)
            {
                User.UserPassword = NewPass;
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }
    }
}
