using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public string UserId { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
    }
}