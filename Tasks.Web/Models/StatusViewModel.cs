using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data;

namespace Tasks.Web.Models
{
    public class StatusViewModel
    {
        public int TaskId { get; set; }
   
        public Status Status { get; set; }
    }
}
