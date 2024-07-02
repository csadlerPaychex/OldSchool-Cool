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
        public readonly GameFlow _gameFlow;
        public readonly IDisplayEngine _displayEngine;
        public readonly IUserInput _userInput;
        public readonly IOptions _options;
        public readonly IMessages _messages;
        public GameSession(GameFlow gameFlow, IDisplayEngine displayEngine, IUserInput userInput, IOptions options, IMessages messages)
        {
            _gameFlow = gameFlow;
            _displayEngine = displayEngine;
            _userInput = userInput;
            _options = options;
            _messages = messages;
        }
    }
}
