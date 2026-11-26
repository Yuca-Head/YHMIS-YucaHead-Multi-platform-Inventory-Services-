using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using YHMIS.Identity.Application.Authentication.Commands;
using YHMIS.Identity.Application.Authentication.Users;
using YHMIS.Identity.Application.Authentication.Users.CreateUser;

namespace YHMIS.API.Features.Authentication.Users;

[ApiController]
[Route("api/auth/users")]
[Authorize(Roles = "Admin")]
public sealed class UserController(CreateUserHandler userHandler) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        AuthCommand request)
    {
        var result = await userHandler.Handle(request);

        return result.Match<IActionResult>(
            _ => NoContent(),
            exists => Conflict(exists),
            error => BadRequest(error)
        );
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {

        return Ok();
    }
}