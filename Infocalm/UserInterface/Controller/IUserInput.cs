using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace UserInterface
{
    internal interface IUserInput
    {
        public string ManageInputSelection(List<string> selections, IMessages messages);
        public string GetNewLine();
    }
}
