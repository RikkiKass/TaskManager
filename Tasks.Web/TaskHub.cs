using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Tasks.Data;

namespace Tasks.Web
{
    public class TaskHub : Hub
    {
        private string _connectionString;

        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
    


      
        public void GetTasks()
        {
            var repo = new TaskRepository(_connectionString);
            var tasks = repo.GetTaskItems();
            Clients.All.SendAsync("getTasks", tasks);
        }
    }
}
