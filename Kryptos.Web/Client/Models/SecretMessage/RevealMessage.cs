using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.SecretMessage;

public class RevealMessage
{
    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; }
    
    [Required(ErrorMessage = "Key is required.")]
    public string Key { get; set; }
}