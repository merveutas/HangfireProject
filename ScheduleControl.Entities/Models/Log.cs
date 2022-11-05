using ScheduleControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleControl.Entities.Models
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
        public string InsertDate { get; set; }
        public string StackTrace { get; set; }
        public string MachineName { get; set; }
    }
}
