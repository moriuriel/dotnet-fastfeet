namespace FastFeet.Infrastructure.ExternalService.Cryptography;

public interface ICryptographyService
{
    string ComputeSha256Hash(string password);
}