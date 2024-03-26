using Infrastructure.Entities;

namespace Presentation.WebApp.ViewModels;

public class AccountDetailsViewModel
{
    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
    //anger information om användaren.
    public UserEntity User { get; set; } = null!;

    public AddressEntity Address { get; set; } = null!;

    public AddressInfoFormViewModel AddressInfoForm { get; set; } = null!;

    public BasicInfoFormViewModel BasicInfoForm { get; set; } = null!;
}
