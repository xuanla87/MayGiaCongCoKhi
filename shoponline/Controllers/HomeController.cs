using PagedList;
using shoponline.Code;
using shoponline.Models;
using ShopOnlineModel.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.EF;

namespace shoponline.Controllers
{
    public class HomeController : Controller
    {
        CategoryDao Category = new CategoryDao();
        ProductDao Product = new ProductDao();
        ContentDao Contents = new ContentDao();
        ContactDao Contacts = new ContactDao();
        SliderDao SDao = new SliderDao();
        SettingDao SetDao = new SettingDao();
        MetaProductDao DaoMetaP = new MetaProductDao();

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Default(string slug, int? page)
        {
            string stAction;
            if (slug == null)
            {
                stAction = null;
                ViewBag.Title = SetDao.GetValue("Site_Title");
            }
            else
            {
                var DetailCategory = Category.DetailBySlug(slug);
                if (DetailCategory != null)
                {
                    stAction = DetailCategory.Taxonomy;
                    if (DetailCategory.Meta_Title != null)
                    {
                        ViewBag.Title = DetailCategory.Meta_Title;
                    }
                    ViewBag.Title = DetailCategory.Title;
                    ViewBag.Description = DetailCategory.Meta_Description;
                    ViewBag.Keywords = DetailCategory.Meta_Keyword;
                }
                else
                {
                    var DetailProduct = Product.DetailBySlug(slug);
                    if (DetailProduct != null)
                    {
                        stAction = "DetailProduct";
                        if (DetailProduct.Meta_Title != null)
                        {
                            ViewBag.Title = DetailProduct.Meta_Title;
                        }
                        ViewBag.Title = DetailProduct.Name;
                        ViewBag.Keywords = DetailProduct.Meta_Keyword;
                        ViewBag.Description = DetailProduct.Meta_Description;
                    }
                    else
                    {
                        var DetailContent = Contents.DetailBySlug(slug);
                        if (DetailContent != null)
                        {
                            if (DetailContent.Taxonomy == "Content")
                            {
                                stAction = "DetailContent";
                            }
                            else
                            {
                                stAction = "DetailPage";
                            }
                            if (DetailContent.Meta_Title != null)
                            {
                                ViewBag.Title = DetailContent.Meta_Title;
                            }
                            ViewBag.Keywords = DetailContent.Meta_Keyword;
                            ViewBag.Description = DetailContent.Meta_Description;
                            ViewBag.Title = DetailContent.Title;
                        }
                        else
                        {
                            stAction = "404";
                            ViewBag.Title = "404";
                        }
                    }
                }
            }
            ViewBag.vAction = stAction;
            ViewBag.vSlug = slug;
            return View();
        }

        public ActionResult CategoryContent(string slug, int? page)
        {
            int PageNum = page ?? 1;
            var list = Contents.ViewAllByCategorySlug(slug);
            var Detail = Category.DetailBySlug(slug);
            if (Detail != null)
            {
                var Parent = Category.Detail(Detail.ParentId);
                if (Parent != null)
                {
                    ViewBag.CPSlug = Parent.Slug;
                    ViewBag.CParent = Parent.Name;
                }
                ViewBag.CTitle = Detail.Name;
            }
            ViewBag.Slug = slug;
            return PartialView(list.ToPagedList(PageNum, 5));
        }

        public ActionResult CategoryProduct(string slug, int? page)
        {
            int PageNum = page ?? 1;
            var list = Product.ViewAllByCategorySlug(slug);
            var Detail = Category.DetailBySlug(slug);
            if (Detail != null)
            {
                var Parent = Category.Detail(Detail.ParentId);
                if (Parent != null)
                {
                    ViewBag.CPSlug = Parent.Slug;
                    ViewBag.CParent = Parent.Name;
                }
                ViewBag.CTitle = Detail.Name;
            }
            ViewBag.Slug = slug;
            ViewBag.Thumbnail = Detail.Thumbnail;
            return PartialView(list.ToPagedList(PageNum, 12));
        }

