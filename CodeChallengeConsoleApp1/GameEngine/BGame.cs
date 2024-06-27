using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    //Superclass to give basic structure to all games
    internal class BGame
    {
        public List<BGameEvent> Events {  get; private set; } 
        public List<Tuple<string, int>> Resources { get; private set; }
        public BGame(string gameName) { }
        private void LoadResources() { }
    }
}
