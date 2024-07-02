using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class EncounterReaction
    {
        public required string ReactionName { get; set; }
        public required string TriggeringOption { get; set; }
        public string? TriggeredEncounter { get; set; } //use the string to lookup the needed encounter
        public List<string>? ReactionMessages { get; set; }
        public List<ResourceReaction>? ResourceReactions {  get; set; } 
    }
}
