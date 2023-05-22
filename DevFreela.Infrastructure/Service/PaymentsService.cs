using DevFreela.Core.DTO;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Service
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IMessageBusService _messageBusService;
        private const string Quee_Name = "Payment";

        public PaymentsService(IMessageBusService messageBusService)
        {
            _messageBusService= messageBusService;
        }

        public void ProcessPayment(PaymentDTO pyment)
        {
            var paymentJson=JsonSerializer.Serialize(pyment);

            var paymentBytes= Encoding.UTF8.GetBytes(paymentJson);

            _messageBusService.Publish(Quee_Name, paymentBytes);

        }
    }
}
