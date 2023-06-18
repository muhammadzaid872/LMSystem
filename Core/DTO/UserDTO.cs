using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? UserRole { get; set; }
        public string Token { get; set; }
    }
}
