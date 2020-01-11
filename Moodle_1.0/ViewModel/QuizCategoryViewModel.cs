using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.ViewModel
{
    public class QuizCategoryViewModel
    {
        [Display(Name = "Candidate")]
        [Required(ErrorMessage = "Candidate is requird..")]
        public string CandidateName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "category is requird..")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> ListofCategory { get; set; }
    }
}