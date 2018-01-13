using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Puma.Prey.Skunk
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			var settings = new FriendlyUrlSettings
			{
				AutoRedirectMode = RedirectMode.Permanent
			};
			routes.EnableFriendlyUrls(settings);
        }
    }
}
