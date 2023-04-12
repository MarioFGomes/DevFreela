using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Core.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators;

public class CreateSkillCommandValidator: AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(u => u.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .WithMessage("A descrição da Tecnologia deve conter apenas 255 caracteres");
    }
}
