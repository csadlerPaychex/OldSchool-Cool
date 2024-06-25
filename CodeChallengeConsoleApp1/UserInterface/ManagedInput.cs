using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class ManagedInput
    {
        public string NewLine { get; private set; } = string.Empty;
        public string CurrentInput { get; private set; } = string.Empty;
        public string LastInput { get; private set; } = string.Empty;
        public ManagedInput() { }
        public string ManageInputSelection(List<string> selections, UserMessages messages, UserOptions options)
            //Manages the input process by ensuring only valid input is allowed to collect in the CurrentInput property
        {
            string input = "";
            bool selectionValid = false;
            options.ReplaceOptions(selections);
            do
            {
                input = gatherInput();
                
                selectionValid = selections.Contains(input);
                if (!selectionValid)
                {
                    NewLine = "";
                    messages.AddMessage("Selection invalid, please choose from the options list");
                }
                CurrentInput = input;
            } while (!selectionValid);
            options.ClearOptions();
            return input;
        }
        private string gatherInput(bool suppressOutput = false)
        {
            LastInput = CurrentInput;
            string input = "";
            string inputLine = "";
            string completedInput = "";
            do
            {
                input = Console.ReadKey(true).KeyChar.ToString() ;
                if ( input == "\r")
                {
                    NewLine = "";
                    completedInput = inputLine;
                }
                else
                {
                    inputLine += input;
                    NewLine = inputLine;
                }
            } while (completedInput == "");
            return completedInput;
        }

    }
}