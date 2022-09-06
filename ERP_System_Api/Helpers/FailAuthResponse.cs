using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwlHR.Payloads.Response
{
    public class FailAuthResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
