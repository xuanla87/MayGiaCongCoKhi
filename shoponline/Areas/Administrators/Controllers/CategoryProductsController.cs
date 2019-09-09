using PagedList;
using shoponline.Code;
using shoponline.Models;
using ShopOnlineModel.Dao;
using ShopOnlineModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Areas.Administrators.Controllers
{
    public class CategoryProductsController : Controller
    {
        CategoryDao Dao = new CategoryDao();
        public JsonResult IsLinksExists(string slug, long? Id)
        {
            return Json(!Dao.CheckSlug(slug, Id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.ListAllByTaxonomy("Product");
            var ouput = new List<CategoryProductsModel>();
            foreach (var item in entity)
            {
                ouput.Add(new CategoryProductsModel
                {
                    Date = item.Date,
                    Id = item.Id,
                    Meta_Description = item.Meta_Description,
                    Meta_Keyword = item.Meta_Keyword,
                    Meta_Title = item.Meta_Title,
                    Modified = item.Modified,
                    Name = item.Name,
                    Order = item.Order,
                    ParentId = item.ParentId,
                    Slug = item.Slug,
                    Status = item.Status,
                    Taxonomy = item.Taxonomy,
                    Thumbnail = item.Thumbnail,
                    Title = item.Title,
                    UserId = item.UserId
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(ouput.Where(x => x.Title.Contains(SearchString) || x.Name.Contains(SearchString)).OrderBy(x => x.Order).OrderBy(x => x.ParentId).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(ouput.OrderBy(x => x.Order).OrderBy(x => x.ParentId).ToPagedList(pageNum, 20));
            }
        }

        public ActionResult Add()
        {

            ViewBag.ParentId = new SelectList(Dao.ListAllByTaxonomy("Product"), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryProductsModel input)
        {
            if (ModelState.IsValid)
            {
                input.UserId = SessionHelper.GetSessionUser().UserId;
                input.Taxonomy = input.Taxonomy ?? "Product";
                input.Status = input.Status ?? true;
                input.Order = input.Order ?? 0;
                input.Date = input.Date ?? DateTime.Now;
                input.Modified = input.Modified ?? DateTime.Now;
                var entity = new tb_Catogory();
                entity.Date = input.Date;
                entity.Meta_Description = input.Meta_Description;
                entity.Meta_Keyword = input.Meta_Keyword;
                entity.Meta_Title = input.Meta_Title;
                entity.Modified = input.Modified;
                entity.Name = input.Name;
                entity.Order = input.Order;
                entity.ParentId = input.ParentId;
                entity.Slug = input.Slug;
                entity.Status = input.Status;
                entity.Taxonomy = input.Taxonomy;
                entity.Thumbnail = input.Thumbnail;
                entity.Title = input.Title;
                entity.UserId = input.UserId;
                long Id = Dao.Insert(entity);
                return RedirectToAction("Edit", "CategoryProducts", new { Id = Id });
            }
            ViewBag.ParentId = new SelectList(Dao.ListAllByTaxonomy("Product"), "Id", "Name", input.ParentId);
            return View(input);
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
            var output = new CategoryProductsModel();
            output.Date = entity.Date;
            output.Id = entity.Id;
            output.Meta_Description = entity.Meta_Description;
            output.Meta_Keyword = entity.Meta_Keyword;
            output.Meta_Title = entity.Meta_Title;
            output.Modified = entity.Modified;
            output.Name = entity.Name;
            output.Order = entity.Order;
            output.ParentId = entity.ParentId;
            output.Slug = entity.Slug;
            output.Status = entity.Status;
            output.Taxonomy = entity.Taxonomy;
            output.Thumbnail = entity.Thumbnail;
            output.Title = entity.Title;
            output.UserId = entity.UserId;
            ViewBag.ParentId = new SelectList(Dao.ListAllByTaxonomy("Product"), "Id", "Name", output.ParentId);
            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryProductsModel entity)
        {
            if (ModelState.IsValid)
            {
                entity.UserId = SessionHelper.GetSessionUser().UserId;
                entity.Taxonomy = entity.Taxonomy ?? "Product";
                entity.Status = entity.Status ?? true;
                entity.Order = entity.Order ?? 0;
                entity.Date = entity.Date ?? DateTime.Now;
                entity.Modified = entity.Modified ?? DateTime.Now;
                var input = new tb_Catogory();
                input.Date = entity.Date;
                input.Id = entity.Id;
                input.Meta_Description = entity.Meta_Description;
                input.Meta_Keyword = entity.Meta_Keyword;
                input.Meta_Title = entity.Meta_Title;
                input.Modified = entity.Modified;
                input.Name = entity.Name;
                input.Order = entity.Order;
                input.ParentId = entity.ParentId;
                input.Slug = entity.Slug;
                input.Status = entity.Status;
                input.Taxonomy = entity.Taxonomy;
                input.Thumbnail = entity.Thumbnail;
                input.Title = entity.Title;
                input.UserId = entity.UserId;
                Dao.Update(input);
            }
            ViewBag.ParentId = new SelectList(Dao.ListAllByTaxonomy("Product"), "Id", "Name", entity.ParentId);
            return View(entity);
        }

        public ActionResult Remove(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Dao.Delete(Id);
            return RedirectToAction("Index");
        }

    }
}