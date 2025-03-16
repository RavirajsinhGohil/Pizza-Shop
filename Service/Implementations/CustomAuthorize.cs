using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;

namespace Service.Implementations;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    private readonly string _role;
    private string _permissionType;
    private string? _moduleName;

    public CustomAuthorize(string moduleName, string permissionType)
    {
        _moduleName = moduleName;
        _permissionType = permissionType;
    }


    public async Task OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }
                                                                                
        var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(userRole))
        {
            context.Result = new ForbidResult();
            return;
        }

        var permissionService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        if (permissionService == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var permissions = await permissionService.GetPermissionsByRoleAsync(userRole);

        // Ensure we are checking the permission for the correct module
        bool hasPermission = permissions.Any(p =>
            p.PermissionName == _moduleName && // ✅ Ensures correct module
            ((_permissionType == "CanView" && p.CanView) ||
            (_permissionType == "CanAddEdit" && p.CanAddEdit) ||
            (_permissionType == "CanDelete" && p.CanDelete)));

        Console.WriteLine($"User Role: {userRole}, Module: {_moduleName}, Permission Type: {_permissionType}, Has Permission: {hasPermission}");

        if (!hasPermission)
        {
            Console.WriteLine("Access Denied!");
            context.Result = new ForbidResult();
        }
        else
        {
            Console.WriteLine("Access Granted!");
        }
        throw new NotImplementedException();
    }

    // void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    // {
    //     throw new NotImplementedException();
    // }

    // public void OnAuthorization(AuthorizationFilterContext context)
    // {
    //     var user = context.HttpContext.User;

    //     if(!user.Identity.IsAuthenticated)
    //     {
    //         context.Result = new UnauthorizedResult();
    //         return;
    //     }

    //     var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    //     var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    //     if(string.IsNullOrEmpty(userRole) || userRole != _role)
    //     {
    //         context.Result = new ForbidResult();
    //         return;
    //     }
    // }

}
