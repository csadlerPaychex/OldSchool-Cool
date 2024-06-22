using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UserInterface;

namespace DiceRollGame
{
    internal class Program
    {
        public string path = Directory.GetCurrentDirectory();
        static void Main()
        {
            Sprite banner = new Sprite("Banner");
            
            do 
            {
                banner.DisplaySprite();
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            } while (!Console.KeyAvailable);

            UserInput currentInput  = new UserInput();
            string overallState = "ON";
            List<string> exitOptions = new List<string>() { "New", "Exit"};
            do {

                GameSession currentSession = new GameSession();
                Console.WriteLine("Starting Game");
                currentSession.PlayTheGame(currentInput);
                Console.WriteLine("Session Over");
                Console.WriteLine("Start a New Session?");
                overallState = currentInput.UpdateInputSelectionList(exitOptions);
            } while (overallState != "Exit" );

        }
    }
}
