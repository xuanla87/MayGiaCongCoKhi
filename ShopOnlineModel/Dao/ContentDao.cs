using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineModel.EF;
using System.Data.Entity;

namespace ShopOnlineModel.Dao
{
    public class ContentDao
    {
        ShopModelDbContext db = null;
        public ContentDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Contents entity)
        {
            db.tb_Contents.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Contents entity)
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
                var entity = db.tb_Contents.Find(Id);
                var meta = db.tb_MetaContent.Where(x => x.ContentId == entity.Id).ToList();
                db.tb_MetaContent.RemoveRange(meta);
                db.SaveChanges();
                db.tb_Contents.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_Contents Detail(long? Id)
        {
            return db.tb_Contents.Find(Id);
        }

        public tb_Contents DetailBySlug(string slug)
        {
            return db.tb_Contents.SingleOrDefault(x => x.Slug == slug && x.Status == true);
        }

        public IEnumerable<tb_Contents> ListAll()
        {
            return db.tb_Contents.ToList();
        }

        public IEnumerable<tb_Contents> ListAllByTaxonomy(string taxonomy)
        {
            var entity = db.tb_Contents.Where(x => x.Taxonomy == taxonomy).ToList();
            return entity;
        }

        public bool CheckSlug(string Slug, long? Id)
        {
            return db.tb_Contents.Any(x => x.Slug == Slug && x.Id != Id);
        }

        public List<tb_Contents> ViewAllByCategoryId(long Id)
        {
            return db.tb_Contents.Where(x => x.CategoryId == Id && x.Status == true).ToList();
        }

        public List<tb_Contents> ViewAllByCategorySlug(string Slug)
        {
            var detaiCategory = db.tb_Catogory.SingleOrDefault(x => x.Slug == Slug && x.Status == true);
            var listContent = new List<tb_Contents>();
            if (detaiCategory != null)
            {
                listContent.AddRange(ViewAllByCategoryId(detaiCategory.Id));
                var listCatogory = db.tb_Catogory.Where(x => x.ParentId == detaiCategory.Id && x.Status == true).ToList();
                foreach (var item in listCatogory)
                {
                    listContent.AddRange(ViewAllByCategoryId(item.Id));
                }
            }
            return listContent.OrderByDescending(x => x.Modified).ToList();
        }

        public List<tb_Contents> ViewRelated(string Slug)
        {
            var detail = db.tb_Contents.SingleOrDefault(x => x.Slug == Slug);
            if (detail != null)
            {
                var list = (from q in db.tb_Contents
                            join c in db.tb_Catogory on q.CategoryId equals c.Id
                            where c.Id == detail.CategoryId && q.Status == true && q.Id != detail.Id
                            orderby q.Modified descending
                            select q).ToList();
                return list;
            }
            return null;
        }
    }
}
