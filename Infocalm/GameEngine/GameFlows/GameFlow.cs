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
        //public List<Encounter>? PreviousEncounters { get; private set; } = new List<Encounter>();
        public GameFlow(string gameName) 
        {
            string gameFile = File.ReadAllText($@"GameRules\{gameName}.json");
            GameFlow holder = JsonSerializer.Deserialize<GameFlow>(gameFile);
            GameName = gameName;
            Encounters = holder.Encounters;
            Resources = holder.Resources;
        }
        public virtual void RunGame()
        { 

        }
        private void RunEncounter(Encounter encounter)
        {

        }
        private void ResourceCheck()
        {
            
        }
    }
}
