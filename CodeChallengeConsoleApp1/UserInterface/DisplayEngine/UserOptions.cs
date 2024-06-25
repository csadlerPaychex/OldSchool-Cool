using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class UserOptions
    {
        public List<string> Options { get; private set; } = new List<string>();
        public int DisplayLength { get; private set; }
        public UserOptions(int displayLength = 20)
        {
            DisplayLength = displayLength;
            //Options.AddRange(Enumerable.Repeat("", DisplayLength));  
        }
        public void ReplaceOptions(List<string> options) { Options.Add("User Options"); Options.AddRange(options); }
        public void ClearOptions() { Options = new List<string>(); }
    }
}
