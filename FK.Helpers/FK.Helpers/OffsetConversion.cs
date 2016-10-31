using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using Enigma.D3.Collections;
using Enigma.D3.UI;

namespace Enigma.D3
{
    public static class OffsetConversion
    {
        public static readonly Version SupportedVersion = new Version(2, 4, 2, 39192);

        public const int UXControlRect                  = 0x468;
        public const int UXControlMinimapRect           = 0x0C50;
        public const int ScreenManager                  = 0x1E31120;
        public const int ActiveSeason                   = 8;
        public const int BattleNetClient                = 0x10;
        public const int SelectedHeroes                 = 0x0A8;
        public const int HeroesData                     = 0x98;

        /* Enigma.D3 */
        public const int ApplicationLoopCount           = 0x1EA6148;
        public const int AttributeDescriptors           = 0x1EBF028;
        public const int BuffManager                    = 0x1E31F44; /* Not*/
        public const int LevelArea                      = 0x1E30B40;
        public const int LevelAreaName                  = 0x1EA6148; /* Invalid */
        public const int LocalData                      = 0x1EA7378;
        public const int ObjectManager                  = 0x1EA60D4;
        public const int TrickleManager                 = 0x1E7F904;
        public const int SnoGroupsByCode                = 0x1EA73B0;
        public const int SnoGroups                      = 0x1EA3FE4;
        public const int VideoPreferences               = 0x1C64510;
        public const int GameplayPreferences            = 0x1C64A74;
        [Obsolete("Offset not updated to latest version")]
        public const int UIHandlers                     = 0x1C28B20;/* Not*/
        public const int UIReference                    = 0x1C7C488;

        /* Counter */
        [Obsolete("Offset not updated to latest version")]
        public const int UIHandlerCount                 = 1257;/* Not*/
        [Obsolete("Offset not updated to latest version")]
        public const int UIReferenceCount               = 2767;/* Not*/
        public const int AttributeDescriptorsCount = ((0x1ECD194 - 0x1EBF044) / AttributeDescriptor.SizeOf) - 1;
    }
}
