using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Shared.Models;

public class BruteForceAttackRequest
{
    [Required]
    [RegularExpression("[0-9a-f]+", ErrorMessage = "Must be a valid hash.")]
    public string Hash { get; set; }

    [Required]
    public HashAlgorithm HashAlgorithm { get; set; }
}