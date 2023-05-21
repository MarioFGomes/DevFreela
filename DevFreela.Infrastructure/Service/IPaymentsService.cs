﻿using DevFreela.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Service
{
    public interface IPaymentsService
    {
        Task<bool> ProcessPayment(PaymentDTO pyment);
    }
}
