using ShopOnlineModel.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineModel.Dao
{
    public class MetaProductDao
    {
        ShopModelDbContext db = null;
        public MetaProductDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_MetaProduct entity)
        {
            db.tb_MetaProduct.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_MetaProduct entity)
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
                var entity = db.tb_MetaProduct.Find(Id);
                db.tb_MetaProduct.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetValue(string Key, long Id)
        {
            var stValue = db.tb_MetaProduct.Where(x => x.Key == Key && x.ProductId == Id).SingleOrDefault();
            if (stValue == null)
            {
                return "";
            }
            return stValue.Value;
        }

        public void SaveValue(string Key, string Value, long Id)
        {
            var meta = db.tb_MetaProduct.Where(x => x.Key == Key && x.ProductId == Id).SingleOrDefault();
            if (meta == null)
            {
                meta = new tb_MetaProduct();
                meta.ProductId = Id;
                meta.Key = Key;
                meta.Value = Value;
                db.tb_MetaProduct.Add(meta);
                db.SaveChanges();
            }
            else
            {
                meta.ProductId = Id;
                meta.Key = Key;
                meta.Value = Value;
                db.Entry(meta).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
