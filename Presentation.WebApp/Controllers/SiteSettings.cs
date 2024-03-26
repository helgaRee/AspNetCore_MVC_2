using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class SiteSettings : Controller
{
    public IActionResult ChangeTheme(string mode)
    {
        //Skapa en cookie med informationen som lever i 30 dagar och stoppa in en parameter
        //Denna cookie ska triggas igång vid klick på en knapp
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(60),
        };
        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }
}
