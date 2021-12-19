using Kryptos.Web.Client.Models.Luhn;

namespace Kryptos.Web.Client.Services.Luhn;

public interface ILuhnService
{
    bool Validate(string cardNumber);
    string GetCreditCardManufacturer(string cardNumber);
    string GetImagePath(string cardNumber);
    string GetMajorIndustryIdentifier(string cardNumber);
    CreditCard Generate(CardManufacturer manufacturer);
}