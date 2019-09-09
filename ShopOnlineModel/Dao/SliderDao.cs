using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class SliderDao
    {
        ShopModelDbContext db = null;
        public SliderDao()
        {
            db = new ShopModelDbContext();
        }
        public long Insert(tb_Sliders entity)
        {
            db.tb_Sliders.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Sliders entity)
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
                var entity = db.tb_Sliders.Find(Id);
                db.tb_Sliders.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_Sliders Detail(long? Id)
        {
            return db.tb_Sliders.Find(Id);
        }

        public IEnumerable<tb_Sliders> ListAll()
        {
            return db.tb_Sliders.ToList();
        }

        public IEnumerable<tb_Sliders> ListAllByTypeId(long TypeId)
        {
            var entity = db.tb_Sliders.Where(x => x.TypeId == TypeId).ToList();
            return entity;
        }

        public long InsertType(tb_SliderType entity)
        {
            db.tb_SliderType.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool UpdateType(tb_SliderType entity)
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

        public bool DeleteType(long? Id)
        {
            try
            {
                var entity = db.tb_SliderType.Find(Id);
                db.tb_SliderType.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_SliderType DetailType(long? Id)
        {
            return db.tb_SliderType.Find(Id);
        }

        public IEnumerable<tb_SliderType> ListAllType()
        {
            return db.tb_SliderType.ToList();
        }
    }
}
