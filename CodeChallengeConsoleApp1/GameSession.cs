using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Services;
using UserInterface;

namespace Infocalm
{
    internal class GameSession
    {
        public string SessionGameType { get; private set; }
        public string GameState { get; private set; }
        //Currently there are two states, ACTIVE and INACTIVE
        private List<string> GameTypes { get; } = new List<string>() { "Guess The Roll" };

        public GameSession() 
        {
            SessionGameType = "SESSION";
            GameState = "ACTIVE";
        }
        public async Task PlayTheGame()
        {
            Console.WriteLine("Select from Available Game Types:");

            SimpleUserInput simpleInput = new SimpleUserInput();
            SessionGameType = simpleInput.UpdateInputSelectionList(GameTypes);
            CancellationTokenSource sessionCanceled = new CancellationTokenSource();
            if (SessionGameType == "Guess The Roll")
                { Task game = PlayGuessTheRoll(sessionCanceled, simpleInput);  game.Wait(); }
            return;
        }
        private async Task PlayGuessTheRoll(CancellationTokenSource session, SimpleUserInput simpleInput)
        {
            Console.WriteLine("****************");
            Console.WriteLine("*Guess The Roll*");
            Console.WriteLine("****************");
            Console.ReadKey(true);
            List<string> validGuesses = new List<string>() { "1","2","3","4","5","6"};
            List<string> options = new List<string>() { "Yes", "No" };
            CancellationTokenSource gameCancelled = new CancellationTokenSource();
            Sprite rollingDice = new Sprite("RollingDice");
            ManagedInput managedInput = new ManagedInput();
            UserMessages userMessages = new UserMessages();
            UserOptions userOptions = new UserOptions(20);
            DisplayEngine userInterface = new DisplayEngine(rollingDice, managedInput, userMessages, userOptions);
            Task displayScreen = userInterface.DisplayInterface(gameCancelled);
            do
            {
                DiceRoll currentRoll = new DiceRoll(1, 6);
                string correctGuess = currentRoll.TotalResult.ToString();
                string currentGuess = "";
                int remainingGuesses = 3;
                
                do
                {
                    userMessages.AddMessage($"You have {remainingGuesses} guesses");
                    userMessages.AddMessage("Make a guess");
                    currentGuess = managedInput.ManageInputSelection(validGuesses, userMessages, userOptions);
                    remainingGuesses--;
                } while (currentGuess != correctGuess && remainingGuesses > 0);
                bool victory =  currentGuess == correctGuess;
                if (victory)
                {
                    userMessages.Clear();
                    userMessages.AddMessage("You Win!!!!");
                    userMessages.AddMessage($"Score: {remainingGuesses}");
                }
                else 
                {
                    userMessages.Clear();
                    userMessages.AddMessage("So Sorry, you FAILED!!!!");
                }
                gameCancelled.Cancel();
                await displayScreen;
                Console.ReadKey(true);
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("************");
                
                Console.WriteLine("Play Again?");
                string playAgain = simpleInput.UpdateInputSelectionList(options);
                if (playAgain == "Yes")
                {
                    await PlayGuessTheRoll(session, simpleInput);
                }
                else
                {
                    GameState = "INACTIVE";
                    session.Cancel();
                }  

            } while (GameState == "ACTIVE" );
            return;
        }
        
    }
}
