using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand,Unit>
{
    private readonly DevFreelaDbContext _devFreelaDbContext;

    public UpdateProjectCommandHandler(DevFreelaDbContext devFreelaDbContext)
    {
        _devFreelaDbContext= devFreelaDbContext;

    }
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _devFreelaDbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

        project.Update(request.Title, request.Description, request.TotalCost);
        
       await  _devFreelaDbContext.SaveChangesAsync();

        return Unit.Value;
    }

  
}
