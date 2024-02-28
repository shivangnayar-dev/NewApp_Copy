using System;
namespace NewApp.Models
{
	 public class ReportSubAttribute
    {
        public string ReportId { get; set; }
        public string Name { get; set; }
        public string AssessmentSubAttributeId { get; set; }
        public string AssessmentSubAttribute { get; set; }
        public int CountofQuestiontoDisplay { get; set; }
        public int QuestionCount { get; set; }
        public int TimeperQuestioninSec { get; set; }
       
    
    }
}

