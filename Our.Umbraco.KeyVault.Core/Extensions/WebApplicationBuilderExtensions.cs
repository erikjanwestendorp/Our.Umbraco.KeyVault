using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Our.Umbraco.KeyVault.Core.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureKeyVault(this WebApplicationBuilder builder)
    {
        var keyVaultSettings = builder.Configuration.GetConfiguredInstance<AppSettings.KeyVault>(PackageConstants.Configuration.SettingsSections.KeyVault);

        var credential = new ClientSecretCredential(keyVaultSettings.TenantId, keyVaultSettings.ClientId, keyVaultSettings.ClientSecret);
        var d = new DefaultAzureCredential();
        if (!string.IsNullOrWhiteSpace(keyVaultSettings.Endpoint) && Uri.TryCreate(keyVaultSettings.Endpoint, UriKind.Absolute, out var validUri))
        {
            builder.Configuration.AddAzureKeyVault(validUri, credential);
        }

        return builder;
    }
}