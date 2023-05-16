using DevFreela.Core.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure.Auth;

public class AuthorizationFilter:Attribute,IAsyncAuthorizationFilter
{
    
    public AuthorizationFilter(){}

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var json = handler.ReadJwtToken(token);
            var email=json.Claims.First(claim => claim.Type == "Useremail").Value;
            var userService = context.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await userService.GetByEmail(email);

            if (user.Active != true)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
        catch
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
