﻿


using Infrastructure.Model;

namespace Presentation.WebApp.ViewModels;

//EN MODELL SOM ÄR TILL FÖR VYN
public class SignUpViewModel
{



    public string Title { get; set; } = "Sign up";
    public SignUpModel Form { get; set; } = new SignUpModel();

    //public string TermsAndConditions { get; set; } = null!;

    //[DataType(DataType.Text)]
    //[Display(Name = "First name", Prompt = "Enter your first name")]
    //[Required(ErrorMessage = "A valid First name is required")]
    //public string FirstName { get; set; } = null!;



    //[DataType(DataType.Text)]
    //[Display(Name = "Last name", Prompt = "Enter your last name")]
    //[Required(ErrorMessage = "A valid Last name is required")]
    //public string LastName { get; set; } = null!;



    //[DataType(DataType.EmailAddress)]
    //[Display(Name = "Email", Prompt = "Enter your email")]
    //[Required(ErrorMessage = "A valid email is required")]
    //public string Email { get; set; } = null!;



    //[DataType(DataType.Password)]
    //[Display(Name = "Password", Prompt = "Enter your Password")]
    //[Required(ErrorMessage = "Password is required")]
    //public string Password { get; set; } = null!;



    //[DataType(DataType.Password)]
    //[Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    //[Required(ErrorMessage = "Password must be confirmed")]
    //[Compare(nameof(Password), ErrorMessage = "Password must be confirmed")]
    //public string ConfirmPassword { get; set; } = null!;



    //[Required(ErrorMessage = "Terms and conditions must be accepted")]
    //public bool TermsAndConditions { get; set; }
}

