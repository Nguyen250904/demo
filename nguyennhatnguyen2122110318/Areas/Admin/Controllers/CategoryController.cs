using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        // GET: Admin/Category
        public ActionResult CategoryList()
        {
            var objCategory = objbhASPEntities1.Categories.ToList();
            return View(objCategory);
        }
        public ActionResult Details(int Id)
        {
            var objCategory = objbhASPEntities1.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategorydelete = objbhASPEntities1.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategorydelete);
        }
        [HttpPost]
        public ActionResult Delete(Category obj)
        {
            var objCategorydelete = objbhASPEntities1.Categories.Where(n => n.Id == obj.Id).FirstOrDefault();
            objbhASPEntities1.Categories.Remove(objCategorydelete);
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    objCategory.Avatar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
             
                }
                objCategory.CreatedOnUtc = DateTime.UtcNow;
                objCategory.UpdatedOnUtc = DateTime.UtcNow;
                objbhASPEntities1.Categories.Add(objCategory);
                objbhASPEntities1.SaveChanges();
           
                return RedirectToAction("CategoryList", "Category");

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategoryEdit = objbhASPEntities1.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategoryEdit);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category objEdit)
        {
            if (objEdit.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objEdit.ImageUpload.FileName);
                string extension = Path.GetExtension(objEdit.ImageUpload.FileName);
                fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                objEdit.Avatar = fileName;
                objEdit.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objEdit.UpdatedOnUtc = DateTime.UtcNow;
            objbhASPEntities1.Entry(objEdit).State = System.Data.Entity.EntityState.Modified;
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }

    }
}