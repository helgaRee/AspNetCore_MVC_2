using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AddressService addressService) : Controller
{
    //läs in identitys services
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressService _addressService;


    #region Details
    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel();

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }



    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {

        if (viewModel.BasicInfoForm != null)
        {
            if (
                viewModel.BasicInfoForm.FirstName != null &&
                viewModel.BasicInfoForm.LastName != null &&
                viewModel.BasicInfoForm.Email != null
                )
            {
                var user = await _userManager.GetUserAsync(User);

                //OM USER FINNS, UPPDATERA
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfoForm.FirstName;
                    user.LastName = viewModel.BasicInfoForm.LastName;
                    user.Email = viewModel.BasicInfoForm.Email;
                    user.PhoneNumber = viewModel.BasicInfoForm.Phone;
                    user.Biography = viewModel.BasicInfoForm.Biography;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("Incorrect values", "Something went wrong! Unable to save data.");
                        ViewData["ErrorMessage"] = "Something went wrong! Unable to update basic information data.";
                    }
                }
            }
        }


        //oavsettvad ska alltid profilinofmraitonen hämtas
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        //Hämtar den NYA informationen och poppulerar den
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);

    }

    #endregion


    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        //HÄMTAR ANVÄNDARINFORMATIONEN BASERAT PÅ CLAIMS
        var user = await _userManager.GetUserAsync(User);
        //kontrollera om null först
        if (user != null)
        {
            return new ProfileInfoViewModel
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
            };
            
        }
        return null!;
    }


    //metod somreturnerar information ur basicinfoform. Gör att information om användaren hämtas till Details-sidan.
    //returnerar en AccountDetailsViewModel
    private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
    {
        //HÄMTAR ANVÄNDARINFORMATIONEN BASERAT PÅ CLAIMS
        var user = await _userManager.GetUserAsync(User);

        if(user!= null)
        {
            return new BasicInfoFormViewModel
            {
                UserId = user!.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Biography = user.Biography,
                Phone = user.PhoneNumber,
            };
        }
            else
        {
            // Hantera fall där användaren är null (t.ex. logga fel eller returnera en tom modell)
            // Här returnerar jag en tom modell som exempel, men du kan ändra detta efter behov
            return new BasicInfoFormViewModel();
        }
    }


    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {
        //Returnerar en tom model
        return new AddressInfoFormViewModel();
    }
}





