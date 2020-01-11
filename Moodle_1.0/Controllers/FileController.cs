using Moodle_1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        MoodleDb db = new MoodleDb();
        Student s = null;
        public ActionResult Index(string id)
        {
            s = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();

            return File(s.Content,s.ContentType);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            string id = User.Identity.GetUserId();
            s = db.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();

            s.ImageName = System.IO.Path.GetFileName(upload.FileName);

            s.ContentType = upload.ContentType;
            
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                s.Content = reader.ReadBytes(upload.ContentLength);
            }
            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Manage");
        }
    }
}