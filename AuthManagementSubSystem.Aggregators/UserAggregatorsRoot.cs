using SharedSubSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Aggregators
{
    public class UserAggregatorsRoot
    {
        public int UserId { get; private set; }

        public string UserName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string Role { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        public DateTime CreateDate { get; private set; }

        private UserAggregatorsRoot()
        {
        }

        public static UserAggregatorsRoot Create(string userName, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new BusinessRuleException("Username is required.");   

            if (string.IsNullOrWhiteSpace(email))
                throw new BusinessRuleException("Email is required.");  

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new BusinessRuleException("Password hash is required.");   

            return new UserAggregatorsRoot
            {
                UserName = userName,
                Email = email,
                PasswordHash = passwordHash,
                Role = "User",
                IsActive = true,
                CreateDate = DateTime.UtcNow
            };
        }
    }
}
