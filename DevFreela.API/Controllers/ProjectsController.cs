using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComent;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProject;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
[Authorize]
[AuthorizationFilter]
public class ProjectsController:ControllerBase
{
 
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
   
        _mediator = mediator;
    }


    [HttpGet()]
    [Authorize(Roles=Roles.both)]
    public async Task<IActionResult> Get()
    {
        var command = new GetAllProjectQuery();

        var Project=await _mediator.Send(command);


        return Ok(Project);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = Roles.both)]
    public async Task<IActionResult> GetById(int id)
    {

        var command=new GetProjectByIdQuery() { Id = id };

        var project = await _mediator.Send(command);

        if(project==null) return NotFound();

        return Ok(project);

    }

    [HttpPost]
    [Authorize(Roles = Roles.client)]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var Id= await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id =Id }, command);

    }

    [HttpPut]
    [Authorize(Roles = Roles.client)]
    public async Task<IActionResult> Put( string id , [FromBody] UpdateProjectCommand command)
    {

        await _mediator.Send(command);

        return NoContent();
    }


    [HttpPost("{id:int}/coments")]
    [Authorize(Roles = Roles.both)]
    public async Task<IActionResult> PostComent(int id , [FromBody] CreateComentCommand command)
    {

        await  _mediator.Send(command);

        return NoContent();
    }


    [HttpPut("{id:int}/start")]
    [Authorize(Roles = Roles.dev)]
    public async Task<IActionResult> Start(int id)
    {
        var command= new StartProjectCommand() { Id=id};
        

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    [Authorize(Roles = Roles.dev)]
    public async Task<IActionResult> Finish(int id)
    {
        var command= new FinishProjectCommand() { Id=id};

        await _mediator.Send(command);    

        return NoContent();
    }


    [HttpDelete]
    [Authorize(Roles = Roles.client)]
    public async Task<IActionResult> Delete(int Id)
    {
        var command= new DeleteProjectCommand() { Id = Id};

        await _mediator.Send(command);    

        return NoContent();
    }



}
