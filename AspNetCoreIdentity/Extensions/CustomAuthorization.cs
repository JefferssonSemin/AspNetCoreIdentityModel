using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreIdentity.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string ClaimName, string ClaimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == ClaimName && c.Value.Contains(ClaimValue));
        }

        public class ClaimsAuthorizeAttribute : TypeFilterAttribute
        {
            public ClaimsAuthorizeAttribute(string ClaimName, string ClaimValue) : base(typeof(RequisitoClaimFiter))
            {
                Arguments = new object[] { new Claim(ClaimName, ClaimValue) };
            }
        }

        public class RequisitoClaimFiter : IAuthorizationFilter
        {
            private readonly Claim _claim;

            public RequisitoClaimFiter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }
}