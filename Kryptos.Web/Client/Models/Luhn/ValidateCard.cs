using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.Luhn;

public class ValidateCard
{
    [Required(ErrorMessage = "Card number is required.")]
    [MinLength(12, ErrorMessage = "Card number must be atleast 12 digits.")]
    [MaxLength(17, ErrorMessage = "Card number must not be greater than 17 digits.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Card number must be digits only.")]
    public string CardNumber { get; set; }
}