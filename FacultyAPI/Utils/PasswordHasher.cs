namespace FacultyApp.Utils;

using System.Text;
using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int keySize = 64;
    private const int iterations = 350000;

    public static string HashPassword(string password)
    {

        byte[] salt = new byte[0];

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            keySize);

        return Convert.ToHexString(hash);
    }
}