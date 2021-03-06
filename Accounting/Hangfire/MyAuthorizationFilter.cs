﻿using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Accounting.Hangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var isAuthorized = httpContext.User.Identity.IsAuthenticated;

            
            if(!isAuthorized && !httpContext.User.IsInRole("Admin"))
            {
                httpContext.Response.Redirect("/Home/Index");
            }
            return true;
        }
    }
}
