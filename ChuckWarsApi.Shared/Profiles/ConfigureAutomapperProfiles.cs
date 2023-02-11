using Microsoft.Extensions.DependencyInjection;

namespace ChuckWarsWebAssembly.Shared.Profiles
{
    public static class ConfigureAutomapperProfiles
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
