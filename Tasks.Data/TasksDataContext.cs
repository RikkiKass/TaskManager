using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Data
{
    public class TasksDataContext: DbContext
    {
        private string _connectionString;
        public TasksDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
