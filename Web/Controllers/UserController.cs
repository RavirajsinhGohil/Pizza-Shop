using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.ViewModel;
using Service.Implementations;
using Service.Interfaces;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public string? UserEmail { get; private set; }

    public UserController(ILogger<LoginController> logger, IAuthService authService, IUserService userService)
    {
        _logger = logger;
        _authService = authService;
        _userService = userService;
    }

    [CustomAuthorize("Admin")]
    [HttpGet]

    public IActionResult UserList(string searchTerm = "", int page = 1, int pageSize = 5, string sortBy = "Name", string sortOrder = "asc")
    {
        var paginatedUsers = _userService.GetUsers(searchTerm, page, pageSize, sortBy, sortOrder);

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_UserList", paginatedUsers);
        }

        return View(paginatedUsers);
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(AddUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.RoleId = 1;
            Task<bool> isAdded = _userService.AddUser(model);
            if (isAdded != null)
            {
                string filePath = @"C:\Users\pct216\Downloads\Pizza Shop\Main Project\Pizza Shop\Web\EmailTemplate\AddUserEmailTemplate.html";
                string emailBody = System.IO.File.ReadAllText(filePath);

                emailBody = emailBody.Replace("{abc123}", model.Username);
                emailBody = emailBody.Replace("{abc@123}", model.Password);

                string subject = "User Details";
                _authService.SendEmailAsync(model.Email, subject, emailBody);
                TempData["success"] = "New User Created";
                return RedirectToAction("UserList", "User");
            }
        }

        if (model.Rolename == "Select Role" || model.Country == "Select Country" || model.State == "Select State" || model.City == "Select City")
        {
            if (model.Rolename == "Select Role")
            {
                ModelState.AddModelError("Rolename", "Please Select Role");
            }
            if (model.Country == "Select Country")
            {
                ModelState.AddModelError("Country", "Please Select Country");
            }
            if (model.State == "Select State")
            {
                ModelState.AddModelError("States", "Please Select States");
            }
            if (model.City == "Select City")
            {
                ModelState.AddModelError("City", "Please Select City");
            }
            return View();
        }
        return View();
    }

    [HttpGet]
    public IActionResult EditUser(int Userid)
    {
        var model = _userService.GetUserForEdit(Userid);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult EditUser(EditUserViewModel model, int Userid)
    {
        if(!ModelState.IsValid)
        {
            return NotFound();
        }
        bool isUpdated = _userService.EditUser(Userid, model);
        if (!isUpdated)
        {
            return NotFound();
        }
        TempData["success"] = "User Data Updated";

        return RedirectToAction("UserList", "User");
    }

    public IActionResult DeleteUser(int Userid)
    {
        bool isDeleted = _userService.DeleteUser(Userid);
        if (!isDeleted)
        {
            return NotFound();
        }

        return RedirectToAction("UserList", "User");
    }

    [HttpGet]
    public IActionResult Roles()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Permissions(string role)
    {
        ViewBag.SelectedRole = role;
        var permissions = await _userService.GetPermissionsByRoleAsync(role);
        return View(permissions);
    }

    [HttpPost]
    public async Task<IActionResult> Permissions([FromBody] List<PermissionsViewModel> updatedPermissions)
    {
        if (updatedPermissions == null || !updatedPermissions.Any())
        {
            return Json(new { success = false, message = "No permissions received." });
        }

        Console.WriteLine("Received Data:");
        foreach (var perm in updatedPermissions)
        {
            Console.WriteLine($"RoleId: {perm.RoleName}, PermissionId: {perm.PermissionName}, CanView: {perm.CanView}, CanEdit: {perm.CanAddEdit}, CanDelete: {perm.CanDelete}");
        }

        var result = await _userService.UpdateRolePermissionsAsync(updatedPermissions);

        return Json(new { success = result });
    }






}
