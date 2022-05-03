using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public class OwnershipMiddleware
    {
        private readonly RequestDelegate _next;
        public OwnershipMiddleware(RequestDelegate next)
        {
            _next = next;   
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethod.Put.Method || context.Request.Method == HttpMethod.Delete.Method)
            {
                var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var paramId = (string)context.Request.RouteValues["id"];
                if (paramId != null && paramId != "")
                {
                    var excludePaths = new List<string>() { "/users" };
                    var currentPath = context.Request.Path.ToString().ToLower();
                    foreach (var path in excludePaths)
                    {
                        if (currentPath.Contains(path))
                        {
                            if (Int32.Parse(claimId) != Int32.Parse(paramId) && !role.Value.Equals("1"))
                            {
                                context.Response.StatusCode = 403;
                                return;
                            }
                        }
                    }
                }
            }
            await _next.Invoke(context);
        }
    }
}
