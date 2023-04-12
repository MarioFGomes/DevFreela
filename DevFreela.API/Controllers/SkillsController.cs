using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/skills")]
[Authorize]
[AuthorizationFilter]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = Roles.both)]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllSkillsQuery();

        var skills = await _mediator.Send(query);

        return Ok(skills);
    }

    [HttpPost]
    [Authorize(Roles = Roles.dev)]
    public async Task<IActionResult> Post([FromBody]  CreateSkillCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }
}
