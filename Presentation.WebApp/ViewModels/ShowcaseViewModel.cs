using Presentation.WebApp.Models.Components;

namespace Presentation.WebApp.Models.Sections;
//modell för det som behövs till section Showcase
public class ShowcaseViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ShowcaseImage { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;

    //skapar en instans av LinkViewModel så den inte är tom
    public LinkViewModel Link { get; set; } = new LinkViewModel();

    public string? BrandsText { get; set; }
    public List<ImageViewModel>? Brands { get; set; }
}
