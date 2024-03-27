using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : Controller
{

    //Läs in Identitys services
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;




    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {

        var viewModel = new SignUpViewModel();
        return View(viewModel);


    }



    [HttpPost]
    [Route("/signup")]

    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
                if (!exists)
                {
                    //SKAPA ENM USERENTITY
                    var userEntity = new UserEntity
                    {
                        FirstName = viewModel.Form.FirstName,
                        LastName = viewModel.Form.LastName,
                        Email = viewModel.Form.Email,
                        UserName = viewModel.Form.Email,
                    };

                    // Anropa CreateUserAsync i UserService för att skapa användaren
                    var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Auth");
                    }
                    else
                    {
                        ViewData["StatusMessage"] = "Something went wrong, try again.";
                    }
                }
                else
                {
                    ViewData["StatusMessage"] = "User with the same email already exists";
                }
            }
            return View(viewModel);

        }
        catch (Exception ex)
        {
            // Sätt ett felmeddelande i ViewData eller ViewBag
            ViewData["StatusMessage"] = "An error occurred while signing up. Please try again.";

            // Returnera vyn med felmeddelandet
            return View(viewModel);
        }

    }


    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }



    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Incorrect Values", "Incorrect email or password");
            ViewData["ErrorMessage"] = "Invalid email or password.";
            return View(viewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, false, false);

        if (result.Succeeded)
        {
            return RedirectToAction("Account", "Details");
        }
        else
        {
            ModelState.AddModelError("Login Failed", "Invalid email or password");
            return View(viewModel);
        }
    }



    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync(); // "dödar" sessionen
        return RedirectToAction("Home", "Default");
    }

}
