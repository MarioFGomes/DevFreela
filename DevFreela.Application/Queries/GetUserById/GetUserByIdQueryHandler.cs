using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository=userRepository;
            _mapper=mapper;
        }
        public async Task<UserDetailViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.GetByIdAsync(request.Id);
            if (User == null)
            {
                return null;
            }

            var user = _mapper.Map<UserDetailViewModel>(User);
            return user;
        }
    }
}
