using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class Resource
    {
        public string ResourceName { get; set; }
        public string? Description { get; set; } = string.Empty;
        public int? Amount { get; set; } = 0;
        public bool? Hidden { get; set; } = false;
        public bool? LoseIfZero { get; set; }
        public int? ResourceMaximum { get; set; }
        public bool? LoseIfMaximum { get; set; }
    }
}
