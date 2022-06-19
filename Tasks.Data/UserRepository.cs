using System;
using System.Linq;

namespace Tasks.Data
{
    public class UserRepository
    {
        private string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Signup(User user, string password)
        {
            user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(password);
            using var context = new TasksDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new TasksDataContext(_connectionString);
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            var validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHashed);
            if (!validPassword)
            {
                return null;
            }
            return user;
        }
        public User GetUserByEmail(string email)
        {
            using var context = new TasksDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
