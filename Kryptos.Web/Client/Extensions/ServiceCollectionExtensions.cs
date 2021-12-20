using Kryptos.Web.Client.Services.Encryption;
using Kryptos.Web.Client.Services.HammingCode;
using Kryptos.Web.Client.Services.Luhn;

namespace Kryptos.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHammingCodeService, HammingCodeService>();
        services.AddScoped<ILuhnService, LuhnService>();
        services.AddScoped<IStreamCipherService, StreamCipherService>();
    }
}