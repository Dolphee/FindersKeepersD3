using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.D3.BattleNet;
using Enigma.D3.Collections;
using Enigma.D3.Memory;
using Enigma.D3.UI;

namespace Enigma.D3
{
	[Version("2.1.0.26451")]
	public partial class ScreenManager : MemoryObject
	{
		public const int SizeOf = 0x20;

		public Ptr<Struct00> x00_Ptr_2008Bytes { get { return ReadPointer<Struct00>(0x00); } }
		public Ptr<Struct04> x04_Ptr_440Bytes { get { return ReadPointer<Struct04>(0x04); } }
		public Ptr<Struct08> x08_Ptr_72Bytes { get { return ReadPointer<Struct08>(0x08); } }
		public Ptr<Struct0C> x0C_Ptr_400Bytes { get { return ReadPointer<Struct0C>(0x0C); } }
		public Ptr<BattleNetClient> x10_BattleNetClient { get { return ReadPointer<BattleNetClient>(0x10); } }
		public Ptr<ModalNotificationManager> x14_ModalNotificationManager { get { return ReadPointer<ModalNotificationManager>(0x14); } }
		public Ptr<Struct18> x18_Ptr_2080Bytes { get { return ReadPointer<Struct18>(0x18); } }
		public Ptr<Struct1C> x1C_Ptr_1168Bytes { get { return ReadPointer<Struct1C>(0x1C); } }

		#region SubStructures
		public class Struct00 : MemoryObject
		{
			public const int SizeOf = 0x7D8; // 2008

