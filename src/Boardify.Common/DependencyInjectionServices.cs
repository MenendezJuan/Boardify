using Microsoft.Extensions.DependencyInjection;

namespace Boardify.Common
{
    public static class DependencyInjectionServices
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            return services;
        }
    }
}