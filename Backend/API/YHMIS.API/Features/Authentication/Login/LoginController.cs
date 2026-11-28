using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using YHMIS.API.Features.Authentication;
using YHMIS.API.Features.Authentication.Responses;
using YHMIS.Identity.Application.Authentication.Login;

namespace YHMIS.API.Features.Authentication.Login;

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
        var result = await loginHandler.Handle(new(request.Email, request.Password), cancellationToken);
        return result.Match<IActionResult>(
            user => Ok(jwtTokenService.Generate(user.Value.UserId.ToString(), user.Value.Email, user.Value.Roles)),
            unauthorized => Unauthorized(unauthorized),
            error => BadRequest(error)
        );
    }
}