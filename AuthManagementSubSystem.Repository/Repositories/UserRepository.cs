using AuthManagementSubSystem.Aggregators;
using AuthManagementSubSystem.Repository.Data;
using AuthManagementSubSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAsync(
            string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<UserAggregatorsRoot?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddAsync(UserAggregatorsRoot user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
