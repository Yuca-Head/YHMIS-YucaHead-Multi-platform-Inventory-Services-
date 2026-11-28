using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using YHMIS.Identity.Application.Authentication.Commands;
using YHMIS.Identity.Application.Authentication.Users;
using YHMIS.Identity.Application.Authentication.Users.CreateUser;
using YHMIS.Identity.Application.Authentication.Users.GetUser;

namespace YHMIS.API.Features.Authentication.Users;

[ApiController]
[Route("api/auth/users")]
[Authorize(Roles = "Admin")]
public sealed class UserController(CreateUserHandler createUserHandler, GetUserHandler getUserHandler) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        AuthCommand request)
    {
        var result = await createUserHandler.Handle(request);

        return result.Match<IActionResult>(
            _ => NoContent(),
            exists => Conflict(exists),
            error => BadRequest(error)
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user =  await getUserHandler.GetUserAsync(id);

        return user is null ? NotFound(id) :
        Ok(user);
    }
}