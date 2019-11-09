using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Pemissao { get; set; }

        public PermissaoNecessaria(string pemissao)
        {
            Pemissao = pemissao;
        }
    }

    public class PermissaoNecessarioHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisito)
        {
            if (context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisito.Pemissao)))
            {
                context.Succeed(requisito);
            }
            return Task.CompletedTask;
        }
    }
}