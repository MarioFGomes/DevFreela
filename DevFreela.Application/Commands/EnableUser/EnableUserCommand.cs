using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.EnableUser
{
    public class EnableUserCommand:IRequest<Unit>
    {
        public int Id { get; set; }

        public EnableUserCommand(int id)
        {
            Id = id;
        }
    }
}
