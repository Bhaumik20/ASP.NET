using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public virtual ICollection<CourseItem> courseItems { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}