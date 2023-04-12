﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services;

public interface IAuthService
{
    string GenereteJwtToken(string email, string role,bool Active);

    string ComputeSha256Hash(string password);
}
