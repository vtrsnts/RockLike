using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RockLike.Extensions
{
    public static class UserExtension
    {
        public static string GetUserID(this ClaimsPrincipal User)
        {
            var existente = User.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            return existente != null && !string.IsNullOrEmpty(existente.Value) ? existente.Value : string.Empty;
        }
    }
}
