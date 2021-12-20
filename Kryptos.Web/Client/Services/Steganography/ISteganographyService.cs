namespace Kryptos.Web.Client.Services.Steganography;

public interface ISteganographyService
{
    string Conceal(string message, string secret);
    string Reveal(string message);
}