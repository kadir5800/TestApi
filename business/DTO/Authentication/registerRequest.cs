﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Login
{
    public class registerRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
