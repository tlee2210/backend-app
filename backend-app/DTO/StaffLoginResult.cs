﻿using backend_app.Models;

namespace backend_app.DTO
{
    public class StaffLoginResult
    {
        public string Token { get; set; }
        public Account user { get; set; }
    }
}
