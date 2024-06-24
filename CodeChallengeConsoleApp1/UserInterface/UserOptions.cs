using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class UserOptions
    {
        public List<string> Options { get; private set; }
        public int DisplayLength { get; private set; }
        public UserOptions(List<string> options, int displayLength = 20)
        {
            DisplayLength = displayLength;
            Options = options.GetRange(0, DisplayLength);
        }
        public void ReplaceOptions(List<string> options) { Options = options.GetRange(0,DisplayLength); }
        public void ClearOptions() { Options.Clear(); }
    }
}
