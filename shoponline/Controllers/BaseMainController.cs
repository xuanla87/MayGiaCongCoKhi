using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShopOnlineModel.Dao;
using ShopOnlineModel.EF;

namespace shoponline.Controllers
{
    public class BaseMainController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var db = new ShopModelDbContext();
            var user = db.tb_Users.ToList();
            if (user.Count < 1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Settings", action = "Index" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}