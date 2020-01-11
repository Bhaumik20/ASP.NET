using Microsoft.AspNet.Identity;
using Moodle_1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    [Authorize(Roles ="Teacher,Admin")]
    public class MailController : Controller
    {
        MoodleDb db = new MoodleDb();
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string id = User.Identity.GetUserName();
            IEnumerable<Course> list = db.Courses.Where(x => x.TeacherId.Equals(id)).ToList();
            var sender = new MailAddress("studygurucommunity@gmail.com");
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender.Address, "atproj@1")
            };
            using (var mess= new MailMessage())
            {
                mess.From = sender;
                foreach(var i in list)
                {
                    foreach(var j in i.Students)
                    {
                        mess.To.Add(j.UserName);
                    }
                }
                mess.Subject = fc["sub"];
                mess.Body = fc["mess"];
                smtp.Send(mess);
            }
            return View();
        }
    }
}