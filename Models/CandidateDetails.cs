using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewApp.Models
{
    public class CandidateDetails
    {
        // Primary key





        public int candidate_id { get; set; }


        // Candidate details
        public string Mobile_No { get; set; } = "0";
        public string Adhar_No { get; set; } = "0";
        public string email_address { get; set; } = "0";
        public string password { get; set; } 

        // Additional properties

        public string gender { get; set; } = "0";
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dob { get; set; } 

        public string country { get; set; } = "0";
        public string location { get; set; } = "0";
        

        [Required]
        public string name { get; set; } = "0";
        public string qualification { get; set; } = "0";
        public string organization { get; set; } = "0";

        // New properties for other organization
        public string otherOrganization { get; set; } = "0";

       

        public string SelectedOptions { get; set; } = "0";

        public string selectedSpecializations { get; set; } = "0";

        public string selectedIndustries { get; set; } = "0";

        public string interest { get; set; } = "0";

        public string pursuing { get; set; } = "0";

        public string transactionId { get; set; } = "0";


    }

}
