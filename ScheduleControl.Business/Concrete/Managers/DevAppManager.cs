using ScheduleControl.DataAccess.Abstract;
using ScheduleControl.DataAccess.Concrete.EntityFramework;
using ScheduleControl.Entities.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using ScheduleControl.Business.Abstract;

namespace ScheduleControl.Business.Concrete.Managers
{
    public class DevAppManager : IDevAppService
    {
        private readonly IDevAppDal _devAppDal;

        public DevAppManager(IDevAppDal devAppDal)   
        {
            _devAppDal = devAppDal;
        }
        public List<DevApp> GetAllDevApp()
        {
            return _devAppDal.GetList();
        }

        public List<DevApp> GetAllUserDevApp(int userId)
        {
            return _devAppDal.GetList(u => u.UserId == userId);
        }

        public DevApp DevAppGetById(int devAppId)
        {
            return _devAppDal.Get(u => u.Id == devAppId);
        }

        public List<DevApp> GetDevAppCheck()
        {
            return _devAppDal.GetList(u => u.IsActivatedMailSend == true && u.Status == false);
        }
        public List<DevApp> GetUserDevAppCheck(int userId)
        {
            return _devAppDal.GetList(u => u.IsActivatedMailSend == true && u.Status == false && u.UserId == userId);
        }


        public void Insert(DevApp devApp)
        {
            _devAppDal.Add(devApp);
        }

        public void Update(DevApp devApp)
        {
            _devAppDal.Update(devApp);
        }

        public bool Delete(DevApp devApp)
        {
            return _devAppDal.Delete(devApp);
        }

        public DevApp EmptyDevApp()
        {
            return new DevApp();
        }
    }
}