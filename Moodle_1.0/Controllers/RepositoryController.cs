using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Moodle_1._0.Controllers
{
    [Authorize]
    public class RepositoryController : Controller
    {
        // GET: Repository
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            string[] files = Directory.GetFiles(Path.Combine(Server.MapPath("~/UploadedFiles"),id));
            List<string> fileList = new List<string>();
            foreach (var i in files)
            {
                fileList.Add(Path.GetFileName(i));
            }
            return View(fileList);
            
        }

        public ActionResult Delete(string name)
        {
            string id = User.Identity.GetUserId();
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedFiles"), id, name));
            return RedirectToAction("Index");
        }

        public FileResult Download(string name)
        {
            string id = User.Identity.GetUserId();
            string type = MimeMapping.GetMimeMapping(name);
            var s = System.IO.File.Open(Path.Combine(Server.MapPath("~/UploadedFiles"),id, name),FileMode.Open);

            return File(s, type, name);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            string id = User.Identity.GetUserId();
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"),id, fileName));
                //Here you can write code for save this information in your database if you want
            }

            return Json("file uploaded successfully");
        }
    }
}