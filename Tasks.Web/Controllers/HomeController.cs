using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Tasks.Data;
using Tasks.Web.Models;

namespace Tasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private string _conn;
        private readonly IHubContext<TaskHub> _context;

   
        public HomeController(IConfiguration configuration, IHubContext<TaskHub> context)
        {
            _conn = configuration.GetConnectionString("ConStr");
            _context = context;
        }
     
        [Authorize]
        [Route("addtask")]
        [HttpPost]
        public void AddTask(TaskItem taskItem)
        {
            var repo = new TaskRepository(_conn);
            taskItem.Status = Status.Available;
            repo.AddTask(taskItem);
            taskItem.UserEmail = User.Identity.Name;
            _context.Clients.All.SendAsync("added-task", taskItem);
        }
        [Authorize]
        [Route("taketask")]
        [HttpPost]
        public void TakeTask(StatusViewModel vm)
        {
            var repo = new TaskRepository(_conn);
            var userRepo = new UserRepository(_conn);
            var user = userRepo.GetUserByEmail(User.Identity.Name);
       
            TaskItem task=repo.TakeTask(vm.TaskId, user.Email);
            
            _context.Clients.All.SendAsync("status-changed", repo.GetTaskItems());
            
        }
        [Authorize]
        [Route("completetask")]
        [HttpPost]
        public void CompleteTask(StatusViewModel vm)
        {
            var repo = new TaskRepository(_conn);
            repo.CompleteTask(vm.TaskId);
            _context.Clients.All.SendAsync("task-completed", repo.GetTaskItems());

        }
    }
}
