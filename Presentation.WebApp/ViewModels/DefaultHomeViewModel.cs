using Infrastructure.Entities;
using Infrastructure.Services;
using Presentation.WebApp.Models.Sections;

namespace Presentation.WebApp.Models.Views;

public class DefaultHomeViewModel()
{
    private readonly FeatureService? _featureService;

    //Behåller konstruktorn för att kunna hämta information
    //public DefaultHomeViewModel(FeatureService featureService)
    //{
    //    _featureService = featureService;


    //    ////hämta informarion från service och poppulera listan med fält
    //    ////man kan inte använda asyn i en konstruktor, för att komma runt det:
    //    //Task.Run(async () =>
    //    //{
    //    //    var result = await _featureService.GetAllFeaturesAsync();
    //    //    var content = (FeatureEntity)result.ContentResult!;   //SÄTT värdena

    //    //    Features.Title = content.Title;
    //    //    Features.Ingress = content.Ingress;

    //    //    foreach (var item in content.FeatureItems)
    //    //        Features.FeatureItems.Add(item);
    //    //});
    //}

    //public DefaultHomeViewModel(FeatureService featureService, string title, ShowcaseViewModel showcase, FeaturesViewModel features) : this(featureService)
    //{
    //    Title = title;
    //    Showcase = showcase;
    //    Features = features;
    //}

    public string Title { get; set; } = "";
    public ShowcaseViewModel Showcase { get; set; } = null!;
    //lägg in feature här också
    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel();
}


public class FeaturesViewModel
{
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public List<FeatureItemEntity> FeatureItems { get; set; } = [];

}

