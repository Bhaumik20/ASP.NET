using Moodle_1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;

namespace Moodle_1._0.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        MoodleDb db = new MoodleDb();
        // GET: Student
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            Student user = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return View(user.Courses);
        }

        public ActionResult Teachers()
        {
            string id = User.Identity.GetUserId();
            Student user = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return View(user.Courses);
        }

        public ActionResult Enroll()
        {
            string id = User.Identity.GetUserId();
            Student user = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
            ViewBag.count = user.Courses.Count;
            return View(db.Courses.ToList());
        }

        [HttpPost]
        public ActionResult Enroll(FormCollection fc)
        {
            string id = User.Identity.GetUserId();
            Student user = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
            IEnumerable<Course> list = db.Courses.ToList();
            List<Course> selected = new List<Course>();
           
            foreach(var i in list)
            {
                
                if (fc[i.CourseName].First().Equals('t'))
                {

                    //r += fc[i.CourseName].First() + " ";
                    selected.Add(i);
                    i.Students.Add(user);
                }
            }
            user.Courses = selected;
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        public ActionResult Result()
        {
            string id = User.Identity.GetUserId();
            var list = db.Marks.Where(x => x.StudentId.Equals(id)).ToList();
            return View(list);
        }
    }
}