using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class ResourceReaction
    {
        //JSON reader will create a new resource object. Let the encounter refer to the correct object
        public Resource? Resource { get; private set; } 
        public int Delta { get; set; }
        public virtual void UpdateResources()
        {
            if (Resource.Amount + Delta > 0)
            {
                Resource.Amount = Resource.Amount + Delta;
            }
        }
    }
}
