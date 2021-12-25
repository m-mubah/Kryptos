namespace Kryptos.Web.Client.Services.Clipboard;

public interface IClipboardService
{
    /// <summary>
    /// Writes a string to the clipboard.
    /// </summary>
    /// <param name="text">string to write.</param>
    /// <returns></returns>
    ValueTask WriteTextAsync(string text);
}