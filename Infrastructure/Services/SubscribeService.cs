using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;


namespace Infrastructure.Services;

public class SubscribeService(SubscribeRepository repository)
{
    private readonly SubscribeRepository _repository = repository;


    public async Task<ResponseResult> CreateOneSubscriberAsync(string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return ResponseFactory.Error("Email is required.");
            }

            var exists = await _repository.AlreadyExistsAsync(x => x.Email == email);

            if (exists == null)
            {
                var result = await _repository.CreateOneAsync(SubscribeFactory.Create(email));

                if (StatusCode.OK == result.StatusCode)
                {
                    return ResponseFactory.Ok();
                }

                return result;
            }
            else
            {
                return ResponseFactory.Error("Email already exists.");
            }
        }
        catch (Exception ex)
        {
            // Logga felmeddelandet eller hantera det på lämpligt sätt
            return ResponseFactory.Error($"An error occurred: {ex.Message}");
        }
    }
}



