using DevFreela.Core.DTO;
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
        private readonly string _paymentBaseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentBaseUrl = configuration.GetSection("Services:payments").Value;
        }

        public async Task<bool> ProcessPayment(PaymentDTO pyment)
        {
            var url = $"{_paymentBaseUrl}/api/payments";
            var paymentJson=JsonSerializer.Serialize(pyment);
            var paymentContent=new StringContent(
                paymentJson,
                Encoding.UTF8,
                "application/json"
                );
            var httpclient= _httpClientFactory.CreateClient("Payments");

            var response= await httpclient.PostAsync(url, paymentContent);

            return response.IsSuccessStatusCode;
        }
    }
}
