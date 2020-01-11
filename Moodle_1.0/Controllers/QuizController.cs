using Moodle_1._0.Models;
using Moodle_1._0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace Moodle_1._0.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {

        private MoodleDb obQuizDbEntities;

        public QuizController()
        {
            obQuizDbEntities = new MoodleDb();
        }
        // GET: Quiz
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
           
            Session["CandidateName"] = User.Identity.GetUserName();
            Session["CategoryId"] = CategoryId;
            Session["co"] = null;

            return View("QuestionIndex");

        }

        public PartialViewResult userQuestionAnswer(CandidateAnswer objCandidateAnswer)

        {

            bool IsLast = false;
            if (Session["co"] == null)
            {
                Session["co"] = 1;

            }
            else
            {
                var i = Convert.ToInt32(Session["co"]);
                i++;
                Session["co"] = i;
            }
            if (objCandidateAnswer.AnswerText != null)
            {
                var ans = (from obj in obQuizDbEntities.Answers
                           where obj.QuestionId.Equals(objCandidateAnswer.QuestionId)
                           select obj).FirstOrDefault();

                Result result = new Result();
                result.AnswerText = objCandidateAnswer.AnswerText;
                result.UserId = Session["CandidateName"].ToString();
                // result.QuestionId = objCandidateAnswer.QuestionId;
                result.QuestionId = Convert.ToInt32(Session["questionid"]);
                obQuizDbEntities.Results.Add(result);
                obQuizDbEntities.SaveChanges();

            }
            if (objCandidateAnswer.AnswerText != null)
            {
                List<CandidateAnswer> objCandidateAnswers = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                if (objCandidateAnswers == null)
                {
                    objCandidateAnswers = new List<CandidateAnswer>();
                }
                objCandidateAnswers.Add(objCandidateAnswer);
                Session["CadQuestionanswer"] = objCandidateAnswers;
            }

            int pageSize = 1;
            int pageNumber = 0;
            int CategoryId = Convert.ToInt32(Session["CategoryId"]);
            if (Session["CadQuestionAnswer"] == null)
            {
                pageNumber = pageNumber + 1;
            }
            else
            {
                List<CandidateAnswer> canAnswer = Session["CanQuestionAnswer"] as List<CandidateAnswer>;
                pageNumber = Convert.ToInt32(Session["co"]);
                //  pageNumber = canAnswer.Count + 1;
                //pageNumber = pageNumber + 1;
            }
            List<Question> listOfQuestion = new List<Question>();
            listOfQuestion = obQuizDbEntities.Questions.Where(model => model.CategoryId == CategoryId).ToList();
            if (pageNumber == listOfQuestion.Count)
            {
                IsLast = true;
                var candidate = Session["CandidateName"].ToString();

                // Session["co"] = null;

            }

            QuestionAnswerViewModel objAnswerViewModel = new QuestionAnswerViewModel();
            Question objQuestion = new Question();
            objQuestion = listOfQuestion.Skip((pageNumber - 1) * pageSize).Take(pageSize).FirstOrDefault();
            objAnswerViewModel.isLast = IsLast;
            objAnswerViewModel.QuestionId = objQuestion.QuestionId;
            Session["questionid"] = objQuestion.QuestionId;
            objAnswerViewModel.QuestionName = objQuestion.QuestionName;
            objAnswerViewModel.ListOfQuizOptions = (from obj in obQuizDbEntities.Options
                                                    where obj.QuestionId == objQuestion.QuestionId
                                                    select new QuizOption()
                                                    {
                                                        OptionName = obj.OptionName,
                                                        OptionId = obj.OptionId
                                                    }).ToList();

            return PartialView("QuizQuestionOption", objAnswerViewModel);
        }

        public JsonResult SaveCandidateAnswer(CandidateAnswer objCandidateAnswer)
        {
            Answer answer = new Answer();

            var ans = (from obj in obQuizDbEntities.Answers
                       where obj.QuestionId == objCandidateAnswer.QuestionId
                       select obj.Answertext);


            Result result = new Result();
            result.AnswerText = objCandidateAnswer.AnswerText;
            result.UserId = Session["CandidateName"].ToString();
            result.QuestionId = Convert.ToInt32(Session["questionid"]);
            obQuizDbEntities.Results.Add(result);
            obQuizDbEntities.SaveChanges();

            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFinalResult()
        {
            int total = Convert.ToInt32(Session["total"]);
            string candidateName = Session["CandidateName"].ToString();
            int categotyId = Convert.ToInt32(Session["CategoryId"]);


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



            return View(UserResult);
        }
    }
}