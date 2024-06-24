using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal class UserInput
    {
        public string NewLine { get; private set; } = string.Empty;
        public string CurrentInput { get; private set; } = string.Empty;
        public string LastInput { get; private set; } = string.Empty;
        public string[] SelectionsDisplay { get; private set; } = new string[20];
        public List<string> MessageDisplay { get; private set; } = new List<string>();
        private readonly int DisplayLines;
        public UserInput(int displayLines = 20)
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
                Console.WriteLine(input);
                selectionValid = selections.Contains(input);
                if (!selectionValid)
                {
                    Console.WriteLine("Selection invalid, please choose from the options list");
                }
                CurrentInput = input;
            } while (!selectionValid);
            return input;
        }
        public string SelectUserInterfaceOption(List<string> selections, List<string> messages)
        {
            string input = "";
            bool selectionValid = false;
            foreach (string message in messages)
            {
                MessageDisplay.Add(message);
            }
            do
            {
                SelectionsDisplay[0] = "Choose from the following list";
                for (int i = 1; i < DisplayLines; i++)
                {
                    try { SelectionsDisplay[i] = selections[i - 1]; }
                    catch { SelectionsDisplay[i] = ""; }
                }
                input = gatherInput(true);
                selectionValid = selections.Contains(input);
                if (!selectionValid)
                {
                    MessageDisplay.Clear();
                    MessageDisplay.Add("Selection invalid, please choose from the options list");
                }
                CurrentInput = input;
            } while (!selectionValid);
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
                // if (!suppressOutput) Console.WriteLine("Enter Selection");
                //var keyPress = Console.ReadKey().ToString();
                input = Console.ReadKey(true).KeyChar.ToString() ; //Convert.ToChar(keyPress).ToString();
                if ( input == "\r")
                {
                    completedInput = inputLine;
                }
                else
                {
                    inputLine += input;

                    if (!suppressOutput) Console.Write(input);
                }
            } while (completedInput == "");
            return inputLine;
        }

    }
}