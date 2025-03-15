using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Implementations;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public CustomAuthorize(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if(!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if(string.IsNullOrEmpty(userRole) || userRole != _role)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
