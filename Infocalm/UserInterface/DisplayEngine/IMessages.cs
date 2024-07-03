using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal interface IMessages
    {
        public void AddMessage(string message, bool clear = false);
        public void AddMessages(List<string> messages, bool clear = false);
        public void RemoveMessage(string message);
        public void Clear();
        public List<string> GetMessages();
    }
}
