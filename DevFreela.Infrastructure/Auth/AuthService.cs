using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ComputeSha256Hash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())//Inicializando o método do sha256 Create
        {
            //ComputeHash - retorna byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));


            //Converte byte array para string
            StringBuilder builder = new StringBuilder();//concatenação de string


            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));// 2x faz com que sseja convertido em representação hexadecimal
            }


            return builder.ToString();
        }
    }

    public string GenereteJwtToken(string email, string role, bool Active)
    {
        var key = _configuration["jwt:key"];
        var issuer= _configuration["jwt:Issuer"];
        var audience= _configuration["jwt:Audience"];

        var secretyKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials= new SigningCredentials(secretyKey, SecurityAlgorithms.HmacSha256);

        var claim = new List<Claim> {
           new  Claim("Useremail",email),
           new Claim("status",Active.ToString()),
           new Claim(ClaimTypes.Role,role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials,
            claims: claim
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken= tokenHandler.WriteToken(token);

        return stringToken;
    }
}
