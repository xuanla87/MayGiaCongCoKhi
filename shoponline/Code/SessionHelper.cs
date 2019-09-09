using shoponline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoponline.Code
{
    [Serializable]
    public class SessionHelper
    {
        public static void SetSessionUser(LoginModel session)
        {
            HttpContext.Current.Session["UserCurent"] = session;
        }
        public static LoginModel GetSessionUser()
        {
            var session = HttpContext.Current.Session["UserCurent"];
            if (session != null)
            {
                return session as LoginModel;
            }
            else
                return null;
        }
        public static void RemoveSessionUser()
        {
            HttpContext.Current.Session["UserCurent"] = null;
        }

        public static void SetSessionCart(List<CartItemModel> session)
        {
            HttpContext.Current.Session["CartCurent"] = session;
        }

        public static List<CartItemModel> GetSessionCart()
        {
            var session = HttpContext.Current.Session["CartCurent"];
            if (session != null)
            {
                return session as List<CartItemModel>;
            }
            else
                return null;
        }

        public static void RemoveSessionCart()
        {
            HttpContext.Current.Session["CartCurent"] = null;
        }
    }
}