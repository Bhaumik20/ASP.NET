using Moodle_1._0.Models;
using Moodle_1._0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    [Authorize(Roles ="Admin,Teacher")]
    public class AdminQuizController : Controller
    {
        private MoodleDb objquizdbEntities;
        public AdminQuizController()
        {
            objquizdbEntities = new MoodleDb();
        }
        // GET: Admin
        public ActionResult Index()
        {
            CategoryViewModel objcategoryViewModel = new CategoryViewModel();
            objcategoryViewModel.ListofCategory = (from objCat in objquizdbEntities.Catagories
                                                   select new SelectListItem()
                                                   {
                                                       Value = objCat.CategoryId.ToString(),
                                                       Text = objCat.CategoryName.ToString()
                                                   }).ToList();

            return View(objcategoryViewModel);
        }
        [HttpPost]
        public JsonResult Index(QuestionOptionViewModel questionOption)
        {
            Question objQuestion = new Question();
            objQuestion.QuestionName = questionOption.QuestionName;
            objQuestion.CategoryId = questionOption.CategoryId;
            objQuestion.IsActive = true;
            objQuestion.IsMultiple = false;
            objquizdbEntities.Questions.Add(objQuestion);
            objquizdbEntities.SaveChanges();

            int questionId = objQuestion.QuestionId;
            foreach (var item in questionOption.ListOfOptions)
            {
                Option objOption = new Option();
                objOption.OptionName = item;
                objOption.QuestionId = questionId;
                objquizdbEntities.Options.Add(objOption);
                objquizdbEntities.SaveChanges();
            }

            Answer objAnswer = new Answer();
            objAnswer.Answertext = questionOption.AnswerText;
            objAnswer.QuestionId = questionId;
            objquizdbEntities.Answers.Add(objAnswer);
            objquizdbEntities.SaveChanges();
            return Json(new { message = "data Successfilly added.", success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFinalResult(QuizCategoryViewModel quizCategoryViewModel)
        {
            int total = Convert.ToInt32(Session["total"]);
            string candidateName = quizCategoryViewModel.CandidateName;
            int categotyId = quizCategoryViewModel.CategoryId;


            ResultModel objresultModel = new ResultModel();

            objresultModel.listOfcandidateAnswers = (from obj in objquizdbEntities.Results
                                                     where obj.UserId == candidateName
                                                     select new CanAnswer()
                                                     {
                                                         AnswerText = obj.AnswerText,
                                                         QuestionId = obj.QuestionId,

                                                     }).ToList();



            var UserResult = (from objResult in objresultModel.listOfcandidateAnswers
                              join objAnswer in objquizdbEntities.Answers on objResult.QuestionId equals objAnswer.QuestionId
                              join objQuestion in objquizdbEntities.Questions on objResult.QuestionId equals objQuestion.QuestionId

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



            return View(UserResult);
        }
    }
}