using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Mail;
using ScheduleControl.Entities.Dtos.Util;
using ScheduleControl.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleControl.BackgroundJob.Managers.RecurringJobs
{
    public class DevAppMailJobManager
    {
        private readonly IDevAppService _devAppService;
        private readonly IMailService _mailService;

        ScheduleControl.Entities.Dtos.Util.Helper Helper;
        public DevAppMailJobManager(IDevAppService devAppService, IMailService mailService)
        {
            _devAppService = devAppService;
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));

        }

        //Statu kontrol edilip mail gönderiyor.
        public async Task Process()
        {
            //var statusCheck = _devAppService.GetDevAppCheck();
            var statusCheck = _devAppService.GetDevAppCheck();
            if (statusCheck != null && statusCheck.Count > 0)
            {
                foreach (var item in statusCheck)
                {
                    await _mailService.SendDevAppUserMailAsync(item.UserId, item);
                }
           
            }
        }

    }
}
