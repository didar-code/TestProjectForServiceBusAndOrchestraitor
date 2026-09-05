using SharedSubSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthManagementSubSystem.DTOs.Commands
{
    public class LoginCommand : ICommand
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
