using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        // GET: Admin/Brand
        public ActionResult BrandList()
        {
            var objBrand = objbhASPEntities1.Brands.ToList();
            return View(objBrand);
        }
        public ActionResult Details(int Id)
        {
            var objBrand = objbhASPEntities1.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBranddelete = objbhASPEntities1.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBranddelete);
        }
        [HttpPost]
        public ActionResult Delete(Brand obj)
        {
            var objBranddelete = objbhASPEntities1.Brands.Where(n => n.Id == obj.Id).FirstOrDefault();
            objbhASPEntities1.Brands.Remove(objBranddelete);
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("BrandList", "Brand");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

                }
                objBrand.CreatedOnUtc = DateTime.UtcNow;
                objBrand.UpdatedOnUtc = DateTime.UtcNow;
                objbhASPEntities1.Brands.Add(objBrand);
                objbhASPEntities1.SaveChanges();

                return RedirectToAction("BrandList", "Brand");

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrandEdit = objbhASPEntities1.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrandEdit);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand objEdit)
        {
            if (objEdit.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objEdit.ImageUpload.FileName);
                string extension = Path.GetExtension(objEdit.ImageUpload.FileName);
                fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                objEdit.Avatar = fileName;
                objEdit.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objEdit.UpdatedOnUtc = DateTime.UtcNow;
            objbhASPEntities1.Entry(objEdit).State = System.Data.Entity.EntityState.Modified;
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("BrandList", "Brand");
        }

    }
}