using System;
namespace NewApp.Models
{
    public class QuestionAnsMap
    {
        public string AnswerId { get; set; }
        public string Options { get; set; }
        public Guid QuestionId { get; set; }
        public int SequenceOfDisplay { get; set; }
        public int MarksTotal { get; set; }
        public string Question { get; set; }
    }
}

