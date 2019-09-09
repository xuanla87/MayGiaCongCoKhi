using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;
using ShopOnlineModel.EF;
using PagedList;
using System.Net;

namespace shoponline.Areas.Administrators.Controllers
{
    public class MenusController : Controller
    {
        MenuDao Dao = new MenuDao();
        public ActionResult Index(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.ListAll().ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(entity.Where(x => x.Text.Contains(SearchString) || x.Url.Contains(SearchString)).OrderBy(x => x.TypeId).OrderBy(x => x.Order).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(entity.OrderBy(x => x.TypeId).OrderBy(x => x.Order).ToPagedList(pageNum, 20));
            }
        }

        public ActionResult Add()
        {
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name");
            ViewBag.ParentId = new SelectList(Dao.ListAll(), "Id", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_Menus entity)
        {
            if (ModelState.IsValid)
            {
                entity.Order = entity.Order ?? 0;
                entity.Status = entity.Status ?? true;
                long Id = Dao.Insert(entity);
                return RedirectToAction("Edit", "Menus", new { Id = Id });
            }
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name", entity.TypeId);
            ViewBag.ParentId = new SelectList(Dao.ListAll(), "Id", "Text", entity.ParentId);
            return View(entity);
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
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name", entity.TypeId);
            ViewBag.ParentId = new SelectList(Dao.ListAll(), "Id", "Text", entity.ParentId);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Menus entity)
        {
            if (ModelState.IsValid)
            {
                entity.Order = entity.Order ?? 0;
                entity.Status = entity.Status ?? true;
                Dao.Update(entity);
            }
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name", entity.TypeId);
            ViewBag.ParentId = new SelectList(Dao.ListAll(), "Id", "Text", entity.ParentId);
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

        public ActionResult MenuType()
        {
            return View(Dao.ListAllType());
        }

        public ActionResult AddMenuType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMenuType(tb_MenuType entity)
        {
            if (ModelState.IsValid)
            {
                long Id = Dao.InsertType(entity);
                return RedirectToAction("EditMenuType", "Menus", new { Id = Id });
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult EditMenuType(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = Dao.DetailType(Id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenuType(tb_MenuType entity)
        {
            if(ModelState.IsValid)
            {
                Dao.UpdateType(entity);
            }
            return View(entity);
        }

        public ActionResult RemoveMenuType(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.DeleteType(Id);
            return RedirectToAction("MenuType", "Menus");
        }
    }
}