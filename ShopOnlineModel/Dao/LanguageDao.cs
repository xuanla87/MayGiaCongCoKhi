using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class LanguageDao
    {
        ShopModelDbContext db = null;
        public LanguageDao()
        {
            db = new ShopModelDbContext();
        }

        public string Insert(tb_Languages entity)
        {
            db.tb_Languages.Add(entity);
            db.SaveChanges();
            return entity.LanguageId;
        }

        public bool Update(tb_Languages entity)
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

        public bool Delete(string Id)
        {
            try
            {
                var entity = db.tb_Languages.Find(Id);
                db.tb_Languages.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public tb_Languages Detail(string Id)
        {
            return db.tb_Languages.Find(Id);
        }

        public IEnumerable<tb_Languages> ListAll()
        {
            return db.tb_Languages.OrderBy(x => x.LanguageId).ToList();
        }
    }
}
