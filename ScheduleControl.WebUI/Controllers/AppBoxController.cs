using Microsoft.AspNetCore.Mvc;
using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Auth;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace ScheduleControl.WebUI.Controllers
{
    public class AppBoxController : Controller
    {
        private readonly IDevAppService _devAppService;

        public AppBoxController(IDevAppService devAppService)
        {
            _devAppService = devAppService;
        }

        public IActionResult Index()
        {
            var model = _devAppService.GetAllDevApp();
            return View(model);
        }
    }
}
