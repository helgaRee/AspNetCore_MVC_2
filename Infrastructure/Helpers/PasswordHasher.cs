using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers;

public class PasswordHasher
{
    public static (string, string) GenerateSecurePassword(string password)
    {
        //kan aldrig un-hasha - enkelriktad (kan inte se vad för lösenord som knappas in)
        using var hmac = new HMACSHA512();
        //vi "saltar" lösenordet, sätter dit en spec nyckel
        var securityKey = hmac.Key;
        //vi tar använadarens lösenord och byter up det till en hashad array, byte-arrayen hashar vi sen
        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (Convert.ToBase64String(securityKey), Convert.ToBase64String(hashedPassword));
    }








    //Lösenordsvaliderare - anvädnds när en användare skapas och sparar ned till db
    public static bool ValidateSecurePassword(string password, string hash, string securityKey)
    {

        var security = Convert.FromBase64String(securityKey);
        var pwd = Convert.FromBase64String(hash);



        //konverterar min byteArray till en securitykey
        using var hmac = new HMACSHA512(security);
        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


        //om ett visst tecken inte stämmer överens mellan de två hasharna, blir det fel.
        for (var i = 0; i < hashedPassword.Length; i++)
        {
            if (hashedPassword[i] != hash[i])
                return false;
        }

        return true;
    }
}
