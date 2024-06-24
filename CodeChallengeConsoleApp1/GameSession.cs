using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Services;
using UserInterface;

namespace DiceRollGame
{
    internal class GameSession
    {
        public string SessionGameType { get; private set; }
        public string GameState { get; private set; }
        //Currently there are two states, ACTIVE and INACTIVE
        private List<string> GameTypes { get; } = new List<string>() { "Guess The Roll" };

        public GameSession() 
        {
            SessionGameType = "";
            GameState = "ACTIVE";
        }
        public void PlayTheGame(UserInput input)
        {
            Console.WriteLine("Select from Available Game Types:");
            SessionGameType = input.UpdateInputSelectionList(GameTypes);
            if (SessionGameType == "Guess The Roll")
                { PlayGuessTheRoll(input); }
        }
        private async void PlayGuessTheRoll(UserInput input)
        {
            Console.WriteLine("****************");
            Console.WriteLine("*Guess The Roll*");
            Console.WriteLine("****************");
            Console.ReadKey(true);
            List<string> validGuesses = new List<string>() { "1","2","3","4","5","6"};
            List<string> options = new List<string>() { "Yes", "No" };
            var cancellationTokenSource = new CancellationTokenSource();
            Sprite rollingDice = new Sprite("RollingDice");
            ManagedInput managedInput = new ManagedInput();
            UserMessages userMessages = new UserMessages();
            UserOptions userOptions = new UserOptions(20);
            ManagedView diceRoll = new ManagedView(rollingDice, input);  
            UserInterface.UserInterface userInterface = new UserInterface.UserInterface(rollingDice, managedInput, userMessages, userOptions);
            //var guessTheRollDisplay = diceRoll.DisplayInterface(cancellationTokenSource);
            //var diceSprite = rollingDice.DisplaySprite(cancellationTokenSource);
            var displayScreen = userInterface.DisplayInterface(cancellationTokenSource);
            if (Console.ReadLine() == "e")
                cancellationTokenSource.Cancel();
            displayScreen.Wait(600000);
            do
            {
                //Console.WriteLine("Rolling the Dice");
                //Console.Clear();
                //do { rollingDice.DisplaySprite();  } while (!Console.KeyAvailable);
                DiceRoll currentRoll = new DiceRoll(1, 6);
                string correctGuess = currentRoll.TotalResult.ToString();
                //Console.WriteLine($"{correctGuess}");
                string currentGuess = "";
                int remainingGuesses = 3;
                
                do
                {
                    Console.WriteLine($"You have {remainingGuesses} guesses");
                    Console.WriteLine("Make a guess");
                    currentGuess = input.UpdateInputSelectionList(validGuesses);
                    remainingGuesses--;
                } while (currentGuess != correctGuess && remainingGuesses > 0);
                bool victory =  currentGuess == correctGuess;
                if (victory)
                {
                    Console.WriteLine("You Win!!!!");
                    Console.WriteLine($"Score: {remainingGuesses}");
                }
                else 
                {
                    Console.WriteLine("So Sorry, you FAILED!!!!");
                }
                Console.WriteLine("");
                Console.WriteLine("************");
                Console.WriteLine("Play Again?");
                string playAgain = input.UpdateInputSelectionList(options);
                if (playAgain == "Yes")
                {
                    PlayGuessTheRoll(input);
                }
                else
                {
                    GameState = "INACTIVE";
                }

            } while (GameState == "ACTIVE" );
        }
        
    }
}
