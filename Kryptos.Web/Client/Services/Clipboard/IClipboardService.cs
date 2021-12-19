namespace Kryptos.Web.Client.Services.Clipboard;

public interface IClipboardService
{
    ValueTask WriteTextAsync(string text);
}