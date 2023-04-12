using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("E-mail não é valido");

        RuleFor(p => p.Password)
            .Must(ValidaPassword)
            .WithMessage("A senha deve conter pelomenos 8 caracteres, uma letra maiuscula uma menuscula e um caracter especial ");

        RuleFor(p => p.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("O nome não pode ser vazio");
    }

    public bool ValidaPassword(string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
        return regex.IsMatch(password);
    }
}
