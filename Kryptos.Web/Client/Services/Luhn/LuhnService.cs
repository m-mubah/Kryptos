using System.Text.RegularExpressions;
using Fare;
using Kryptos.Web.Client.Models.Luhn;

namespace Kryptos.Web.Client.Services.Luhn;

public class LuhnService : ILuhnService
{
    private const string AmericanExpressRegexPattern = @"^3[47][0-9]{13}$";
    private const string DinersClubRegexPattern = @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$";
    private const string DiscoverRegexPattern = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
    private const string JcbRegexPattern = @"^(?:2131|1800|35\d{3})\d{11}$";
    private const string MasterCardRegexPattern = @"^5[1-5][0-9]{14}";
    private const string VisaRegexPattern = @"^4[0-9]{12}(?:[0-9]{3})?$";

    private readonly Dictionary<int, string> _cardManufacturerPatterns = new()
    {
        {1, AmericanExpressRegexPattern},
        {2, DinersClubRegexPattern},
        {3, DiscoverRegexPattern},
        {4, JcbRegexPattern},
        {5, MasterCardRegexPattern},
        {6, VisaRegexPattern}
    };
    
    /// <summary>
    /// Returns validity of a credit card number by applying the Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>validity</returns>
    public bool Validate(string cardNumber)
    {
        int sumOfDigits = cardNumber.Where((c) => c >= '0' && c <= '9')
            .Reverse()
            .Select((c, i) => (c - 48) * (i % 2 == 0 ? 1 : 2))
            .Sum(i => i / 10 + i % 10);

        return sumOfDigits % 10 == 0;
    }   
    
    /// <summary>
    /// Gets the manufacturer of a credit card
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>name of manufacturer</returns>
    public string GetCreditCardManufacturer(string cardNumber)
    {
        var manufacturer = cardNumber switch
        {
            var masterCard when Regex.IsMatch(masterCard, MasterCardRegexPattern) => "Master Card",
            var visa when Regex.IsMatch(visa, VisaRegexPattern) => "Visa",
            var americanExpress when Regex.IsMatch(americanExpress, AmericanExpressRegexPattern) =>
                "American Express",
            var jcb when Regex.IsMatch(jcb, JcbRegexPattern) => "JCB",
            var dinersClub when Regex.IsMatch(dinersClub, DinersClubRegexPattern) => "Diner's Club",
            var discover when Regex.IsMatch(discover, DiscoverRegexPattern) => "Discover",
            _ => "unknown" //default, nothing matched
        };

        return manufacturer;
    }
    
    /// <summary>
    /// Maps a credit card to the image of it's manufacturer
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>image path</returns>
    public string GetImagePath(string cardNumber)
    {
        var manufacturer = cardNumber switch
        {
            var masterCard when Regex.IsMatch(masterCard, MasterCardRegexPattern)
                => "/img/cards/mastercard.png",
            var visa when Regex.IsMatch(visa, VisaRegexPattern)
                => "/img/cards/visa.png",
            var americanExpress when Regex.IsMatch(americanExpress, AmericanExpressRegexPattern) =>
                "/img/cards/american-express.png",
            var jcb when Regex.IsMatch(jcb, JcbRegexPattern) 
                => "/img/cards/jcb.png",
            var dinersClub when Regex.IsMatch(dinersClub, DinersClubRegexPattern) =>
                "/img/cards/diners-club.png",
            var discover when Regex.IsMatch(discover, DiscoverRegexPattern) =>
                "/img/cards/discover.png",
            _ => "/img/cards/unknown.png" //default, nothing matched
        };

        return manufacturer;
    }
    
    /// <summary>
    /// Gets the Major Industry Identifier of a credit card.
    /// </summary>
    /// <param name="cardNumber">credit card number</param>
    /// <returns>Industry identifier</returns>
    public string GetMajorIndustryIdentifier(string cardNumber)
    {
        switch (cardNumber[0])
        {
            case '0':
                return "ISO/TC 68";
            case '1':
            case '2':
                return "Airlines";
            case '3':
                return "Travel and entertainment and banking/financial";
            case '4':
            case '5':
                return "Banking and financial";
            case '6':
                return "Merchandising and banking/financial";
            case '7':
                return "Petroleum";
            case '8':
                return "Healthcare and telecommunications";
            case '9':
                return "National assignment";
            default:
                return "Unknown";
        }
    }

    /// <summary>
    /// Generates a new valid credit card number from a selected manufacturer.
    /// </summary>
    /// <param name="manufacturer">credit card manufacturer</param>
    /// <returns>Valid credit card</returns>
    public CreditCard Generate(CardManufacturer manufacturer)
    {
        var pattern = _cardManufacturerPatterns[(int) manufacturer];

        var xeger = new Xeger(pattern, new Random());

        CreditCard card = new CreditCard
        {
            Number = xeger.Generate()
        };
        
        // iterate until a valid number is generated
        while (!Validate(card.Number))
        {
            card.Number = xeger.Generate();
        }

        return new CreditCard
        {
            Number = card.Number,
            Issuer = GetCreditCardManufacturer(card.Number),
            ImagePath = GetImagePath(card.Number)
        };
    }
}