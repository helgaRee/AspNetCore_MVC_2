using Infrastructure.Entities;
using Infrastructure.Model;

namespace Infrastructure.Factories;

//för att skapa olika former av addresses
public class AddressFactory
{
    public static AddressEntity Create()
    {
        try
        {
            return new AddressEntity();
        } 
        catch { }
        return null!;
    }


    public static AddressEntity Create(string addressLine1, string postalCode, string city)
    {
        try
        {
            return new AddressEntity
            {
                AddressLine1 = addressLine1,
                PostalCode = postalCode,
                City = city
            };
        }
        catch { }
        return null!;
    }

    public static AddressModel Create(AddressEntity entity)
    {
        try
        {
            return new AddressModel
            {
                AddressLine1 = entity.AddressLine1,
                PostalCode = entity.PostalCode,
                City = entity.City
            };
        }
        catch { }
        return null!;
    }
}
