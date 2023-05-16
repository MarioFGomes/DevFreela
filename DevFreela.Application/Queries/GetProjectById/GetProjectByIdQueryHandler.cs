using AutoMapper;
using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
    
        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetByIdAsync(request.Id);
                
                //await _devFreelaDbContext.Projects
                //.Include(p => p.Cliente)
                //.Include(p => p.Freelancer)
                //.SingleOrDefaultAsync(i => i.Id == request.Id);


            if (projects == null)
            {
                return null;
            }
            var projectDetailsViewModel = _mapper.Map<ProjectDetailsViewModel>(projects);

            return projectDetailsViewModel;
        }
    }
}
