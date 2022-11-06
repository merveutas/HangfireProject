using Microsoft.AspNetCore.Mvc;
using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Auth;
using ScheduleControl.Entities.Models;
using System;
using System.IO;
using System.Net;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace ScheduleControl.WebUI.Controllers
{
    [MyValidation]
    public class AppBoxController : Controller
    {
        private readonly IDevAppService _devAppService;

        private readonly ILogger<AccountController> _logger;

        public AppBoxController(IDevAppService devAppService, ILogger<AccountController> logger)
        {
            _devAppService = devAppService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllDevApp()
        {
            var model = _devAppService.GetAllUserDevApp(HttpContext.Session.GetInt32("userId").Value);
            return PartialView("_PartialDevApp", model);
        }

        public IActionResult GetDevAppById(int id)
        {
            var model = id > 0 ? _devAppService.DevAppGetById(id) : _devAppService.EmptyDevApp();
            return PartialView("_AddEditDevApp", model);
        }

        public IActionResult InsertUpdateDevApp(DevApp devApp)
        {
            bool status = false;
            try
            {
                devApp.UserId = HttpContext.Session.GetInt32("userId").HasValue ? HttpContext.Session.GetInt32("userId").Value : 0;
                if (devApp.Id > 0)
                {
                    _devAppService.Update(devApp);
                    _logger.LogInformation("Ekleme veya silme işlemi başarısız.");

                }
                else
                {
                    _devAppService.Insert(devApp);
                    _logger.LogInformation("Ekleme veya silme işlemi başarısız.");

                }

                status = true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Ekleme veya silme işlemi başarısız.");
            }

            return Json(status);
        }

        public IActionResult RemoveDevApp(int id)
        {
            DevApp model = _devAppService.DevAppGetById(id);
            bool status = _devAppService.Delete(model);
            return Json(status);
        }
    }

    //action filter
    public class MyValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("userId").HasValue)
            {
                int id = context.HttpContext.Session.GetInt32("userId").Value;
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Account" }));
            }
        }
    }
}
