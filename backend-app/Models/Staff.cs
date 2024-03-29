﻿using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Staff
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string FileAvatar { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        //public bool IsTokenValid { get; set; } = true;
    }
}
