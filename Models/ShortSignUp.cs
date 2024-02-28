using System;
using System.ComponentModel.DataAnnotations;


namespace NewApp.Models;


public class ShortSignUpViewModel
{
    public string AssessmentCode { get; set; }

    public int TotalExperience { get; set; }

    public Guid Role { get; set; }

    public Guid WorkAtmospheres { get; set; }

    public string Email { get; set; }

    public string Summary { get; set; }

    public bool IsHRManagement { get; set; }

    public string Salary { get; set; }

    public string JobNote { get; set; }

    [Required]
    [Display(Name = "First name")]
    [RegularExpression(@"^[ a-zA-Z. ]+$", ErrorMessage = "Use letters only please")]
    public string FirstName { get; set; }

    [Display(Name = "Middle name")]
    [RegularExpression(@"^[ a-zA-Z. ]+$", ErrorMessage = "Use letters only please")]
    public string MiddleName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    [RegularExpression(@"^[ a-zA-Z. ]+$", ErrorMessage = "Use letters only please")]
    public string LastName { get; set; }

    [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,15}",
         ErrorMessage = "Password must be 8 characters long with at least one numeric, one  upper case and lower case character and one special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "What is your current country?")]
    [Required(ErrorMessage = "This field is required")]
    public Guid Country { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "What is your current location?")]
    public Guid CurrentLocation { get; set; }

    [Display(Name = "If your current location is not in the list above, add it here")]
    public string CurrentLocationOther { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "What is your Subject/Stream")]
    public Guid Subject { get; set; }


    //  [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Date of Birth")]
    public string DateOfBirth { get; set; }

    //[Required(ErrorMessage = "This field is required")]
    [Display(Name = "Gender")]
    public Guid? Gender { get; set; }

    [Display(Name = "Other Functional Area")]
    public string CurrentFunctionalAreaOther { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Qualification/Certification Type")]
    public Guid QualificationType { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Degree/Certificate")]
    public Guid Qualification { get; set; }

    [Display(Name = "Other Degree/Certificate")]
    public string QualificationOther { get; set; }

    [Display(Name = "Contact Number")]
    [RegularExpression("^\\+?[1-9]{1}[0-9]{3,14}$", ErrorMessage = "Enter a valid phone number")]
    public string ContactNo { get; set; }

    public bool isDocument { get; set; }

    public bool IsPhoneNumber { get; set; }

    public bool IsCorporate { get; set; }

    public bool IsCareergraph { get; set; }

    public bool IsPublicJob { get; set; }

    public string AdharNo { get; set; }

    public bool IsAadhar { get; set; }

    public bool IsPan { get; set; }

    public bool ReadytoRelocate { get; set; }

    public string PanNo { get; set; }

    public bool HasVehicleRequired { get; set; }

    public bool HasVehicle { get; set; }

    public int Experience { get; set; }

    [Display(Name = "Industry")]
    public List<Guid?> Industries { get; set; }




    public ShortSignUpViewModel()
    {
        Documents = new List<documents>();

    }

    //   [RegularExpression("^.+(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG)$", ErrorMessage = "Please upload a jpg/jpeg/png file only")]
    // [DataType(DataType.Upload)]
    public IFormFile DocumentUrl { get; set; }

    public List<documents> Documents { get; set; }

    public List<ShortEducationViewModel> Educations { get; set; }

    public List<Jobkill> Jobkill { get; set; }


}
public class Jobkill
{
    public string Score { get; set; }
    public Guid Id { get; set; }

}
public class ShortEducationViewModel
{
    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Qualification/Certification Type")]
    public Guid QualificationType { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Degree/Certificate")]
    public Guid Qualification { get; set; }

    [Display(Name = "Other Degree/Certificate")]
    public string QualificationOther { get; set; }

    [Display(Name = "Year of Passout")]
    public string YearOfPassout { get; set; }

    //  [Required(ErrorMessage = "This field is required")]
    [Display(Name = "Percentage")]
    public double? Percentage { get; set; }

    [Display(Name = "Grade/Level/Cum Laude")]
    public Guid? Grade { get; set; }

    [Display(Name = "Is this your highest qualification?")]
    public bool IsHighestQualification { get; set; }
}

public class documents
{

    public Guid DocumentTypeId { get; set; }

    public string DocumentTypeName { get; set; }

    public Guid JobDescriptionId { get; set; }


    [RegularExpression("^.+(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG|.pdf|.PDF)$", ErrorMessage = "Please upload a jpg/jpeg/png/pdf file only")]
    [DataType(DataType.Upload)]
    public IFormFile DocumentUrl { get; set; }

}



