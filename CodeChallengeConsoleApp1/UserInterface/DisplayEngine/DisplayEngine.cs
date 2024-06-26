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
        private static readonly int FrameDelay = 100;

        public DisplayEngine(Sprite sprite, ManagedInput input, UserMessages messages, UserOptions options)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = sprite.DisplayLines;
            SpriteWidth = sprite.DisplayWidth;
            CurrentMessages = messages;
            CurrentOptions = options;
        }
        //Use these to ensure sprite and message updates do not break write process
        private struct Frames { public List<string[]> frames;}
        private struct Options { public List<string> options; }
        private struct Messages { public List<string> messages; }
        //Invoke to create a persistent display interface.
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do 
            {
                try
                {
                    Frames spriteFrames = new Frames();
                    spriteFrames.frames = CurrentSprite.Frames;
                    foreach (string[] frame in spriteFrames.frames)
                    {
                        if (!token.IsCancellationRequested && spriteFrames.frames == CurrentSprite.Frames)
                        {
                            Console.Clear();
                            WriteDisplayFrame(0, frame);
                            await Task.Delay(FrameDelay);
                        }
                    }
                }
                catch { }
                
            } while (!token.IsCancellationRequested);
            return;
        }
        //This is the normal avenue for changing the sprite shown in the display engine.
        public void UpdateSprite(Sprite sprite)
        {
            CurrentSprite = sprite;
        }
        private void WriteDisplayFrame(int delay, string[] frame)
        {
            //establish structs here in case list size of base object changes mid write
            Options optionLines = new Options();
            optionLines.options = CurrentOptions.Options;
            Messages messages = new Messages();
            messages.messages = CurrentMessages.Messages;
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
