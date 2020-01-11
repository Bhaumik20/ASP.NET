using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string QuestionName { get; set; }
        public bool IsActive { get; set; }
        public bool IsMultiple { get; set; }
    }
}