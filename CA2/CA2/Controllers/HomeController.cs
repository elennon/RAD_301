using System;
using System.Collections.Generic;
using System.Data;
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
      
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            IEnumerable<Order> orders;
            //if (Request.IsAjaxRequest())
            if(id != 0)
            {
                orders = cd.Orders.Where(a => a.EmployeeID == id);
            }
            else
            {
                orders = cd.Orders;
            }
                foreach (var item in orders)
                {
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
                return View("index", orders);
        }
        

        public string GetAddress(int id)
        {
            var ad = cd.Orders.Find(id);
            string address = string.Format("{0}, {1}, {2}, {3}, {4}", ad.ShipAddress, ad.ShipCity, ad.ShipRegion, ad.ShipPostalCode, ad.ShipCountry);
            return address;
        }

        [HttpGet]
        public PartialViewResult getEmployee(int id)
        {
            
                //var emp = cd.Orders.Where(a => a.OrderID == id);
                //var emp = cd.Orders.Find(id);
                var emp = cd.Employees.Find(id);
                //return PartialView("vv", emp);
                return PartialView("_getEmployee", emp);
            
        }

        
        public ActionResult Edit(int id = 0)
        {
            //IEnumerable<Order> order;
            
            if (!Request.IsAjaxRequest())
            {

                Order ord = cd.Orders.Find(id);
                var t = cd.Employees.Find(ord.EmployeeID);
                if (ord == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Shipper = new SelectList(cd.Shippers, "ShipperID", "CompanyName", ord.Shipper);
                ViewBag.Employee = new SelectList(cd.Employees, "EmployeeID", "LastName", ord.EmployeeID);
                return View("Edit", ord);
            }
            else
            {
                var em = cd.Orders.Find(id);
               
                return View("_EditEmployee", em.Employee);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                cd.Entry(order).State = EntityState.Modified;
                cd.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Employee employ)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        cd.Entry(employ).State = EntityState.Modified;
        //        cd.SaveChanges();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(employ);
        //}


        public ActionResult Delete(int id)
        {
            var t = cd.Orders.Find(id);

            if (t == null)
            {
                return HttpNotFound();
            }
            return View("Delete", t);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = cd.Orders.Find(id);
            var dOrder = cd.Order_Details.Where(a => a.OrderID == order.OrderID);
            foreach (var item in dOrder)
            {
                cd.Order_Details.Remove(item);
            }
            //cd.SaveChanges();
            cd.Orders.Remove(order);
            cd.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
