using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class EncounterReaction
    {
        public string ReactionName { get; set; }
        public string TriggeringOption { get; set; }
        public Encounter? TriggeredEncounter { get; set; }
        public List<string>? ReactionMessages { get; set; }
        public List<ResourceReaction>? ResourceReactions {  get; set; } 
    }
}
