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
            string nextEncounter = this.EncounterName;
            Sprite currentSprite = new Sprite(Sprite);
            gameSession._displayEngine.UpdateSprite(currentSprite);
            List<string> cleanedOptions = CleanOptions(gameSession._options);
            DisplayMessages(gameSession._messages);
            //gameSession._options.ReplaceOptions(cleanedOptions);


            GenerateWins();



            return nextEncounter;
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
        internal List<string> PopulateOptions()
        {
            List<string> numbers =
                    Enumerable.Range(MinimumGuess, MaximumGuess).Select(number => number.ToString()).ToList<string>();
            List<string> options = new List<string>();
            if (FlavorText == null)
            {
                options = numbers; 
            }
            else 
            {
                int range = MaximumGuess - MinimumGuess - 1;
                double calc = FlavorText.Count / range;
                int repeats = ((int)Math.Ceiling(calc)); //We are going to stretch the flavor text to enough new choices to populate every guess option
                List<string> flavorCopies = new List<string>();
                for (int i = 0; i < repeats; i++)
                {
                    flavorCopies.AddRange(FlavorText);
                }
                flavorCopies = flavorCopies.GetRange(0, range);
                int n = 0;
                foreach (string number in numbers) 
                {
                    numbers[n] = number + " " + flavorCopies[n];
                }
            }
            return options;
        }
    }
}
