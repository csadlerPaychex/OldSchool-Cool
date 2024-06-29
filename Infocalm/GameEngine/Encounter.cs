using Infocalm.UserInterface;
using Infocalm.UserInterface.DisplayEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    internal class Encounter
    {
        public string EncounterName {  get; private set; }
        public string EncounterType { get; private set; }
        public Sprite Sprite { get; private set; }
        public List<EncounterReactions>? EncounterReactions { get; private set; }
        public List<string>? EncounterMessages { get; private set; }
        public Encounter(string eventName, string eventType, List<string> eventOptions, List<EncounterReactions> eventReactions, Sprite sprite) 
        {
            EncounterName = eventName;
            EncounterType = eventType;
            EncounterReactions = eventReactions;
            Sprite = sprite;
        }
        //Takes in interfaces for managing encounter, and returns the next encounter based on the reactions
        public virtual Encounter RunEncounter(IDisplayEngine displayEngine, IUserInput inputInterface, IMessages messageInterface, IOptions optionInterface)
        {
            displayEngine.UpdateSprite(Sprite);

            //clean options and messages and prepare for display
            var encounterOptions = EncounterReactions?.Select(options => options.TriggeringOption);
            if (encounterOptions!= null && encounterOptions.Any()) 
            {
                optionInterface.ReplaceOptions(encounterOptions.ToList());
            }

            if (EncounterMessages != null && EncounterMessages.Any())
            {
                messageInterface.Clear();
                messageInterface.AddMessages(EncounterMessages);
            }
            //This allows configuration data to have an empty list without breaking downstream services
            else { EncounterMessages = new List<string>() ; }

            string encounterChoice = inputInterface
                .ManageInputSelection(EncounterMessages, messageInterface, optionInterface);
            
            return this;
        }

    }
}
