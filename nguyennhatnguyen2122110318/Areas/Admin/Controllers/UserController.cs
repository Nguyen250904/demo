using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        bhASPEntities1 objbhASPEntities1 = new bhASPEntities1();
        // GET: Admin/User
        public ActionResult UserList()
        {
            var objUser = objbhASPEntities1.Users.ToList();
            return View(objUser);
        }
        public ActionResult Details(int Id)
        {
            var objUser = objbhASPEntities1.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUserdelete = objbhASPEntities1.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUserdelete);
        }
        [HttpPost]
        public ActionResult Delete(User obj)
        {
            var objUserdelete = objbhASPEntities1.Users.Where(n => n.Id == obj.Id).FirstOrDefault();
            objbhASPEntities1.Users.Remove(objUserdelete);
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("UserList", "User");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            try
            {
               
                objbhASPEntities1.Users.Add(objUser);
                objbhASPEntities1.SaveChanges();
                return RedirectToAction("UserList", "User");
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUserEdit = objbhASPEntities1.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUserEdit);
        }
        [HttpPost]
        public ActionResult Edit(int id, User objEdit)
        {
           
            objbhASPEntities1.Entry(objEdit).State = System.Data.Entity.EntityState.Modified;
            objbhASPEntities1.SaveChanges();
            return RedirectToAction("UserList", "User");
        }

    }
}