using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model;

public class SignUpModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 1)]
    [Required(ErrorMessage = "A valid First name is required")]
    public string FirstName { get; set; } = null!;



    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 2)]
    [Required(ErrorMessage = "A valid Last name is required")]
    public string LastName { get; set; } = null!;



    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email", Order = 3)]
    [Required(ErrorMessage = "A valid email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Ogiltig e-postadress")]
    public string Email { get; set; } = null!;



    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your Password", Order = 4)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
        ErrorMessage = "Lösenordet måste innehålla minst en stor bokstav, en liten bokstav, ett nummer och ett specialtecken.")]
    public string Password { get; set; } = null!;



    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password", Order = 5)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Fields does not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the Terms and Conditions", Order = 6)]
    [CheckBoxRequired(ErrorMessage = "Terms and conditions must be accepted")]
    public bool TermsAndConditions { get; set; } = false;
}
