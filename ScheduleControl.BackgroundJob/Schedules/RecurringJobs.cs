using Hangfire;
using ScheduleControl.BackgroundJob.Managers.RecurringJobs;
using System;

namespace ScheduleControl.BackgroundJob.Schedules
{
    public static class RecurringJobs
    {
        [Obsolete]
        public static void AppStatusCheckOperation()
        {
            /*  RemoveIfExists yöntemini çağırarak var olan yinelenen bir işi kaldırabilirsiniz. 
                Böyle tekrar eden bir iş olmadığında bir istisna oluşturmaz */
            RecurringJob.RemoveIfExists(nameof(DevAppMailJobManager));
            RecurringJob.AddOrUpdate<DevAppMailJobManager>(nameof(DevAppMailJobManager),
                job => job.Process(), "* * * * *", TimeZoneInfo.Local);
        }

        [Obsolete]
        public static void AppListenCheckOperation()
        {
            /*  RemoveIfExists yöntemini çağırarak var olan yinelenen bir işi kaldırabilirsiniz. 
                Böyle tekrar eden bir iş olmadığında bir istisna oluşturmaz */
            RecurringJob.RemoveIfExists(nameof(DevAppStatusJobManager));
            RecurringJob.AddOrUpdate<DevAppStatusJobManager>(nameof(DevAppStatusJobManager),
                job => job.Process(), "* * * * *", TimeZoneInfo.Local);
        }

        //[Obsolete]
        //public static void AppStatusCheckOperation()
        //{
        //    /*  RemoveIfExists yöntemini çağırarak var olan yinelenen bir işi kaldırabilirsiniz. 
        //        Böyle tekrar eden bir iş olmadığında bir istisna oluşturmaz */
        //    RecurringJob.RemoveIfExists(nameof(DevAppMailJobManager));
        //    RecurringJob.AddOrUpdate<DevAppMailJobManager>(nameof(DevAppMailJobManager),
        //        job => job.Process((int?)null), "* * * * *", TimeZoneInfo.Local);
        //}

        //[Obsolete]
        //public static void AppListenCheckOperation()
        //{
        //    /*  RemoveIfExists yöntemini çağırarak var olan yinelenen bir işi kaldırabilirsiniz. 
        //        Böyle tekrar eden bir iş olmadığında bir istisna oluşturmaz */
        //    RecurringJob.RemoveIfExists(nameof(DevAppStatusJobManager));
        //    RecurringJob.AddOrUpdate<DevAppStatusJobManager>(nameof(DevAppStatusJobManager),
        //        job => job.Process((int?)null), "* * * * *", TimeZoneInfo.Local);
        //}
    }
}


