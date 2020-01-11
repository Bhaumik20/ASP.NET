using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.ViewModel
{
    public class QuestionOptionViewModel
    {
        public int CategoryId { get; set; }

        public string QuestionName { get; set; }

        public List<string> ListOfOptions { get; set; }

        public string AnswerText { get; set; }
    }
}