using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;
using ShopOnlineModel.EF;
using System.Net;
using shoponline.Code;
using PagedList;
using shoponline.Models;

namespace shoponline.Areas.Administrators.Controllers
{
    public class ProductsController : BaseController
    {
        ProductDao Dao = new ProductDao();
        CategoryDao DaoCategories = new CategoryDao();
        LanguageDao DaoLanguage = new LanguageDao();
        MetaProductDao DaoMeta = new MetaProductDao();
        public JsonResult IsLinksExists(string Title, long? Id)
        {
            return Json(!Dao.CheckSlug(Title, Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCodeExists(string code, long? Id)
        {
            return Json(!Dao.CheckCode(code, Id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.ListAll();
            var output = new List<ProductsModel>();
            foreach (var item in entity)
            {
                output.Add(new ProductsModel
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
                    warranty = item.warranty,
                    CategoryName = gCategoryName(item.CategoryId)
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(output.Where(x => x.Title.Contains(SearchString) || x.Name.Contains(SearchString)).OrderByDescending(x => x.Modified).ToPagedList(pageNum, 5));
            }
            else
            {
                return View(output.OrderByDescending(x => x.Modified).ToPagedList(pageNum, 5));
            }
        }

        public ActionResult Add()
        {
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Product"), "Id", "Title");
            ViewBag.LanguageId = new SelectList(DaoLanguage.ListAll(), "LanguageId", "Description", "vi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(ProductsModel entity)
        {
            entity.UserId = SessionHelper.GetSessionUser().UserId;
            if (ModelState.IsValid)
            {
                entity.Status = entity.Status ?? true;
                entity.ViewCount = entity.ViewCount ?? 0;
                entity.Date = entity.Date ?? DateTime.Now;
                entity.Modified = entity.Modified ?? DateTime.Now;
                entity.Price = entity.Price ?? 0;
                entity.Sale = entity.Sale ?? 0;
                entity.Quantity = entity.Quantity ?? 1;
                entity.IncludedVAT = entity.IncludedVAT ?? true;
                var input = new tb_Products();
                input.CategoryId = entity.CategoryId;
                input.Code = entity.Code;
                input.Content = entity.Content;
                input.Date = entity.Date;
                input.Description = entity.Description;
                input.Id = entity.Id;
                input.Images = entity.Images;
                input.IncludedVAT = entity.IncludedVAT;
                input.LanguageId = entity.LanguageId;
                input.Meta_Description = entity.Meta_Description;
                input.Meta_Keyword = entity.Meta_Keyword;
                input.Meta_Title = entity.Meta_Title;
                input.Modified = entity.Modified;
                input.Name = entity.Name;
                input.Price = entity.Price;
                input.Quantity = entity.Quantity;
                input.Sale = entity.Sale;
                input.Status = entity.Status;
                input.Tag = entity.Tag;
                input.Thumbnail = entity.Thumbnail;
                input.Title = entity.Title;
                input.TopHot = entity.TopHot;
                input.UserId = entity.UserId;
                input.ViewCount = entity.ViewCount;
                input.warranty = entity.warranty;
                long Id = Dao.Insert(input);
                DaoMeta.SaveValue("Guide", entity.Guide, Id);
                DaoMeta.SaveValue("Infor", entity.Infor, Id);
                DaoMeta.SaveValue("HomeDisplay", entity.DisplayHome.ToString(), Id);
                DaoMeta.SaveValue("FullProduct", entity.FullProduct.ToString(), Id);
                return RedirectToAction("Edit", "Products", new { Id = Id });
            }
            ViewBag.LanguageId = new SelectList(DaoLanguage.ListAll(), "LanguageId", "Description", entity.LanguageId);
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Product"), "Id", "Title", entity.CategoryId);
            return View(entity);
        }

        public ActionResult Edit(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = Dao.Detail(Id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var input = new ProductsModel();
            input.CategoryId = entity.CategoryId;
            input.Code = entity.Code;
            input.Content = entity.Content;
            input.Date = entity.Date;
            input.Description = entity.Description;
            input.Id = entity.Id;
            input.Images = entity.Images;
            input.IncludedVAT = entity.IncludedVAT;
            input.LanguageId = entity.LanguageId;
            input.Meta_Description = entity.Meta_Description;
            input.Meta_Keyword = entity.Meta_Keyword;
            input.Meta_Title = entity.Meta_Title;
            input.Modified = entity.Modified;
            input.Name = entity.Name;
            input.Price = entity.Price;
            input.Quantity = entity.Quantity;
            input.Sale = entity.Sale;
            input.Status = entity.Status;
            input.Tag = entity.Tag;
            input.Thumbnail = entity.Thumbnail;
            input.Title = entity.Title;
            input.TopHot = entity.TopHot;
            input.UserId = entity.UserId;
            input.ViewCount = entity.ViewCount;
            input.warranty = entity.warranty;
            input.Guide = DaoMeta.GetValue("Guide", entity.Id);
            input.Infor = DaoMeta.GetValue("Infor", entity.Id);
            try
            {
                input.DisplayHome = bool.Parse(DaoMeta.GetValue("HomeDisplay", entity.Id));
                input.FullProduct = bool.Parse(DaoMeta.GetValue("FullProduct", entity.Id));
            }
            catch
            {
                input.DisplayHome = false;
                input.FullProduct = false;
            }
            ViewBag.LanguageId = new SelectList(DaoLanguage.ListAll(), "LanguageId", "Description", input.LanguageId);
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Product"), "Id", "Title", input.CategoryId);
            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProductsModel entity)
        {
            if (ModelState.IsValid)
            {
                entity.UserId = SessionHelper.GetSessionUser().UserId;
                entity.Status = entity.Status ?? true;
                entity.ViewCount = entity.ViewCount ?? 0;
                entity.Date = entity.Date ?? DateTime.Now;
                entity.Modified = entity.Modified ?? DateTime.Now;
                entity.Price = entity.Price ?? 0;
                entity.Sale = entity.Sale ?? 0;
                entity.Quantity = entity.Quantity ?? 1;
                entity.IncludedVAT = entity.IncludedVAT ?? true;
                var input = new tb_Products();
                input.CategoryId = entity.CategoryId;
                input.Code = entity.Code;
                input.Content = entity.Content;
                input.Date = entity.Date;
                input.Description = entity.Description;
                input.Id = entity.Id;
                input.Images = entity.Images;
                input.IncludedVAT = entity.IncludedVAT;
                input.LanguageId = entity.LanguageId;
                input.Meta_Description = entity.Meta_Description;
                input.Meta_Keyword = entity.Meta_Keyword;
                input.Meta_Title = entity.Meta_Title;
                input.Modified = entity.Modified;
                input.Name = entity.Name;
                input.Price = entity.Price;
                input.Quantity = entity.Quantity;
                input.Sale = entity.Sale;
                input.Status = entity.Status;
                input.Tag = entity.Tag;
                input.Thumbnail = entity.Thumbnail;
                input.Title = entity.Title;
                input.TopHot = entity.TopHot;
                input.UserId = entity.UserId;
                input.ViewCount = entity.ViewCount;
                input.warranty = entity.warranty;
                Dao.Update(input);
                DaoMeta.SaveValue("Guide", entity.Guide, entity.Id);
                DaoMeta.SaveValue("Infor", entity.Infor, entity.Id);
            }
            ViewBag.LanguageId = new SelectList(DaoLanguage.ListAll(), "LanguageId", "Description", entity.LanguageId);
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Product"), "Id", "Title", entity.CategoryId);
            return View(entity);
        }

        public ActionResult Remove(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.Delete(Id);
            return RedirectToAction("Index");
        }

        private string gCategoryName(long? Id)
        {
            if (Id == null)
            {
                return "None";
            }
            var entity = DaoCategories.Detail(Id);
            if (entity == null)
            {
                return "None";
            }
            return entity.Name;
        }

    }
}