using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Model;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class SubscribeRepository(DataContext context) : Repo<SubscribeEntity>(context)
{
    private readonly DataContext _context = context;

    public override Task<ResponseResult> GetOneAsync(Expression<Func<SubscribeEntity, bool>> expression)
    {
        return base.GetOneAsync(expression);
    }
}
