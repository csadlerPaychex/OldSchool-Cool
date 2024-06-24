using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class UserMessages
    {
        public List<string> Messages = new List<string>();

        public UserMessages(List<string> messages)
        {
            Messages = messages;
        }

        public void AddMessage(string message, bool clear = false)
        {
            if (clear)
            {
                Messages.Clear();
            }
            Messages.Add(message);
        }
        public void AddMessages(List<string> messages, bool clear = false)
        {
            if (clear)
            {
                Messages.Clear();
            }
            Messages.AddRange(messages);
        }
        public void RemoveMessage(string message) { Messages.Remove(message); }
        public void Clear() { Messages.Clear(); }

    }
}
