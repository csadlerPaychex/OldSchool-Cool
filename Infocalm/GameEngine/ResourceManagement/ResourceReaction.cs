using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class ResourceReaction
    {
        public Resource Resource { get; private set; }
        public int Delta { get; private set; }
        public ResourceReaction(Resource resource, int delta)
        {
            Resource = resource;
            Delta = delta;
        }
        public virtual void UpdateResources()
        {
            Resource.Amount = Resource.Amount + Delta;
        }
    }
}
