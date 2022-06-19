using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Data
{
    public class TaskRepository
    {
        private string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<TaskItem> GetTaskItems()
        {
            using var context = new TasksDataContext(_connectionString);
            return context.TaskItems.ToList();
        }
        public void AddTask(TaskItem task)
        {
            using var context = new TasksDataContext(_connectionString);
            
            context.TaskItems.Add(task);
            context.SaveChanges();
        }
        public TaskItem TakeTask(int taskId,  string email)
        {
            using var context = new TasksDataContext(_connectionString);
            var task = context.TaskItems.FirstOrDefault(t => t.Id == taskId);
            task.Status = Status.Taken;
            task.UserEmail = email;
            context.SaveChanges();
            return task;
                

        }
        public void CompleteTask( int taskId)
        {
            using var context = new TasksDataContext(_connectionString);
            var task = context.TaskItems.FirstOrDefault(t => t.Id == taskId);
            context.TaskItems.Remove(task);
            context.SaveChanges();
        }

    }
}
