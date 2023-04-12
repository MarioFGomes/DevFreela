using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DisableUser
{
    public class DisableUserCommand:IRequest<Unit>
    {
        public int Id { get; set; }

        public DisableUserCommand(int id)
        {
            Id = id;
        }
    }
}
