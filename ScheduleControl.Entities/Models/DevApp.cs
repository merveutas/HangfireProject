using ScheduleControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleControl.Entities.Models
{
    public class DevApp : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; }
        public bool IsActivatedMailSend { get; set; }
        public string StatusMessage { get; set; }
        public DateTime? ModifyDate { get; set; }


    }
}
