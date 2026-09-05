using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Repository.Security
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string email, string role);
    }
}
