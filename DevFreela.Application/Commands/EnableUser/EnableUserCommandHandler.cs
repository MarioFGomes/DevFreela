using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.EnableUser
{
    public class EnableUserCommandHandler : IRequestHandler<EnableUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        public EnableUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.GetByIdAsync(request.Id);
            User.EnableUser();
           await _userRepository.SaveChangeAsync();
            return Unit.Value;  
        }
    }
}
