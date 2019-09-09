using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class CategoryDao
    {
        ShopModelDbContext db = null;
        public CategoryDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Catogory entity)
        {
            db.tb_Catogory.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Catogory entity)
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

        public tb_Catogory Detail(long? Id)
        {
            return db.tb_Catogory.Find(Id);
        }

        public tb_Catogory DetailBySlug(string slug)
        {
            return db.tb_Catogory.SingleOrDefault(x => x.Slug == slug && x.Status == true);
        }

        public IEnumerable<tb_Catogory> ListAll()
        {
            return db.tb_Catogory.ToList();
        }

        public IEnumerable<tb_Catogory> ListAllByTaxonomy(string taxonomy)
        {
            return db.tb_Catogory.Where(x => x.Taxonomy == taxonomy).ToList();
        }

        public bool CheckSlug(string Slug, long? Id)
        {
            return db.tb_Catogory.Any(x => x.Slug == Slug && x.Id != Id);
        }

        public bool Delete(long? Id)
        {
            try
            {
                var entity = db.tb_Catogory.Find(Id);
                db.tb_Catogory.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<tb_Catogory> ViewAllByTaxonomy(string taxonomy)
        {
            return db.tb_Catogory.Where(x => x.Taxonomy == taxonomy && x.Status == true).OrderBy(x => x.Order).ToList();
        }

        public IEnumerable<tb_Catogory> ViewAllByParentId(long? Id)
        {
            return db.tb_Catogory.Where(x => x.ParentId == Id && x.Status == true).OrderBy(x => x.Order).ToList();
        }
    }
}
