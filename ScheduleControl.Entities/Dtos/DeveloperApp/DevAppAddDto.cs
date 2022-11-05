using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleControl.Entities.Dtos.DeveloperApp
{
    public class DevAppAddDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActivatedMailSend { get; set; }
       
    }
}
