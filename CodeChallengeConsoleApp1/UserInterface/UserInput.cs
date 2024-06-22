using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class UserInput
    {
        public string CurrentInput { get; private set; } = string.Empty;
        public string LastInput { get; private set; } = string.Empty;
        public UserInput()
        {
            CurrentInput = "";
        }
        //Proive a list of valid selections, and check against it.
        public string UpdateInputSelectionList(List<string> selections)
        {
            string input = "";
            bool selectionValid = false;
            do
            {
                foreach (var selection in selections)
                {
                    Console.WriteLine($"{selection}");
                }
                input = gatherInput();
                selectionValid = selections.Contains(input);
                if (!selectionValid)
                {
                    Console.WriteLine("Selection invalid, please choose from the following list");
                }
                CurrentInput = input;
            } while (!selectionValid);
            return input;
        }
        private string gatherInput()
        {
            LastInput = CurrentInput;
            var input = "";
            do { Console.WriteLine("Enter Selection"); input = Console.ReadLine(); } while (input == "" || input == null);
            return input;
        }

    }
}
