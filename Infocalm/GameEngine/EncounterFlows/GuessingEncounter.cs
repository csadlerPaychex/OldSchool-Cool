using GameEngine;
using Infocalm.UserInterface.DisplayEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace GameEngine
{
    internal class GuessingEncounter : Encounter
    {
        public bool GiveHint { get; set; } = false;
        public List<string>? Hints { get; set; } =new List<string>();
        public List<string> FlavorText { get; set; } = new List<string>(); 
        public override Encounter RunEncounter(IDisplayEngine displayEngine, IUserInput inputInterface, IMessages messageInterface, IOptions optionInterface)
        {
            return base.RunEncounter(displayEngine, inputInterface, messageInterface, optionInterface);
        }
    }
}
