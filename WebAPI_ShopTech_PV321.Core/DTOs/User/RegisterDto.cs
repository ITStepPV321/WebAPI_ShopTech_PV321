﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_ShopTech_PV321.Core.DTOs.User
{
    public class RegisterDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }  
        public string? Password { get; set; }
    }
}
