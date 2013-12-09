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
        //northwndEntities cd = new northwndEntities();
        nwEntities cd = new nwEntities();
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
            return View("index",orders);
        }

        public string GetAddress(int id)
        {
            var ad = cd.Orders.Find(id);
            string address = string.Format("{0}, {1}, {2}, {3}, {4}", ad.ShipAddress, ad.ShipCity, ad.ShipRegion, ad.ShipPostalCode, ad.ShipCountry);
            return address;
        }

        //[HttpGet]
        public PartialViewResult getEmployee(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var emp = cd.Orders.Where(a => a.OrderID == id);
                //var emp = cd.Orders.Find(id);
                return PartialView("_EmployeeDetails", emp);
            }
            else
            {
                var orders = cd.Orders.Where(a => a.EmployeeID == id);
                return PartialView("_EmployeeDetails", orders);
            }
        }

        public ActionResult Delete(int id )
        {
            var t = cd.Orders.Find(id);

            if (t == null)
            {
                return HttpNotFound();
            }
            return View("Delete", t);
        }

        public ActionResult Edit(int id = 0)
        {
            var order = cd.Orders.Where(a => a.OrderID == id).FirstOrDefault();
            //var t = cd.Employees.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = new SelectList(cd.Orders.OrderBy(g => g.Employee.LastName), "OrderID", "ShipName");
            //ViewBag.Employee = new SelectList(cd.Orders, "OrderID","EmployeeID",order.EmployeeID);
            //ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", a.ArtistId);

            //return (a == null) ? View() : View(a);

            return View("Edit", order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = cd.Orders.Find(id);
            cd.Orders.Remove(order);
            cd.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
