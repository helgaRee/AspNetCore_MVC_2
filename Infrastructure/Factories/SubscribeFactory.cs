using Infrastructure.Entities;

namespace Infrastructure.Factories;

public class SubscribeFactory
{
    public static SubscribeEntity Create()
    {
        try
        {
            return new SubscribeEntity();
        }
        catch { }

        return null!;
    }


    public static SubscribeEntity Create(string email)
    {
        try
        {
            return new SubscribeEntity() { Email = email };
        }
        catch { }

        return null!;
    }
}
