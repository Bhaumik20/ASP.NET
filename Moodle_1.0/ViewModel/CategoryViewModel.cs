using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.ViewModel
{
    public class CategoryViewModel
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "category is required..")]
        public int CategoryId { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question is required..")]
        public string QuestionName { get; set; }

        public string OptionName { get; set; }
        // public string CandidateName { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> ListofCategory { get; set; }
    }
}