using DiceRollGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class Sprite
    {
        public string SpriteName { get; private set; }
        public string[] TextArt { get; private set; }
        public List<string[]> Frames { get; private set; } = new List<string[]>();
        //Dictates # of lines to be used in a sprite display
        public readonly int DisplayLines;
        public readonly int DisplayWidth;

        public Sprite(string fileName, int displayLines = 20, int displayWidth = 60)
        {
            SpriteName = fileName;
            DisplayLines = displayLines;
            DisplayWidth = displayWidth;
            TextArt = File.ReadAllLines($@"Resources\{fileName}.txt");
            RenderSprites(TextArt, 0);
        }
        public void DisplaySprite()
        {
            //Write once, used for static in line displays.
            WriteSpriteFrame(TextArt, 0);
        }
        public async Task DisplaySprite(CancellationTokenSource token)
        {
            //If the call includes a cancellation token, it can be persisted. Otherwise, send to the draw once service.
            do { await WritePersistedSprite(0, token); } while (!token.IsCancellationRequested);
            return;
        }
        public void RollSprite(int speed)
        {
            WriteSpriteFrame(TextArt, speed);
        }
        private void WriteSpriteFrame(string[] frame, int delay)
        {
            foreach (string line in frame)
            {
                if (!line.Contains("NEXT_FRAME"))
                {
                    Console.WriteLine(line);
                    Thread.Sleep(delay);
                }
                //Delay for 40 milliseconds to put FR at 25 per second
                else { Thread.Sleep(150); Console.Clear(); }

            }
        }
        private void RenderSprites(string[] textArt, int cursor)
        {
            int frameCursor = cursor + 1;
            int nextFrameCursor = textArt.ToList().FindIndex(cursor, item => item.Contains("NEXT_FRAME"));
            bool moreFrames = nextFrameCursor != -1;
            if (!moreFrames) { nextFrameCursor = textArt.Length; }
            List<string> currentSprite = textArt[cursor..nextFrameCursor].ToList();
            int spriteLines = currentSprite.Count;

            //If sprite is larger than range, cut off sprite
            if (spriteLines > DisplayLines)
            {
                currentSprite.RemoveRange(DisplayLines, spriteLines - DisplayLines);
                spriteLines = DisplayLines;
            }

            if (spriteLines < DisplayLines) { currentSprite.AddRange(Enumerable.Repeat("", DisplayLines - spriteLines)); }

            //Set sprite width to display width
            for (int i = 0; i < spriteLines; i++)
            {
                string line = currentSprite[i];
                if (line.Length < 0) { currentSprite[i] = new string(' ', DisplayWidth); }
                else if (line.Length < DisplayWidth) { currentSprite[i] += new string(' ', DisplayWidth - line.Length); }
                else if (line.Length > DisplayWidth) { currentSprite[i] = line[..DisplayWidth]; }
                else { currentSprite[i] = line; }
            }

            string[] renderedFrame = new string[DisplayLines];
            renderedFrame = currentSprite.ToArray();
            Frames.Add(renderedFrame);
            if (moreFrames)
                RenderSprites(textArt, nextFrameCursor + 1);
        }
        //This is meant to persist sprites until disposed, while the other is meant to draw once. 
        private async Task WritePersistedSprite(int delay, CancellationTokenSource token)
        {
            try
            {
                foreach (string[] frame in Frames)
                {
                    foreach (string line in frame)
                    {
                        Console.WriteLine(line);
                        await Task.Delay(delay, token.Token);

                    }
                    //Delay for 40 milliseconds to put FR at 25 per second
                    { await Task.Delay(150, token.Token); Console.Clear(); }
                }
                return;
            }
            catch { return; }
        }
    }
}