using BffApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var tenantId = config["AzureAd:TenantId"]!;
var appClientId = config["AzureAd:ClientIdAppToken"]!;

// App Token Middleware
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority =
            $"https://login.microsoftonline.com/{tenantId}/v2.0";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidIssuers = new[]
            {
                $"https://login.microsoftonline.com/{tenantId}/v2.0",
                $"https://sts.windows.net/{tenantId}/"
            },

            ValidateAudience = true,
            ValidAudience = $"api://{appClientId}",

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };

        // Helpful diagnostics
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("AUTH FAILED:");
                Console.WriteLine(context.Exception.Message);

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// ============================
// User Token Middleware
// ============================
app.UseMiddleware<UserTokenMiddleware>();

// ============================
// API Endpoint
// ============================
app.MapPost("/api/process", EndpointHandler.ProcessRequest)
   .RequireAuthorization();

app.Run();