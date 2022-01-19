using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LongRuningAPI.Models
{
    public class LongRunningRequest
    {
        public string ExternalId { get; set; }
        public string ThreadId { get; set; }
    }
}