        public ActionResult DetailProduct(string slug)
        {
            var detail = Product.DetailBySlug(slug);
            var Guide = DaoMetaP.GetValue("Guide", detail.Id);
            var Infor = DaoMetaP.GetValue("Infor", detail.Id);
            string output = "";
            output += "<a title=\"Home\" href=\"/\">Trang chủ</a> <i class=\"fa fa-angle-double-right\"></i>";
            output += breadcrumb(detail.CategoryId);
            output += detail.Name;
            ViewBag.breadcrumb = output;
            var Related = Product.ViewRelated(slug).OrderByDescending(x => x.Modified).Take(6).ToList();
            var stRelated = "";
            if (Related.Count > 0)
            {
                stRelated += "<ul class=\"product-related\">";
                foreach (var item in Related)
                {
                    stRelated += "<li>";
                    stRelated += "<div class=\"item-thumnail\">";
                    if (item.Thumbnail != null)
                    {
                        stRelated += "<a href=\"/" + item.Title + "\" title=\"" + item.Name + "\">";
                        stRelated += "<img src=\"" + item.Thumbnail + "\" alt=\"" + item.Name + "\">";
                        stRelated += "</a>";
                    }
                    stRelated += "</div>";
                    stRelated += "<div class=\"item-info\"><h3>";
                    stRelated += "<a href=\"/" + item.Title + "\" title=\"" + item.Name + "\">";
                    stRelated += item.Name;
                    stRelated += "</a></h3>";
                    stRelated += "<div class=\"item-price\">";
                    if (item.Price > 0)
                    {
                        if (item.Sale > 0)
                            stRelated += "Giá bán: <span>" + String.Format("{0:#,##0}", (item.Price * item.Sale) / 100) + "</span>";
                        else
                            stRelated += "Giá bán: <span>" + String.Format("{0:#,##0}", item.Price) + "</span>";
                    }
                    else
                    {
                        stRelated += "Giá bán: <span>Liên hệ</span>";
                    }
                    stRelated += "</div>";
                    stRelated += "</div>";
                    stRelated += "</li>";
                }
                stRelated += "</ul>";
            }
            ViewBag.Related = stRelated;
            ViewBag.Guide = Guide;
            ViewBag.Infor = Infor;
            ViewBag.Phone01 = SetDao.GetValue("Phone_Support01");
            ViewBag.Phone02 = SetDao.GetValue("Phone_Support02");
            ViewBag.Phone03 = SetDao.GetValue("Phone_Support03");
            Product.upView(slug);
            return PartialView(detail);
        }

        public ActionResult DetailContent(string slug)
        {
            var detail = Contents.DetailBySlug(slug);
            string output = "";
            output += "<a title=\"Home\" href=\"/\">Trang chủ</a> <i class=\"fa fa-angle-double-right\"></i>";
            output += breadcrumb(detail.CategoryId);
            output += detail.Name;
            ViewBag.breadcrumb = output;
            var Related = Contents.ViewRelated(slug).OrderByDescending(x => x.Modified).ToList();
            var stRelated = "";
            if (Related.Count > 0)
            {
                stRelated += "<ul class=\"content-related\">";
                foreach (var item in Related)
                {
                    stRelated += "<li>";
                    stRelated += "<a href=\"/" + item.Slug + "\" title=\"" + item.Title + "\">";
                    stRelated += item.Title;
                    stRelated += "</a>";
                    stRelated += "</li>";
                }
                stRelated += "</ul>";
            }
            ViewBag.Related = stRelated;
            return PartialView(detail);
        }

        public ActionResult DetailPage(string slug)
        {
            var detail = Contents.DetailBySlug(slug);
            string output = "";
            output += "<a title=\"Home\" href=\"/\">Trang chủ</a> <i class=\"fa fa-angle-double-right\"></i>";
            output += breadcrumb(detail.CategoryId);
            output += detail.Title;
            ViewBag.breadcrumb = output;
            return PartialView(detail);
        }

