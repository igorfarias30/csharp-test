namespace DoroTech.BookStore.Application.Common;

public interface IPasswordEncrypter
{
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    PasswordHashed CreatePasswordHash(string password);
}

public record struct PasswordHashed(byte[] Hash, byte[] Salt);