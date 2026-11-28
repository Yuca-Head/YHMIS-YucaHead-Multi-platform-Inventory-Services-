using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YHMIS.API.Features.Authentication;
using YHMIS.Identity.Application;
using YHMIS.Identity.Application.Authentication.Login;
using YHMIS.Identity.Infrastructure.Persistence;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);





// Controllers
builder.Services.AddControllers();

// Modules

    //Identity
    builder.Services
        .AddIdentityApplication()
        .AddIdentityInfrastructure(builder.Configuration);


// JWT
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.Section));

builder.Services.AddScoped<JwtTokenService>();

// Authentication
// JWT configuration
var jwt = builder.Configuration
    .GetSection(JwtOptions.Section)
    .Get<JwtOptions>()!;

var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(jwt.SecretKey));

    
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,

            ValidateAudience = true,
            ValidAudience = jwt.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,

            ValidateLifetime = true,

            RoleClaimType = "role"
        };
    });
    
builder.Services.AddAuthentication();

// Authorization
builder.Services.AddAuthorization();


// OpenAPI
builder.Services.AddOpenApi(options =>
{
    // Declarar autenticación Bearer
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes =
            new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                }
            };

        return Task.CompletedTask;
    });

    // Indicar qué endpoints necesitan autenticación
    options.AddOperationTransformer((operation, context, _) =>
    {
        var requiresAuthorization = context.Description
            .ActionDescriptor.EndpointMetadata
            .OfType<IAuthorizeData>()
            .Any();

        if (requiresAuthorization)
        {
            operation.Security ??= [];

            operation.Security.Add(
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                        "Bearer", context.Document)] = []
                });
        }

        return Task.CompletedTask;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//Pipeline

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
