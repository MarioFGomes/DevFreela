using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComent;

public class CreateComentCommand:IRequest<Unit>
{
    public string Content { get; set; }
    public int IdUser { get; set; }
    public int IdProject { get; set; }

}
