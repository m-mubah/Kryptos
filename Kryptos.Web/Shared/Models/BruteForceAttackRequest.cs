using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Shared.Models;

public class BruteForceAttackRequest
{
    [Required(ErrorMessage = "Hash is required.")]
    [RegularExpression("[0-9a-f]+", ErrorMessage = "Must be a valid hash.")]
    public string Hash { get; set; }

    [Required(ErrorMessage = "A hash algorithm must be selected.")]
    public HashAlgorithm HashAlgorithm { get; set; }
}