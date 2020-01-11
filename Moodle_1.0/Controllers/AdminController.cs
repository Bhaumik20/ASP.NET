using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moodle_1._0.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        public ActionResult Index()
        {
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"];
            }
            ViewBag.users = userManager.Users.ToList();
            ViewBag.userManager = userManager;
            ViewBag.roles = new SelectList(roleManager.Roles, "Id","Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string id = fc["Filter"];
            string uname = fc["search"];
            ViewBag.temp = uname;
            var list = userManager.Users.Select(x => x).ToList();
            ViewBag.userManager = userManager;
            ViewBag.roles = new SelectList(roleManager.Roles, "Id", "Name", id);
            
            List<ApplicationUser> filterList = new List<ApplicationUser>();
            if (id != "")
            {
                foreach (var i in list)
                {
                    if (i.Roles.FirstOrDefault().RoleId == id)
                    {
                        filterList.Add(i);
                    }
                }
            }
            else
            {
                filterList = list;
            }
            if (uname != "")
            {
                ViewBag.users = filterList.Where(x => x.UserName.Contains(uname)).ToList();
                return View();
            }
            ViewBag.users = filterList;
           
            return View();
        }

        public string DeleteUser(string id)
        {
            
            userManager.Delete(userManager.FindById(id));
            MoodleDb db = new MoodleDb();
            db.Students.Remove(db.Students.Find(id));
            
            db.SaveChanges();
            return "User deleted";
            
        }

        public ActionResult AddUser()
        {
            ViewBag.ddl = new SelectList(roleManager.Roles, "Name", "Name", "Select Role");
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(FormCollection fc)
        {
            var newuser = new ApplicationUser { UserName=fc["email"],Email=fc["email"] };
            var result=userManager.Create(newuser, fc["password"]);
            if (result.Succeeded)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                userManager.AddToRole(newuser.Id, fc["ddl"]);
                if (fc["ddl"] == "Student")
                {
                    using (MoodleDb db = new MoodleDb())
                    {
                        Student s = new Student
                        {
                            Id = newuser.Id,
                            UserName = newuser.UserName
                        };
                        db.Students.Add(s);
                        Directory.CreateDirectory(Path.Combine(Server.MapPath("~/UploadedFiles"), newuser.Id));
                        db.SaveChanges();
                    }
                }
                
            }
            else
            {
                TempData["msg"] = "An error occured while adding user";
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}