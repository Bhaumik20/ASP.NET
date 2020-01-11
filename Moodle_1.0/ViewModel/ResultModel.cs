using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.ViewModel
{
    public class ResultModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerByUser { get; set; }
        public string Status { get; set; }
        public int total { get; set; }
        public List<CanAnswer> listOfcandidateAnswers { get; set; }
    }

    public class CanAnswer
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
    }
}