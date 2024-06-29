using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface;

namespace Infocalm.UserInterface.DisplayEngine
{
    internal interface IDisplayEngine
    {
        Task DisplayInterface(CancellationTokenSource token);
        void UpdateSprite(Sprite sprite);
    }
}
