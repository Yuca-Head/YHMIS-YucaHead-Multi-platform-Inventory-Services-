using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YHMIS.API.Features.Authentication;
using YHMIS.Identity.Application;
using YHMIS.Identity.Application.Authentication.Login;
using YHMIS.Identity.Infrastructure.Persistence;

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
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Pipeline

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();