using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FeatureRepository(DataContext context) : Repo<FeatureEntity>(context)
{
    private readonly DataContext? _context;
    //Hämta featureItems - gör en ovveride





    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {

            if (_context == null)
            {
                return ResponseFactory.Error("Context is null");
            }

            if (_context!.Features == null)
            {
                return ResponseFactory.Error("Features context is null");
            }

            try
            {
                IEnumerable<FeatureEntity> result = await _context.Features
                    .Include(i => i.FeatureItems)
                    .ToListAsync();

                return ResponseFactory.Ok(result);

            }
            catch (Exception ex)
            {
                return ResponseFactory.Error("Error Fetching data: " + ex.Message);

            }

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }







}
