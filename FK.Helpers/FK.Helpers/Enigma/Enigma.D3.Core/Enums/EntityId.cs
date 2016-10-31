using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.D3
{
    public struct EntityId
    {
        public ulong High64;
        public ulong Low64;

        public override string ToString()
        {
            if (High64 == 0)
                return Low64.ToString();
            return "0x" + High64.ToString("X16") + Low64.ToString("X16");
        }
    }
}

