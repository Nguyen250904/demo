using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Controllers
{
    public class ListingLargeController : Controller
    {
        // GET: ListingLarge
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        public ActionResult AllListingLarge(int Id)
        {
            var lstListingLarge = objbhASPEntities1.Products.Where(n => n.CategoryId == Id).ToList();
            return View(lstListingLarge);
        }
    }
}