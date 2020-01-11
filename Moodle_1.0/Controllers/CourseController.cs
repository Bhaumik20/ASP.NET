using Moodle_1._0.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Moodle_1._0.Controllers
{
    
    public class CourseController : Controller
    {
        // GET: Course
        MoodleDb db = new MoodleDb();

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Index()
        {
            string id = User.Identity.GetUserName();
            return View(db.Courses.Where(x=>x.TeacherId.Equals(id)).ToList());
        }

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Students()
        {
            string id = User.Identity.GetUserName();
            return View(db.Courses.Where(x => x.TeacherId.Equals(id)).ToList());
        }

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Result()
        {
            string id = User.Identity.GetUserName();
            var course = db.Courses.Where(c => c.TeacherId.Equals(id)).ToList();
            ViewBag.course = course;
            var list = db.Marks.ToList();
            return View(list);
        }

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult AddMark()
        {
            string id = User.Identity.GetUserName();
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.TeacherId.Equals(id)).ToList(), "CourseId", "CourseName");
           
            return View();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        public ActionResult AddMark([Bind(Include ="Total,Obtain,CourseId,StudentId")]Mark mark)
        {
            
            mark.Course = db.Courses.Find(mark.CourseId);
            mark.Student = db.Students.Where(x=>x.UserName.Equals(mark.StudentId)).FirstOrDefault();
            db.Marks.Add(mark);
            db.SaveChanges();
            return RedirectToAction("Result");
        }

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult EditMark(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mark mark = db.Marks.Find(id);
            if (mark == null)
            {
                return HttpNotFound();
            }
            return View(mark);
        }

        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult DeleteMark(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mark mark = db.Marks.Find(id);
            db.Marks.Remove(mark);
            db.SaveChanges();
            return RedirectToAction("Result");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult EditMark([Bind(Include = "MarkId,Total,Obtain,CourseId,StudentId")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mark).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Result");
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            TempData["id"] = id;
            return View(db.CourseItems.Where(x=>x.CourseId==id).ToList());
        }
        [Authorize(Roles ="Teacher,Admin")]
        public ActionResult Delete(int? id)
        {
            int CourseId = (int)TempData.Peek("id");
            db.CourseItems.Remove(db.CourseItems.Find(id));
            db.SaveChanges();
            return RedirectToAction("Details", new { id = CourseId });
        }
        [Authorize]
        public FileResult Download(int? id)
        {
            CourseItem file = db.CourseItems.Where(x => x.Id == id).FirstOrDefault();
            return File(file.Content,file.ContentType,file.FileName);
        }

        [Authorize(Roles ="Teacher,Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase upload,FormCollection fc)
        {
            int CourseId = (int)TempData["id"];
            CourseItem File = new CourseItem();
            File.CourseId = CourseId;
            File.Description = fc["desc"];
            File.ContentType = upload.ContentType;
            File.FileName = System.IO.Path.GetFileName(upload.FileName);
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                File.Content = reader.ReadBytes(upload.ContentLength);
            }
            File.Course=db.Courses.Where(x => x.CourseId == CourseId).FirstOrDefault();
            db.CourseItems.Add(File);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = CourseId });
        }

    }
}