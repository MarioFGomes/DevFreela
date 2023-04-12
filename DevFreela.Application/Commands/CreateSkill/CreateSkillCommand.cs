using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateSkill;

public class CreateSkillCommand:IRequest<Unit>
{
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public CreateSkillCommand(string description)
    {
        Description = description;
        CreatedAt=DateTime.Now;
    }
}
