using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using ScheduleControl.BackgroundJob.Schedules;
using ScheduleControl.Business.Abstract.Auth;
using ScheduleControl.Entities.Dtos.Account;
using ScheduleControl.Entities.Dtos.Util;
using ScheduleControl.Entities.Models;
using ScheduleControl.WebUI.ViewModels;
using System;
using System.Diagnostics.Eventing.Reader;

namespace ScheduleControl.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;    
        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            var data = new AuthViewModel() { UserForLoginDto = new UserForLoginDto(), UserForRegisterDto = new UserForRegisterDto() };
            return PartialView("_Register", data);
        }

        [HttpPost("Register")]
        [Obsolete]
        public IActionResult Register(AuthViewModel authViewModel)
        {
            var user = _authService.Register(authViewModel.UserForRegisterDto);
            if (user != null)
            {
                HttpContext.Session.SetInt32("userId", user.UserId);
                HttpContext.Session.SetString("userEmail", user.Email);

                DelayedJobs.SendMailRegisterJobs(user.UserId);
                _logger.LogInformation("Register başarılı.");
            }
            else { _logger.LogError("Register başarısız."); }

            return RedirectToAction("Index", "Account");
        }


        [HttpGet("Login")]
        public PartialViewResult Login()
        {
            return PartialView("Login", new AuthViewModel());
        }

        [HttpPost("Login")]
        public IActionResult Login(AuthViewModel authViewModel)
        {
            var user = _authService.Login(authViewModel.UserForLoginDto);
            if (user != null)
            {
                HttpContext.Session.SetInt32("userId", user.UserId);
                HttpContext.Session.SetString("userEmail", user.Email);
                if (user != null)
                {
                    //var id = HttpContext.Session.GetInt32("userId");
                    _logger.LogInformation("Login başarılı.");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Account");
        }

        //[HttpGet("UserRegisterCheck")]
        public IActionResult UserRegisterCheck(string reqUrl)
        {
            if (string.IsNullOrEmpty(reqUrl))
            {
                _logger.LogError("UserRegisterCheck başarısız.");
                return RedirectToAction("Error", "Home");
            }

            _authService.UserActivatedRegister(reqUrl);
            _logger.LogError("UserRegisterCheck başarılı.");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userEmail");

            return RedirectToAction("Index", "Account");
        }
        
    }
}