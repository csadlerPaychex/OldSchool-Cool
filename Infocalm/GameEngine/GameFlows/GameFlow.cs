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
        public string GameName { get; set; }  
        public string? GameType { get; set; }
        public List<Encounter>? Encounters {  get; set; } 
        public List<Resource>? Resources { get; set; }
        public List<Encounter>? PreviousEncounters { get; set; } = new List<Encounter>();
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
