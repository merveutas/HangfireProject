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
            RecurringJob.RemoveIfExists(nameof(DevAppMailJobManager));
            RecurringJob.AddOrUpdate<DevAppMailJobManager>(nameof(DevAppMailJobManager),
                job => job.Process(), "* * * * *", TimeZoneInfo.Local);
        }

        [Obsolete]
        public static void AppListenCheckOperation()
        {
            RecurringJob.RemoveIfExists(nameof(DevAppStatusJobManager));
            RecurringJob.AddOrUpdate<DevAppStatusJobManager>(nameof(DevAppStatusJobManager),
                job => job.Process(), "* * * * *", TimeZoneInfo.Local);
        }
       
    }
}


