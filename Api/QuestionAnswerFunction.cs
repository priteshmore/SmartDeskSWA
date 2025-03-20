using Api.DB;
using Api.DB.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Api
{
    public class QuestionAnswerFunction
    {
        private readonly SmartDeskDBContext _context;

        public QuestionAnswerFunction(SmartDeskDBContext context)
        {
            _context = context;
        }

        // ?? CREATE Question & Answer
        [Function("CreateQuestionWithAnswer")]
        public async Task<IActionResult> CreateQuestionWithAnswer(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating a new question with an answer.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var question = JsonConvert.DeserializeObject<Question>(requestBody);

            if (question == null || question.Answer == null)
                return new BadRequestObjectResult("Invalid input data");

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return new OkObjectResult(question);
        }

        // ?? READ ALL Questions & Answers
        [Function("GetQuestionsWithAnswers")]
        public async Task<IActionResult> GetQuestionsWithAnswers(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Fetching all questions with answers.");
            var questions = await _context.Questions.Include(q => q.Answer).ToListAsync();
            return new OkObjectResult(questions);
        }

        // ?? READ Single Question & Answer
        [Function("GetQuestionWithAnswerById")]
        public async Task<IActionResult> GetQuestionWithAnswerById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "question/{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            log.LogInformation($"Fetching question with ID: {id}");
            var question = await _context.Questions.Include(q => q.Answer).FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
                return new NotFoundResult();

            return new OkObjectResult(question);
        }

        // ?? UPDATE Question & Answer
        [Function("UpdateQuestionWithAnswer")]
        public async Task<IActionResult> UpdateQuestionWithAnswer(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "question/{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            log.LogInformation($"Updating question with ID: {id}");

            var existingQuestion = await _context.Questions.Include(q => q.Answer).FirstOrDefaultAsync(q => q.Id == id);
            if (existingQuestion == null)
                return new NotFoundResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedData = JsonConvert.DeserializeObject<Question>(requestBody);

            existingQuestion.Title = updatedData.Title;
            existingQuestion.SkillName = updatedData.SkillName;
            existingQuestion.Concept = updatedData.Concept;
            existingQuestion.MostAskedCategory = updatedData.MostAskedCategory;

            if (updatedData.Answer != null)
            {
                if (existingQuestion.Answer != null)
                {
                    existingQuestion.Answer.Text = updatedData.Answer.Text;
                }
                else
                {
                    existingQuestion.Answer = new Answer
                    {
                        Text = updatedData.Answer.Text,
                        QuestionId = existingQuestion.Id
                    };
                }
            }

            await _context.SaveChangesAsync();
            return new OkObjectResult(existingQuestion);
        }

        // ?? DELETE Question & Answer
        [Function("DeleteQuestionWithAnswer")]
        public async Task<IActionResult> DeleteQuestionWithAnswer(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "question/{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            log.LogInformation($"Deleting question with ID: {id}");

            var question = await _context.Questions.Include(q => q.Answer).FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
                return new NotFoundResult();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return new OkObjectResult($"Deleted question with ID {id}");
        }
    }
}
