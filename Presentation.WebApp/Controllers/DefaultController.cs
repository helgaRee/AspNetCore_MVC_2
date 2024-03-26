using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models.Sections;
using Presentation.WebApp.Models.Views;

namespace Presentation.WebApp.Controllers;

public class DefaultController : Controller
{

    [Route("/")]
    public IActionResult Home()
    {
        var viewModel = new DefaultHomeViewModel
        {
            Title = "Ultimate Task Management Assistant",
            Showcase = new ShowcaseViewModel
            {
                Id = "showcase",
                ShowcaseImage = new() { ImageUrl = "Images/sections/showcase-image.svg", AltText = "Task Management Assistant" },
                Title = "Task Management Assistant",
                Text = "Home",
                Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free" },
                BrandsText = "Largest companies use our tool to work efficently",
                Brands = new List<Models.Components.ImageViewModel>
                {
                    new() {ImageUrl = "Images/brands/brand1.svg", AltText ="Brand Name 1"},
                    new() {ImageUrl = "Images/brands/brand2.svg", AltText ="Brand Name 2"},
                    new() {ImageUrl = "Images/brands/brand3.svg", AltText ="Brand Name 3"},
                    new() {ImageUrl = "Images/brands/brand4.svg", AltText ="Brand Name 4"},
                }
            }
        };
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }









    [Route("/error")]
    public IActionResult Error404(int? statusCode)
    {
        if (statusCode.HasValue && statusCode.Value == 404)
        {
            // Anpassa din felhanteringssida för 404-fel
            return View("Error404");
        }
        // För andra felstatuskoder, returnera standardfelhanteringssidan
        return View("Error");
    }

}