using System.Security.Cryptography;
using System.Text;

namespace FastFeet.Infrastructure.ExternalService.Cryptography;

internal sealed class CryptographyService : ICryptographyService
{
    public string ComputeSha256Hash(string password)
    {
        using var sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        var builder = new StringBuilder();

        foreach (var t in bytes)
        {
            builder.Append(t.ToString(format: "x2"));
        }

        return builder.ToString();
    }
}