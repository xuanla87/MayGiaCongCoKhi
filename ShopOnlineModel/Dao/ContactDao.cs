using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class ContactDao
    {
        ShopModelDbContext db = null;
        public ContactDao()
        {
            db = new ShopModelDbContext();
        }
        public long Insert(tb_Contact entity)
        {
            db.tb_Contact.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(tb_Contact entity)
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
        public bool Delete(long? Id)
        {
            try
            {
                var entity = db.tb_Contact.Find(Id);
                db.tb_Contact.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
