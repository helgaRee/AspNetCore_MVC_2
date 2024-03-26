using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, UserRepository userRepository, UserService userService) : Controller
{

    //Läs in Identitys services
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly UserService _userService = userService;
    private readonly UserRepository _userRepository = userRepository;


    /// <summary>
    /// Används för att visa reg.formuläret. Är usern inloggad, kmr den dirigeras till Details i account-controllen
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {

        var viewModel = new SignUpViewModel();


        //kontroll: om usern loggas in/ är inloggad, dirigera till Details account
        //if (_signInManager.IsSignedIn(User))
        //{
        //    return RedirectToAction("Details", "Account");
        //}

        return View(viewModel);
    }


    /// <summary>
    /// Hanterar reg.formulärets postback. Tar emot data från formuläret i en Signupviewmodel och kotrollerar datan. 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("/signup")]

    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {

        if (!ModelState.IsValid)
            return View(viewModel);

        var result = await _userService.CreateUserAsync(viewModel.Form);
        if (result.StatusCode == Infrastructure.Model.StatusCode.OK)
            return RedirectToAction("SignIn", "Auth"); //återgå till SignInsidan om lyckas? till detaisl eller?

        return View(viewModel);

        //if (ModelState.IsValid)
        //{
        //    var exists = await _userRepository.AlreadyExistsAsync(x => x.Email == viewModel.Email);

        //    if (exists != null)
        //    {
        //        ModelState.AddModelError("AlreadyExists", "User with same email address aldready exists");
        //        ViewData["ErrorMessage"] = "User with the same email address aldready exists";
        //        return View(viewModel);
        //    }

        //    else
        //    {
        //        var userEntity = new UserEntity
        //        {
        //            FirstName = viewModel.FirstName,
        //            LastName = viewModel.LastName,
        //            Email = viewModel.Email,
        //            UserName = viewModel.Email,
        //        };

        //        var result = await _userManager.CreateAsync(userEntity, viewModel.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("SignIn", "Auth");
        //        }

        //    }

        //}
        //return View(viewModel);

    }

    /// <summary>
    /// Hanterar inloggning
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {

        var viewModel = new SignInViewModel();
        viewModel.ErrorMessage = "Ditt felmeddelande här";
        return View(viewModel);
        //om inloggad, gå till 
        //if (_signInManager.IsSignedIn(User))
        //{
        //    RedirectToAction("Details", "Account");
        //}
        //return View();
    }


    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.SignInUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Model.StatusCode.OK)
                return RedirectToAction("Details", "Account"); //Om OK
        }
        //ModelState.AddModelError("Incorrect values", "Incorrect email or password");
        //ViewData["ErrorMessage"] = "Incorrect email or password";
        viewModel.ErrorMessage = "Fel email eller lösenord";
        return View(viewModel);
    }




    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync(); // "dödar" sessionen
        return RedirectToAction("Home", "Default");
    }

}
