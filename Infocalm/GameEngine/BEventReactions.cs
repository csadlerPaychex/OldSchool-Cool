using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class BEventReactions
    {
        public string ReactionName { get; private set; }
        public string TriggeringOption { get; private set; }
        public string TriggeredEvent { get; private set; }
        public string TriggeredEventType { get; private set; }
        public List<string> ReactionMessages { get; private set; }
        public List<ResourceReaction> ResourceReactions {  get; private set; } 

        public BEventReactions(string reactionName, string triggeringOption,  string triggeredEvent, string triggeredEventType, List<string> reactionMessages, List<ResourceReaction> resourceReaction)
        {
            this.ReactionName = reactionName;
            this.TriggeringOption = triggeringOption;
            this.TriggeredEvent = triggeredEvent;
            this.TriggeredEventType = triggeredEventType;
            this.ReactionMessages = reactionMessages;
            this.ResourceReactions = resourceReaction;
        }
    }
}