			public int _x000 { get { return Read<int>(0x000); } } // 0
			public Ptr x004_Ptr_ { get { return Read<Ptr>(0x004); } } // 0x102AC5A0
			public int _x008 { get { return Read<int>(0x008); } } // 0
			public int _x00C { get { return Read<int>(0x00C); } } // 0
			public int _x010 { get { return Read<int>(0x010); } } // 0
			public int _x014 { get { return Read<int>(0x014); } } // 0
			public Ptr x018_Ptr_ { get { return Read<Ptr>(0x018); } } // 0x102AC5D0
			public int _x01C { get { return Read<int>(0x01C); } } // 0
			public Ptr x020_Ptr_ { get { return Read<Ptr>(0x020); } } // 0x0175D1C0
			public int _x024 { get { return Read<int>(0x024); } } // 0
			public Ptr x028_Ptr_ { get { return Read<Ptr>(0x028); } } // 0x102AC6A0
			public Ptr x02C_Ptr_ { get { return Read<Ptr>(0x02C); } } // 0x102AC7D0
			public int _x030 { get { return Read<int>(0x030); } } // 1
			public int _x034 { get { return Read<int>(0x034); } } // -1
			public int _x038 { get { return Read<int>(0x038); } } // 0
			public int _x03C { get { return Read<int>(0x03C); } } // 0
			public int _x040 { get { return Read<int>(0x040); } } // -1
			public int _x044 { get { return Read<int>(0x044); } } // 0
			public int _x048 { get { return Read<int>(0x048); } } // 0
			public int _x04C { get { return Read<int>(0x04C); } } // 1
			public Ptr x050_Ptr_ { get { return Read<Ptr>(0x050); } } // 0x5D4EF97F
			public int _x054 { get { return Read<int>(0x054); } } // 1
			public int _x058 { get { return Read<int>(0x058); } } // 0
			public int _x05C { get { return Read<int>(0x05C); } } // -1
			public Ptr x060_Ptr_ { get { return Read<Ptr>(0x060); } } // 0x4E90AACC
			public int _x064 { get { return Read<int>(0x064); } } // 0
			public int _x068 { get { return Read<int>(0x068); } } // 0
			public int _x06C { get { return Read<int>(0x06C); } } // -1
			public Ptr x070_Ptr_ { get { return Read<Ptr>(0x070); } } // 0x73A6E4FB
			public int _x074 { get { return Read<int>(0x074); } } // 0
			public int _x078 { get { return Read<int>(0x078); } } // 0
			public int _x07C { get { return Read<int>(0x07C); } } // -1
			public int _x080 { get { return Read<int>(0x080); } } // -1533912119
			public int _x084 { get { return Read<int>(0x084); } } // 15
			public int _x088 { get { return Read<int>(0x088); } } // 0
			public int _x08C { get { return Read<int>(0x08C); } } // -1
			public int _x090 { get { return Read<int>(0x090); } } // -1511364167
			public int _x094 { get { return Read<int>(0x094); } } // 0
			public int _x098 { get { return Read<int>(0x098); } } // 0
			public int _x09C { get { return Read<int>(0x09C); } } // -1
			public int _x0A0 { get { return Read<int>(0x0A0); } } // -1511328230
			public int _x0A4 { get { return Read<int>(0x0A4); } } // 0
			public int _x0A8 { get { return Read<int>(0x0A8); } } // 0
			public int _x0AC { get { return Read<int>(0x0AC); } } // -1
			public Ptr x0B0_Ptr_ { get { return Read<Ptr>(0x0B0); } } // 0x15C8F8D2
			public int _x0B4 { get { return Read<int>(0x0B4); } } // 2
			public int _x0B8 { get { return Read<int>(0x0B8); } } // 0
			public int _x0BC { get { return Read<int>(0x0BC); } } // -1
			public Ptr x0C0_Ptr_ { get { return Read<Ptr>(0x0C0); } } // 0x313BE833
			public int _x0C4 { get { return Read<int>(0x0C4); } } // 0
			public int _x0C8 { get { return Read<int>(0x0C8); } } // 0
			public int _x0CC { get { return Read<int>(0x0CC); } } // -1
			public int _x0D0 { get { return Read<int>(0x0D0); } } // 0
			public int _x0D4 { get { return Read<int>(0x0D4); } } // 0
			public int _x0D8 { get { return Read<int>(0x0D8); } } // -1
			public int _x0DC { get { return Read<int>(0x0DC); } } // 0
			public int _x0E0 { get { return Read<int>(0x0E0); } } // 0
			public int _x0E4 { get { return Read<int>(0x0E4); } } // -1
			public int _x0E8 { get { return Read<int>(0x0E8); } } // -1
			public int _x0EC { get { return Read<int>(0x0EC); } } // 0
			public int _x0F0 { get { return Read<int>(0x0F0); } } // 0
			public int _x0F4 { get { return Read<int>(0x0F4); } } // 0
			public int _x0F8 { get { return Read<int>(0x0F8); } } // -1
			public int _x0FC { get { return Read<int>(0x0FC); } } // 0
			public int _x100 { get { return Read<int>(0x100); } } // 0
			public int _x104 { get { return Read<int>(0x104); } } // 0
			public int _x108 { get { return Read<int>(0x108); } } // -1
			public int _x10C { get { return Read<int>(0x10C); } } // 0
			public int _x110 { get { return Read<int>(0x110); } } // 0
			public int _x114 { get { return Read<int>(0x114); } } // 0
			public int _x118 { get { return Read<int>(0x118); } } // -1
			public int _x11C { get { return Read<int>(0x11C); } } // 0
			public int _x120 { get { return Read<int>(0x120); } } // 0
			public int _x124 { get { return Read<int>(0x124); } } // 0
			public int _x128 { get { return Read<int>(0x128); } } // -1
			public int _x12C { get { return Read<int>(0x12C); } } // 0
			public int _x130 { get { return Read<int>(0x130); } } // 0
			public int _x134 { get { return Read<int>(0x134); } } // 0
			public int _x138 { get { return Read<int>(0x138); } } // -1
			public int _x13C { get { return Read<int>(0x13C); } } // 0
			public int _x140 { get { return Read<int>(0x140); } } // 0
			public int _x144 { get { return Read<int>(0x144); } } // 0
			public int _x148 { get { return Read<int>(0x148); } } // -1
			public int _x14C { get { return Read<int>(0x14C); } } // 0
			public int _x150 { get { return Read<int>(0x150); } } // 0
			public int _x154 { get { return Read<int>(0x154); } } // 0
			public int _x158 { get { return Read<int>(0x158); } } // -1
			public int _x15C { get { return Read<int>(0x15C); } } // 0
			public int _x160 { get { return Read<int>(0x160); } } // 0
			public int _x164 { get { return Read<int>(0x164); } } // 0
			public int _x168 { get { return Read<int>(0x168); } } // 0
			public int _x16C { get { return Read<int>(0x16C); } } // 0
			public int _x170 { get { return Read<int>(0x170); } } // -1
			public int _x174 { get { return Read<int>(0x174); } } // 0
			public int _x178 { get { return Read<int>(0x178); } } // 0
			public int _x17C { get { return Read<int>(0x17C); } } // -1
			public int _x180 { get { return Read<int>(0x180); } } // -1
			public int _x184 { get { return Read<int>(0x184); } } // 0
			public int _x188 { get { return Read<int>(0x188); } } // 0
			public int _x18C { get { return Read<int>(0x18C); } } // 0
			public int _x190 { get { return Read<int>(0x190); } } // -1
			public int _x194 { get { return Read<int>(0x194); } } // 0
			public int _x198 { get { return Read<int>(0x198); } } // 0
			public int _x19C { get { return Read<int>(0x19C); } } // 0
			public int _x1A0 { get { return Read<int>(0x1A0); } } // -1
			public int _x1A4 { get { return Read<int>(0x1A4); } } // 0
			public int _x1A8 { get { return Read<int>(0x1A8); } } // 0
			public int _x1AC { get { return Read<int>(0x1AC); } } // 0
			public int _x1B0 { get { return Read<int>(0x1B0); } } // -1
			public int _x1B4 { get { return Read<int>(0x1B4); } } // 0
			public int _x1B8 { get { return Read<int>(0x1B8); } } // 0
			public int _x1BC { get { return Read<int>(0x1BC); } } // 0
			public int _x1C0 { get { return Read<int>(0x1C0); } } // -1
			public int _x1C4 { get { return Read<int>(0x1C4); } } // 0
			public int _x1C8 { get { return Read<int>(0x1C8); } } // 0
			public int _x1CC { get { return Read<int>(0x1CC); } } // 0
			public int _x1D0 { get { return Read<int>(0x1D0); } } // -1
			public int _x1D4 { get { return Read<int>(0x1D4); } } // 0
			public int _x1D8 { get { return Read<int>(0x1D8); } } // 0
			public int _x1DC { get { return Read<int>(0x1DC); } } // 0
			public int _x1E0 { get { return Read<int>(0x1E0); } } // -1
			public int _x1E4 { get { return Read<int>(0x1E4); } } // 0
			public int _x1E8 { get { return Read<int>(0x1E8); } } // 0
			public int _x1EC { get { return Read<int>(0x1EC); } } // 0
			public int _x1F0 { get { return Read<int>(0x1F0); } } // -1
			public int _x1F4 { get { return Read<int>(0x1F4); } } // 0
			public int _x1F8 { get { return Read<int>(0x1F8); } } // 0
			public int _x1FC { get { return Read<int>(0x1FC); } } // 0
			public int _x200 { get { return Read<int>(0x200); } } // 0
			public int _x204 { get { return Read<int>(0x204); } } // 0
			public int _x208 { get { return Read<int>(0x208); } } // -1
			public int _x20C { get { return Read<int>(0x20C); } } // 0
			public int _x210 { get { return Read<int>(0x210); } } // 0
			public int _x214 { get { return Read<int>(0x214); } } // -1
			public int _x218 { get { return Read<int>(0x218); } } // -1
			public int _x21C { get { return Read<int>(0x21C); } } // 0
			public int _x220 { get { return Read<int>(0x220); } } // 0
			public int _x224 { get { return Read<int>(0x224); } } // 0
			public int _x228 { get { return Read<int>(0x228); } } // -1
			public int _x22C { get { return Read<int>(0x22C); } } // 0
			public int _x230 { get { return Read<int>(0x230); } } // 0
			public int _x234 { get { return Read<int>(0x234); } } // 0
			public int _x238 { get { return Read<int>(0x238); } } // -1
			public int _x23C { get { return Read<int>(0x23C); } } // 0
			public int _x240 { get { return Read<int>(0x240); } } // 0
			public int _x244 { get { return Read<int>(0x244); } } // 0
			public int _x248 { get { return Read<int>(0x248); } } // -1
			public int _x24C { get { return Read<int>(0x24C); } } // 0
			public int _x250 { get { return Read<int>(0x250); } } // 0
			public int _x254 { get { return Read<int>(0x254); } } // 0
			public int _x258 { get { return Read<int>(0x258); } } // -1
			public int _x25C { get { return Read<int>(0x25C); } } // 0
			public int _x260 { get { return Read<int>(0x260); } } // 0
			public int _x264 { get { return Read<int>(0x264); } } // 0
			public int _x268 { get { return Read<int>(0x268); } } // -1
			public int _x26C { get { return Read<int>(0x26C); } } // 0
			public int _x270 { get { return Read<int>(0x270); } } // 0
			public int _x274 { get { return Read<int>(0x274); } } // 0
			public int _x278 { get { return Read<int>(0x278); } } // -1
			public int _x27C { get { return Read<int>(0x27C); } } // 0
			public int _x280 { get { return Read<int>(0x280); } } // 0
			public int _x284 { get { return Read<int>(0x284); } } // 0
			public int _x288 { get { return Read<int>(0x288); } } // -1
			public int _x28C { get { return Read<int>(0x28C); } } // 0
			public int _x290 { get { return Read<int>(0x290); } } // 0
			public int _x294 { get { return Read<int>(0x294); } } // 0
			public int _x298 { get { return Read<int>(0x298); } } // 0
			public int _x29C { get { return Read<int>(0x29C); } } // 0
			public int _x2A0 { get { return Read<int>(0x2A0); } } // -1
			public int _x2A4 { get { return Read<int>(0x2A4); } } // 0
			public int _x2A8 { get { return Read<int>(0x2A8); } } // -1
			public int _x2AC { get { return Read<int>(0x2AC); } } // 0
			public int _x2B0 { get { return Read<int>(0x2B0); } } // 0
			public int _x2B4 { get { return Read<int>(0x2B4); } } // 0
			public float x2B8_float { get { return Read<float>(0x2B8); } } // -1
			public int _x2BC { get { return Read<int>(0x2BC); } } // 0
			public Ptr x2C0_Ptr_ { get { return Read<Ptr>(0x2C0); } } // 0x0176DD70
			public int _x2C4 { get { return Read<int>(0x2C4); } } // 0
			public Map x2C8_Map { get { return Read<Map>(0x2C8); } }
			public UIReference x338_UIRef { get { return Read<UIReference>(0x338); } } // None
			public int _x540 { get { return Read<int>(0x540); } } // 0
			public int _x544 { get { return Read<int>(0x544); } } // 3
			public int _x548 { get { return Read<int>(0x548); } } // 2
			public Ptr x54C_Ptr_ { get { return Read<Ptr>(0x54C); } } // 0x11239920
			public int _x550 { get { return Read<int>(0x550); } } // 0
			public int _x554 { get { return Read<int>(0x554); } } // -1
			public int _x558 { get { return Read<int>(0x558); } } // -1
			public int _x55C { get { return Read<int>(0x55C); } } // -1
			public UIReference x560_UIRef { get { return Read<UIReference>(0x560); } } // None
			public Map x768_Map { get { return Read<Map>(0x768); } }
		}

