using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Repository.ViewModel;
using Service.Interfaces;

namespace Web.Controllers;

public class LoginController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    public LoginController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        string request_cookie = Request.Cookies["Email"];
        if (!string.IsNullOrEmpty(request_cookie))
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _authService.Login(model.Email, model.password);
            if (user == null)
            {
                TempData["error"] = "Login Unsuccessful";
                ModelState.AddModelError("Email", "Wrong Email");
                ModelState.AddModelError("Password", "Wrong Password");
                return View();
            }
            string token = _authService.GenerateJwtToken(user.Email, user.Roleid);

            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            var JWTtoken = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("Token", token, JWTtoken);
            Response.Cookies.Append("Username", user.Username, cookie);
            Response.Cookies.Append("Image", user.Profileimagepath, cookie);

            if (model.RememberMe)
            {
                Response.Cookies.Append("Email", model.Email, cookie);
                cookie.Expires = DateTime.UtcNow.AddDays(30);
            }
            TempData["success"] = "Login Successful";
            return RedirectToAction("UserList", "User");
        }
        return View();
    }
    public IActionResult Logout()
    {
        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }
        return RedirectToAction("Login", "Login");
    }

    [HttpGet]
    public IActionResult ForgotPassword(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            ViewData["Email"] = email;
        }
        else
        {
            ViewData["Email"] = "";
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {

            string filePath = @"C:\Users\pct216\Downloads\Pizza Shop\Main Project\Pizza Shop\Web\EmailTemplate\ResetPasswordEmailTemplate.html";
            string emailBody = System.IO.File.ReadAllText(filePath);

            var url = Url.Action("ResetPassword", "Login", new { email = model.Email }, Request.Scheme); //email to token
            emailBody = emailBody.Replace("{ResetLink}", url);

            string subject = "Reset Password";
            _authService.SendEmailAsync(model.Email, subject, emailBody);

            TempData["success"] = "Password reset link have been sent to your email.";

        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (_userService.ResetPassword(model.Email, model.NewPassword, model.ConfirmPassword, out string message))
        {
            // TempData["success"] = message;
            return RedirectToAction("Login", "Login");
        }

        ModelState.AddModelError(string.Empty, message);
        return View(model);

    }

}