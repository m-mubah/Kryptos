using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.HammingCode;

public class DecodeSequence
{
    [Required(ErrorMessage = "A sequence is required.")]
    [MinLength(7, ErrorMessage = "Sequence must be 7 characters.")]
    [MaxLength(7, ErrorMessage = "Sequence must be 7 characters.")]
    [RegularExpression("[01]+", ErrorMessage = "Sequence must be 1s and 0s only.")]
    public string Value { get; set; }

    [Required(ErrorMessage = "A parity is required for decoding.")]
    public Parity Parity { get; set; }
}