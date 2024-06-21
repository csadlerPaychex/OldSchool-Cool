using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DiceRollGame
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("*Welcome to the Game Room!*");
            Console.WriteLine("***************************");
            Console.WriteLine();

            UserInterface currentInput  = new UserInterface();
            string overallState = "ON";
            List<string> exitOptions = new List<string>() { "New", "Exit"};
            do {

                GameSession currentSession = new GameSession(currentInput);
                Console.WriteLine("Starting Game");
                currentSession.PlayTheGame(currentSession, currentInput);
                Console.WriteLine("Session Over");
                Console.WriteLine("Start a New Session?");
                overallState = currentInput.UpdateInputSelectionList(exitOptions);
            } while (overallState != "Exit" );

        }
    }
}
