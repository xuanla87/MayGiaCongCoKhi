using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.EF;
using ShopOnlineModel.Dao;
using System.Net;

namespace shoponline.Areas.Administrators.Controllers
{
    public class RolesController : BaseController
    {


        RoleDao Dao = new RoleDao();
        // GET: Administrators/Roles
        public ActionResult Index()
        {
            return View(Dao.ListAll());
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_Roles entity)
        {
            if (ModelState.IsValid)
            {
                Dao.Insert(entity);
                return RedirectToAction("Index");
            }
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
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Roles entity)
        {
            if (ModelState.IsValid)
            {
                Dao.Update(entity);
            }
            return View(entity);
        }
    }
}