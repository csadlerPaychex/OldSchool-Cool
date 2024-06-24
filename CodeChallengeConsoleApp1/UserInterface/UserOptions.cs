using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollGame.UserInterface
{
    internal class UserOptions
    {
        public string[] Options { get; set; }
        public UserOptions(string[] options)
        {
            Options = options;
        }
        public void RefreshOptions(string[] options)
        {
            Options = options;
        }
    }
}