		public class Struct04 : Struct0C
		{
			public new const int SizeOf = 0x1B8; // 440

			public int x190_AnimSetSnoId { get { return Read<int>(0x190); } } // 168627 (AnimSet: a2dun_Cald_Belial_Room_A_Breakable_Section_5)
			public int _x194 { get { return Read<int>(0x194); } } // 1
			public int _x198 { get { return Read<int>(0x198); } } // 1
			public int _x19C { get { return Read<int>(0x19C); } } // 0
			public int _x1A0 { get { return Read<int>(0x1A0); } } // 0
			public int _x1A4 { get { return Read<int>(0x1A4); } } // 0
			public int _x1A8 { get { return Read<int>(0x1A8); } } // 0
			public int _x1AC { get { return Read<int>(0x1AC); } } // 0
			public int _x1B0 { get { return Read<int>(0x1B0); } } // 0
			public int _x1B4 { get { return Read<int>(0x1B4); } } // 0
		}

		public class Struct08 : MemoryObject
		{
			public const int SizeOf = 0x48; // 72

			public ListPack x00_ListPack { get { return Read<ListPack>(0x00); } }
			public int x30_UISnoId { get { return Read<int>(0x30); } } // 178639 (UI: BattleNetLightBox)
			public Ptr x34_Ptr_ { get { return Read<Ptr>(0x34); } } // 0x0018FC88
			public Ptr x38_Ptr_ { get { return Read<Ptr>(0x38); } } // 0x6542A61E
			public Ptr x3C_Ptr_ { get { return Read<Ptr>(0x3C); } } // 0x00EE312B
			public int _x40 { get { return Read<int>(0x40); } } // 0
			public int _x44 { get { return Read<int>(0x44); } } // 0
		}

		public class Struct0C : MemoryObject
		{
			public const int SizeOf = 0x190; // 400

			public Ptr x000_VTable { get { return Read<Ptr>(0x000); } } // 0x017CBCE8
			public int _x004 { get { return Read<int>(0x004); } } // 0
			public ListPack x008_ListPack_ItemSize4 { get { return Read<ListPack>(0x008); } } // 102AB298 ListPack Count = 0 { Valid: True, ItemSize: 4, Count: 0}
			public ListPack x038_ListPack_ItemSize4 { get { return Read<ListPack>(0x038); } } // 102AB2C8 ListPack Count = 0 { Valid: True, ItemSize: 4, Count: 0}
			public ListPack x068_ListPack_ItemSize4 { get { return Read<ListPack>(0x068); } } // 102AB2F8 ListPack Count = 0 { Valid: True, ItemSize: 4, Count: 0}
			public int _x098 { get { return Read<int>(0x098); } } // 1
			public int x09C_NotifyDuration { get { return Read<int>(0x09C); } } // 210
			public int _x0A0 { get { return Read<int>(0x0A0); } } // 0
			public int _x0A4 { get { return Read<int>(0x0A4); } } // 0
			public int _x0A8 { get { return Read<int>(0x0A8); } } // 0
			public int _x0AC { get { return Read<int>(0x0AC); } } // 1
			public Vector x0B0_Array { get { return Read<Vector>(0x0B0); } }
			public Vector x0E8_Array { get { return Read<Vector>(0x0E8); } }
			public Map x120_Map { get { return Read<Map>(0x120); } }
		}

		public class Struct18 : MemoryObject
		{
			public const int SizeOf = 0x820; // 2080

