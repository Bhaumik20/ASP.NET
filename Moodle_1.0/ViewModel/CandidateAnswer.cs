using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle_1._0.ViewModel
{
    public class CandidateAnswer
    {
        public string UserId { get; set; }

        public int QuestionId { get; set; }

        public string AnswerText { get; set; }
    }
    public class useranswer
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
    }
}