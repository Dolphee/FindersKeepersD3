using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Collections;
using Enigma.Memory;

namespace Enigma.D3.BattleNet
{
	public class GameAccount : MemoryObject
	{
		public const int SizeOf = 0x240; // 576

		public Ptr x000_Ptr_ { get { return Read<Ptr>(0x000); } } // 0x017E84D0
		public int x004_Text { get { return Read<int>(0x004); } } // iali
		public int x008_Text { get { return Read<int>(0x008); } } // 3D
		public Ptr x00C_Ptr_ { get { return Read<Ptr>(0x00C); } } // 0x02000002
		public int x010_Text { get { return Read<int>(0x010); } } // \+E
		public int _x014 { get { return Read<int>(0x014); } } // 0
		public int _x018 { get { return Read<int>(0x018); } } // 15
		public Ptr x01C_Ptr_ { get { return Read<Ptr>(0x01C); } } // 0x112CA410
		public Ptr x020_Ptr_ { get { return Read<Ptr>(0x020); } } // 0x10BCF7F0
		public int _x024 { get { return Read<int>(0x024); } } // 192
		public int _x028 { get { return Read<int>(0x028); } } // 24
		public Ptr x02C_Ptr_ { get { return Read<Ptr>(0x02C); } } // 0x10BCF7F0
		public Ptr x030_Ptr_ { get { return Read<Ptr>(0x030); } } // 0x012E9F00
		public int _x034 { get { return Read<int>(0x034); } } // 0
		[ArraySize(10)]
		public Vector<EntityId> x038_HeroIds { get { return Read<Vector<EntityId>>(0x038); } }
		[ArraySize(10)]
		public Map<int, HeroEntity> x070_Map { get { return Read<Map<int, HeroEntity>>(0x070); } } // { count = 10, entrySize = 360 }
		public Ptr x0E0_Ptr_ { get { return Read<Ptr>(0x0E0); } } // 0x01820358
		public int _x0E4 { get { return Read<int>(0x0E4); } } // 0
		public int _x0E8 { get { return Read<int>(0x0E8); } } // 0
		public int _x0EC { get { return Read<int>(0x0EC); } } // 905
		public Ptr x0F0_Ptr_ { get { return Read<Ptr>(0x0F0); } } // 0x112CB8D0
		public Ptr x0F4_Ptr_ { get { return Read<Ptr>(0x0F4); } } // 0x112CAC80
		public Ptr x0F8_Ptr_ { get { return Read<Ptr>(0x0F8); } } // 0x003DAC15
		public int _x0FC { get { return Read<int>(0x0FC); } }
		public int _x100 { get { return Read<int>(0x100); } }
		public int _x104 { get { return Read<int>(0x104); } }
		public int _x108 { get { return Read<int>(0x108); } }
		public int _x10C { get { return Read<int>(0x10C); } }
		public int _x110 { get { return Read<int>(0x110); } }
		public int _x114 { get { return Read<int>(0x114); } }
		public int _x118 { get { return Read<int>(0x118); } }
		public int _x11C { get { return Read<int>(0x11C); } }
		public int _x120 { get { return Read<int>(0x120); } }
		public int _x124 { get { return Read<int>(0x124); } }
		public int _x128 { get { return Read<int>(0x128); } }
		public int _x12C { get { return Read<int>(0x12C); } }
		public int _x130 { get { return Read<int>(0x130); } }
		public int _x134 { get { return Read<int>(0x134); } } // 0
		public int x138_QuestSnoId { get { return Read<int>(0x138); } } // 87700 (Quest: ProtectorOfTristram)
		public int _x13C { get { return Read<int>(0x13C); } } // 42
		public int _x140 { get { return Read<int>(0x140); } } // 268
		public int _x144 { get { return Read<int>(0x144); } } // -1
		public Ptr x148_Ptr_ { get { return Read<Ptr>(0x148); } } // 0x53933ADB
		public Ptr x14C_Ptr_ { get { return Read<Ptr>(0x14C); } } // 0x53933BF2
		public Ptr x150_Ptr_ { get { return Read<Ptr>(0x150); } } // 0x53933BFF
		public int _x154 { get { return Read<int>(0x154); } } // 0
		public int _x158 { get { return Read<int>(0x158); } }
		public int _x15C { get { return Read<int>(0x15C); } }
		public int _x160 { get { return Read<int>(0x160); } }
		public int _x164 { get { return Read<int>(0x164); } }
		public int _x168 { get { return Read<int>(0x168); } }
		public int _x16C { get { return Read<int>(0x16C); } }
		public int _x170 { get { return Read<int>(0x170); } }
		public int _x174 { get { return Read<int>(0x174); } }
		public int _x178 { get { return Read<int>(0x178); } }
		public int _x17C { get { return Read<int>(0x17C); } }
		public int _x180 { get { return Read<int>(0x180); } }
		public int _x184 { get { return Read<int>(0x184); } }
		public int _x188 { get { return Read<int>(0x188); } }
		public int _x18C { get { return Read<int>(0x18C); } }
		public int _x190 { get { return Read<int>(0x190); } } // 0
		public int _x194 { get { return Read<int>(0x194); } } // 0
		public int _x198 { get { return Read<int>(0x198); } } // 0
		public int _x19C { get { return Read<int>(0x19C); } } // 0
		public Ptr x1A0_Ptr_ { get { return Read<Ptr>(0x1A0); } } // 0x10BCFC00 (Field x110)
		public int x1A4_Text { get { return Read<int>(0x1A4); } } // tion
		public int x1A8_Text { get { return Read<int>(0x1A8); } } // Adde
		public int _x1AC { get { return Read<int>(0x1AC); } } // 100
		public Ptr x1B0_Ptr_ { get { return Read<Ptr>(0x1B0); } } // 0x10BCFCB0 (Field x1C0)
		public int _x1B4 { get { return Read<int>(0x1B4); } } // 0
		public int _x1B8 { get { return Read<int>(0x1B8); } } // 0
		public int _x1BC { get { return Read<int>(0x1BC); } } // 4
		public Ptr x1C0_Ptr_ { get { return Read<Ptr>(0x1C0); } } // 0x10BCFB10 (Field x020)
		public int _x1C4 { get { return Read<int>(0x1C4); } } // 0
		public Ptr x1C8_Ptr_ { get { return Read<Ptr>(0x1C8); } } // 0x10BCFC80 (Field x190)
		public int _x1CC { get { return Read<int>(0x1CC); } } // 128
		public int _x1D0 { get { return Read<int>(0x1D0); } } // 0
		public int _x1D4 { get { return Read<int>(0x1D4); } } // 0
		public int _x1D8 { get { return Read<int>(0x1D8); } } // 0
		public int _x1DC { get { return Read<int>(0x1DC); } } // 0
		public int _x1E0 { get { return Read<int>(0x1E0); } } // 0
		public int _x1E4 { get { return Read<int>(0x1E4); } } // 0
		public int _x1E8 { get { return Read<int>(0x1E8); } } // 0
		public int _x1EC { get { return Read<int>(0x1EC); } } // 0
		public int _x1F0 { get { return Read<int>(0x1F0); } } // 0
		public int _x1F4 { get { return Read<int>(0x1F4); } } // 0
		public int _x1F8 { get { return Read<int>(0x1F8); } } // 0
		public int _x1FC { get { return Read<int>(0x1FC); } } // 0
		public int _x200 { get { return Read<int>(0x200); } } // 0
		public int _x204 { get { return Read<int>(0x204); } } // 0
		public int _x208 { get { return Read<int>(0x208); } } // 0
		public int _x20C { get { return Read<int>(0x20C); } } // 0
		public int _x210 { get { return Read<int>(0x210); } } // 0
		public int _x214 { get { return Read<int>(0x214); } } // 0
		public int _x218 { get { return Read<int>(0x218); } } // 0
		public int _x21C { get { return Read<int>(0x21C); } } // 0
		public int _x220 { get { return Read<int>(0x220); } } // 0
		public int _x224 { get { return Read<int>(0x224); } } // 0
		public int _x228 { get { return Read<int>(0x228); } } // 0
		public int _x22C { get { return Read<int>(0x22C); } } // 0
		public int _x230 { get { return Read<int>(0x230); } } // 0
		public int _x234 { get { return Read<int>(0x234); } } // 0
		public int _x238 { get { return Read<int>(0x238); } } // 0
		public int _x23C { get { return Read<int>(0x23C); } } // 0
	}
}
