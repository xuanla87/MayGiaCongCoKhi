﻿using PagedList;
using ShopOnlineModel.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoponline.Models;
using shoponline.Code;
using ShopOnlineModel.EF;
using System.Net;

namespace shoponline.Areas.Administrators.Controllers
{
    public class ContentsController : BaseController
    {
        ContentDao Dao = new ContentDao();
        CategoryDao DaoCategories = new CategoryDao();
        public JsonResult IsLinksExists(string slug, long? Id)
        {
            return Json(!Dao.CheckSlug(slug, Id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.ListAllByTaxonomy("Content");
            var output = new List<ContentsModel>();
            foreach (var item in entity)
            {
                output.Add(new ContentsModel
                {
                    CategoryId = item.CategoryId,
                    Content = item.Content,
                    Date = item.Date,
                    Description = item.Description,
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
                    UserId = item.UserId,
                    View = item.View
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(output.Where(x => x.Title.Contains(SearchString) || x.Name.Contains(SearchString)).OrderByDescending(x => x.Modified).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(output.OrderByDescending(x => x.Modified).ToPagedList(pageNum, 20));
            }
        }

        public ActionResult Add()
        {
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Content"), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(ContentsModel input)
        {
            if (ModelState.IsValid)
            {
                input.UserId = SessionHelper.GetSessionUser().UserId;
                input.Taxonomy = input.Taxonomy ?? "Content";
                input.Status = input.Status ?? true;
                input.View = input.View ?? 0;
                input.Order = input.Order ?? 0;
                input.Date = input.Date ?? DateTime.Now;
                input.Modified = input.Modified ?? DateTime.Now;
                var entity = new tb_Contents();
                entity.CategoryId = input.CategoryId;
                entity.Content = input.Content;
                entity.Date = input.Date;
                entity.Description = input.Description;
                entity.Id = input.Id;
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
                entity.View = input.View;
                long Id = Dao.Insert(entity);
                return RedirectToAction("Edit", "Contents", new { Id = Id });
            }
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Content"), "Id", "Title", input.CategoryId);
            return View(input);
        }

        [HttpGet]
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
            var ouput = new ContentsModel();
            ouput.CategoryId = entity.CategoryId;
            ouput.Content = entity.Content;
            ouput.Date = entity.Date;
            ouput.Description = entity.Description;
            ouput.Id = entity.Id;
            ouput.Meta_Description = entity.Meta_Description;
            ouput.Meta_Keyword = entity.Meta_Keyword;
            ouput.Meta_Title = entity.Meta_Title;
            ouput.Modified = Convert.ToDateTime(entity.Modified);
            ouput.Name = entity.Name;
            ouput.Order = entity.Order;
            ouput.ParentId = entity.ParentId;
            ouput.Slug = entity.Slug;
            ouput.Status = entity.Status;
            ouput.Taxonomy = entity.Taxonomy;
            ouput.Thumbnail = entity.Thumbnail;
            ouput.Title = entity.Title;
            ouput.UserId = entity.UserId;
            ouput.View = entity.View;
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Content"), "Id", "Title", ouput.CategoryId);
            return View(ouput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ContentsModel input)
        {
            if (ModelState.IsValid)
            {
                var entity = new tb_Contents();
                entity.CategoryId = input.CategoryId;
                entity.Content = input.Content;
                entity.Date = input.Date;
                entity.Description = input.Description;
                entity.Id = input.Id;
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
                entity.View = input.View;
                entity.Modified = Convert.ToDateTime(input.Modified);
                Dao.Update(entity);
            }
            ViewBag.CategoryId = new SelectList(DaoCategories.ListAllByTaxonomy("Content"), "Id", "Title", input.CategoryId);
            return View(input);
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
    }
}