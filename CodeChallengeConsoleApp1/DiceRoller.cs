using DiceRollGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollGame
{
    internal class Die
    {
        public int DieSides { get; private set; }
        public Die(int sides) { DieSides = sides; }
    }
    internal class DiceRoll
    {
        private readonly Random _rand = new Random();
        public int NumberOfDice { get; private set; }
        public Die DieType { get; private set; }
        public int TotalResult { get; private set; }
        public List<int> RollResults { get; private set; }

        //Constructors to set up the dice roll
        public DiceRoll(int numberOfDice, Die die)
        {
            NumberOfDice = numberOfDice;
            DieType = die;
            (int, List<int>) outcome = RollTheDice(die, numberOfDice);
            TotalResult = outcome.Item1;
            RollResults = outcome.Item2;
        }
        public DiceRoll(int numberOfDice, int diceSides)
        {
            NumberOfDice = numberOfDice;
            var die = new Die(diceSides);
            DieType = die;
            (int, List<int>) outcome = RollTheDice(die, numberOfDice);
            TotalResult = outcome.Item1;
            RollResults = outcome.Item2;
        }

        //Method to separate rolling the act of rolling the dice from the act of selecting the dice to roll
        private(int, List<int> ) RollTheDice(Die die, int totalRolls)
        {
            int sides = die.DieSides;
            int rollsRemaining = totalRolls;
            List<int> RollResults = new List<int>(0);
            int result = 0;
            do
            {
                int rollOutcome = _rand.Next(1, sides);
                RollResults.Add(rollOutcome);
                result = result + rollOutcome;
                rollsRemaining--;

            } while (rollsRemaining > 0);

            return (result, RollResults);
        }
    }
}


