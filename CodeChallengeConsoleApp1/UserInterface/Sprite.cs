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
            foreach (string line in TextArt) { Console.WriteLine(line); }
        }
    }

}
