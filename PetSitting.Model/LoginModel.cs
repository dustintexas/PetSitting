using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSitting.Model
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string message { get; set; }
        public string ReturnURL { get; set; }
    }
}
