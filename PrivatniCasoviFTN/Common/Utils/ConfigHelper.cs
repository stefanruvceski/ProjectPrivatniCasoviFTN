using System.Configuration;
using Common.Models;

namespace Common.Utils
{
    public static class ConfigHelper
    {
        /// <summary>
        /// The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        /// </summary>
        public static string AADInstance { get; } = Util.EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);

        /// <summary>
        /// The AppKey is a credential used to authenticate the application to Azure AD.  Azure AD supports password and certificate credentials.
        /// </summary>
        public static string AppKey { get; } = ConfigurationManager.AppSettings["ida:ClientSecret"];

        /// <summary>
        /// The Client ID is used by the application to uniquely identify itself to Azure AD.
        /// </summary>
        public static string ClientId { get; } = ConfigurationManager.AppSettings["ida:ClientId"];

        /// <summary>
        /// The Post Logout Redirect Uri is the URL where the user will be redirected after they sign out.
        /// </summary>
        public static string PostLogoutRedirectUri { get; } = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

        /// <summary>
        /// The TenantId is the DirectoryId of the Azure AD tenant being used in the sample
        /// </summary>
        public static string TenantId { get; } = ConfigurationManager.AppSettings["ida:TenantId"];

        /// <summary>
        /// The Authority is the sign-in URL of the tenant.
        /// </summary>
        public static string Authority = AADInstance + TenantId;

        /// <summary>
        /// The GraphResourceId the resource ID of the Microsoft Graph API.  We'll need this to request a token to call the Microsoft Graph API.
        /// </summary>
        public const string GraphResourceId = "https://graph.microsoft.com";

        public static string MSGraphBaseUrl = $"{GraphResourceId}/v1.0";

        /// <summary>
        /// The AADGraphResourceId the resource ID of the AAD Graph API.  We'll need this to request a token to call the Azure AD Graph API.
        /// </summary>
        public static string AADGraphResourceId = "https://graph.windows.net";
    }
}
