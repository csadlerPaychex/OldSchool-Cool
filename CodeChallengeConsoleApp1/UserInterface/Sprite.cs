using DiceRollGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class Sprite
    {
        public string SpriteName { get; private set; }
        public string[] TextArt { get; set; }

        public Sprite(string fileName) 
        {
            SpriteName = fileName;
            TextArt = File.ReadAllLines($@"Resources\{fileName}.txt");
        }
        public void DisplaySprite()
        {
             WriteSpriteFrame(TextArt, 0);
        }
        public async Task DisplaySprite(CancellationTokenSource token)
        {
            //If the call includes a cancellation token, it can be persisted. Otherwise, send to the draw once service.
            do { await WritePersistedSprite(TextArt, 0, token); } while (!token.IsCancellationRequested);
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
                else { Thread.Sleep(150); Console.Clear();  }
                
            }
        }
        //This is meant to persist sprites until disposed, while the other is meant to draw once.
        private async Task WritePersistedSprite(string[] frame, int delay, CancellationTokenSource token)
        {
            try
            {
                foreach (string line in frame)
                {
                    if (!line.Contains("NEXT_FRAME"))
                    {
                        Console.WriteLine(line);
                        await Task.Delay(delay, token.Token);
                    }
                    //Delay for 40 milliseconds to put FR at 25 per second
                    else { await Task.Delay(150, token.Token); Console.Clear(); }
                }
                return;
            }
            catch { return; }
        }
    }
}
