using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.EF;
using ShopOnlineModel.Dao;
using ToolAnhxuan;
using System.Net;
using shoponline.Models;
using shoponline.Code;

namespace shoponline.Areas.Administrators.Controllers
{
    public class UsersController : BaseController
    {
        UserDao Dao = new UserDao();
        RoleDao RDao = new RoleDao();
        Mahoa mhPass = new Mahoa();

        public JsonResult IsUserNameExists(string UserName, long? Id)
        {
            return Json(!Dao.CheckUserNameExits(UserName, Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsUserEmailExists(string UserEmail, long? Id)
        {
            return Json(!Dao.CheckUserEmailExits(UserEmail, Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsUserPassExists(string OldPassword, long? Id)
        {
            return Json(Dao.CheckOldPass(mhPass.MahoaMD5(OldPassword), Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(Dao.ListAll());
        }

        public ActionResult Add()
        {
            ViewBag.RoleId = new SelectList(RDao.ListAll(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(tb_Users entity)
        {
            if (ModelState.IsValid)
            {
                var Input = new tb_Users();
                Input.RoleId = entity.RoleId;
                Input.UserName = entity.UserName;
                Input.UserEmail = entity.UserEmail;
                Input.UserPassword = mhPass.MahoaMD5(entity.UserPassword);
                Input.Status = true;
                Input.Date = DateTime.Now;
                Input.Modified = DateTime.Now;
                Dao.Insert(Input);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RDao.ListAll(), "Id", "RoleName", entity.RoleId);
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
            ViewBag.RoleId = new SelectList(RDao.ListAll(), "Id", "RoleName", entity.RoleId);
            var Ouput = new UsersModel();
            Ouput.Date = entity.Date;
            Ouput.Id = entity.Id;
            Ouput.Modified = entity.Modified;
            Ouput.RoleId = entity.RoleId;
            Ouput.Status = entity.Status;
            Ouput.UserEmail = entity.UserEmail;
            Ouput.UserName = entity.UserName;
            return View(Ouput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(UsersModel entity)
        {
            if (ModelState.IsValid)
            {
                var Input = new tb_Users();
                Input.Id = entity.Id;
                Input.RoleId = entity.RoleId;
                Input.UserName = entity.UserName;
                Input.UserEmail = entity.UserEmail;
                Input.UserPassword = mhPass.MahoaMD5(entity.UserPassword);
                Input.Status = entity.Status;
                Input.Date = entity.Date;
                Input.Modified = DateTime.Now;
                Dao.Update(Input);
            }
            ViewBag.RoleId = new SelectList(RDao.ListAll(), "Id", "RoleName", entity.RoleId);
            return View(entity);
        }

        public ActionResult Delete(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.Delete(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePass(long? Id)
        {
            var User = Dao.Detail(Id);
            var entity = new UsersModel();
            entity.Date = User.Date;
            entity.Id = User.Id;
            entity.Modified = User.Modified;
            entity.RoleId = User.RoleId;
            entity.Status = User.Status;
            entity.UserEmail = User.UserEmail;
            entity.UserName = User.UserName;
            entity.UserPassword = User.UserPassword;
            entity.UserGender = null;
            entity.UserFullName = null;
            entity.UserBirthDay = null;
            entity.UserAvata = null;
            entity.UserAbout = null;
            entity.ConfirmPassword = User.UserPassword;
            entity.UserPassword = User.UserPassword;
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(UsersModel entity)
        {
            //if (ModelState.IsValid)
            //{
                long Id = entity.Id;
                string OldPass = mhPass.MahoaMD5(entity.OldPassword);
                string NewPass = mhPass.MahoaMD5(entity.NewPassword);
                Dao.ChangePass(Id, OldPass, NewPass);
            return View(entity);
            //}
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult InforUser()
        {
            var session = SessionHelper.GetSessionUser();
            var Usser = Dao.Detail(session.UserId);
            return View(Usser);
        }
    }
}