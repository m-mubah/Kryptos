using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Kryptos.Web.Shared.Models;

public class DictionaryAttackRequest
{
    [Required]
    [RegularExpression("[0-9a-f]+", ErrorMessage = "Must be a valid hash.")]
    public string Hash { get; set; }

    [Required]
    public HashAlgorithm HashAlgorithm { get; set; }
    
    // public IFormFile? FormFile { get; set; }
}