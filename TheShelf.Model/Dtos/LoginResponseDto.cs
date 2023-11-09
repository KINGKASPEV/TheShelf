using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShelf.Model.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
