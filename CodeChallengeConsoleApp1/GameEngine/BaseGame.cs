using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    //Superclass to give basic structure to all games
    internal class BaseGame
    {
        public List<BaseEvent> Events {  get; private set; } 
    }
}
