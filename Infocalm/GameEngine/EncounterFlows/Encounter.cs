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
        public required string EncounterName {  get; set; } //How are we going to enforce uniqueness here? 
        public static string EncounterType { get; set; } = "BaseEncounter"; 
        public required string Sprite { get; set; }
        public required List<EncounterReaction> EncounterReactions { get; set; }
        public List<string>? EncounterMessages { get; set; }
        public virtual string RunEncounter(GameSession gameSession)
        {
            Sprite currentSprite = new Sprite(Sprite);
            gameSession._displayEngine.UpdateSprite(currentSprite);
            List<string> cleanedOptions = CleanOptions(gameSession._options);
            DisplayMessages(gameSession._messages);
            gameSession._options.ReplaceOptions(cleanedOptions);

            string encounterChoice = GetUserChoice(gameSession, cleanedOptions);

            //Process game reactions, and return implied encounter to the game flow
            return ProcessEncounterReactions(gameSession._messages, encounterChoice); ;
        }
        internal string GetUserChoice(GameSession gameSession, List<string> cleanedOptions)
        {
            string encounterChoice = gameSession._userInput 
                .ManageInputSelection(cleanedOptions, gameSession._messages);
            return encounterChoice;
        }
        internal List<string> CleanOptions(IOptions optionInterface)
        {
            var encounterOptions = EncounterReactions?.Select(options => options.TriggeringOption);
            List<string> cleanedOptions = new List<string>(); //Used to ensure list is not null
            if (encounterOptions != null && encounterOptions.Any())
            {
                cleanedOptions.AddRange(encounterOptions);
            }
            return cleanedOptions; //This list may be empty, but there may be default selections for the user
        }
        internal void DisplayMessages(IMessages messageInterface)
        {
            if (EncounterMessages != null && EncounterMessages.Any())
            {
                messageInterface.Clear();
                messageInterface.AddMessages(EncounterMessages);
            }
            //This allows configuration data to have an empty list without breaking downstream services
            else { EncounterMessages = new List<string>(); }
        }
        internal string ProcessEncounterReactions(IMessages messageInterface, string encounterChoice)
        {
            //Re-run the encounter if the current result does not provide a next encounter. This allows for implicit event looping, which may cause some design issues
            string nextEncounter = this.EncounterName;
            
            EncounterReaction reaction = EncounterReactions.
                Single(option => option.TriggeringOption == encounterChoice);
            if (reaction.ResourceReactions != null && reaction.ResourceReactions.Any()) 
            {
                List<ResourceReaction> resourceOutcome = reaction.ResourceReactions;
                foreach (ResourceReaction resourceReaction in resourceOutcome)
                {
                    resourceReaction.UpdateResources();
                }

            }
            if (reaction.ReactionMessages != null) { messageInterface.AddMessages(reaction.ReactionMessages); }
            if (reaction.TriggeredEncounter != null) { nextEncounter = reaction.TriggeredEncounter; }
            return nextEncounter;
        }
    }
}