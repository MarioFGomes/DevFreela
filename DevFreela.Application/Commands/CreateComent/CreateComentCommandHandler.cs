using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComent
{
    public class CreateComentCommandHandler : IRequestHandler<CreateComentCommand,Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateComentCommandHandler(IProjectRepository projectRepository) {

            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(CreateComentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectRepository.CreateComentAsync(comment);

            return Unit.Value;
        }
    }
}
