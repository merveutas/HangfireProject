using Microsoft.AspNetCore.Hosting.Server;
using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Mail;
using ScheduleControl.Entities.Dtos.Util;
using ScheduleControl.Entities.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleControl.BackgroundJob.Managers.RecurringJobs
{
    public class DevAppStatusJobManager
    {
        private readonly IDevAppService _devAppService;
        ScheduleControl.Entities.Dtos.Util.Helper Helper;
        public DevAppStatusJobManager(IDevAppService devAppService)
        {
            _devAppService = devAppService;
        }

        //Request atıyor web sitelerine,sonuca göre durum güncelliyor.
        public async Task Process()
        {
            //Helper = new Helper();
            var appList = _devAppService.GetAllDevApp();
            if (appList != null && appList.Count > 0)
            {
                foreach (var item in appList)
                {
                    try
                    {
                        string Url = item.Url.Contains("http") ? item.Url : "http://" + item.Url;
                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                        if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                        {
                            item.StatusMessage = "";
                            item.ModifyDate = DateTime.Now;
                            item.Status = true;
                            _devAppService.Update(item);
                        }
                        else
                        {
                            item.StatusMessage = "HataKodu :" + myHttpWebResponse.StatusCode + "Request isteği başarısız oldu.";
                            item.Status = false;
                            item.ModifyDate = DateTime.Now;
                            _devAppService.Update(item);
                        }
                    }
                    catch(Exception ex)
                    {
                        item.StatusMessage = "Request isteği başarısız oldu. " + ex.Message;
                        item.Status = false;
                        item.ModifyDate = DateTime.Now;
                        _devAppService.Update(item);
                    }

                }

            }
        }

    }
}
