using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace UserInterface
{
    internal class UserInterface
    {

        public Sprite CurrentSprite { get; private set; }
        public ManagedInput CurrentInput { get; private set; }
        public UserMessages CurrentMessages { get; private set; }
        public UserOptions CurrentOptions { get; private set; }
        private readonly int DisplayLines;
        private readonly int SpriteWidth;
        public List<string[]> DisplayFrames { get; private set; } = new List<string[]>();
        public List<string> Messages { get; private set; } = new List<string>();
        public UserInterface(Sprite sprite, ManagedInput input, UserMessages messages, UserOptions options)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = sprite.DisplayLines;
            CurrentMessages = messages;
            CurrentOptions = options;
        }
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do { await WritePersistedDisplay(0, token); } while (!token.IsCancellationRequested);
            return;
        }
        private async Task WritePersistedDisplay(int delay, CancellationTokenSource token)
        {
            try
            {
                foreach (string[] frame in CurrentSprite.Frames)
                {
                    int i = 0;
                    foreach (string line in frame)
                    {
                        string optionLine = "";
                        try { optionLine = CurrentOptions.Options[i]; }
                        catch { }
                        Console.WriteLine(line + optionLine);
                        //await Task.Delay(delay, token.Token);
                        i++;
                    }
                    Console.WriteLine(new string('-', SpriteWidth));
                    foreach (string line in CurrentMessages.Messages)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine(CurrentInput.NewLine);

                    //Delay for 40 milliseconds to put FR at 25 per second
                    await Task.Delay(15, token.Token); 
                    Console.Clear(); 
                }
                return;
            }
            catch { return; }
        }
    }
}
