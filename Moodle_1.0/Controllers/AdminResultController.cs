using Moodle_1._0.Models;
using Moodle_1._0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminResultController : Controller
    {
        private MoodleDb obQuizDbEntities;
        public AdminResultController()
        {
            obQuizDbEntities = new MoodleDb();
        }
        // GET: AdminResult
        public ActionResult Index()
        {
            QuizCategoryViewModel objcategoryViewModel = new QuizCategoryViewModel();
            objcategoryViewModel.ListofCategory = (from objCat in obQuizDbEntities.Catagories
                                                   select new SelectListItem()
                                                   {
                                                       Value = objCat.CategoryId.ToString(),
                                                       Text = objCat.CategoryName.ToString()
                                                   }).ToList();

            return View(objcategoryViewModel);

        }
        [HttpPost]
        public ActionResult Index(string CandidateName, int CategoryId)
        {
            int total = Convert.ToInt32(Session["total"]);
            string candidateName = CandidateName;
            int categotyId = CategoryId;


            ResultModel objresultModel = new ResultModel();

            objresultModel.listOfcandidateAnswers = (from obj in obQuizDbEntities.Results
                                                     where obj.UserId == candidateName
                                                     select new CanAnswer()
                                                     {
                                                         AnswerText = obj.AnswerText,
                                                         QuestionId = obj.QuestionId,

                                                     }).ToList();



            var UserResult = (from objResult in objresultModel.listOfcandidateAnswers
                              join objAnswer in obQuizDbEntities.Answers on objResult.QuestionId equals objAnswer.QuestionId
                              join objQuestion in obQuizDbEntities.Questions on objResult.QuestionId equals objQuestion.QuestionId

                              select new ResultModel()
                              {
                                  Question = objQuestion.QuestionName,
                                  Answer = objAnswer.Answertext,
                                  AnswerByUser = objResult.AnswerText,
                                  Status = objAnswer.Answertext == objResult.AnswerText ? "Correct" : "Wrong",
                                  total = objAnswer.Answertext == objResult.AnswerText ? total = total + 1 : total,

                              }).ToList();
            Session["total"] = total;
            Session.Abandon();



            return View("../Quiz/GetFinalResult", UserResult);
        }
    }
}