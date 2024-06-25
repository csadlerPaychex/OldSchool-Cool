using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace UserInterface
{
    internal class DisplayEngine
    {

        public Sprite CurrentSprite { get; private set; }
        public ManagedInput CurrentInput { get; private set; }
        public UserMessages CurrentMessages { get; private set; }
        public UserOptions CurrentOptions { get; private set; }
        private readonly int DisplayLines;
        private readonly int SpriteWidth;
        public List<string[]> DisplayFrames { get; private set; } = new List<string[]>();
        public List<string> Messages { get; private set; } = new List<string>();
        public DisplayEngine(Sprite sprite, ManagedInput input, UserMessages messages, UserOptions options)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = sprite.DisplayLines;
            SpriteWidth = sprite.DisplayWidth;
            CurrentMessages = messages;
            CurrentOptions = options;
        }
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do 
            { 
                foreach (string[] frame in CurrentSprite.Frames)
                {
                    Console.Clear();
                    WriteFrame(0, frame);
                    await Task.Delay(150);
                }
                
            } while (!token.IsCancellationRequested);
            return;
        }
        private void WriteFrame(int delay, string[] frame)
        {
            int i = 0;
            foreach (string line in frame)
            {
                string optionLine = "";
                try { optionLine = CurrentOptions.Options[i]; }
                catch { }
                Console.WriteLine(line + "|" + optionLine);
                Thread.Sleep(delay);
                i++;
            }
            Console.WriteLine(new string('-', SpriteWidth));
            foreach (string line in CurrentMessages.Messages)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(CurrentInput.NewLine);
        }
    }
}
