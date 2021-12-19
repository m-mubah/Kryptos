using Kryptos.Web.Client.Services.HammingCode;

namespace Kryptos.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHammingCodeService, HammingCodeService>();
    }
}