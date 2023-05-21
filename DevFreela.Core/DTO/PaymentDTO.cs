using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTO
{
    public class PaymentDTO
    {
        public int IdProject { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiresAt { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }

        public PaymentDTO(int id, string creditcardnumber, string cvv,string expirestat, string fullname, decimal amount)
        {
            IdProject = id;
            CreditCardNumber = creditcardnumber;
            Cvv = cvv;
            ExpiresAt = expirestat;
            FullName = fullname;
            Amount = amount;
        }
    }
}
