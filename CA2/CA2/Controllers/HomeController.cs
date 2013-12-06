using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CA2.Controllers
{
    public class HomeController : Controller
    {
        northwndEntities cd = new northwndEntities();
        public ActionResult Index()
        {
            var orders = cd.Orders;
            foreach (var item in orders)
            {
                int id = item.OrderID;
                string address = string.Format("{0}, {1}, {2}, {3}, {4}", item.ShipAddress, item.ShipCity, item.ShipRegion, item.ShipPostalCode, item.ShipCountry);
                if (address.Length < 50)
                {
                    item.ShipAddress = address;                   
                }
                else
                {
                    var builder = new TagBuilder("input");                   
                    builder.Attributes.Add("value", "Address is too long(click me!)");
                    string value = "getLongAddress('" + address + "')";
                    builder.Attributes.Add("onclick", value);
                    builder.Attributes.Add("id", "aBtn");
                    builder.Attributes.Add("data-toggle", "modal");
                    builder.Attributes.Add("class", "btn btn-block btn-primary");
                    builder.MergeAttributes(new RouteValueDictionary());
                    item.ShipAddress = builder.ToString(TagRenderMode.Normal);   
                }
            }
            return View(orders);
        }

        public string GetAddress(int id)
        {
            var ad = cd.Orders.Find(id);
            string address = string.Format("{0}, {1}, {2}, {3}, {4}", ad.ShipAddress, ad.ShipCity, ad.ShipRegion, ad.ShipPostalCode, ad.ShipCountry);
            return address;
        }

        public PartialViewResult partial(int id)
        {
            var ord = cd.Orders;
            return PartialView("_employee");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
