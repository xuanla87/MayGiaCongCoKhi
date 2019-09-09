using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnlineModel.EF;
namespace shoponline.Models
{
    public class CartItemModel
    {
        public tb_Products Product { get; set; }
        public int Quantity { get; set; }
    }
}