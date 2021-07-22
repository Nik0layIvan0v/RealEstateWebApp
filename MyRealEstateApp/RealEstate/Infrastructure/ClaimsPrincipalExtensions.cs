using System;
using System.Security.Claims;

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
    }
}