using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.EF;
using ShopOnlineModel.Dao;
using System.Net;
using shoponline.Models;
using ToolAnhxuan;

namespace shoponline.Areas.Administrators.Controllers
{
    public class AdminController : Controller
    {
        RoleDao Dao = new RoleDao();
        UserDao UDao = new UserDao();
        Mahoa mhPass = new Mahoa();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AdminModel entity)
        {
            if (ModelState.IsValid)
            {
                var RoleName = entity.RoleName;
                long RoleId = 0;
                var Role = Dao.GetByName(RoleName);
                if (Role != null)
                {
                    RoleId = Role.Id;
                    var User = new tb_Users();
                    User.Date = DateTime.Now;
                    User.Modified = DateTime.Now;
                    User.RoleId = RoleId;
                    User.Status = true;
                    User.UserEmail = entity.UserEmail;
                    User.UserName = entity.UserName;
                    User.UserPassword = mhPass.MahoaMD5(entity.UserPassword);
                    UDao.Insert(User);
                }
                else
                {
                    Role = new tb_Roles();
                    Role.RoleName = RoleName;
                    Role.RoleDescription = "Người quản trị cao nhất!";
                    Role.RoleAction = 7;
                    Dao.Insert(Role);
                    RoleId = Role.Id;
                    var User = new tb_Users();
                    User.Date = DateTime.Now;
                    User.Modified = DateTime.Now;
                    User.RoleId = RoleId;
                    User.Status = true;
                    User.UserEmail = entity.UserEmail;
                    User.UserName = entity.UserName;
                    User.UserPassword = mhPass.MahoaMD5(entity.UserPassword);
                    UDao.Insert(User);
                }
                return RedirectToAction("Index");
            }
            return View(entity);
        }
    }
}