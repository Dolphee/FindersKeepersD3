using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindersKeepers.Controller.Enums
{
    public enum ItemQuality : short
    {
        NotSet = -1,
        Invalid = 0,
        Normal = 1,
        Magic = 3,
        Rare = 6,
        Legendary = 9,
        Set = 11
    }

    public enum ItemQualityShort : short
    {
        Normal = 1,
        Magic = 2,
        Rare = 3,
        Legendary = 4
    }

    public enum ItemQualityMinimap : uint
    {
        Invalid = 0,
        Legendary = 275968,
        SetItem = 404424,
        KeyWarden = 1562500,
    }
}