        public ActionResult MenuProduct()
        {
            var list = Category.ViewAllByTaxonomy("Product");
            string output = "";
            if (list.Count() > 0)
            {
                int level = 1;
                output += "<div class=\"product-menu\">";
                output += "<div class=\"title\"><i class=\"fa fa-fw fa-bars\"></i> DANH MỤC SẢN PHẨM";
                output += "</div>";
                output += "<ul class=\"nav\">";
                foreach (var item in list)
                {
                    if (item.ParentId == null)
                    {
                        output += "<li>";
                        output += "<a title=\"" + item.Title + "\" href=\"/" + item.Slug + "\">";
                        output += item.Title;
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

        public ActionResult MenuService()
        {
            var list = Category.ViewAllByTaxonomy("Service");
            string output = "";
            if (list.Count() > 0)
            {
                int level = 1;
                output += "<div class=\"product-menu\">";
                output += "<div class=\"title\"><i class=\"fa fa-fw fa-bars\"></i> DANH MỤC DỊCH VỤ";
                output += "</div>";
                output += "<ul class=\"nav\">";
                foreach (var item in list)
                {
                    if (item.ParentId == null)
                    {
                        output += "<li>";
                        output += "<a title=\"" + item.Title + "\" href=\"/" + item.Slug + "\">";
                        output += item.Title;
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

        public ActionResult ViewProductFeature()
        {
            var list = Product.ViewTopHot().Take(12).ToList();
            string output = "";
            output += "<div id=\"carousel-example-Human-Resource\" class=\"carousel-example carousel slide hidden-xs\" data-ride=\"carousel\">";
            output += "<div class=\"carousel-inner\">";

            var item = 0;
            if (list.Count % 4 == 0)
            {
                item = list.Count / 4;
            }
            else
            {
                item = (list.Count / 4) + 1;
            }
            for (int j = 0; j < item; j++)
            {
                if (j == 0)
                {
                    output += "<div class=\"item active\">";
                }
                else
                {
                    output += "<div class=\"item\">";
                }
                output += "<div class=\"row\">";
                for (int i = 0; i < 4; i++)
                {
                    if (i + (j * 4) < list.Count)
                    {
                        output += "<div class=\"col-md-3\">";
                        output += "<div class=\"col-item\">";
                        output += "<div class=\"item-event\">";
                        output += "<a href=\"" + list[i + (j * 4)].Title + "\" title=\"" + list[i + (j * 4)].Name + "\">";
                        output += "<img src=\"" + list[i + (j * 4)].Thumbnail + "\" alt=\"" + list[i + (j * 4)].Name + "\" >";
                        output += "</a>";
                        output += "</div>";
                        output += "</div>";
                        output += "</div>";
                    }
                }
                output += "</div></div>";
            }
            output += "</div>";
            output += "</div>";
            output += "";
            ViewBag.Data = output;
            return PartialView();
        }

        public ActionResult MenuContent()
        {
            var list = Category.ViewAllByTaxonomy("Content");
            string output = "";
            if (list.Count() > 0)
            {
                int level = 0;
                foreach (var item in list)
                {
                    if (item.ParentId == null)
                    {
                        output += "<div class=\"content-menu\">";
                        output += "<h2 class=\"title\">";
                        output += "<a title=\"" + item.Title + "\" href=\"/" + item.Slug + "\">";
                        output += item.Title;
                        output += "</a>";
                        output += "</h2>";
                        output += submenu(item.Id, level++);
                        output += "</div>";
                    }
                }
            }
            ViewBag.Data = output;
            return PartialView();
        }

        public ActionResult HeaderCart()
        {
            var cart = SessionHelper.GetSessionCart();
            var list = new List<CartItemModel>();
            if (cart != null)
            {
                list = (List<CartItemModel>)cart;
            }
            return PartialView(list);
        }

        public ActionResult Contactf(string FullName, string Email, string Content)
        {
            var entity = new tb_Contact();
            entity.Date = DateTime.Now;
            entity.Modified = DateTime.Now;
            entity.FullName = FullName;
            entity.Email = Email;
            entity.Content = Content;
            long Id = Contacts.Insert(entity);
            return Redirect("/");
        }

        public ActionResult SearchProduct(string keyword, int? page, int pagesize = 1)
        {
            int totalRecord = 0;
            int pageIndex = page ?? 1;
            var model = Product.ViewSearch(keyword, ref totalRecord, page, pagesize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.Keyword = keyword;
            return View(model.ToPagedList(pageIndex, 12));
        }

        public ActionResult Page404()
        {
            return PartialView();
        }

        public ActionResult Infor_Site()
        {
            ViewBag.Infor_Site = new SettingDao().GetValue("Infor_Site");
            return PartialView();
        }

        public ActionResult Customize_Site()
        {
            ViewBag.Customize_Site = new SettingDao().GetValue("Customize_Site");
            return PartialView();
        }

        public ActionResult Terms_Site()
        {
            ViewBag.Terms_Site = new SettingDao().GetValue("Terms_Site");
            return PartialView();
        }

        private string submenu(long Id, int level)
        {
            var list = Category.ViewAllByParentId(Id);
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
                    output += "<a title=\"" + item.Title + "\" href=\"/" + item.Slug + "\">";
                    output += item.Title;
                    output += "</a>";
                    output += submenu(item.Id, level + 1);
                    output += "</li>";
                }
                output += "</ul>";
            }
            return output;
        }

        private string breadcrumb(long? Id)
        {
            var st = "";
            var detail = Category.Detail(Id);
            if (detail != null)
            {
                st += breadcrumb(detail.ParentId);
                st += "<a href=\"/" + detail.Slug + "\">";
                st += detail.Name;
                st += "</a> <i class=\"fa fa-angle-double-right\"></i> ";
            }
            return st;
        }

        public ActionResult LinkPartner()
        {
            var slider = SDao.ListAll();
            string output = "";
            if (slider.Count() > 0)
            {
                output += "<div class=\"product-menu\">";
                output += "<div class=\"title\"><i class=\"fa fa-fw fa-bars\"></i> ĐỐI TÁC LIÊN KẾT";
                output += "</div>";
                output += "<ul class=\"nav partner\">";
                foreach (var item in slider)
                {
                    if (item.Image != null)
                    {
                        output += "<li>";
                        //output += "<a title=\"" + item.Text + "\" href=\"/" + item.Url + "\">";
                        output += "<img src=\"" + item.Image + "\" alt=\"" + item.Text + "\" /> ";
                        //output += "</a>";
                        output += "</li>";
                    }

                }
                output += "</ul>";
                output += "</div>";
            }
            ViewBag.Data = output;
            return PartialView();
        }

        public ActionResult ViewTopService()
        {
            var list = Contents.ListAllByTaxonomy("Service").OrderByDescending(x => x.Modified).Take(15).ToList();
            string output = "";
            if (list.Count > 0)
            {
                output += "<div id=\"owl-demo\" class=\"owl-carousel owl-theme\">";
                foreach (var item in list)
                {
                    if (item.Thumbnail != null)
                    {
                        output += "<div class=\"item\">";
                        output += "<div class=\"item-img\">";
                        output += "<a href=\"" + item.Slug + "\" title=\"" + item.Title + "\">";
                        output += "<img src=\"" + item.Thumbnail + "\" alt=\"" + item.Title + "\" />";
                        output += "</a>";
                        output += "</div>";
                        output += "<div class=\"item-text\">";
                        output += "<a href=\"" + item.Slug + "\" title=\"" + item.Title + "\">";
                        output += item.Title;
                        output += "</a>";
                        output += "</div>";
                        output += "</div>";
                    }
                }
                output += "</div>";
            }

            ViewBag.Data = output;
            return PartialView();
        }

        public ActionResult ViewTopContent()
        {
            return PartialView();
        }

        public ActionResult ViewSupport()
        {
            var Name01 = SetDao.GetValue("Name_Support01");
            var Phone01 = SetDao.GetValue("Phone_Support01");
            var Yahoo01 = SetDao.GetValue("Yahoo_Support01");
            var Skype01 = SetDao.GetValue("Skype_Support01");
            var Name02 = SetDao.GetValue("Name_Support02");
            var Phone02 = SetDao.GetValue("Phone_Support02");
            var Yahoo02 = SetDao.GetValue("Yahoo_Support02");
            var Skype02 = SetDao.GetValue("Skype_Support02");
            var Name03 = SetDao.GetValue("Name_Support03");
            var Phone03 = SetDao.GetValue("Phone_Support03");
            var Yahoo03 = SetDao.GetValue("Yahoo_Support03");
            var Skype03 = SetDao.GetValue("Skype_Support03");
            string output = "<ul class=\"nav\">";
            output += "<li>";
            output += "<a href=\"ymsgr:sendim?" + Yahoo01 + "\" title=\"" + Name01 + "\"><img src = \"Images/yahoo.png\" alt = \"" + Name01 + "\" /></a>";
            output += "<a href=\"skype:" + Skype01 + "?chat\" title=\"" + Name01 + "\"><img src = \"Images/skypeimg.png\" alt = \"" + Name01 + "\" /></a>";
            output += "<div class=\"text\">";
            output += "<p>" + Name01 + "</p>";
            output += "<b>" + Phone01 + "</b>";
            output += "</li>";
            output += "<li>";
            output += "<a href=\"ymsgr:sendim?" + Yahoo02 + "\" title=\"" + Name02 + "\"><img src = \"Images/yahoo.png\" alt = \"" + Name02 + "\" /></a>";
            output += "<a href=\"skype:" + Skype02 + "?chat\" title=\"" + Name02 + "\"><img src = \"Images/skypeimg.png\" alt = \"" + Name02 + "\" /></a>";
            output += "<div class=\"text\">";
            output += "<p>" + Name02 + "</p>";
            output += "<b>" + Phone02 + "</b>";
            output += "</li>";
            output += "<li>";
            output += "<a href=\"ymsgr:sendim?" + Yahoo03 + "\" title=\"" + Name03 + "\"><img src = \"Images/yahoo.png\" alt = \"" + Name03 + "\" /></a>";
            output += "<a href=\"skype:" + Skype03 + "?chat\" title=\"" + Name03 + "\"><img src = \"Images/skypeimg.png\" alt = \"" + Name03 + "\" /></a>";
            output += "<div class=\"text\">";
            output += "<p>" + Name03 + "</p>";
            output += "<b>" + Phone03 + "</b>";
            output += "</li>";
            output += "</ul>";
            ViewBag.Data = output;
            return PartialView();
        }
    }
}