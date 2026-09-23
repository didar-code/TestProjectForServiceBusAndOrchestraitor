using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Redis.Configuration
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } =
            "localhost:6379,abortConnect=false";

        public int DefaultTtlMinutes { get; set; } = 60;

        public Dictionary<string, int> ProjectTtls { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);
    }
}
