using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AddressService(AddressRepository repository)
{
    //hämta in repo
    private readonly AddressRepository _repository = repository;
    //anv min model för att kunna returnera olika slutmeddelanden och koder



    public async Task<ResponseResult> GetOrCreateAddressAsync(string addressLine1, string postalCode, string city)
    {
        try
        {
            //sökning
            var result = await GetAddressAsync(addressLine1, postalCode, city);
            if (result.StatusCode == StatusCode.NOT_FOUND)
            {
                result = await CreateAddressAsync(addressLine1, postalCode, city);

            }
            return result; 
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }


    public async Task<ResponseResult> CreateAddressAsync(string addressLine1, string postalCode, string city)
    {
        try
        {
            //kontrollera om addressen redan finns
            var exists = await _repository.AlreadyExistsAsync(x => x.AddressLine1 == addressLine1 && x.PostalCode == postalCode && x.City == city);
            
            if (exists == null)
            {
                var result = await _repository.CreateOneAsync(AddressFactory.Create(addressLine1, postalCode, city));
                //om  resultatet ger en ok statuskod, återge en responsefacotry OK
                if (result.StatusCode == StatusCode.OK)            
                    return ResponseFactory.Ok(AddressFactory.Create((AddressEntity)result.ContentResult!));
                
                return result; 
            }
            return exists;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }






    public async Task<ResponseResult> GetAddressAsync(string addressLine1, string postalCode, string city)
    {
        try
        {
            //sökning
            var result = await _repository.GetOneAsync(x => x.AddressLine1 == addressLine1 && x.PostalCode == postalCode && x.City == city);
            if (result != null)          
                return result;

            return ResponseFactory.NotFound();
            
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }

}
