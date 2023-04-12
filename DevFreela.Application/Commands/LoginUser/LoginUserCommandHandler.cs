using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginViewModel>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }
    public async Task<LoginViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Transformar a senha informada pelo usuario em uma hash
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        // Buscar usuario no banco de dados

        var user = await _userRepository.GetByEmailPasswordAsync(request.Email, passwordHash);

        if (user == null) { return null; }

        // Gerar Token de Acesso 

        var token = _authService.GenereteJwtToken(user.Email, user.Role,user.Active);

        var Loginveewmodel = new LoginViewModel(user.Email, token);

        return Loginveewmodel;
    }
}
