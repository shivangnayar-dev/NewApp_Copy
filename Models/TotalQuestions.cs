using System;
namespace NewApp.Models
{
    public class TotalQuestion
    {
        public Guid QuestionId { get; set; }
        public string AssessmentSubAttributeId { get; set; }
        public string Question { get; set; }
        public string Weight { get; set; }
        public string Type { get; set; }
        public string OriginalTable { get; set; }
    }

}


