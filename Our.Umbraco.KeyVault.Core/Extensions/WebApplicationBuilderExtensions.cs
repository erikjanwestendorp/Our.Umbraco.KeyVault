using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Our.Umbraco.KeyVault.Core.Enumerations;

namespace Our.Umbraco.KeyVault.Core.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureKeyVault(this WebApplicationBuilder builder)
    {
        var keyVaultSettings = builder.Configuration.GetConfiguredInstance<AppSettings.KeyVault>(PackageConstants.Configuration.SettingsSections.KeyVault);

        if (!string.IsNullOrWhiteSpace(keyVaultSettings.Endpoint) && Uri.TryCreate(keyVaultSettings.Endpoint, UriKind.Absolute, out var validUri))
        {
            switch (keyVaultSettings.CredentialType)
            {
                case CredentialType.Default:
                    builder.AddAzureKeyVault(validUri, new ClientSecretCredential(keyVaultSettings.TenantId, keyVaultSettings.ClientId, keyVaultSettings.ClientSecret));
                    break;
                case CredentialType.ClientSecret:
                    builder.AddAzureKeyVault(validUri, new DefaultAzureCredential());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return builder;
    }

    private static void AddAzureKeyVault(this WebApplicationBuilder builder, Uri endPoint, ClientSecretCredential credential)
    {
        builder.Configuration.AddAzureKeyVault(endPoint, credential);
    }

    private static void AddAzureKeyVault(this WebApplicationBuilder builder, Uri endPoint, DefaultAzureCredential credential)
    {
        builder.Configuration.AddAzureKeyVault(endPoint, credential);
    }
}