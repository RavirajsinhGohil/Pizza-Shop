using Microsoft.AspNetCore.Mvc;
using Repository.ViewModel;
using Service.Interfaces;

namespace Web.Controllers;

public class ProfileController : Controller
{
    private readonly IUserService _userService;
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Profile()
    {
        var token = Request.Cookies["Token"];
        var email = _userService.GetEmailFromToken(token);

        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Login", "Login");
        }

        var model = _userService.GetUserProfile(email);

        if (model == null)
        {
            return NotFound("User Not Found");
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Profile(UserViewModel model)
    {
        var token = Request.Cookies["Token"];
        var email = _userService.GetEmailFromToken(token);

        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Login", "Login");
        }

        var success = _userService.UpdateUserProfile(email, model);

        if (!success)
        {
            return NotFound("User Not Found");
        }

        TempData["success"] = "Profile updated successfully.";
        return View(model);
    }

    [HttpGet]
    public IActionResult ProfileChangePassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProfileChangePassword(ProfileChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var token = Request.Cookies["Token"];
            var userEmail = _userService.GetEmailFromToken(token);

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Login");
            }

            var result = _userService.ChangePassword(userEmail, model);

            if (result == "UserNotFound")
            {
                TempData["error"] = "User not found.";
                return View(model);
            }

            if (result == "IncorrectPassword")
            {
                TempData["error"] = "Current password is incorrect.";
                return View(model);
            }

            TempData["success"] = "Password updated successfully.";
            return RedirectToAction("Login", "Login");
        }
        return View();

    }
}
