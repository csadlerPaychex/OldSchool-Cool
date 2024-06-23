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
            var cancellationTokenSource = new CancellationTokenSource();
            UserInput currentInput  = new UserInput();

            var bannerRun = IntroBanner(cancellationTokenSource, banner);
            if (Console.ReadKey(true).ToString() != "" )
                cancellationTokenSource.Cancel();
            string overallState = "ON";
            List<string> exitOptions = new List<string>() { "New", "Exit"};
            bannerRun.Wait(5000);
            do {

                GameSession currentSession = new GameSession();
                Console.WriteLine("Starting Game");
                currentSession.PlayTheGame(currentInput);
                Console.WriteLine("Session Over");
                Console.WriteLine("Start a New Session?");
                overallState = currentInput.UpdateInputSelectionList(exitOptions);
            } while (overallState != "Exit" );
            static async Task IntroBanner(CancellationTokenSource cancellationToken, Sprite banner)
            {
                do
                {
                    try
                    {
                        banner.RollSprite(200);
                        await Task.Delay(1000, cancellationToken.Token);
                        Console.Clear();
                        await Task.Delay(500, cancellationToken.Token);
                    }
                    catch (OperationCanceledException)
                    {
                       return;
                    }
                    
                } while (!cancellationToken.IsCancellationRequested);
                return;
            }
        }
    }
}
