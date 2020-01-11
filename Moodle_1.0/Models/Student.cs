using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}