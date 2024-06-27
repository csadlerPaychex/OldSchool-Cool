using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using UserInterface;

namespace GameEngine
{
    //Superclass to give basic structure to all games
    internal class BGame
    {
        public string GameName { get; private set; }  
        public List<BGameEvent> Events {  get; private set; } 
        public List<Resource> Resources { get; private set; }
        public BGame(string gameName) 
        {
            string gameFile = File.ReadAllText($@"Resources\{gameName}.json");
            BGame holder = JsonSerializer.Deserialize<BGame>(gameFile);
            GameName = holder.GameName;
            Events = holder.Events;
            Resources = holder.Resources;
        }

        //Loading all game events
        private void LoadEvents() { }

        //Loading all game resources
        private void LoadResources() { }
    }
}
