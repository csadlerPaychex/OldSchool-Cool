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
        public Resource(string name, string description, int amount = 0)
        {
            ResourceName = name;
            Description = description;
            Amount = amount;
        }
    }
}
