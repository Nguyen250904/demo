using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        public ActionResult ProductList(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                lstProduct = objbhASPEntities1.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstProduct = objbhASPEntities1.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct);
        }
        public ActionResult Details(int Id)
        {
            var objProduct = objbhASPEntities1.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProductdelete = objbhASPEntities1.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProductdelete);
        }
        [HttpPost]
        public ActionResult Delete(Product obj)
        {
            var objProductdelete = objbhASPEntities1.Products.Where(n => n.Id == obj.Id).FirstOrDefault();
            objbhASPEntities1.Products.Remove(objProductdelete);
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objbhASPEntities1.Products.Add(objProduct);
                objbhASPEntities1.SaveChanges();
                return RedirectToAction("ProductList", "Product");
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProductEdit = objbhASPEntities1.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProductEdit);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product objEdit)
        {
            if (objEdit.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objEdit.ImageUpload.FileName);
                string extension = Path.GetExtension(objEdit.ImageUpload.FileName);
                fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                objEdit.Avatar = fileName;
                objEdit.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }

            objbhASPEntities1.Entry(objEdit).State = System.Data.Entity.EntityState.Modified;
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }

    }
}