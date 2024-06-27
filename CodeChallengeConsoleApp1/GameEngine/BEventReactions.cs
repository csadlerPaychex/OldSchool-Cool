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
        public List<string> ReactionMessages { get; private set; }
        public List<Tuple<string, int>> ResourceReaction {  get; private set; }

        public BEventReactions(string reactionName, string triggeringOption,  string triggeredEvent, List<string> reactionMessages, List<Tuple<string, int>> resourceReaction)
        {
            this.ReactionName = reactionName;
            this.TriggeringOption = triggeringOption;
            this.TriggeredEvent = triggeredEvent;
            this.ReactionMessages = reactionMessages;
            this.ResourceReaction = resourceReaction;
        }
    }
}
