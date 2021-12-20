using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.SecretMessage;

public class ConcealMessage
{
    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; }
    
    [Required(ErrorMessage = "Key is required.")]
    public string Key { get; set; }

    [Required(ErrorMessage = "Secret is required.")]
    public string Secret { get; set; }
}