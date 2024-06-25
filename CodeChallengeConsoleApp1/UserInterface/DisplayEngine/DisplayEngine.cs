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
        //Current Needs; Method to point to new sprite (needed in the case multiple sprites need to persist)

        public Sprite CurrentSprite { get; private set; }
        public ManagedInput CurrentInput { get; private set; }
        public UserMessages CurrentMessages { get; private set; }
        public UserOptions CurrentOptions { get; private set; }
        //These will currently break if a sprite is updated to different size
        private readonly int DisplayLines;
        private readonly int SpriteWidth;
        public DisplayEngine(Sprite sprite, ManagedInput input, UserMessages messages, UserOptions options)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = sprite.DisplayLines;
            SpriteWidth = sprite.DisplayWidth;
            CurrentMessages = messages;
            CurrentOptions = options;
        }
        //Invoke to create a persistent display interface.
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do 
            { 
                foreach (string[] frame in CurrentSprite.Frames)
                {
                    if (!token.IsCancellationRequested)
                    {
                        Console.Clear();
                        WriteDisplayFrame(0, frame);
                        await Task.Delay(250);
                    }
                }
                
            } while (!token.IsCancellationRequested);
            return;
        }
        private void WriteDisplayFrame(int delay, string[] frame)
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
