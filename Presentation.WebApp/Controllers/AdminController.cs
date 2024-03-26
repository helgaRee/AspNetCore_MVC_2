using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AdminController : Controller
{
    [Route("/admin")]
    public IActionResult Index()
    {
        return View();
    }
}
