using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoponline.Models;
using ShopOnlineModel.Dao;

namespace shoponline.Areas.Administrators.Controllers
{
    public class SettingsController : BaseController
    {
        SettingDao Dao = new SettingDao();
        MenuDao MDao = new MenuDao();
        public ActionResult Index()
        {
            var ouput = new SettingsModel();
            ouput.Banener = Dao.GetValue("Banener");
            ouput.Customize_Site = Dao.GetValue("Customize_Site");
            ouput.Infor_Site = Dao.GetValue("Infor_Site");
            ouput.Logo = Dao.GetValue("Logo");
            ouput.Site_Description = Dao.GetValue("Site_Description");
            ouput.Site_Keywords = Dao.GetValue("Site_Keywords");
            ouput.Site_Title = Dao.GetValue("Site_Title");
            ouput.Terms_Site = Dao.GetValue("Terms_Site");
            ouput.Main_Menu = Dao.GetValue("Main_Menu");
            ouput.Menu_01 = Dao.GetValue("Menu_01");
            ouput.Menu_02 = Dao.GetValue("Menu_02");
            ouput.Menu_03 = Dao.GetValue("Menu_03");
            ouput.Menu_04 = Dao.GetValue("Menu_04");
            ouput.Menu_05 = Dao.GetValue("Menu_05");

    
            ouput.Name_Support01 = Dao.GetValue("Name_Support01");
            ouput.Phone_Support01 = Dao.GetValue("Phone_Support01");
            ouput.Skype_Support01 = Dao.GetValue("Skype_Support01");
            ouput.Yahoo_Support01 = Dao.GetValue("Yahoo_Support01");
            ouput.Name_Support02 = Dao.GetValue("Name_Support02");
            ouput.Phone_Support02 = Dao.GetValue("Phone_Support02");
            ouput.Skype_Support02 = Dao.GetValue("Skype_Support02");
            ouput.Yahoo_Support02 = Dao.GetValue("Yahoo_Support02");
            ouput.Name_Support03 = Dao.GetValue("Name_Support03");
            ouput.Phone_Support03 = Dao.GetValue("Phone_Support03");
            ouput.Skype_Support03 = Dao.GetValue("Skype_Support03");
            ouput.Yahoo_Support03 = Dao.GetValue("Yahoo_Support03");

            ViewBag.Main_Menu = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Main_Menu);
            ViewBag.Menu_01 = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Menu_01);
            ViewBag.Menu_02 = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Menu_02);
            ViewBag.Menu_03 = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Menu_03);
            ViewBag.Menu_04 = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Menu_04);
            ViewBag.Menu_05 = new SelectList(MDao.ListAllType(), "Id", "Name", ouput.Menu_05);
            return View(ouput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(SettingsModel setting)
        {
            if (ModelState.IsValid)
            {
                Dao.SaveValue("Banener", setting.Banener);
                Dao.SaveValue("Customize_Site", setting.Customize_Site);
                Dao.SaveValue("Infor_Site", setting.Infor_Site);
                Dao.SaveValue("Logo", setting.Logo);
                Dao.SaveValue("Site_Description", setting.Site_Description);
                Dao.SaveValue("Site_Keywords", setting.Site_Keywords);
                Dao.SaveValue("Site_Title", setting.Site_Title);
                Dao.SaveValue("Terms_Site", setting.Terms_Site);
                Dao.SaveValue("Main_Menu", setting.Main_Menu);
                Dao.SaveValue("Menu_01", setting.Menu_01);
                Dao.SaveValue("Menu_02", setting.Menu_02);
                Dao.SaveValue("Menu_03", setting.Menu_03);
                Dao.SaveValue("Menu_04", setting.Menu_04);
                Dao.SaveValue("Menu_05", setting.Menu_05);

                Dao.SaveValue("Name_Support01", setting.Name_Support01);
                Dao.SaveValue("Phone_Support01", setting.Phone_Support01);
                Dao.SaveValue("Skype_Support01", setting.Skype_Support01);
                Dao.SaveValue("Yahoo_Support01", setting.Yahoo_Support01);

                Dao.SaveValue("Name_Support02", setting.Name_Support02);
                Dao.SaveValue("Phone_Support02", setting.Phone_Support02);
                Dao.SaveValue("Skype_Support02", setting.Skype_Support02);
                Dao.SaveValue("Yahoo_Support02", setting.Yahoo_Support02);

                Dao.SaveValue("Name_Support03", setting.Name_Support03);
                Dao.SaveValue("Phone_Support03", setting.Phone_Support03);
                Dao.SaveValue("Skype_Support03", setting.Skype_Support03);
                Dao.SaveValue("Yahoo_Support03", setting.Yahoo_Support03);
            }
            ViewBag.Main_Menu = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Main_Menu);
            ViewBag.Menu_01 = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Menu_01);
            ViewBag.Menu_02 = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Menu_02);
            ViewBag.Menu_03 = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Menu_03);
            ViewBag.Menu_04 = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Menu_04);
            ViewBag.Menu_05 = new SelectList(MDao.ListAllType(), "Id", "Name", setting.Menu_05);
            return View(setting);
        }
    }
}