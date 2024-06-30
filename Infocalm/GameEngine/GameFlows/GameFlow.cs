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
    internal class GameFlow
    {
        public string GameName { get; private set; }  
        public List<Encounter> Encounters {  get; private set; } 
        public List<Resource> Resources { get; private set; }
        public GameFlow(string gameName) 
        {
            string gameFile = File.ReadAllText($@"Resources\{gameName}.json");
            GameFlow holder = JsonSerializer.Deserialize<GameFlow>(gameFile);
            GameName = holder.GameName;
            Encounters = holder.Encounters;
            Resources = holder.Resources;
        }

        //Loading all game events
        private void LoadEvents() { }

        //Loading all game resources
        private void LoadResources() { }
    }
}