			public int _x000 { get { return Read<int>(0x000); } } // 0
			public int _x004 { get { return Read<int>(0x004); } } // 0
			public int _x008 { get { return Read<int>(0x008); } } // 0
			public int _x00C { get { return Read<int>(0x00C); } } // 0
			public int _x010 { get { return Read<int>(0x010); } } // 0
			public int _x014 { get { return Read<int>(0x014); } } // 0
			public int _x018 { get { return Read<int>(0x018); } } // 0
			public int _x01C { get { return Read<int>(0x01C); } } // 0
			public int _x020 { get { return Read<int>(0x020); } } // 0
			public Ptr x024_Ptr_ { get { return Read<Ptr>(0x024); } } // 0x05000500
			public Ptr x028_Ptr_ { get { return Read<Ptr>(0x028); } } // 0x0176F45C
			public Ptr x02C_Ptr_ { get { return Read<Ptr>(0x02C); } } // 0x05000500
			public int _x030 { get { return Read<int>(0x030); } } // 0
			public int _x034 { get { return Read<int>(0x034); } } // 0
			public int _x038 { get { return Read<int>(0x038); } } // 0
			public int _x03C { get { return Read<int>(0x03C); } } // 0
			public Ptr x040_Ptr_ { get { return Read<Ptr>(0x040); } } // 0x01A9F790
			public Ptr x044_Ptr_ { get { return Read<Ptr>(0x044); } } // 0x01A9F7A4
			public int _x048 { get { return Read<int>(0x048); } } // 0
			public Ptr x04C_Ptr_ { get { return Read<Ptr>(0x04C); } } // 0x05000500
			public int _x050 { get { return Read<int>(0x050); } } // 0
			public int _x054 { get { return Read<int>(0x054); } } // 0
			public int _x058 { get { return Read<int>(0x058); } } // 0
			public int _x05C { get { return Read<int>(0x05C); } } // 0
			public Ptr x060_Ptr_ { get { return Read<Ptr>(0x060); } } // 0x01A9F790
			public Ptr x064_Ptr_ { get { return Read<Ptr>(0x064); } } // 0x01A9F7A4
			public int _x068 { get { return Read<int>(0x068); } } // 0
			public int _x06C { get { return Read<int>(0x06C); } } // 0
			public int _x070 { get { return Read<int>(0x070); } } // 0
			public int _x074 { get { return Read<int>(0x074); } } // -1
			public int _x078 { get { return Read<int>(0x078); } } // 0
			public int _x07C { get { return Read<int>(0x07C); } } // 0
			public Ptr x080_Ptr_ { get { return Read<Ptr>(0x080); } } // 0x05000500
			public int _x084 { get { return Read<int>(0x084); } } // 0
			public Ptr x088_Ptr_ { get { return Read<Ptr>(0x088); } } // 0x05000500
			public int _x08C { get { return Read<int>(0x08C); } } // 0
			public int _x090 { get { return Read<int>(0x090); } } // 0
			public int _x094 { get { return Read<int>(0x094); } } // 0
			public int _x098 { get { return Read<int>(0x098); } } // 0
			public int _x09C { get { return Read<int>(0x09C); } } // 0
			public int _x0A0 { get { return Read<int>(0x0A0); } } // 0
			public int _x0A4 { get { return Read<int>(0x0A4); } } // 0
			public int _x0A8 { get { return Read<int>(0x0A8); } } // 0
			public int _x0AC { get { return Read<int>(0x0AC); } } // 0
			public int _x0B0 { get { return Read<int>(0x0B0); } } // 0
			public int _x0B4 { get { return Read<int>(0x0B4); } } // 0
			public int _x0B8 { get { return Read<int>(0x0B8); } } // -1
			public int _x0BC { get { return Read<int>(0x0BC); } } // 0
			public int _x0C0 { get { return Read<int>(0x0C0); } } // 0
			public Ptr x0C4_Ptr_ { get { return Read<Ptr>(0x0C4); } } // 0x05000500
			public int _x0C8 { get { return Read<int>(0x0C8); } } // 0
			public Ptr x0CC_Ptr_ { get { return Read<Ptr>(0x0CC); } } // 0x05000500
			public Ptr x0D0_Ptr_ { get { return Read<Ptr>(0x0D0); } } // 0x01A9F790
			public Ptr x0D4_Ptr_ { get { return Read<Ptr>(0x0D4); } } // 0x01A9F7A4
			public int _x0D8 { get { return Read<int>(0x0D8); } } // 0
			public int _x0DC { get { return Read<int>(0x0DC); } } // 0
			public int _x0E0 { get { return Read<int>(0x0E0); } } // 0
			public int _x0E4 { get { return Read<int>(0x0E4); } } // 0
			public int _x0E8 { get { return Read<int>(0x0E8); } } // 0
			public int _x0EC { get { return Read<int>(0x0EC); } } // 0
			public int _x0F0 { get { return Read<int>(0x0F0); } } // 0
			public Ptr x0F4_Ptr_ { get { return Read<Ptr>(0x0F4); } } // 0x05000500
			public int _x0F8 { get { return Read<int>(0x0F8); } } // 1
			public Ptr x0FC_Ptr_ { get { return Read<Ptr>(0x0FC); } } // 0x05000500
			public int _x100 { get { return Read<int>(0x100); } } // -1
			public int _x104 { get { return Read<int>(0x104); } } // -1
			public int _x108 { get { return Read<int>(0x108); } } // -1
			public Ptr x10C_Ptr_ { get { return Read<Ptr>(0x10C); } } // 0x05000500
			public int _x110 { get { return Read<int>(0x110); } } // 0
			public int _x114 { get { return Read<int>(0x114); } } // 0
			public int _x118 { get { return Read<int>(0x118); } } // 0
			public Ptr x11C_Ptr_ { get { return Read<Ptr>(0x11C); } } // 0x05000500
			public int _x120 { get { return Read<int>(0x120); } } // 0
			public Ptr x124_Ptr_ { get { return Read<Ptr>(0x124); } } // 0x05000500
			public int _x128 { get { return Read<int>(0x128); } } // 0
			public int _x12C { get { return Read<int>(0x12C); } } // 0
			public int _x130 { get { return Read<int>(0x130); } } // -1
			public Ptr x134_Ptr_ { get { return Read<Ptr>(0x134); } } // 0x05000500
			public int _x138 { get { return Read<int>(0x138); } } // 0
			public int _x13C { get { return Read<int>(0x13C); } } // 0
			public int _x140 { get { return Read<int>(0x140); } } // 0
			public int _x144 { get { return Read<int>(0x144); } } // 0
			public Ptr x148_Ptr_ { get { return Read<Ptr>(0x148); } } // 0x01A9F790
			public Ptr x14C_Ptr_ { get { return Read<Ptr>(0x14C); } } // 0x01A9F7A4
			public int _x150 { get { return Read<int>(0x150); } } // 0
			public int _x154 { get { return Read<int>(0x154); } } // 0
			public Ptr x158_Ptr_ { get { return Read<Ptr>(0x158); } } // 0x01A9F790
			public Ptr x15C_Ptr_ { get { return Read<Ptr>(0x15C); } } // 0x01A9F7A4
			public int _x160 { get { return Read<int>(0x160); } } // 0
			public int _x164 { get { return Read<int>(0x164); } } // 0
			public Ptr x168_Ptr_ { get { return Read<Ptr>(0x168); } } // 0x01A9F790
			public Ptr x16C_Ptr_ { get { return Read<Ptr>(0x16C); } } // 0x01A9F7A4
			public int _x170 { get { return Read<int>(0x170); } } // 0
			public int _x174 { get { return Read<int>(0x174); } } // 0
			public int _x178 { get { return Read<int>(0x178); } } // 0
			public int _x17C { get { return Read<int>(0x17C); } } // 0
			public int _x180 { get { return Read<int>(0x180); } } // 0
			public int _x184 { get { return Read<int>(0x184); } } // 0
			public Map x188_Map { get { return Read<Map>(0x188); } }
			public int _x1F8 { get { return Read<int>(0x1F8); } } // -1
			public int _x1FC { get { return Read<int>(0x1FC); } } // 0
			public int _x200 { get { return Read<int>(0x200); } } // 0
			public Ptr x204_Ptr_ { get { return Read<Ptr>(0x204); } } // 0x05000500
			public UIReference x208_UIRef { get { return Read<UIReference>(0x208); } } // None
			public UIReference x410_UIRef { get { return Read<UIReference>(0x410); } } // None
			public UIReference x618_UIRef { get { return Read<UIReference>(0x618); } } // None
		}

