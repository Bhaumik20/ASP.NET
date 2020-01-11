using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle_1._0.ViewModel
{
    public class QuestionAnswerViewModel
    {
        public bool isLast { get; set; }
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public List<QuizOption> ListOfQuizOptions { get; set; }
    }

    public class QuizOption
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
    }
}