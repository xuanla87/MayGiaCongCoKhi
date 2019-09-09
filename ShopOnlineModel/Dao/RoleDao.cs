using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class RoleDao
    {
        ShopModelDbContext db = null;
        public RoleDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Roles entity)
        {
            db.tb_Roles.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Roles entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool Delete(long? Id)
        {
            if (Id == null)
            {
                return false;
            }
            else
            {
                tb_Roles entity = db.tb_Roles.Find(Id);
                if (entity == null)
                {
                    return false;
                }
                else
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }

            }

        }

        public IEnumerable<tb_Roles> ListAll()
        {
            return db.tb_Roles.ToList();
        }

        public tb_Roles Detail(long? Id)
        {
            return db.tb_Roles.Find(Id);
        }

        public tb_Roles GetByName(string RoleName)
        {
            var role = (from q in db.tb_Roles where q.RoleName == RoleName select q).FirstOrDefault();
            return role;
        }
    }
}
