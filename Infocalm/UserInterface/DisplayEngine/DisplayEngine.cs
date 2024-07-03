using Infocalm.UserInterface.DisplayEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace UserInterface
{
    internal class DisplayEngine : IDisplayEngine
    {
        //Current Needs; Method to point to new sprite (needed in the case multiple sprites need to persist)

        public Sprite CurrentSprite { get; private set; }
        public IUserInput _userInput { get; private set; }
        public IMessages _messages { get; private set; }
        public IOptions _options { get; private set; }
        //These will currently break if a sprite is updated to different size
        private readonly int DisplayLines;
        private readonly int SpriteWidth;
        private static readonly int FrameDelay = 100;

        public DisplayEngine(Sprite sprite, IUserInput input, IMessages messages, IOptions options)
        {
            CurrentSprite = sprite;
            _userInput = input;
            DisplayLines = sprite.DisplayLines;
            SpriteWidth = sprite.DisplayWidth;
            _messages = messages;
            _options = options;
        }
        //Use these to ensure sprite and message updates do not break write process
        private struct Frames { public List<string[]> frames;}
        //Invoke to create a persistent display interface.
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do 
            {
                try
                {
                    //Allows the sprite to be changed out mid write without cancelling the display
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
            List<string> optionLines = _options.GetOptions();
            List<string>  messages = _messages.GetMessages();
            int i = 0;
            foreach (string line in frame)
            {
                string optionLine = "";
                try { optionLine = optionLines[i]; }
                catch { }
                Console.WriteLine(line + "|" + optionLine);
                Thread.Sleep(delay);
                i++;
            }
            Console.WriteLine(new string('-', SpriteWidth));
            foreach (string line in messages)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(_userInput.GetNewLine());
        }
    }
}
