using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.SecretMessage;

public class ChatConcealMessage : ConcealMessage
{
    [Required(ErrorMessage = "Sender must have a name.")]
    public string Name { get; set; }
}