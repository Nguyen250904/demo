using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;
using nguyennhatnguyen2122110318.Models;

namespace nguyennhatnguyen2122110318.Controllers
{
    public class ListingGirdController : Controller
    {
        // GET: ListingGird
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        // GET: ListingGrid
        public ActionResult All()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objbhASPEntities1.Categories.ToList();
            objHomeModel.ListProduct = objbhASPEntities1.Products.ToList();
            return View(objHomeModel);
        }
    }
}
