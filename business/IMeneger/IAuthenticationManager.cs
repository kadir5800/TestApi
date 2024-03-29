﻿using Business.DTO.BaseObjects;
using Business.DTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IMeneger
{
    public interface IAuthenticationManager
    {
        ClientResult Register(registerRequest request);
        ClientResult<loginResponse> login(loginRequest request);
    }
}
