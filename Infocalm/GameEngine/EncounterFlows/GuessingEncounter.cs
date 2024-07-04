using GameEngine;
using Infocalm.UserInterface.DisplayEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    [JsonDerivedType(typeof(GuessingEncounter), typeDiscriminator: "guessing")]
    internal class GuessingEncounter : Encounter
    {
        public int MinimumGuess {  get; set; }
        public int MaximumGuess { get; set; }
        public bool GiveHint { get; set; } = false;
        public List<Tuple<int, EncounterReaction>> CorrectGuesses { get; private set; } 
            = new List<Tuple<int, EncounterReaction>>(); //populate once, then used on the encounter instance
        public List<string>? Hints { get; set; } = new List<string>();
        public required EncounterReaction FailureReaction { get; set; }
        public List<string>? FlavorText { get; set; } = new List<string>();
        public override string RunEncounter(GameSession gameSession)
        {
            Sprite currentSprite = new Sprite(Sprite);
            gameSession._displayEngine.UpdateSprite(currentSprite);
            List<string> options = CleanOptions(); //overridden
            DisplayMessages(gameSession._messages); 
            GenerateWins(); //new method, randomly 
            gameSession._options.ReplaceOptions(options);
            string encounterChoice = gameSession._userInput
                .ManageInputSelection(options, gameSession._messages);

            return ProcessEncounterReactions(gameSession._messages, encounterChoice);
        }
        internal void GenerateWins()
        {
            Random _rand = new Random();
            if (CorrectGuesses.Count == 0) //This ensures guess consistency if the same encounter is tried multiple times
            {
                
                CorrectGuesses = new List<Tuple<int, EncounterReaction>>();
                //If there are multiple reactions, then each receives it's own unique number
                foreach (EncounterReaction outcome in base.EncounterReactions)
                {
                    int guess;
                    do { guess = _rand.Next(MinimumGuess, MaximumGuess); } 
                    while (CorrectGuesses.Where(items => items.Item1 == guess).Count()>0);
                    var tuple = Tuple.Create(guess, outcome);
                    CorrectGuesses.Add(tuple);
                }
            }
        }
        internal override List<string> CleanOptions()
        {
            List<string> options =
                    Enumerable.Range(MinimumGuess, MaximumGuess).Select(number => number.ToString()).ToList<string>();
            return options;
        }
        internal override string ProcessEncounterReactions(IMessages messageInterface, string encounterChoice)
        {
            //Re-run the encounter if the current result does not provide a next encounter. This allows for implicit event looping, which may cause some design issues
            string nextEncounter = this.EncounterName;
            EncounterReaction reaction = GetEncounter(encounterChoice);
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
        private string GetHint(string encounterChoice)
        {
            string hint = string.Empty;
            if (Hints != null) 
            { 
                int guessDistance = GetGuessDistance(encounterChoice);
                hint = Hints[guessDistance];
            }
            return hint;
        }
        private int GetGuessDistance(string encounterChoice)
        {
            int choice = Int32.Parse(encounterChoice); 
            List<int> guesses = CorrectGuesses.Select(item => item.Item1).ToList();
            int uncappedGuess = guesses.Select(number => Math.Abs(number - choice)).Min() - 1;
            int guessDistance = Math.Min(uncappedGuess, Hints.Count);
            return guessDistance;
        }
        private EncounterReaction GetEncounter(string encounterChoice)
        {
            EncounterReaction reaction = FailureReaction;
            if (GiveHint)
            {
                if (reaction.ReactionMessages == null) { reaction.ReactionMessages = new List<string>(); }
                string hint = GetHint(encounterChoice);
                reaction.ReactionMessages.Add(hint);
            }
            if (CorrectGuesses.Where(items => items.Item1.ToString() == encounterChoice).Count() > 0)
            {
                reaction = CorrectGuesses.Where(items => items.Item1.ToString() == encounterChoice).First().Item2;
            }
            return reaction;
        }
    }
}
