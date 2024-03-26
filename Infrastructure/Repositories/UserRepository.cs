using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Migrations;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext context) : Repo<UserEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<UserEntity> listOfITems = await _context.Users
                .Include(i => i.Address)
                .ToListAsync();
            return ResponseFactory.Ok(listOfITems);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            //se om id existerar i db och isåfall hämta den
            var existingEntity = await _context.Set<UserEntity>()
                .Include(i => i.Address)
                .FirstOrDefaultAsync(expression);
            if (existingEntity != null)
                return ResponseFactory.Ok(existingEntity);

            return ResponseFactory.NotFound();

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}




