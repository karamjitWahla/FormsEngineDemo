namespace BffApp
{
    public class UserTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public UserTokenMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var userToken = context.Request.Headers["X-On-Behalf-Of"].ToString();

            if (!string.IsNullOrWhiteSpace(userToken))
            {
                try
                {
                    var tenantId = _config["AzureAd:TenantId"]!;
                    var userClientId = _config["AzureAd:ClientIdUserToken"]!;

                    var principal = await AzureTokenValidator.ValidateIdTokenAsync(
                        userToken,
                        tenantId,
                        userClientId
                    );

                    context.Items["UserPrincipal"] = principal;
                }
                catch
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid user token");
                    return;
                }
            }

            await _next(context);
        }
    }
}
