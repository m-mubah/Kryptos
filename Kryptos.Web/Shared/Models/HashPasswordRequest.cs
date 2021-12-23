using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Shared.Models;

public class HashPasswordRequest
{
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(1, ErrorMessage = "Password cannot be empty.")]
    [MaxLength(6, ErrorMessage = "Password must not be greater than 6 characters.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "A hash algorithm must be selected.")]
    public HashAlgorithm HashAlgorithm { get; set; }
}