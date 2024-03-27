using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService(UserRepository repository, AddressService addressService)
{
    private readonly UserRepository _repository = repository;
    private readonly AddressService AddressService = addressService;


    public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
    {
        try
        {
            //sökning
            var exists = await _repository.AlreadyExistsAsync(x => x.Email == model.Email);
            if (exists.StatusCode == StatusCode.EXISTS)
                return exists;

            var result = await _repository.CreateOneAsync(UserFactory.Create(model));
            if (result.StatusCode != StatusCode.OK)
                return result;

            return ResponseFactory.Ok("användaren skapades korrekt.");

        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }



    public async Task<ResponseResult> SignInUserAsync(SignInModel model)
    {
        try
        {
            // Sökning - hämtar från repo
            var result = await _repository.GetOneAsync(x => x.Email == model.Email);

            // Kontroll om användaren stämmer
            if (result.StatusCode == StatusCode.OK && result.ContentResult != null)
            {
                // Konvertera till en UserEntity
                var userEntity = (UserEntity)result.ContentResult;

                // Kontroll om lösenordet stämmer
                //if (PasswordHasher.ValidateSecurePassword(model.Password, userEntity.Password, userEntity.SecurityKey))
                //{
                //    return ResponseFactory.Ok();
                //}
            }

            // Om vi är här, antingen användaren finns inte eller lösenordet är felaktigt
            return ResponseFactory.Error("Fel email!");

        }
        catch (Exception ex)
        {
            // Hantera eventuella undantag
            return ResponseFactory.Error($"Ett fel inträffade: {ex.Message}");
        }
    }
}
