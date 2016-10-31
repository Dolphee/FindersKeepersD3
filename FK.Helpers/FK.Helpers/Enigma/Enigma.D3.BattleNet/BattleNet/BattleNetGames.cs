using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.D3.Collections;

namespace Enigma.D3.BattleNet
{
	public class BattleNetGames : MemoryObject
	{
		public const int SizeOf = 0x810; // 2064

		public int x000_VTable { get { return Read<int>(0x000); } }
		public int _x004 { get { return Read<int>(0x004); } }
		public BattleNetClient x008_BNetClient { get { return Dereference<BattleNetClient>(0x008); } }
		public int _x00C { get { return Read<int>(0x00C); } }
		public Vector x010_Vector { get { return Read<Vector>(0x010); } }
		public Map x048_Map { get { return Read<Map>(0x048); } }
		public Map x0B8_Map { get { return Read<Map>(0x0B8); } }
		public int _x128 { get { return Read<int>(0x128); } }
		public int _x12C { get { return Read<int>(0x12C); } }
		public int _x130 { get { return Read<int>(0x130); } }
		public int _x134 { get { return Read<int>(0x134); } }
		public int _x138 { get { return Read<int>(0x138); } }
		public int _x13C { get { return Read<int>(0x13C); } }
		public int _x140 { get { return Read<int>(0x140); } }
		public int _x144 { get { return Read<int>(0x144); } }
		public int _x148 { get { return Read<int>(0x148); } }
		public int _x14C { get { return Read<int>(0x14C); } }
		public int _x150 { get { return Read<int>(0x150); } }
		public int _x154 { get { return Read<int>(0x154); } }
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
		public Ptr<BattleNetPlatform> x190_Ptr_Platform { get { return ReadPointer<BattleNetPlatform>(0x190); } }
		public int _x194 { get { return Read<int>(0x194); } }
		public int _x198 { get { return Read<int>(0x198); } }
		public int _x19C { get { return Read<int>(0x19C); } }
		public int _x1A0 { get { return Read<int>(0x1A0); } }
		public int _x1A4 { get { return Read<int>(0x1A4); } }
		public int _x1A8 { get { return Read<int>(0x1A8); } }
		public int _x1AC { get { return Read<int>(0x1AC); } }
		public Map x1B0_Map { get { return Read<Map>(0x1B0); } }
		public Map x220_Map { get { return Read<Map>(0x220); } }
		public int _x290 { get { return Read<int>(0x290); } }
		public int _x294 { get { return Read<int>(0x294); } }
		public int _x298 { get { return Read<int>(0x298); } }
		public int _x29C { get { return Read<int>(0x29C); } }
		public int _x2A0 { get { return Read<int>(0x2A0); } }
		public int _x2A4 { get { return Read<int>(0x2A4); } }
		public int _x2A8 { get { return Read<int>(0x2A8); } }
		public int _x2AC { get { return Read<int>(0x2AC); } }
		public int _x2B0 { get { return Read<int>(0x2B0); } }
		public int _x2B4 { get { return Read<int>(0x2B4); } }
		public int _x2B8 { get { return Read<int>(0x2B8); } }
		public int _x2BC { get { return Read<int>(0x2BC); } }
		public int _x2C0 { get { return Read<int>(0x2C0); } }
		public int _x2C4 { get { return Read<int>(0x2C4); } }
		public int _x2C8 { get { return Read<int>(0x2C8); } }
		public int _x2CC { get { return Read<int>(0x2CC); } }
		public int _x2D0 { get { return Read<int>(0x2D0); } }
		public int _x2D4 { get { return Read<int>(0x2D4); } }
		public int _x2D8 { get { return Read<int>(0x2D8); } }
		public int _x2DC { get { return Read<int>(0x2DC); } }
		public int _x2E0 { get { return Read<int>(0x2E0); } }
		public int _x2E4 { get { return Read<int>(0x2E4); } }
		public int _x2E8 { get { return Read<int>(0x2E8); } }
		public int _x2EC { get { return Read<int>(0x2EC); } }
		public int _x2F0 { get { return Read<int>(0x2F0); } }
		public int _x2F4 { get { return Read<int>(0x2F4); } }
		public int _x2F8 { get { return Read<int>(0x2F8); } }
		public int _x2FC { get { return Read<int>(0x2FC); } }
		public int _x300 { get { return Read<int>(0x300); } }
		public int _x304 { get { return Read<int>(0x304); } }
		public int _x308 { get { return Read<int>(0x308); } }
		public int _x30C { get { return Read<int>(0x30C); } }
		public int _x310 { get { return Read<int>(0x310); } }
		public int _x314 { get { return Read<int>(0x314); } }
		public int _x318 { get { return Read<int>(0x318); } }
		public int _x31C { get { return Read<int>(0x31C); } }
		public int _x320 { get { return Read<int>(0x320); } }
		public int _x324 { get { return Read<int>(0x324); } }
		public int _x328 { get { return Read<int>(0x328); } }
		public int _x32C { get { return Read<int>(0x32C); } }
		public int _x330 { get { return Read<int>(0x330); } }
		public int _x334 { get { return Read<int>(0x334); } }
		public int _x338 { get { return Read<int>(0x338); } }
		public int _x33C { get { return Read<int>(0x33C); } }
		public X340 x340_Struct { get { return Read<X340>(0x340); } }
		public Map x3E8_Map { get { return Read<Map>(0x3E8); } }
		public Map x458_Map { get { return Read<Map>(0x458); } }
		public Map x4C8_Map { get { return Read<Map>(0x4C8); } }
		public int _x538 { get { return Read<int>(0x538); } }
		public int _x53C { get { return Read<int>(0x53C); } }
		public int _x540 { get { return Read<int>(0x540); } }
		public int _x544 { get { return Read<int>(0x544); } }
		public int _x548 { get { return Read<int>(0x548); } }
		public int _x54C { get { return Read<int>(0x54C); } }
		public Map x550_Map { get { return Read<Map>(0x550); } }
		public Map x5C0_Map { get { return Read<Map>(0x5C0); } }
		public Map x630_Map { get { return Read<Map>(0x630); } }
		public Map x6A0_Map { get { return Read<Map>(0x6A0); } }
		public Map x710_Map { get { return Read<Map>(0x710); } }
		public Map x780_Map { get { return Read<Map>(0x780); } }
		public int _x7F0 { get { return Read<int>(0x7F0); } }
		public int _x7F4 { get { return Read<int>(0x7F4); } }
		public int _x7F8 { get { return Read<int>(0x7F8); } }
		public int _x7FC { get { return Read<int>(0x7FC); } }
		public int _x800 { get { return Read<int>(0x800); } }
		public int _x804 { get { return Read<int>(0x804); } }
		public int _x808 { get { return Read<int>(0x808); } }
		public int _x80C { get { return Read<int>(0x80C); } }

		public class X340 : MemoryObject
		{
			public const int SizeOf = 0xA8; // 168

			public int x00 { get { return Read<int>(0x00); } }
			public int x04 { get { return Read<int>(0x04); } }
			public ListPack x08_ListPack_ItemSize72 { get { return Read<ListPack>(0x08); } }
			public Map x38_Map { get { return Read<Map>(0x38); } }
		}
	}
}
