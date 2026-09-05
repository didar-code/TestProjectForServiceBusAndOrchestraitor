using Microsoft.AspNetCore.Identity;
using SharedSubSystem.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Repository.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher;

        public PasswordHasher()
        {
            _hasher = new PasswordHasher<object>();
        }

        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");

            return _hasher.HashPassword(
                null!,
                password);
        }

        public bool Verify(
            string password,
            string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(passwordHash))
                return false;

            var result =
                _hasher.VerifyHashedPassword(
                    null!,
                    passwordHash,
                    password);

            return result ==
                       PasswordVerificationResult.Success
                   ||
                   result ==
                       PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
