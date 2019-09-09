using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shoponline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                       name: "Add cart",
                       url: "them-gio-hang",
                       defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
                   );
            routes.MapRoute(
                      name: "view cart",
                      url: "gio-hang",
                      defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
                  );
            routes.MapRoute(
                       name: "update cart",
                       url: "Cart/Update",
                       defaults: new { controller = "Cart", action = "Update", id = UrlParameter.Optional }
                    );
            routes.MapRoute(
                     name: "delete all cart",
                     url: "Cart/DeleteAll",
                     defaults: new { controller = "Cart", action = "DeleteAll", id = UrlParameter.Optional }
                 );
            routes.MapRoute(
                    name: "tim kiem",
                    url: "tim-kiem",
                    defaults: new { controller = "Home", action = "SearchProduct", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                  name: "delete cart",
                  url: "Cart/Delete",
                  defaults: new { controller = "Cart", action = "Delete", id = UrlParameter.Optional }
                 );
            routes.MapRoute(
                 name: "payment success",
                 url: "hoan-thanh",
                 defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
                 );
            routes.MapRoute(
                   name: "payment cart",
                   url: "thanh-toan",
                   defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
                   );
            routes.MapRoute(
                         name: "Slug",
                         url: "{slug}",
                         defaults: new { controller = "Home", action = "Default", Slug = UrlParameter.Optional }
                     );
            routes.MapRoute(
                          name: "Slug/page",
                          url: "{slug}/{page}",
                          defaults: new { controller = "Home", action = "Default", Slug = UrlParameter.Optional, Page = UrlParameter.Optional }
                      );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
