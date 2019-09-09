using ShopOnlineModel.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineModel.Dao
{
    public class CartDao
    {
        ShopModelDbContext db = null;
        public CartDao()
        {
            db = new ShopModelDbContext();
        }

        public long Insert(tb_Carts entity)
        {
            db.tb_Carts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(tb_Carts entity)
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
                var entity = db.tb_Carts.Find(Id);
                var cdetail = db.tb_DetailCart.Where(x => x.CartId == Id).ToList();
                db.tb_DetailCart.RemoveRange(cdetail);
                db.SaveChanges();
                db.tb_Carts.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public long InsertCDetail(tb_DetailCart entity)
        {
            db.tb_DetailCart.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool UpdateCDetail(tb_DetailCart entity)
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

        public bool DeleteCDetail(long? Id)
        {
            try
            {
                var entity = db.tb_DetailCart.Find(Id);
                db.tb_DetailCart.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<tb_Carts> ListSuccess()
        {
            var list = db.tb_Carts.Where(x => x.Approval == true && x.Status == true).OrderByDescending(x => x.Modified).ToList();
            return list;
        }

        public List<tb_Carts> listAll()
        {
            return db.tb_Carts.OrderByDescending(x => x.Modified).ToList();
        }

        public List<tb_Carts> listNew()
        {
            var list = db.tb_Carts.Where(x => x.Approval == false && x.Status == true).OrderByDescending(x => x.Modified).ToList();
            return list;
        }

        public bool Approval(long? Id)
        {
            try
            {
                var entity = db.tb_Carts.Find(Id);
                if (entity.Approval == true)
                    entity.Approval = false;
                else
                    entity.Approval = true;
                entity.Modified = DateTime.Now;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<tb_Products> ListDetailByCartId(long Id)
        {
            var list = (from q in db.tb_Products
                        join dc in db.tb_DetailCart on q.Id equals dc.ProductId
                        where dc.CartId == Id
                        select new
                        {
                            q.CategoryId,
                            q.Code,
                            q.Content,
                            q.Date,
                            q.Description,
                            q.Id,
                            q.Images,
                            q.IncludedVAT,
                            q.LanguageId,
                            q.Meta_Description,
                            q.Meta_Keyword,
                            q.Meta_Title,
                            q.Modified,
                            q.Name,
                            dc.Price,
                            dc.Quantity,
                            q.Sale,
                            q.Status,
                            q.Tag,
                            q.Thumbnail,
                            q.Title,
                            q.TopHot,
                            q.UserId,
                            q.ViewCount,
                            q.warranty
                        }).ToList();
            var listP = new List<tb_Products>();
            foreach (var item in list)
            {
                listP.Add(new tb_Products
                {
                    CategoryId = item.CategoryId,
                    Code = item.Code,
                    Content = item.Content,
                    Date = item.Date,
                    Description = item.Description,
                    Id = item.Id,
                    Images = item.Images,
                    IncludedVAT = item.IncludedVAT,
                    LanguageId = item.LanguageId,
                    Meta_Description = item.Meta_Description,
                    Meta_Keyword = item.Meta_Keyword,
                    Meta_Title = item.Meta_Title,
                    Modified = item.Modified,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Sale = item.Sale,
                    Status = item.Status,
                    Tag = item.Tag,
                    Thumbnail = item.Thumbnail,
                    Title = item.Title,
                    TopHot = item.TopHot,
                    UserId = item.UserId,
                    ViewCount = item.ViewCount,
                    warranty = item.warranty
                });
            }
            return listP;
        }
    }
}
