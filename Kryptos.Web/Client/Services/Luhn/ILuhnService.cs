using Kryptos.Web.Client.Models.Luhn;

namespace Kryptos.Web.Client.Services.Luhn;

public interface ILuhnService
{
    /// <summary>
    /// Returns validity of a credit card number by applying the Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>validity</returns>
    bool Validate(string cardNumber);
    
    /// <summary>
    /// Gets the manufacturer of a credit card
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>name of manufacturer</returns>
    string GetCreditCardManufacturer(string cardNumber);

    /// <summary>
    /// Gets the Major Industry Identifier of a credit card.
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>Industry identifier</returns>
    string GetMajorIndustryIdentifier(string cardNumber);
    
    /// <summary>
    /// Generates a new valid credit card number from a selected manufacturer.
    /// </summary>
    /// <param name="manufacturer">credit card manufacturer</param>
    /// <returns>Valid credit card</returns>
    CreditCard Generate(CardManufacturer manufacturer);
}