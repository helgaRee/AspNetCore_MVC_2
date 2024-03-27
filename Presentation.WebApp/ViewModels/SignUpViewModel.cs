using Infrastructure.Model;

namespace Presentation.WebApp.ViewModels;

//EN MODELL SOM ÄR TILL FÖR VYN
//titel och formuläret
public class SignUpViewModel
{

    public string Title { get; set; } = "Sign up";
    public SignUpModel Form { get; set; } = new SignUpModel();

}

