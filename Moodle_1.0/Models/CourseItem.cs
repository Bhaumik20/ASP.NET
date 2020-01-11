using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class CourseItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}