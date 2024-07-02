using GameEngine;
using Infocalm.UserInterface.DisplayEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    internal class GuessingEncounter : Encounter
    {
        new public static string EncounterType = "GuessingEncounter";
        public int MinimumGuess {  get; set; }
        public int MaximumGuess { get; set; }
        public bool GiveHint { get; set; } = false;
        public List<int>? CorrectGuesses { get; private set; } //populate once, then used on the encounter instance
        public List<string>? Hints { get; set; } =new List<string>();
        public required List<EncounterReaction> FailureReactions { get; set; }
        public List<string>? FlavorText { get; set; } = new List<string>();
        public override string RunEncounter(GameSession gameSession)
        {
            Sprite currentSprite = new Sprite(Sprite);
            gameSession._displayEngine.UpdateSprite(currentSprite);
            List<string> cleanedOptions = CleanOptions(gameSession._options);
            DisplayMessages(gameSession._messages);
            GenerateWins();

        }
        internal void GenerateWins()
        {
            Random _rand = new Random();
            if (CorrectGuesses == null) //This ensures guess consistency if the same encounter is tried multiple times
            {
                
                CorrectGuesses = new List<int>();
                foreach (EncounterReaction outcome in base.EncounterReactions)
                {
                    CorrectGuesses.Add(_rand.Next(MinimumGuess, MaximumGuess));
                }
            }
        }
        internal List<string> PopulateOptions(GameSession gameSession)
        {
            List<string> options = new List<string>();
            if (FlavorText == null)
            {
                List<string> numbers = 
                    Enumerable.Range(MinimumGuess, MaximumGuess).Select(number => number.ToString()).ToList<string>();
                options = numbers; 
            }
            else 
            {
                double calc = FlavorText.Count / (MaximumGuess - MinimumGuess - 1);
                int repeats = ((int)Math.Ceiling(calc)); //
                List<string> flavorCopies = FlavorText;

                for (int i = MinimumGuess; i < MaximumGuess + 1; i++) 
                {
                    options.Add(FlavorText[i]);
                }
            }
            return options;
        }
    }
}
