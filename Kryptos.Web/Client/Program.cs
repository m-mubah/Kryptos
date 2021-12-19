using Kryptos.Web.Client;
using Kryptos.Web.Client.Extensions;
using Kryptos.Web.Client.Services.Clipboard;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IClipboardService, ClipboardService>();

await builder.Build().RunAsync();
