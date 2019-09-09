using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class MenuDao
    {
        ShopModelDbContext db = null;
        public MenuDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Menus entity)
        {
            db.tb_Menus.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Menus entity)
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
                var entity = db.tb_Menus.Find(Id);
                db.tb_Menus.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_Menus Detail(long? Id)
        {
            return db.tb_Menus.Find(Id);
        }

        public IEnumerable<tb_Menus> ListAll()
        {
            return db.tb_Menus.ToList();
        }

        public IEnumerable<tb_Menus> ListAllByTypeId(long TypeId)
        {
            var entity = db.tb_Menus.Where(x => x.TypeId == TypeId).ToList();
            return entity;
        }

        public long InsertType(tb_MenuType entity)
        {
            db.tb_MenuType.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool UpdateType(tb_MenuType entity)
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
                var entity = db.tb_MenuType.Find(Id);
                db.tb_MenuType.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_MenuType DetailType(long? Id)
        {
            return db.tb_MenuType.Find(Id);
        }

        public IEnumerable<tb_MenuType> ListAllType()
        {
            return db.tb_MenuType.ToList();
        }

        public IEnumerable<tb_Menus> ViewByTypeId(long Id)
        {
            var lMenu = db.tb_Menus.Where(x => x.TypeId == Id && x.Status == true).ToList();
            return lMenu;
        }

        public IEnumerable<tb_Menus> ViewByParent(long Id)
        {
            var lMenu = db.tb_Menus.Where(x => x.ParentId == Id && x.Status == true).ToList();
            return lMenu;
        }

    }
}
