using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AccountDetailsAddressInfoModel
{
    [DataType(DataType.Text)]
    [Display(Name = "AddressLine1", Prompt = "Enter your address line")]
    [Required(ErrorMessage = "Du måste ange ett giltigt address")]
    public string AddressLine_1 { get; set; } = null!;



    [DataType(DataType.Text)]
    [Display(Name = "AddressLine2 (optional)", Prompt = "Enter your second address line")]
    public string? AddressLine_2 { get; set; }


    [DataType(DataType.PostalCode)]
    [Display(Name = "PostalCode", Prompt = "Ange din postkod")]
    [Required(ErrorMessage = "Du måste ange en giltig postkod!")]
    public string PostalCode { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Ange stad")]
    [Required(ErrorMessage = "Du måste ange en giltigt stad! För fan")]
    public string City { get; set; } = null!;
}
