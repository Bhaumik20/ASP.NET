using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle_1._0.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Answertext { get; set; }
    }
}