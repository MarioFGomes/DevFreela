using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateComentCommandHandler(IProjectRepository projectRepository, IMapper mapper) {

            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateComentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<ProjectComment>(request);

            await _projectRepository.CreateComentAsync(comment);

            return Unit.Value;
        }
    }
}
