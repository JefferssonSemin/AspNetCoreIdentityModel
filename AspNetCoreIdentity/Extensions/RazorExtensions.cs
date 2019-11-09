using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspNetCoreIdentity.Extensions
{
    public static class RazorExtensions
    {
        public static bool IfClaim(this RazorPage page, string claimName, string ClaimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, ClaimValue);
        }

        public static string IfClaimShow(this RazorPage page, string claimName, string ClaimValues)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, ClaimValues) ? "" : "disable";
        }

        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string ClaimValues)
        {
            return CustomAuthorization.ValidarClaimsUsuario(context, claimName, ClaimValues) ? page : null;
        }
    }
}