using Microsoft.Extensions.Configuration;

namespace Our.Umbraco.KeyVault.Core.Extensions;

public static class ConfigurationExtensions
{
    public static T GetConfiguredInstance<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var instance = new T();

        var section = configuration.GetSection(sectionName);
        section.Bind(instance);

        return instance;
    }
}