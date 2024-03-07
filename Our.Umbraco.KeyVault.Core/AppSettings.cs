using Our.Umbraco.KeyVault.Core.Enumerations;

namespace Our.Umbraco.KeyVault.Core;

public class AppSettings
{
    public class KeyVault
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public CredentialType CredentialType { get; set; } = CredentialType.Default;
        public string Endpoint { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
    }
}