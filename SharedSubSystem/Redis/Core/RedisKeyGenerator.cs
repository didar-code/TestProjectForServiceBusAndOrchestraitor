using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Redis.Core
{
    public static class RedisKeyGenerator
    {
        public static string Payment(int paymentId)
        {
            return $"payment:{paymentId}";
        }
    }
}
