using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class Resource
    {
        public string ResourceName { get; private set; }
        public string? Description { get; private set; } = string.Empty;
        public int? Amount { get; set; } = 0;
        public bool? Hidden { get; set; } = false;
        public bool? LoseIfZero { get; set; }
        public int? ResourceMaximum { get; set; }
        public bool? LoseIfMaximum { get; set; }
        public Resource(string name, string description, int amount = 0, bool hidden =false, bool loseIfZero = false)
        {
            ResourceName = name;
            Description = description;
            Amount = amount;
            Hidden = hidden;
            LoseIfZero = loseIfZero;
        }
    }
}
