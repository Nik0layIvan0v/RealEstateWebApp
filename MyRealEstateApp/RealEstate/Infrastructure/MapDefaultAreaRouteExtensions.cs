using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace RealEstate.Infrastructure
{
    public static class MapDefaultAreaRouteExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute
            (
                name: "Areas",
                pattern: "{area:exists}/{controller=home}/{action=index}/{id?}"
            );
        }
    }
}
