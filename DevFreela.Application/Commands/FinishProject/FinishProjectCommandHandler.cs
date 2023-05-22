using DevFreela.Core.DTO;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentsService _paymentsService;
        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentsService paymentsService)
        {
           _projectRepository=projectRepository;
           _paymentsService=paymentsService;
        }
        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
       

            var paymentDTO = new PaymentDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, request.Amount);

            _paymentsService.ProcessPayment(paymentDTO);

             project.Payment();
           
           await _projectRepository.UpdateChangesAsync(project);

            return Unit.Value;
        }
    }
}
