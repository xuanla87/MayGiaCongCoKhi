using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;

namespace shoponline.Controllers
{
    public class MenuViewController : Controller
    {
        MenuDao Dao = new MenuDao();
        SettingDao SDao = new SettingDao();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewMain()
        {
            long Id = 0;
            try
            {
                Id = long.Parse(SDao.GetValue("Main_Menu"));
            }
            catch
            {
                Id = 0;
            }

            var lMneu = Dao.ViewByTypeId(Id);
            return PartialView(lMneu);
        }

        public ActionResult ViewAbout()
        {
            long Id = 0;
            try
            {
                Id = long.Parse(SDao.GetValue("Menu_03"));
            }
            catch
            {
                Id = 0;
            }
            var lMneu = Dao.ViewByTypeId(Id);
            return PartialView(lMneu);
        }
        public ActionResult ViewGiude()
        {
            long Id = 0;
            try
            {
                Id = long.Parse(SDao.GetValue("Menu_04"));
            }
            catch
            {
                Id = 0;
            }
            var lMneu = Dao.ViewByTypeId(Id);
            return PartialView(lMneu);
        }
        public ActionResult ViewIncentive()
        {
            long Id = 0;
            try
            {
                Id = long.Parse(SDao.GetValue("Menu_05"));
            }
            catch
            {
                Id = 0;
            }
            var lMneu = Dao.ViewByTypeId(Id);
            return PartialView(lMneu);
        }

        public ActionResult ViewMenu()
        {
            long Id = 0;
            try
            {
                Id = long.Parse(SDao.GetValue("Menu_01"));
            }
            catch
            {
                Id = 0;
            }
            var lMneu = Dao.ViewByTypeId(Id);
            string output = "";
            if (lMneu.Count() > 0)
            {
                int level = 0;
                output += "<div class=\"product-menu\">";
                output += "<div class=\"title\">" + Dao.DetailType(Id).Name;
                output += "</div>";
                output += "<ul class=\"nav\">";
                foreach (var item in lMneu)
                {
                    if ((item.ParentId == null) || (item.ParentId == 0))
                    {
                        output += "<li>";
                        output += "<a title=\"" + item.Text + "\" href=\"/" + item.Url + "\">";
                        output += item.Text;
                        output += "</a>";
                        output += submenu(item.Id, level++);
                        output += "</li>";
                    }
                }
                output += "</ul>";
                output += "</div>";
            }
            ViewBag.Data = output;
            return PartialView();
        }

        private string submenu(long Id, int level)
        {
            var list = Dao.ViewByParent(Id);
            string output = "";
            if (list.Count() > 0)
            {
                if (level > 0)
                {
                    output += "<ul class=\"nav nav-sub-" + level + "\">";
                }
                else
                {
                    output += "<ul class=\"nav\">";
                }
                foreach (var item in list)
                {
                    output += "<li>";
                    output += "<a title=\"" + item.Text + "\" href=\"/" + item.Url + "\">";
                    output += item.Text;
                    output += "</a>";
                    output += submenu(item.Id, level + 1);
                    output += "</li>";
                }
                output += "</ul>";
            }
            return output;
        }
    }
}