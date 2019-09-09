using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;
using ShopOnlineModel.EF;
using System.Net;

namespace shoponline.Areas.Administrators.Controllers
{
    public class LanguagesController : Controller
    {
        LanguageDao Dao = new LanguageDao();

        public ActionResult Index(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(Dao.ListAll().Where(x => x.LanguageId.Contains(SearchString) || x.Description.Contains(SearchString)).OrderBy(x => x.LanguageId).ToList());
            }
            else
            {
                return View(Dao.ListAll().OrderBy(x => x.LanguageId).ToList());
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_Languages entity)
        {
            if (ModelState.IsValid)
            {
                string Id = Dao.Insert(entity);
                return RedirectToAction("Edit", "Languages", new { LanguageId = Id });
            }
            return View(entity);
        }
        [HttpGet]
        public ActionResult Edit(string LanguageId)
        {
            if (string.IsNullOrEmpty(LanguageId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = Dao.Detail(LanguageId);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Languages entity)
        {
            if (ModelState.IsValid)
            {
                Dao.Update(entity);
            }
            return View(entity);
        }

        public ActionResult Remove(string LanguageId)
        {
            if (string.IsNullOrEmpty(LanguageId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.Delete(LanguageId);
            return RedirectToAction("Index");
        }
    }
}