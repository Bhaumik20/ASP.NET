using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public int Total { get; set; }
        public int Obtain { get; set; }
        public int  CourseId { get; set; }
        public string StudentId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }
}