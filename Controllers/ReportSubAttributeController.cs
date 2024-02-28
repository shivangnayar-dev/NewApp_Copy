using Microsoft.AspNetCore.Mvc;
using NewApp.Models;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class ReportSubAttributeController : Controller
    {
        private readonly CandidateDbContext _context;

        public ReportSubAttributeController(CandidateDbContext context)
        {
            _context = context;
        }

[HttpPost]
        [Route("CheckreportIdValidity")]
        public IActionResult CheckreportIdValidity([FromBody] ReportSubAttribute reportSubAttribute)
        {
            bool isValid = CheckTestCodeValidityInDatabase(reportSubAttribute.ReportId);
            Dictionary<string, Dictionary<string, Dictionary<int, List<object>>>> assessmentSubAttributesData = GetAssessmentSubAttributesBy(reportSubAttribute.ReportId);

            // Randomly select questions
            var (selectedQuestionIds, combinedQuestions) = CombineAndReturnIdsAndQuestions(assessmentSubAttributesData);

            // Combine selected questions
            var optionsAndAnswerIds = GetOptionsAndAnswerIdsWithQuestions(selectedQuestionIds);
            var questionTextList = FetchQuestionData(selectedQuestionIds);

            // Fetch AssessmentSubAttribute, CountofQuestiontoDisplay, and QuestionCount
            var assessmentSubAttributes = assessmentSubAttributesData.Keys.Select(subAttribute =>
            {
                var subAttributeData = assessmentSubAttributesData[subAttribute];
                var countOfQuestionToDisplay = subAttributeData.Values.Last().Keys.Last();
                var questionCount = subAttributeData.Values.Last().Values.Last()[0]; // Assuming there's at least one question in the subAttribute

                // Extract selected questions for the current AssessmentSubAttribute
                var selectedQuestionsForSubAttribute = combinedQuestions
                    .Where(kv => selectedQuestionIds.Contains(kv.Key))
                    .ToDictionary(kv => kv.Key.ToString(), kv => kv.Value);

                return new
                {
                    AssessmentSubAttribute = subAttribute,
                    CountofQuestiontoDisplay = countOfQuestionToDisplay,
                    QuestionCount = questionCount,
                    SelectedQuestionsCount = selectedQuestionsForSubAttribute.Count, // Add this line to get the count of selected questions
                    SelectedQuestions = selectedQuestionsForSubAttribute
                };
            }).ToList();

            // Modify the structure to match the expected format
            var formattedOptionsAndAnswerIds = optionsAndAnswerIds.ToDictionary(
    kvp => kvp.Key.ToString(), // Assuming kvp.Key is the question ID
    kvp => new
    {
        question_id = kvp.Key.ToString(), // Assigning kvp.Key as question_id
        question = kvp.Value.Question,
        optionsAndAnswerIds = kvp.Value.OptionsAndAnswerIds.Select(opt => new
        {
            Item1 = opt.Options,
            Item2 = opt.AnswerId,
            Item3 = opt.SequenceOfDisplay,
            Item4 = opt.MarksTotal
        }).ToList()
    });
            // Check if the sum of countToDisplay of all attributes is equal to the length of combinedQuestions
            int sumCountToDisplay = assessmentSubAttributesData.Values
                .SelectMany(subAttributeData => subAttributeData.Values)
                .Sum(questionData => questionData.Keys.First());

            bool isCountMatch = sumCountToDisplay == combinedQuestions.Count;


            return Json(new
            {
                isValid = isValid,
                assessmentSubAttributes = assessmentSubAttributes,
                questionOptionsAndAnswers = formattedOptionsAndAnswerIds,
                isCountMatch = isCountMatch,
                 sumCountToDisplay = sumCountToDisplay
            });
        }

        private Dictionary<Guid, (Guid QuestionId, string Question, List<(string Options, string AnswerId, int SequenceOfDisplay, int MarksTotal)> OptionsAndAnswerIds)> GetOptionsAndAnswerIdsWithQuestions(List<Guid> selectedQuestionIds)
        {
            Dictionary<Guid, (Guid QuestionId, string Question, List<(string Options, string AnswerId, int SequenceOfDisplay, int MarksTotal)> OptionsAndAnswerIds)> questionOptionsAndAnswers = new Dictionary<Guid, (Guid QuestionId, string Question, List<(string Options, string AnswerId, int SequenceOfDisplay, int MarksTotal)>)>();

            using (var dbContext = new QuestionAnsMapDbContext())
            {
                foreach (var questionId in selectedQuestionIds)
                {
                    // Ensure that the Select statement returns the correct types
                    var questionDataList = _context.QuestionAnsMaps
                        .Where(q => q.QuestionId == questionId)
                        .Select(q => new
                        {
                            q.QuestionId,
                            q.Question,
                            q.Options,
                            q.AnswerId,
                            q.SequenceOfDisplay,
                            q.MarksTotal
                        })
                        .OrderBy(q => q.SequenceOfDisplay)
                        .ToList();

                    if (questionDataList.Any())
                    {
                        // Use the correct property names and types when adding to the dictionary
                        var optionsAndAnswerList = questionDataList
                            .Select(q => (
                                Options: q.Options,
                                AnswerId: q.AnswerId,
                                SequenceOfDisplay: q.SequenceOfDisplay,
                                MarksTotal: q.MarksTotal))
                            .ToList();

                        var questionAndOptions = (QuestionId: questionDataList.First().QuestionId, Question: questionDataList.First().Question, OptionsAndAnswerIds: optionsAndAnswerList);

                        questionOptionsAndAnswers.Add(questionId, questionAndOptions);
                    }
                }
            }

            return questionOptionsAndAnswers;
        }


        private List<string> FetchQuestionData(List<Guid> selectedQuestionIds)
        {
            List<string> questionTextList = new List<string>();

            using (var dbContext = new QuestionAnsMapDbContext())
            {
                foreach (var questionId in selectedQuestionIds)
                {
                    var questionText = _context.QuestionAnsMaps
                        .Where(q => q.QuestionId == questionId)
                        .Select(q => q.Question)
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(questionText))
                    {
                        questionTextList.Add(questionText);
                    }
                }
            }

            return questionTextList;
        }

        private Dictionary<string, Dictionary<string, Dictionary<int, List<object>>>> GetAssessmentSubAttributesBy(string ReportId)
        {
            var distinctAssessmentSubAttributeIds = _context.ReportSubAttributes
                .Where(tc => tc.ReportId == ReportId)
                .Select(tc => tc.AssessmentSubAttributeId)
                .ToList();

            Dictionary<string, Dictionary<string, Dictionary<int, List<object>>>> assessmentSubAttributesData = new Dictionary<string, Dictionary<string, Dictionary<int, List<object>>>>();

            foreach (var assessmentSubAttributeId in distinctAssessmentSubAttributeIds)
            {
                var values = _context.ReportSubAttributes
                    .Where(tc => tc.ReportId == ReportId && tc.AssessmentSubAttributeId == assessmentSubAttributeId)
                    .Select(tc => new
                    {
                        tc.AssessmentSubAttributeId,
                        tc.AssessmentSubAttribute,
                        tc.CountofQuestiontoDisplay,
                        tc.QuestionCount,
                        tc.TimeperQuestioninSec
                    })
                    .ToList();

                Dictionary<string, Dictionary<int, List<object>>> subAttributeData = new Dictionary<string, Dictionary<int, List<object>>>();
                int cumulativeCount = 0;

                foreach (var value in values)
                {
                    cumulativeCount += value.CountofQuestiontoDisplay;

                    if (!subAttributeData.ContainsKey(value.AssessmentSubAttributeId))
                    {
                        subAttributeData.Add(value.AssessmentSubAttributeId, new Dictionary<int, List<object>>());
                    }

                    var questions = _context.TotalQuestions
                        .Where(tq => tq.AssessmentSubAttributeId == value.AssessmentSubAttributeId)
                        .ToDictionary(tq => tq.QuestionId, tq => tq.Question);

                    subAttributeData[value.AssessmentSubAttributeId].Add(cumulativeCount, new List<object> { value.QuestionCount, value.TimeperQuestioninSec, questions });
                }

                assessmentSubAttributesData.Add(assessmentSubAttributeId, subAttributeData);
            }

            return assessmentSubAttributesData;
        }


        private bool CheckTestCodeValidityInDatabase(string ReportId)
        {
            return _context.ReportSubAttributes.Any(tc => tc.ReportId == ReportId);
        }


        private (List<Guid> selectedQuestionIds, Dictionary<Guid, string> combinedQuestions) CombineAndReturnIdsAndQuestions(Dictionary<string, Dictionary<string, Dictionary<int, List<object>>>> assessmentSubAttributesData)
        {
            List<Guid> selectedQuestionIds = new List<Guid>();
            Dictionary<Guid, string> combinedQuestions = new Dictionary<Guid, string>();

            int totalQuestionCount = assessmentSubAttributesData.Values
                .SelectMany(subAttributeData => subAttributeData.Values)
                .Sum(questionData => questionData.Keys.First());

            foreach (var subAttributeData in assessmentSubAttributesData.Values)
            {
                foreach (var questionData in subAttributeData.Values)
                {
                    int countToDisplay = questionData.Keys.First();

                    if (questionData.ContainsKey(countToDisplay))
                    {
                        List<object> questionsList = questionData[countToDisplay];

                        if (questionsList.Count > 2 && questionsList[2] is Dictionary<Guid, string> subSelectedQuestions)
                        {
                            // Shuffle the questions
                            var shuffledQuestions = subSelectedQuestions.OrderBy(x => Guid.NewGuid()).ToDictionary(pair => pair.Key, pair => pair.Value);

                            // Select a subset of questions based on countToDisplay
                            var selectedSubset = shuffledQuestions.Take(countToDisplay).ToDictionary(pair => pair.Key, pair => pair.Value);

                            foreach (var question in selectedSubset)
                            {
                                {
                                    selectedQuestionIds.Add(question.Key);
                                    combinedQuestions.Add(question.Key, question.Value);
                                }
                            }
                        }
                    }
                }
            }

            // Check if sum of countToDisplay of all attributes is equal to the length of combinedQuestions
            int sumCountToDisplay = assessmentSubAttributesData.Values
                .SelectMany(subAttributeData => subAttributeData.Values)
                .Sum(questionData => questionData.Keys.First());

            if (sumCountToDisplay == combinedQuestions.Count)
            {
                // Sum of countToDisplay matches the length of combinedQuestions
                Console.WriteLine("Sum of countToDisplay matches the length of combinedQuestions.");
            }
            else
            {
                // Sum of countToDisplay does not match the length of combinedQuestions
                Console.WriteLine("Sum of countToDisplay does not match the length of combinedQuestions.");
            }

            return (selectedQuestionIds, combinedQuestions);
        }






    }
}
