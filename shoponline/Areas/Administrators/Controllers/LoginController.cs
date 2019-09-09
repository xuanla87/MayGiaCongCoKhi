using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoponline.Models;
using ShopOnlineModel.Dao;
using shoponline.Code;
using ToolAnhxuan;
namespace shoponline.Areas.Administrators.Controllers
{
    public class LoginController : Controller
    {
        // GET: Administrators/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var mahoaPass = new Mahoa();
                var result = dao.Login(model.UserName, mahoaPass.MahoaMD5(model.UserPassword));
                if (result)
                {
                    var user = dao.DetailByUser(model.UserName);
                    var userSession = new LoginModel();
                    userSession.UserId = user.Id;
                    userSession.UserName = model.UserName;
                    userSession.RememberMe = model.RememberMe;
                    SessionHelper.SetSessionUser(userSession);
                    return RedirectToAction("Index", "Default");
                }
                else
                    ModelState.AddModelError("", "Đăng nhập không thành công!");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            SessionHelper.SetSessionUser(null);
            return RedirectToAction("Index");
        }
    }
}