		public class Struct1C : MemoryObject
		{
			public const int SizeOf = 0x490; // 1168

			public Ptr x000_Ptr_ { get { return Read<Ptr>(0x000); } } // 0x102AB91C (Field x00C)
			public int _x004 { get { return Read<int>(0x004); } } // 0
			public int _x008 { get { return Read<int>(0x008); } } // 12
			public int _x00C { get { return Read<int>(0x00C); } } // 0
			public int _x010 { get { return Read<int>(0x010); } } // 7
			public int x014_AppearanceSnoId { get { return Read<int>(0x014); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x018_AnimSnoId { get { return Read<int>(0x018); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x01C { get { return Read<int>(0x01C); } } // 5
			public int x020_AnimSnoId { get { return Read<int>(0x020); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int x024_AnimSnoId { get { return Read<int>(0x024); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x028_Ptr_ { get { return Read<Ptr>(0x028); } } // 0x017CBC40
			public Ptr x02C_Ptr_ { get { return Read<Ptr>(0x02C); } } // 0x0176F0A4
			public int _x030 { get { return Read<int>(0x030); } } // 5
			public int _x034 { get { return Read<int>(0x034); } } // 1
			public int _x038 { get { return Read<int>(0x038); } } // 0
			public int _x03C { get { return Read<int>(0x03C); } } // 1
			public int _x040 { get { return Read<int>(0x040); } } // 1
			public int _x044 { get { return Read<int>(0x044); } } // 0
			public int _x048 { get { return Read<int>(0x048); } } // -1
			public int _x04C { get { return Read<int>(0x04C); } } // 22
			public int _x050 { get { return Read<int>(0x050); } } // -1
			public Ptr x054_Ptr_ { get { return Read<Ptr>(0x054); } } // 0x1137E8B0
			public Ptr x058_Ptr_ { get { return Read<Ptr>(0x058); } } // 0x1137E8B0
			public Ptr x05C_Ptr_ { get { return Read<Ptr>(0x05C); } } // 0x1137E8F0
			public int _x060 { get { return Read<int>(0x060); } } // 0
			public int _x064 { get { return Read<int>(0x064); } } // 0
			public Ptr x068_Ptr_ { get { return Read<Ptr>(0x068); } } // 0x000145E2
			public int _x06C { get { return Read<int>(0x06C); } } // 1
			public int _x070 { get { return Read<int>(0x070); } } // 15
			public int x074_AppearanceSnoId { get { return Read<int>(0x074); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x078_AnimSnoId { get { return Read<int>(0x078); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x07C { get { return Read<int>(0x07C); } } // 5
			public int x080_AnimSnoId { get { return Read<int>(0x080); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int x084_AnimSnoId { get { return Read<int>(0x084); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x088_Ptr_ { get { return Read<Ptr>(0x088); } } // 0x017CBC28
			public Ptr x08C_Ptr_ { get { return Read<Ptr>(0x08C); } } // 0x0176F0A4
			public int _x090 { get { return Read<int>(0x090); } } // 5
			public int _x094 { get { return Read<int>(0x094); } } // 1
			public int _x098 { get { return Read<int>(0x098); } } // 0
			public int _x09C { get { return Read<int>(0x09C); } } // 1
			public int _x0A0 { get { return Read<int>(0x0A0); } } // 1
			public int _x0A4 { get { return Read<int>(0x0A4); } } // 0
			public int _x0A8 { get { return Read<int>(0x0A8); } } // -1
			public int _x0AC { get { return Read<int>(0x0AC); } } // 22
			public int _x0B0 { get { return Read<int>(0x0B0); } } // -1
			public Ptr x0B4_Ptr_ { get { return Read<Ptr>(0x0B4); } } // 0x111F40A0
			public Ptr x0B8_Ptr_ { get { return Read<Ptr>(0x0B8); } } // 0x111F40A0
			public Ptr x0BC_Ptr_ { get { return Read<Ptr>(0x0BC); } } // 0x111F40E0
			public int _x0C0 { get { return Read<int>(0x0C0); } } // 0
			public int _x0C4 { get { return Read<int>(0x0C4); } } // 0
			public int x0C8_MarkerSetSnoId { get { return Read<int>(0x0C8); } } // 71343 (MarkerSet: a3dun_rmpt_SW_02)
			public int _x0CC { get { return Read<int>(0x0CC); } } // 2
			public int _x0D0 { get { return Read<int>(0x0D0); } } // 3
			public int x0D4_AppearanceSnoId { get { return Read<int>(0x0D4); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x0D8_AnimSnoId { get { return Read<int>(0x0D8); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x0DC { get { return Read<int>(0x0DC); } } // 5
			public int _x0E0 { get { return Read<int>(0x0E0); } } // 1500
			public int x0E4_AnimSnoId { get { return Read<int>(0x0E4); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x0E8_Ptr_ { get { return Read<Ptr>(0x0E8); } } // 0x017CBC08
			public Ptr x0EC_Ptr_ { get { return Read<Ptr>(0x0EC); } } // 0x0176F0A4
			public int _x0F0 { get { return Read<int>(0x0F0); } } // 4
			public int _x0F4 { get { return Read<int>(0x0F4); } } // 1
			public int _x0F8 { get { return Read<int>(0x0F8); } } // 0
			public int _x0FC { get { return Read<int>(0x0FC); } } // 1
			public int _x100 { get { return Read<int>(0x100); } } // 0
			public Ptr x104_Ptr_ { get { return Read<Ptr>(0x104); } } // 0x10130350
			public int _x108 { get { return Read<int>(0x108); } } // -1
			public int _x10C { get { return Read<int>(0x10C); } } // 21
			public int _x110 { get { return Read<int>(0x110); } } // -1
			public int _x114 { get { return Read<int>(0x114); } } // 0
			public int _x118 { get { return Read<int>(0x118); } } // 0
			public int _x11C { get { return Read<int>(0x11C); } } // 0
			public int _x120 { get { return Read<int>(0x120); } } // 0
			public int _x124 { get { return Read<int>(0x124); } } // 0
			public int _x128 { get { return Read<int>(0x128); } } // 0
			public int _x12C { get { return Read<int>(0x12C); } } // 3
			public int _x130 { get { return Read<int>(0x130); } } // 1
			public int x134_AppearanceSnoId { get { return Read<int>(0x134); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x138_AnimSnoId { get { return Read<int>(0x138); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x13C { get { return Read<int>(0x13C); } } // 2
			public int _x140 { get { return Read<int>(0x140); } } // 1500
			public int x144_AnimSnoId { get { return Read<int>(0x144); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x148_Ptr_ { get { return Read<Ptr>(0x148); } } // 0x017CBBEC
			public Ptr x14C_Ptr_ { get { return Read<Ptr>(0x14C); } } // 0x0175DB54
			public int _x150 { get { return Read<int>(0x150); } } // 2
			public int _x154 { get { return Read<int>(0x154); } } // 1
			public int x158_ActorSnoId { get { return Read<int>(0x158); } } // 360 (Actor: Emitter)
			public int _x15C { get { return Read<int>(0x15C); } } // 1
			public int _x160 { get { return Read<int>(0x160); } } // 1
			public int _x164 { get { return Read<int>(0x164); } } // 0
			public int _x168 { get { return Read<int>(0x168); } } // -1
			public int _x16C { get { return Read<int>(0x16C); } } // -1
			public int _x170 { get { return Read<int>(0x170); } } // 258
			public int _x174 { get { return Read<int>(0x174); } } // 0
			public int _x178 { get { return Read<int>(0x178); } } // 0
			public int _x17C { get { return Read<int>(0x17C); } } // 0
			public int _x180 { get { return Read<int>(0x180); } } // 0
			public int _x184 { get { return Read<int>(0x184); } } // 0
			public int _x188 { get { return Read<int>(0x188); } } // 0
			public int _x18C { get { return Read<int>(0x18C); } } // 4
			public int _x190 { get { return Read<int>(0x190); } } // 3
			public int x194_AppearanceSnoId { get { return Read<int>(0x194); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x198_AnimSnoId { get { return Read<int>(0x198); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x19C { get { return Read<int>(0x19C); } } // 5
			public int x1A0_AnimSnoId { get { return Read<int>(0x1A0); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int x1A4_AnimSnoId { get { return Read<int>(0x1A4); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x1A8_Ptr_ { get { return Read<Ptr>(0x1A8); } } // 0x017CBBD0
			public Ptr x1AC_Ptr_ { get { return Read<Ptr>(0x1AC); } } // 0x0176F0A4
			public int _x1B0 { get { return Read<int>(0x1B0); } } // 21
			public int _x1B4 { get { return Read<int>(0x1B4); } } // 1
			public int _x1B8 { get { return Read<int>(0x1B8); } } // 0
			public int _x1BC { get { return Read<int>(0x1BC); } } // 1
			public int _x1C0 { get { return Read<int>(0x1C0); } } // 1
			public int _x1C4 { get { return Read<int>(0x1C4); } } // 0
			public int _x1C8 { get { return Read<int>(0x1C8); } } // 3
			public int _x1CC { get { return Read<int>(0x1CC); } } // 30
			public int _x1D0 { get { return Read<int>(0x1D0); } } // -1
			public int _x1D4 { get { return Read<int>(0x1D4); } } // 0
			public int _x1D8 { get { return Read<int>(0x1D8); } } // 0
			public int _x1DC { get { return Read<int>(0x1DC); } } // 0
			public int _x1E0 { get { return Read<int>(0x1E0); } } // 0
			public int _x1E4 { get { return Read<int>(0x1E4); } } // 0
			public int _x1E8 { get { return Read<int>(0x1E8); } } // 0
			public int _x1EC { get { return Read<int>(0x1EC); } } // 5
			public int _x1F0 { get { return Read<int>(0x1F0); } } // 2
			public int x1F4_AppearanceSnoId { get { return Read<int>(0x1F4); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x1F8_AnimSnoId { get { return Read<int>(0x1F8); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x1FC { get { return Read<int>(0x1FC); } } // 5
			public int x200_AnimSnoId { get { return Read<int>(0x200); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int x204_AnimSnoId { get { return Read<int>(0x204); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x208_Ptr_ { get { return Read<Ptr>(0x208); } } // 0x017CBBB4
			public Ptr x20C_Ptr_ { get { return Read<Ptr>(0x20C); } } // 0x0176F0A4
			public int _x210 { get { return Read<int>(0x210); } } // 0
			public int _x214 { get { return Read<int>(0x214); } } // 1
			public int _x218 { get { return Read<int>(0x218); } } // 0
			public int _x21C { get { return Read<int>(0x21C); } } // 1
			public int _x220 { get { return Read<int>(0x220); } } // 1
			public int _x224 { get { return Read<int>(0x224); } } // 0
			public int _x228 { get { return Read<int>(0x228); } } // 3
			public int _x22C { get { return Read<int>(0x22C); } } // 30
			public int _x230 { get { return Read<int>(0x230); } } // -1
			public int _x234 { get { return Read<int>(0x234); } } // 0
			public int _x238 { get { return Read<int>(0x238); } } // 0
			public int _x23C { get { return Read<int>(0x23C); } } // 0
			public int _x240 { get { return Read<int>(0x240); } } // 0
			public int _x244 { get { return Read<int>(0x244); } } // 0
			public int _x248 { get { return Read<int>(0x248); } } // 0
			public int _x24C { get { return Read<int>(0x24C); } } // 6
			public int _x250 { get { return Read<int>(0x250); } } // 3
			public int x254_AppearanceSnoId { get { return Read<int>(0x254); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x258_AnimSnoId { get { return Read<int>(0x258); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x25C { get { return Read<int>(0x25C); } } // 5
			public int _x260 { get { return Read<int>(0x260); } } // 1500
			public int x264_AnimSnoId { get { return Read<int>(0x264); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x268_Ptr_ { get { return Read<Ptr>(0x268); } } // 0x017CBB94
			public Ptr x26C_Ptr_ { get { return Read<Ptr>(0x26C); } } // 0x0176F0A4
			public int _x270 { get { return Read<int>(0x270); } } // 4
			public int _x274 { get { return Read<int>(0x274); } } // 1
			public int _x278 { get { return Read<int>(0x278); } } // 0
			public int _x27C { get { return Read<int>(0x27C); } } // 1
			public int _x280 { get { return Read<int>(0x280); } } // 1
			public Ptr x284_Ptr_ { get { return Read<Ptr>(0x284); } } // 0x10130580
			public int _x288 { get { return Read<int>(0x288); } } // 3
			public int _x28C { get { return Read<int>(0x28C); } } // 21
			public int _x290 { get { return Read<int>(0x290); } } // -1
			public int _x294 { get { return Read<int>(0x294); } } // 0
			public int _x298 { get { return Read<int>(0x298); } } // 0
			public int _x29C { get { return Read<int>(0x29C); } } // 0
			public int _x2A0 { get { return Read<int>(0x2A0); } } // 0
			public int _x2A4 { get { return Read<int>(0x2A4); } } // 0
			public int _x2A8 { get { return Read<int>(0x2A8); } } // 0
			public int _x2AC { get { return Read<int>(0x2AC); } } // 7
			public int _x2B0 { get { return Read<int>(0x2B0); } } // 2
			public int x2B4_AppearanceSnoId { get { return Read<int>(0x2B4); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x2B8_AnimSnoId { get { return Read<int>(0x2B8); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x2BC { get { return Read<int>(0x2BC); } } // 5
			public int _x2C0 { get { return Read<int>(0x2C0); } } // 1500
			public int x2C4_AnimSnoId { get { return Read<int>(0x2C4); } } // 640 (Anim: OmniNPC_Female_2HS_Parry)
			public Ptr x2C8_Ptr_ { get { return Read<Ptr>(0x2C8); } } // 0x017CBB70
			public Ptr x2CC_Ptr_ { get { return Read<Ptr>(0x2CC); } } // 0x0176F0A4
			public int _x2D0 { get { return Read<int>(0x2D0); } } // 0
			public int _x2D4 { get { return Read<int>(0x2D4); } } // 1
			public int _x2D8 { get { return Read<int>(0x2D8); } } // 0
			public int _x2DC { get { return Read<int>(0x2DC); } } // 1
			public int _x2E0 { get { return Read<int>(0x2E0); } } // 1
			public int _x2E4 { get { return Read<int>(0x2E4); } } // 0
			public int _x2E8 { get { return Read<int>(0x2E8); } } // 3
			public int _x2EC { get { return Read<int>(0x2EC); } } // 21
			public int _x2F0 { get { return Read<int>(0x2F0); } } // -1
			public int _x2F4 { get { return Read<int>(0x2F4); } } // 0
			public int _x2F8 { get { return Read<int>(0x2F8); } } // 0
			public int _x2FC { get { return Read<int>(0x2FC); } } // 0
			public int _x300 { get { return Read<int>(0x300); } } // 0
			public int _x304 { get { return Read<int>(0x304); } } // 0
			public int _x308 { get { return Read<int>(0x308); } } // 0
			public int _x30C { get { return Read<int>(0x30C); } } // 8
			public int _x310 { get { return Read<int>(0x310); } } // 3
			public int x314_AppearanceSnoId { get { return Read<int>(0x314); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x318_AnimSnoId { get { return Read<int>(0x318); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x31C { get { return Read<int>(0x31C); } } // 5
			public int _x320 { get { return Read<int>(0x320); } } // 1500
			public int x324_AnimSnoId { get { return Read<int>(0x324); } } // 634 (Anim: morluMelee_mega_knockback_end_02)
			public Ptr x328_Ptr_ { get { return Read<Ptr>(0x328); } } // 0x017CBB50
			public Ptr x32C_Ptr_ { get { return Read<Ptr>(0x32C); } } // 0x0176F0A4
			public int _x330 { get { return Read<int>(0x330); } } // 4
			public int _x334 { get { return Read<int>(0x334); } } // 1
			public int _x338 { get { return Read<int>(0x338); } } // 0
			public int _x33C { get { return Read<int>(0x33C); } } // 1
			public int _x340 { get { return Read<int>(0x340); } } // 1
			public Ptr x344_Ptr_ { get { return Read<Ptr>(0x344); } } // 0x101318D0
			public int _x348 { get { return Read<int>(0x348); } } // 5
			public int _x34C { get { return Read<int>(0x34C); } } // 21
			public int _x350 { get { return Read<int>(0x350); } } // -1
			public int _x354 { get { return Read<int>(0x354); } } // 0
			public int _x358 { get { return Read<int>(0x358); } } // 0
			public int _x35C { get { return Read<int>(0x35C); } } // 0
			public int _x360 { get { return Read<int>(0x360); } } // 0
			public int _x364 { get { return Read<int>(0x364); } } // 0
			public int _x368 { get { return Read<int>(0x368); } } // 0
			public int _x36C { get { return Read<int>(0x36C); } } // 9
			public int _x370 { get { return Read<int>(0x370); } } // 2
			public int x374_AppearanceSnoId { get { return Read<int>(0x374); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x378_AnimSnoId { get { return Read<int>(0x378); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x37C { get { return Read<int>(0x37C); } } // 5
			public int _x380 { get { return Read<int>(0x380); } } // 1500
			public int x384_AnimSnoId { get { return Read<int>(0x384); } } // 640 (Anim: OmniNPC_Female_2HS_Parry)
			public Ptr x388_Ptr_ { get { return Read<Ptr>(0x388); } } // 0x017CBB2C
			public Ptr x38C_Ptr_ { get { return Read<Ptr>(0x38C); } } // 0x0176F0A4
			public int _x390 { get { return Read<int>(0x390); } } // 0
			public int _x394 { get { return Read<int>(0x394); } } // 1
			public int _x398 { get { return Read<int>(0x398); } } // 0
			public int _x39C { get { return Read<int>(0x39C); } } // 1
			public int _x3A0 { get { return Read<int>(0x3A0); } } // 1
			public int _x3A4 { get { return Read<int>(0x3A4); } } // 0
			public int _x3A8 { get { return Read<int>(0x3A8); } } // 5
			public int _x3AC { get { return Read<int>(0x3AC); } } // 21
			public int _x3B0 { get { return Read<int>(0x3B0); } } // -1
			public int _x3B4 { get { return Read<int>(0x3B4); } } // 0
			public int _x3B8 { get { return Read<int>(0x3B8); } } // 0
			public int _x3BC { get { return Read<int>(0x3BC); } } // 0
			public int _x3C0 { get { return Read<int>(0x3C0); } } // 0
			public int _x3C4 { get { return Read<int>(0x3C4); } } // 0
			public int _x3C8 { get { return Read<int>(0x3C8); } } // 0
			public int _x3CC { get { return Read<int>(0x3CC); } } // 10
			public int _x3D0 { get { return Read<int>(0x3D0); } } // 2
			public int x3D4_AppearanceSnoId { get { return Read<int>(0x3D4); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x3D8_AnimSnoId { get { return Read<int>(0x3D8); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x3DC { get { return Read<int>(0x3DC); } } // 2
			public int _x3E0 { get { return Read<int>(0x3E0); } } // 1500
			public int x3E4_AnimSnoId { get { return Read<int>(0x3E4); } } // 640 (Anim: OmniNPC_Female_2HS_Parry)
			public Ptr x3E8_Ptr_ { get { return Read<Ptr>(0x3E8); } } // 0x017CBAFC
			public Ptr x3EC_Ptr_ { get { return Read<Ptr>(0x3EC); } } // 0x0176F0A4
			public int _x3F0 { get { return Read<int>(0x3F0); } } // 0
			public int _x3F4 { get { return Read<int>(0x3F4); } } // 1
			public int _x3F8 { get { return Read<int>(0x3F8); } } // 0
			public int _x3FC { get { return Read<int>(0x3FC); } } // 1
			public int _x400 { get { return Read<int>(0x400); } } // 1
			public int _x404 { get { return Read<int>(0x404); } } // 0
			public int _x408 { get { return Read<int>(0x408); } } // 8
			public int _x40C { get { return Read<int>(0x40C); } } // 21
			public int _x410 { get { return Read<int>(0x410); } } // -1
			public int _x414 { get { return Read<int>(0x414); } } // 0
			public int _x418 { get { return Read<int>(0x418); } } // 0
			public int _x41C { get { return Read<int>(0x41C); } } // 0
			public int _x420 { get { return Read<int>(0x420); } } // 0
			public int _x424 { get { return Read<int>(0x424); } } // 0
			public int _x428 { get { return Read<int>(0x428); } } // 0
			public int _x42C { get { return Read<int>(0x42C); } } // 11
			public int _x430 { get { return Read<int>(0x430); } } // 2
			public int x434_AppearanceSnoId { get { return Read<int>(0x434); } } // 1000 (Appearance: trDun_Cave_S_Entrance_01)
			public int x438_AnimSnoId { get { return Read<int>(0x438); } } // 10000 (Anim: snakeMan_melee_run_01)
			public int _x43C { get { return Read<int>(0x43C); } } // 2
			public int _x440 { get { return Read<int>(0x440); } } // 1500
			public int x444_AnimSnoId { get { return Read<int>(0x444); } } // 640 (Anim: OmniNPC_Female_2HS_Parry)
			public Ptr x448_Ptr_ { get { return Read<Ptr>(0x448); } } // 0x017CBAD0
			public Ptr x44C_Ptr_ { get { return Read<Ptr>(0x44C); } } // 0x0176F0A4
			public int _x450 { get { return Read<int>(0x450); } } // 0
			public int _x454 { get { return Read<int>(0x454); } } // 1
			public int _x458 { get { return Read<int>(0x458); } } // 0
			public int _x45C { get { return Read<int>(0x45C); } } // 1
			public int _x460 { get { return Read<int>(0x460); } } // 1
			public int _x464 { get { return Read<int>(0x464); } } // 0
			public int _x468 { get { return Read<int>(0x468); } } // 8
			public int _x46C { get { return Read<int>(0x46C); } } // 21
			public int _x470 { get { return Read<int>(0x470); } } // -1
			public int _x474 { get { return Read<int>(0x474); } } // 0
			public int _x478 { get { return Read<int>(0x478); } } // 0
			public int _x47C { get { return Read<int>(0x47C); } } // 0
			public int _x480 { get { return Read<int>(0x480); } } // 0
			public int _x484 { get { return Read<int>(0x484); } } // 0
			public int _x488 { get { return Read<int>(0x488); } } // 0
			public int _x48C { get { return Read<int>(0x48C); } } // 0
		}
		#endregion
	}

	public partial class ScreenManager
	{
		public static ScreenManager Instance { get { return Engine.TryGet(a => a.GetScreenManager()); } }
	}
}