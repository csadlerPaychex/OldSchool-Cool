using GameEngine;
using Infocalm.UserInterface.DisplayEngine;
using UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    //Ties together game assets used by a specific session to ensure all methods 
    //update the same set of assets. The session will be passed to methods called when running a game flow or encounter
    internal class GameSession
    {
        public GameFlow GameFlow {  get; private set; }
        public IDisplayEngine DisplayEngine { get; private set; }
        public IUserInput UserInput { get; private set; }
        public IOptions Options { get; private set; }
        public IMessages Messages { get; private set; }
    }
}
