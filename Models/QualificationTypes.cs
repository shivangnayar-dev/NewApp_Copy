using System;
namespace NewApp.Models
{
    public class QualificationTypes
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public int SortOrder { get; set; }
        public int IsOther { get; set; }
    }
}

