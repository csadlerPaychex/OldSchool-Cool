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
        public string EncounterName {  get; set; }
        public static string EncounterType { get; set; } 
        public string Sprite { get; set; }
        public List<EncounterReaction>? EncounterReactions { get; set; }
        public List<string>? EncounterMessages { get; set; }
        public virtual Encounter RunEncounter(IDisplayEngine displayEngine, IUserInput inputInterface, IMessages messageInterface, IOptions optionInterface)
        {
            Sprite currentSprite = new Sprite(Sprite);
            displayEngine.UpdateSprite(currentSprite);
            List<string> cleanedOptions = CleanOptions(optionInterface);
            DisplayMessages(messageInterface);

            string encounterChoice = GetUserChoice(inputInterface, messageInterface, optionInterface, cleanedOptions);

            //Process game reactions, and return implied encounter to the game flow
            return ProcessEncounterReactions(messageInterface, encounterChoice); ;
        }
        private string GetUserChoice(IUserInput inputInterface, IMessages messageInterface, IOptions optionInterface, List<string> cleanedOptions)
        {
            string encounterChoice = inputInterface
                .ManageInputSelection(cleanedOptions, messageInterface, optionInterface);
            return encounterChoice;
        }
        private List<string> CleanOptions(IOptions optionInterface)
        {
            var encounterOptions = EncounterReactions?.Select(options => options.TriggeringOption);
            List<string> cleanedOptions = new List<string>(); //Used to ensure list is not null
            if (encounterOptions != null && encounterOptions.Any())
            {
                cleanedOptions.AddRange(encounterOptions);
            }
            return cleanedOptions; //This list may be empty, but there may be default selections for the user
        }
        private void DisplayMessages(IMessages messageInterface)
        {
            if (EncounterMessages != null && EncounterMessages.Any())
            {
                messageInterface.Clear();
                messageInterface.AddMessages(EncounterMessages);
            }
            //This allows configuration data to have an empty list without breaking downstream services
            else { EncounterMessages = new List<string>(); }
        }
        private Encounter ProcessEncounterReactions(IMessages messageInterface, string encounterChoice)
        {
            //Re-run the encounter if the current result does not provide a next encounter. This allows for implicit event looping, which may cause some design issues
            Encounter nextEncounter = this;
            if (EncounterReactions != null && EncounterReactions.Any())
            {
                EncounterReaction reaction = EncounterReactions.
                Single(option => option.TriggeringOption == encounterChoice);
                List<ResourceReaction> resourceOutcome = reaction.ResourceReactions;
                foreach (ResourceReaction resourceReaction in resourceOutcome)
                {
                    resourceReaction.UpdateResources();
                }
                messageInterface.AddMessages(reaction.ReactionMessages);
                if (reaction.TriggeredEncounter != null)
                { nextEncounter = reaction.TriggeredEncounter; }
            }
            return nextEncounter;
        }

    }
}
