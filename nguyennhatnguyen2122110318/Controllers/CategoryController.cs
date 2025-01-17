using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();

        public ActionResult AllCategory()
        {
            var lstCategory = objbhASPEntities1.Categories.ToList();
            return View(lstCategory);
        }
    }
}