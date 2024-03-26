using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
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
            //sökning - hämtar från repo
            var result = await _repository.GetOneAsync(x => x.Email == model.Email);
            //kontroll om användaren stämmer
            if (result.StatusCode == StatusCode.OK && result.ContentResult != null)
            {

                //kontroll om lösenordet stämmer - validerar med hasher - genererar OK om lyckas
                var userEntity = (UserEntity)result.ContentResult; //conv om till en UserEntity
                if (PasswordHasher.ValidateSecurePassword(model.Password, userEntity.Password, userEntity.SecurityKey))
                    return ResponseFactory.Ok();

            }
            return ResponseFactory.Error("Fel email ELLER lösenoooooRD!");

        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }
}
