using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;
using nguyennhatnguyen2122110318.Models;

namespace nguyennhatnguyen2122110318.Controllers
{
    public class HomeController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objbhASPEntities1.Categories.ToList();

            objHomeModel.ListProduct = objbhASPEntities1.Products.ToList();
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objbhASPEntities1.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objbhASPEntities1.Configuration.ValidateOnSaveEnabled = false;
                    objbhASPEntities1.Users.Add(_user);
                    objbhASPEntities1.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }
        //dang nhap
        [HttpGet]
public ActionResult Login()
        {
            return View();
        }
        // POST: UserLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objbhASPEntities1.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    // Add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;

                    // Redirect to Home/Index
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }


        // Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Remove session
            return RedirectToAction("Login");
        }

        // Create a string MD5
        public static string GetMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] fromData = Encoding.UTF8.GetBytes(str);
                byte[] targetData = md5.ComputeHash(fromData);
                StringBuilder byte2String = new StringBuilder();

                for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String.Append(targetData[i].ToString("x2"));
                }
                return byte2String.ToString();
            }
        }
        public JsonResult GetProducts(int page = 1, int pageSize = 10)
        {
            var items = objbhASPEntities1.Products.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalItems = objbhASPEntities1.Products.Count();

            var result = new
            {
                Products = items,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                CurrentPage = page
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string query)
        {
            var products = objbhASPEntities1.Products.Where(p => p.Name.Contains(query) || p.FullDescription.Contains(query)).ToList();
            return View(products);
        }
    }
}