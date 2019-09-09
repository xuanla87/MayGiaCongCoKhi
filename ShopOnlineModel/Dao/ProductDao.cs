using ShopOnlineModel.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineModel.Dao
{
    public class ProductDao
    {
        ShopModelDbContext db = null;
        public ProductDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Products entity)
        {
            db.tb_Products.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(tb_Products entity)
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
                var entity = db.tb_Products.Find(Id);
                var meta = db.tb_MetaProduct.Where(x => x.ProductId == entity.Id).ToList();
                db.tb_MetaProduct.RemoveRange(meta);
                db.SaveChanges();
                db.tb_Products.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public tb_Products Detail(long? Id)
        {
            return db.tb_Products.Find(Id);
        }

        public tb_Products DetailBySlug(string Title)
        {
            return db.tb_Products.SingleOrDefault(x => x.Title == Title && x.Status == true);
        }

        public IEnumerable<tb_Products> ListAll()
        {
            return db.tb_Products.ToList();
        }

        public bool CheckSlug(string Title, long? Id)
        {
            return db.tb_Products.Any(x => x.Title == Title && x.Id != Id);
        }

        public bool CheckCode(string Code, long? Id)
        {
            return db.tb_Products.Any(x => x.Code == Code && x.Id != Id);
        }

        public List<tb_Products> ViewAll()
        {
            return db.tb_Products.Where(x => x.Status == true).OrderByDescending(x => x.Modified).ToList();
        }

        public List<tb_Products> ViewAllByCategorySlug(string Slug)
        {
            var detaiCategory = db.tb_Catogory.SingleOrDefault(x => x.Slug == Slug && x.Status == true);
            var listProduct = new List<tb_Products>();
            if (detaiCategory != null)
            {
                listProduct.AddRange(ViewAllByCategoryId(detaiCategory.Id));
                var listCatogory = db.tb_Catogory.Where(x => x.ParentId == detaiCategory.Id && x.Status == true).ToList();
                foreach (var item in listCatogory)
                {
                    listProduct.AddRange(ViewAllByCategoryId(item.Id));
                }
            }
            return listProduct.OrderByDescending(x => x.Modified).ToList();
        }

        public List<tb_Products> ViewAllByCategoryId(long Id)
        {
            return db.tb_Products.Where(x => x.CategoryId == Id && x.Status == true).ToList();
        }

        public List<tb_Products> ViewTopHot()
        {
            return db.tb_Products.Where(x => x.Status == true).OrderByDescending(x => x.Modified).ToList();
        }

        public List<tb_Products> ViewTopNew()
        {
            return db.tb_Products.Where(x => x.Status == true).OrderByDescending(x => x.Modified).Take(6).ToList();
        }

        public List<tb_Products> ViewTopFull()
        {
            var list = db.tb_Products.Where(x => x.Status == true).OrderByDescending(x => x.Modified).ToList();
            List<tb_Products> pro = new List<tb_Products>();
            for (int i = 0; i < list.Count; i++)
            {
                if (new MetaProductDao().GetValue("FullProduct", list[i].Id) == "True")
                {
                    pro.Add(list[i]);
                }
            }
            return pro;
        }

        public List<tb_Products> ViewSearch(string keyword, ref int totalRecord, int? pageIndex = 1, int pageSize = 1)
        {
            totalRecord = db.tb_Products.Where(x => x.Name.Contains(keyword) && x.Status == true).Count();
            var model = db.tb_Products.Where(x => x.Name.Contains(keyword) && x.Status == true).OrderByDescending(x => x.Modified).ToList();
            return model;
        }

        public List<tb_Products> ViewRelated(string slug)
        {
            var detail = db.tb_Products.SingleOrDefault(x => x.Title == slug && x.Status == true);
            if (detail != null)
            {
                var list = (from q in db.tb_Products
                            join cp in db.tb_Catogory on q.CategoryId equals cp.Id
                            where q.Id != detail.Id && cp.Id == detail.CategoryId
                            select q).ToList();
                return list;
            }
            return null;
        }

        public void upView(string slug)
        {
            var detail = db.tb_Products.SingleOrDefault(x => x.Title == slug && x.Status == true);
            if (detail != null)
            {
                detail.ViewCount++;
                db.Entry(detail).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
