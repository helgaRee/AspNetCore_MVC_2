using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService(DataContext context, UserManager<UserEntity> userManager)
{
    private readonly DataContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;


    public async Task<bool> UpdateUserAsync(UserEntity user)
    {
        //kan använda din egna service för att ha mer kontroll på vad du vill göra
        _context.Users.Add(user);

        //eller använda identitys service
        await _userManager.Users.FirstOrDefaultAsync(user => user.Email == user.Email);
        return true;
    }
}