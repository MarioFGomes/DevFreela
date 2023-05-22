using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DisableUser;
using DevFreela.Application.Commands.EnableUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/Users")]
[Authorize]
public class UsersController : ControllerBase
{

    private readonly IMediator _mediator;


    public UsersController(IMediator mediator)
    {
        _mediator = mediator;

    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = Roles.both)]
    [AuthorizationFilter]
    public async Task<IActionResult> GetById(int id)
    {
        var command = new GetUserByIdQuery { Id = id };

        var User = await _mediator.Send(command);

        return Ok(User);
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserModel)
    {
        var Id = await _mediator.Send(createUserModel);

        return CreatedAtAction(nameof(GetById), new { id = Id }, createUserModel);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var LoginveewModel = await _mediator.Send(command);

        if (LoginveewModel is null) return BadRequest();

        return Ok(LoginveewModel);
    }

    [HttpPut("{id:int}/enable")]
    [Authorize(Roles = Roles.admin)]
    [AuthorizationFilter]
    public async Task<IActionResult> Enable(int id)
    {
        var command = new EnableUserCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id:int}/disable")]
    [Authorize(Roles = Roles.admin)]
    [AuthorizationFilter]
    public async Task<IActionResult> Disable(int id)
    {
        var command = new DisableUserCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}
