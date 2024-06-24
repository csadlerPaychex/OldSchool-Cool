using DiceRollGame.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class ManagedView
    {
        public Sprite CurrentSprite { get; private set; }
        public UserInput CurrentInput { get; private set; }
        public UserMessages CurrentMessages { get; private set; }
        //public UserOptions CurrentOptions { get; private set; }
        private readonly int DisplayLines;
        private static readonly int SpriteWidth = 60;
        public List<string[]> DisplayFrames { get; private set; } = new List<string[]>();
        public List<string> Messages { get; private set; } = new List<string>();
        public ManagedView(Sprite sprite, UserInput input)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = 20;
            CurrentMessages = new UserMessages([""]); ;
            //CurrentOptions = new UserOptions([""]);
            RenderDisplayFrames(CurrentSprite, CurrentInput);
        }
        public ManagedView(Sprite sprite, UserInput input, UserMessages messages, UserOptions options)
        {
            CurrentSprite = sprite;
            CurrentInput = input;
            DisplayLines = 20;
            CurrentMessages = messages;
            //CurrentOptions = options;
            RenderDisplayFrames(CurrentSprite, CurrentInput);
        }
        public async Task DisplayInterface(CancellationTokenSource token)
        {
            do { await WritePersistedDisplay(0, token, CurrentInput); } while (!token.IsCancellationRequested);
            return;
        }
        public async Task DisplayInterface(CancellationTokenSource token, UserInput input)
        {
            do { await WritePersistedDisplay(0, token, input); } while (!token.IsCancellationRequested);
            return;
        }
        public void DisplayMessage(string message, bool clearMessages)
        {
            if (clearMessages) { Messages.Clear(); }
            Messages.Add(message);
            RenderDisplayFrames(CurrentSprite, CurrentInput);
        }
        private async Task WritePersistedDisplay(int delay, CancellationTokenSource token, UserInput input)
        {
            try
            {
                foreach (string[] frame in DisplayFrames)
                {
                    foreach (string line in frame)
                    {
                        Console.WriteLine(line);
                        await Task.Delay(delay, token.Token);

                    }
                    foreach (string line in input.MessageDisplay)
                    {
                        Console.WriteLine(line);
                    }

                    //Delay for 40 milliseconds to put FR at 25 per second
                    { await Task.Delay(150, token.Token); Console.Clear(); }
                }
                return;
            }
            catch { return; }
        }
        //This writes each display frame so that the selections are included on the right side. 
        private void RenderDisplayFrames(Sprite currentSprite, UserInput currentInput)
        {

            foreach (string[] frame in currentSprite.Frames)
            {
                int messageCount = Messages.Count;
                string[] displayFrame = new string[DisplayLines + messageCount + 1];
                displayFrame[DisplayLines] = new string('_', SpriteWidth);
                for (int i = 0; i < DisplayLines; i++)
                {
                    int lineLength = frame[i].Length;
                    if (lineLength > SpriteWidth)
                        frame[i] = frame[i].Substring(0, SpriteWidth) + "|" + currentInput.SelectionsDisplay[i];
                    else if (lineLength < SpriteWidth)
                    {
                        string whitespace = new string(' ', SpriteWidth - lineLength);
                        frame[i] = frame[i] + whitespace + "|" + currentInput.SelectionsDisplay[i];
                    }
                    else
                        frame[i] = frame[i] + "|" + currentInput.SelectionsDisplay[i];
                    displayFrame[i] = frame[i];
                }
                int counter = 1;
                foreach (string message in Messages)
                {
                    displayFrame[DisplayLines + counter] = message;
                    counter++;
                }
                DisplayFrames.Add(displayFrame);
            }

        }
    }
}
