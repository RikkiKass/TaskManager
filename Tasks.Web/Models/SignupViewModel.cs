﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data;

namespace Tasks.Web.Models
{
    public class SignupViewModel: User
    {
        public string Password { get; set; } 
    }
}
