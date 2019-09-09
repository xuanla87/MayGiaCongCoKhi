using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;
using PagedList;

namespace shoponline.Controllers
{
    public class ProductController : Controller
    {
        CategoryDao Category = new CategoryDao();
        ProductDao Product = new ProductDao();
        ContentDao Contents = new ContentDao();

        public ActionResult Index(string slug, int? page)
        {
            string controllerAction;
            if (string.IsNullOrEmpty(slug))
            {
                controllerAction = "AllProduct";
            }
            else
            {
                var DetailCategory = Category.CheckSlug(slug, null);
                if (DetailCategory)
                {
                    controllerAction = "CategoryProduct";
                }
                else
                {
                    var Detail = Product.CheckSlug(slug, null);
                    if (Detail)
                    {
                        controllerAction = "DetailProduct";
                    }
                    else
                    {
                        controllerAction = "404";
                    }
                }

            }
            ViewBag.Action = controllerAction;
            return View();
        }

        public ActionResult AllProduct(int? page)
        {
            int pageNum = page ?? 1;
            var list = Product.ViewAll().ToPagedList(pageNum, 15);
            return PartialView(list);
        }
        public ActionResult CategoryProduct(int? page, string slug)
        {
            return PartialView();
        }

        public ActionResult DetailProduct(string slug)
        {
            return PartialView();
        }

        public ActionResult boxCategory()
        {
            var list = Category.ViewAllByTaxonomy("Product");
            string ouput = "";
            foreach (var item in list)
            {
                if (item.ParentId == null)
                {
                    ouput += "<div>";
                    ouput += item.Title;
                    ouput += subboxCategory(item.Id);
                    ouput += "</div>";
                }
            }
            ViewBag.Data = ouput;
            return PartialView();
        }

        public ActionResult Tophot()
        {
            var list = Product.ViewTopHot();
            return PartialView(list);
        }
        public ActionResult TopNew()
        {
            var list = Product.ViewTopNew();
            return PartialView(list);
        }

        public ActionResult TopFull()
        {
            var list = Product.ViewTopFull();
            return PartialView(list);
        }

        private string subboxCategory(long Id)
        {
            var list = Category.ViewAllByParentId(Id);
            if (list.Count() > 0)
            {
                string ouput = "<ul>";
                foreach (var item in list)
                {
                    ouput += "<li>";
                    ouput += item.Title;
                    ouput += subboxCategory(item.Id);
                    ouput += "</li>";
                }
                ouput += "<ul>";
                return ouput;
            }
            return ""; ;
        }
    }
}