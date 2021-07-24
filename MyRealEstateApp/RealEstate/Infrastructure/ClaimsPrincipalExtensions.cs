using System;
using System.Security.Claims;
using static RealEstate.Common.WebConstants;

namespace RealEstate.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLoggedInUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

        public static bool IsBroker(this ClaimsPrincipal user)
            => user.IsInRole(BrokerRoleName);

        public static bool IsClient(this ClaimsPrincipal user)
            => user.IsInRole(ClientRoleName);
    }
}