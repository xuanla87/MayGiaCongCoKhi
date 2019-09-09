using shoponline.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoponline.Models;
using ShopOnlineModel.Dao;
using System.Web.Script.Serialization;
using ShopOnlineModel.EF;

namespace shoponline.Controllers
{
    public class CartController : Controller
    {
        private CartDao Dao = new CartDao();
        public ActionResult Index()
        {
            var cart = SessionHelper.GetSessionCart();
            var list = new List<CartItemModel>();
            if (cart != null)
            {
                list = (List<CartItemModel>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(long ProductId, int Quantity)
        {
            var product = new ProductDao().Detail(ProductId);
            var cart = SessionHelper.GetSessionCart();
            if (cart != null)
            {
                var list = (List<CartItemModel>)cart;
                if (list.Exists(x => x.Product.Id == ProductId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == ProductId)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItemModel();
                    item.Product = product;
                    item.Quantity = Quantity;
                    list.Add(item);
                }
                SessionHelper.SetSessionCart(list);
            }
            else
            {
                var item = new CartItemModel();
                item.Product = product;
                item.Quantity = Quantity;
                var list = new List<CartItemModel>();
                list.Add(item);
                SessionHelper.SetSessionCart(list);
            }
            return Redirect("/gio-hang");
        }

        public JsonResult Update(string CartItemModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItemModel>>(CartItemModel);
            var sessioncart = (List<CartItemModel>)SessionHelper.GetSessionCart();
            foreach (var item in sessioncart)
            {
                var jsonItem = cart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            SessionHelper.SetSessionCart(sessioncart);
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            SessionHelper.RemoveSessionCart();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long Id)
        {
            var sessioncart = (List<CartItemModel>)SessionHelper.GetSessionCart();
            sessioncart.RemoveAll(x => x.Product.Id == Id);
            SessionHelper.SetSessionCart(sessioncart);
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(CartModel entity)
        {
            if (ModelState.IsValid)
            {
                var cart = SessionHelper.GetSessionCart();
                var list = new List<CartItemModel>();
                if (cart != null)
                {
                    list = (List<CartItemModel>)cart;
                    var dCart = new tb_Carts();
                    dCart.Address = entity.Address;
                    dCart.Date = DateTime.Now;
                    dCart.Description = entity.Description;
                    dCart.Email = entity.Email;
                    dCart.FullName = entity.FullName;
                    dCart.Id = entity.Id;
                    dCart.Modified = DateTime.Now;
                    dCart.PhoneNumber = entity.PhoneNumber;
                    dCart.Status = true;
                    dCart.Total = list.Sum(x => x.Product.Price - ((x.Product.Price * x.Product.Sale) / 100));
                    dCart.UserId = entity.UserId;
                    dCart.Approval = false;
                    long Id = Dao.Insert(dCart);
                    foreach (var item in list)
                    {
                        var detail = new tb_DetailCart();
                        detail.CartId = Id;
                        detail.ProductId = item.Product.Id;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Product.Price - ((item.Product.Price * item.Product.Sale) / 100);
                        Dao.InsertCDetail(detail);
                    }
                    return Redirect("/hoan-thanh");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không có sản phẩm nào trong đơn hàng!");
                }
            }
            return View(entity);
        }

        public ActionResult Success()
        {
            SessionHelper.RemoveSessionCart();
            return View();
        }
    }
}