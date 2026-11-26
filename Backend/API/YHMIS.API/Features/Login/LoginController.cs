using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using YHMIS.API.Features.Authentication;
using YHMIS.Identity.Application.Authentication.Login;

namespace YHMIS.API.Features.Login;

[ApiController]
[Route("api/auth")]
public sealed class LoginController(LoginHandler loginHandler,
JwtTokenService jwtTokenService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await loginHandler.Handle(
            new(request.Email, request.Password),
            cancellationToken); 
        
        
        //Success (Returns UserDto)
        if(result.IsT0)
        {
            var user = result.AsT0.Value;

            var token = jwtTokenService.Generate(
            user.Id.ToString(),
            user.Email);

            return Ok(new LoginResponse(
                token.AccessToken,
                token.ExpiresAt,
                user));
        }
        //Unauthorized
        if(result.IsT1)
            return Unauthorized();
        
        //Error
        return BadRequest(result.AsT2);
    }
}