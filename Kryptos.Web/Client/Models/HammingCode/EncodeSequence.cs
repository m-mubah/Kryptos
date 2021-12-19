using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.HammingCode;

public class EncodeSequence
{
    [Required(ErrorMessage = "A sequence is required.")]
    [MinLength(4, ErrorMessage = "Sequence must be 4 characters.")]
    [MaxLength(4, ErrorMessage = "Sequence must be 4 characters.")]
    [RegularExpression("[01]+", ErrorMessage = "Sequence must be 1s and 0s only.")]
    public string Value { get; set; }
}