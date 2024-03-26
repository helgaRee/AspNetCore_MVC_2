using Infrastructure.Contexts;

namespace Infrastructure.Services;

public class AddressManager(DataContext context)
{
    private readonly DataContext _context = context;

    //public async Task<AddressEntity> GetAddressAsync(string UserId)
    //{
    //    var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == UserId);
    //    return addressEntity!;

    //}
}
