using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Controllers
{
    public class ProductController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();

        // GET: Product
        public ActionResult Detail(int Id)
        {
            var ojbProduct = objbhASPEntities1.Products.Where(n=>n.Id == Id).FirstOrDefault();
            return View(ojbProduct);
        }
    }
}