using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Unit>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        public CreateSkillCommandHandler(ISkillRepository skillRepository,IMapper mapper)
        {
            _skillRepository= skillRepository;
            _mapper= mapper;
        }
        public async Task<Unit> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Skill>(request); 

            await _skillRepository.AddAsync(skill);

            return Unit.Value;
        }
    }
}
