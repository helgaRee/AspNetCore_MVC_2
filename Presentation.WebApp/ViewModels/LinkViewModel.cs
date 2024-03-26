namespace Presentation.WebApp.Models.Components;
//En modell för länkar och omdirigering (för showcase bara?)
public class LinkViewModel
{
    public string ControllerName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string? Text { get; set; }

}
