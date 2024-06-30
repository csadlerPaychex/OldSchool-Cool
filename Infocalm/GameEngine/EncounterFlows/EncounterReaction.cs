using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class EncounterReaction
    {
        public string ReactionName { get; private set; }
        public string TriggeringOption { get; private set; }
        public Encounter? TriggeredEncounter { get; private set; }
        public List<string> ReactionMessages { get; private set; }
        public List<ResourceReaction> ResourceReactions {  get; private set; } 

        public EncounterReaction(string reactionName, string triggeringOption,  Encounter triggeredEncounter, List<string> reactionMessages, List<ResourceReaction> resourceReaction)
        {
            this.ReactionName = reactionName;
            this.TriggeringOption = triggeringOption;
            this.TriggeredEncounter = triggeredEncounter;
            this.ReactionMessages = reactionMessages;
            this.ResourceReactions = resourceReaction;
        }
    }
}
