using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task<UserDetailViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.GetByIdAsync(request.Id);
            if (User == null)
            {
                return null;
            }

            var user = new UserDetailViewModel(User.FullName, User.Email, User.BirthDate, User.Active,User.Password,User.Role);
            return user;
        }
    }
}
