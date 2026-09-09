using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }

   
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message) { }
    }

    
    public class ConflictException : DomainException
    {
        public ConflictException(string message) : base(message) { }
    }

 
    public class BusinessRuleException : DomainException
    {
        public BusinessRuleException(string message) : base(message) { }
    }
 
    public class AuthenticationException : DomainException
    {
        public AuthenticationException(string message) : base(message) { }
    }
}
