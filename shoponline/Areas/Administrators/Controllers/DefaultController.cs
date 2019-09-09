using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Areas.Administrators.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: Administrators/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}