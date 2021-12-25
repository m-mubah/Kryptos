using Microsoft.JSInterop;

namespace Kryptos.Web.Client.Services.Clipboard;

public class ClipboardService : IClipboardService
{
    private readonly IJSRuntime _jsRuntime;

    public ClipboardService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }
    
    /// <summary>
    /// Writes a string to the clipboard.
    /// </summary>
    /// <param name="text">string to write.</param>
    /// <returns></returns>
    public ValueTask WriteTextAsync(string text)
    {
        return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}