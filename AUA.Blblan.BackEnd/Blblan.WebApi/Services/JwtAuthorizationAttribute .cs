using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blblan.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public class JwtAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var jwtService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
        var senderId = jwtService.GetSenderIdFromToken(token);

        if (senderId == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Add the sender ID to the context so it can be accessed in the controller action
        context.HttpContext.Items["senderId"] = senderId;
    }
}
