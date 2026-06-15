using BffApp.Models;
using BffApp.Services;
using System.Security.Claims;

namespace BffApp
{
    public static class EndpointHandler
    {
        public static async Task<IResult> ProcessRequest(HttpContext context)
        {
            Console.WriteLine("----- Incoming Request -----");

            var appPrincipal = context.User;

            if (!appPrincipal.Identity?.IsAuthenticated ?? true)
                return Results.Unauthorized();

            var userPrincipal = context.Items["UserPrincipal"] as ClaimsPrincipal;

            if (userPrincipal is null)
                return Results.BadRequest("Missing user token");

            var user = new UserInfo
            {
                Email = userPrincipal.FindFirst("preferred_username")?.Value
                     ?? userPrincipal.FindFirst("email")?.Value,

                Id = userPrincipal.FindFirst("oid")?.Value
                  ?? userPrincipal.FindFirst("sub")?.Value
            };

            Console.WriteLine($"User: {user.Email} ({user.Id})");

            var body = await context.Request.ReadFromJsonAsync<RequestModel>();

            if (body is null)
                return Results.BadRequest("Missing request body");

            var response = ResponseBuilder.Build(body.Payload?.Action, user);

            return Results.Ok(new
            {
                success = true,
                data = response
            });
        }
    }

}