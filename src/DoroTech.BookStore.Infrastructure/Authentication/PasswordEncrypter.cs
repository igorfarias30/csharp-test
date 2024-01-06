using System.Security.Cryptography;
using DoroTech.BookStore.Application.Common;

namespace DoroTech.BookStore.Infrastructure.Authentication;

public class PasswordEncrypter : IPasswordEncrypter
{
    public PasswordHashed CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        return new PasswordHashed(hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != passwordHash[i]) return false;
        return true;
    }
}
