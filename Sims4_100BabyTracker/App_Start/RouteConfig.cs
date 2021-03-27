using System.Web.Mvc;
using System.Web.Routing;

namespace Sims4_100BabyTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Summary", id = UrlParameter.Optional }
            );
        }
    }
}
