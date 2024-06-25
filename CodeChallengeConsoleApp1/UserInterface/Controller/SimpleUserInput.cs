using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    //Helper class to allow for very simple console interaction while building. 
    internal class SimpleUserInput
    {
        public string NewLine { get; private set; } = string.Empty;
        public string CurrentInput { get; private set; } = string.Empty;
        public string LastInput { get; private set; } = string.Empty;
        public string[] SelectionsDisplay { get; private set; } = new string[20];
        public List<string> MessageDisplay { get; private set; } = new List<string>();
        private readonly int DisplayLines;
        public SimpleUserInput(int displayLines = 20)
        {
            CurrentInput = "";
            DisplayLines = displayLines;
        }
        //Provide a list of valid selections, and check against it.
        public string UpdateInputSelectionList(List<string> selections)
        {
            string input = "";
            bool selectionValid = false;
            do
            {
                Console.WriteLine("Choose from the following list");
                foreach (var selection in selections)
                {
                    Console.WriteLine($"{selection}");
                }
                input = gatherInput();
                selectionValid = selections.Contains(input);
                if (!selectionValid)
                {
                    Console.WriteLine("Selection invalid, please choose from the options list");
                }
                CurrentInput = input;
            } while (!selectionValid);
            return input;
        }
        private string gatherInput(bool suppressOutput = false)
        {
            LastInput = CurrentInput;
            string input = "";
            do
            {
                input = Console.ReadLine();
            } while (input == "" || input == null);
            return input;
        }

    }
}