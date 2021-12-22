namespace Kryptos.Web.Client.Models.SecretMessage;

public class ChatMessage
{
    public string Username { get; set; }
    public string Message { get; set; }
    public DateTime SentTime { get; set; }
    public bool SentMessage { get; set; }
}