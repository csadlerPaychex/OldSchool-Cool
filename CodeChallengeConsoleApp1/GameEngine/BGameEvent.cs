using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    internal class BGameEvent
    {
        public string EventName {  get; private set; }
        public string EventType { get; private set; }
        public List<string> EventOptions { get; private set; }
        public List<BEventReactions> EventReactions { get; private set; }
        public Sprite Sprite { get; private set; }
        public BGameEvent(string eventName, string eventType, List<string> eventOptions, List<BEventReactions> eventReactions, Sprite sprite) 
        {
            EventName = eventName;
            EventType = eventType;
            EventOptions = eventOptions;
            EventReactions = eventReactions;
            Sprite = sprite;
        }

    }
}
