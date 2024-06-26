using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    internal class BaseEvent
    {
        public string EventName {  get; private set; }
        public string EventType { get; private set; }
        public List<string> EventOptions { get; private set; }
        public List<BaseEventReaction> EventReactions { get; private set; }
        public Sprite Sprite { get; private set; }
        public BaseEvent(string eventName, string eventType, List<string> eventOptions, List<BaseEventReaction> eventReactions, Sprite sprite) 
        {
            EventName = eventName;
            EventType = eventType;
            EventOptions = eventOptions;
            EventReactions = eventReactions;
            Sprite = sprite;
        }

    }
}
