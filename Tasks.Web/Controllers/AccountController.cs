using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tasks.Data;
using Tasks.Web.Models;

namespace Tasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Constr");
        }
        [Route("signup")]
        [HttpPost]
        public void Signup(SignupViewModel user)
        {
            UserRepository repo = new UserRepository(_connectionString);
            repo.Signup(user, user.Password);
        }
        [Route("login")]
        [HttpPost]
        public User Login(LoginViewModel vm)
        {
            var repo = new UserRepository(_connectionString);
            var user=repo.Login(vm.Email, vm.Password);
            if (user == null)
            {
                return null;
            }
            var claims = new List<Claim>()
            {
                new Claim("user",  vm.Email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;

        }
      
        [Route("getcurrentuser")]
        [HttpGet]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new UserRepository(_connectionString);
            return repo.GetUserByEmail(User.Identity.Name);
        }
        [HttpPost]
        [Route("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }
    }
}
