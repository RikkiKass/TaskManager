using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Data
{
   public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public Status Status { get; set; }
    }
}
