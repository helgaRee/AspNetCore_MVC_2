using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class CourseController : Controller
{
    [Route("/courses")]
    public IActionResult Courses()
    {
        return View();
    }
}
