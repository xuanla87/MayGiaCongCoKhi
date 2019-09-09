using PagedList;
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
    public class SlidersController : Controller
    {
        SliderDao Dao = new SliderDao();
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_Sliders entity)
        {
            if (ModelState.IsValid)
            {
                entity.Order = entity.Order ?? 0;
                entity.Status = entity.Status ?? true;
                long Id = Dao.Insert(entity);
                return RedirectToAction("Edit", "Sliders", new { Id = Id });
            }
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name", entity.TypeId);
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
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Sliders entity)
        {
            if (ModelState.IsValid)
            {
                entity.Order = entity.Order ?? 0;
                entity.Status = entity.Status ?? true;
                Dao.Update(entity);
            }
            ViewBag.TypeId = new SelectList(Dao.ListAllType(), "Id", "Name", entity.TypeId);
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

        public ActionResult SliderType()
        {
            return View(Dao.ListAllType());
        }

        public ActionResult AddSliderType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSliderType(tb_SliderType entity)
        {
            if (ModelState.IsValid)
            {
                long Id = Dao.InsertType(entity);
                return RedirectToAction("EditSliderType", "Sliders", new { Id = Id });
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult EditSliderType(long? Id)
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
        public ActionResult EditSliderType(tb_SliderType entity)
        {
            if (ModelState.IsValid)
            {
                Dao.UpdateType(entity);
            }
            return View(entity);
        }

        public ActionResult RemoveSliderType(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.DeleteType(Id);
            return RedirectToAction("SliderType", "Sliders");
        }
    }
}