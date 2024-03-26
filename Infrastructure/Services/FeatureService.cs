using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class FeatureService(FeatureRepository repository, FeatureItemRepository itemRepository)
{
    private readonly FeatureRepository repository = repository;
    private readonly FeatureItemRepository itemRepository = itemRepository;


    //gör en getAll metod som returnerar ett responseresultat 
    public async Task<ResponseResult> GetAllFeaturesAsync()
    {
        try
        {
            //hämta alla
            var result = await repository.GetAllAsync();
            //kontroll - returnera resultat om true annars
            return result;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }

}
