using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    internal interface IOptions
    {
        public void ReplaceOptions(List<string> options);
        public void ClearOptions();
        public List<string> GetOptions();
    }
}
