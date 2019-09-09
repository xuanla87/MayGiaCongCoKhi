using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnlineModel.Dao;
using shoponline.Models;
using PagedList;
using System.Net;

namespace shoponline.Areas.Administrators.Controllers
{
    public class CartsController : BaseController
    {
        private CartDao Dao = new CartDao();
        public ActionResult Index(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.listNew();
            var output = new List<CartModel>();
            foreach (var item in entity)
            {
                var ldetail = Dao.ListDetailByCartId(item.Id);
                output.Add(new CartModel
                {
                    Address = item.Address,
                    Approval = item.Approval,
                    Date = item.Date,
                    Description = item.Description,
                    Email = item.Email,
                    FullName = item.FullName,
                    Id = item.Id,
                    Modified = item.Modified,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status,
                    Total = item.Total,
                    UserId = item.UserId,
                    ListDetail = ldetail
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(output.Where(x => x.Address.Contains(SearchString) || x.FullName.Contains(SearchString) || x.Email.Contains(SearchString) || x.PhoneNumber.Contains(SearchString)).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(output.ToPagedList(pageNum, 20));
            }
        }

        public ActionResult OrderSuccess(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.ListSuccess();
            var output = new List<CartModel>();
            foreach (var item in entity)
            {
                var ldetail = Dao.ListDetailByCartId(item.Id);
                output.Add(new CartModel
                {
                    Address = item.Address,
                    Approval = item.Approval,
                    Date = item.Date,
                    Description = item.Description,
                    Email = item.Email,
                    FullName = item.FullName,
                    Id = item.Id,
                    Modified = item.Modified,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status,
                    Total = item.Total,
                    UserId = item.UserId,
                    ListDetail = ldetail
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(output.Where(x => x.Address.Contains(SearchString) || x.FullName.Contains(SearchString) || x.Email.Contains(SearchString) || x.PhoneNumber.Contains(SearchString)).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(output.ToPagedList(pageNum, 20));
            }
        }

        public ActionResult ListAll(int? page, string SearchString)
        {
            int pageNum = page ?? 1;
            var entity = Dao.listAll();
            var output = new List<CartModel>();
            foreach (var item in entity)
            {
                var ldetail = Dao.ListDetailByCartId(item.Id);
                output.Add(new CartModel
                {
                    Address = item.Address,
                    Approval = item.Approval,
                    Date = item.Date,
                    Description = item.Description,
                    Email = item.Email,
                    FullName = item.FullName,
                    Id = item.Id,
                    Modified = item.Modified,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status,
                    Total = item.Total,
                    UserId = item.UserId,
                    ListDetail = ldetail
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(output.Where(x => x.Address.Contains(SearchString) || x.FullName.Contains(SearchString) || x.Email.Contains(SearchString) || x.PhoneNumber.Contains(SearchString)).ToPagedList(pageNum, 20));
            }
            else
            {
                return View(output.ToPagedList(pageNum, 20));
            }
        }

        public ActionResult Remove(long? Id, string name)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.Delete(Id);
            return RedirectToAction(name);
        }

        public ActionResult OrderApproval(long? Id, string name)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dao.Approval(Id);
            return RedirectToAction(name);
        }
    }
}