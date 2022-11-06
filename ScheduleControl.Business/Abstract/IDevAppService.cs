using ScheduleControl.Entities.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ScheduleControl.Business.Abstract
{
    public interface IDevAppService
    {
        DevApp DevAppGetById(int devAppId);

        void Insert(DevApp devApp);

        void Update(DevApp devApp);
        bool Delete(DevApp devApp);

        List<DevApp> GetAllDevApp();

        List<DevApp> GetDevAppCheck();

        List<DevApp> GetUserDevAppCheck(int userId);

        List<DevApp> GetAllUserDevApp(int userId);

        DevApp EmptyDevApp();
    }
}