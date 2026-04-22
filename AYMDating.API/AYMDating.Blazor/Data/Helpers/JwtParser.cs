using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AYMDating.Blazor.Data.Helpers
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;

            return jsonToken?.Claims ?? Enumerable.Empty<Claim>();
        }
    }
}
