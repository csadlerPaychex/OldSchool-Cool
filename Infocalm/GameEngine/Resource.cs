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
        public string Description { get; private set; } = string.Empty;
        public int Amount { get; private set; }
        public Resource(string name, string description, int amount)
        {
            ResourceName = name;
            Description = description;
            Amount = amount;
        }
    }
    internal class ResourceReaction
    {
        public string ResourceName { get; private set; }
        public int Delta { get; private set; }
        public ResourceReaction(string resourceName, int delta)
        {
            ResourceName = resourceName;
            Delta = delta;
        }
    }
}
