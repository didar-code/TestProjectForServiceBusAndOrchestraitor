using AuthManagementSubSystem.Aggregators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);

        Task<UserAggregatorsRoot?> GetByEmailAsync(string email);

        Task AddAsync(UserAggregatorsRoot user);

        Task SaveAsync();
    }
}
