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
        public UserOptions(List<string> options)
        {
            Options = options;
        }
           
    }
}
