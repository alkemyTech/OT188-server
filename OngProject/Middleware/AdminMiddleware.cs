using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public class AdminMiddleware
    {
        private readonly RequestDelegate _next;
        public AdminMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            List<string> methods = new List<string>();
            methods.Add("post");
            methods.Add("delete");
            methods.Add("put");
            methods.Add("patch");
            //... otro métodos
            var method = context.Request.Method;
            List<string> paths = new List<string>();
            paths.Add("/api/activities");
            paths.Add("/api/categories");
            paths.Add("/api/contacts");
            paths.Add("/api/members");
            paths.Add("/api/news");
            paths.Add("/organization");
            paths.Add("/api/roles");
            paths.Add("/api/slides");
            paths.Add("/api/testimonials");
            paths.Add("/api/users");
            paths.Add("/comments");
            //... otras url
            string path = context.Request.Path;
            var id = (string)context.Request.RouteValues["id"];
            if (id is not null)
            {
                path = path.ToString().Replace("/" + id, "");
            }
            if (methods.Contains(method.ToLower()) && paths.Contains(path.ToLower()))
            {
                if (!context.User.IsInRole("Administrator"))
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
