using AgendamentoEventos.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace AgendamentoEventos.Extensions
{
    public static class TypeClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Role, user.TypeUser),
            };

            return result;
        }
    }
}
