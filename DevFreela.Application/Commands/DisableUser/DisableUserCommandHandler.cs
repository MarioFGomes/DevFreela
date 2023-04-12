using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        public DisableUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task<Unit> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.GetByIdAsync(request.Id);
            User.DisableUser();
            await _userRepository.SaveChangeAsync();
            return Unit.Value;  
        }
    }
}
