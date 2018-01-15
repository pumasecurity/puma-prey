using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using Microsoft.EntityFrameworkCore;

using Puma.Prey.Rabbit.EF;

namespace Skunk
{
	public class Global : HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			// Code that runs on application startup
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var context = new RabbitDBContext();
			if (!context.AllMigrationsApplied())
				context.Database.Migrate();
			context.EnsureSeedData();
		}
	}
}