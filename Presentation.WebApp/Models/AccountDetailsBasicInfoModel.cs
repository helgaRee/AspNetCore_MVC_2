using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AccountDetailsBasicInfoModel
{
    [DataType(DataType.ImageUrl)]
    public string? ProfileImage { get; set; }
    public string UserId { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Skriv ditt förnamn")]
    [Required(ErrorMessage = "Du måste ange ett giltigt förnamn")]
    public string FirstName { get; set; } = null!;




    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "skriv in ditt efternamn")]
    [Required(ErrorMessage = "Du måste fylla i ett giltigt efternamn")]
    public string LastName { get; set; } = null!;




    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Ange din emailaddress")]
    [Required(ErrorMessage = "Du måste ange en giltig Email!")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
    public string Email { get; set; } = null!;




    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone (optional)", Prompt = "Ange ditt nummer!")]
    [Required(ErrorMessage = "Du måste ange ett giltigt nummer! För fan")]
    public string? Phone { get; set; }





    [DataType(DataType.MultilineText)]
    [Display(Name = "Biography (optional)", Prompt = "Add a short bio...")]
    public string? Biography { get; set; }
}
