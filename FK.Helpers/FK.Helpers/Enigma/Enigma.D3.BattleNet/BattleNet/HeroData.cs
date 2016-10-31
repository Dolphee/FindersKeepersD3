using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Enums;
using Enigma.Memory;

namespace Enigma.D3.BattleNet
{
	public class HeroData : MemoryObject
	{
		public const int SizeOf = 0x150;

		public X000 x000_Struct { get { return Read<X000>(0x000); } }
		public int _x084 { get { return Read<int>(0x084); } }
		public X088 x088_Struct { get { return Read<X088>(0x98); } } // 136

		public class X000 : MemoryObject
		{
            public const int SizeOf = 0x84; // 132
            public int x000 { get { return Read<int>(0x000); } }
			public int _x004 { get { return Read<int>(0x004); } }
			public int _x008 { get { return Read<int>(0x008); } }
			public int _x00C { get { return Read<int>(0x00C); } }
			public int _x010 { get { return Read<int>(0x010); } }
			public int _x014 { get { return Read<int>(0x014); } }
			public int _x018 { get { return Read<int>(0x018); } }
			public int _x01C { get { return Read<int>(0x01C); } }
			public int _x020 { get { return Read<int>(0x020); } }
			public int _x024 { get { return Read<int>(0x024); } }
			public int _x028 { get { return Read<int>(0x028); } }
			public int _x02C { get { return Read<int>(0x02C); } }
			public int _x030 { get { return Read<int>(0x030); } }
			public int _x034 { get { return Read<int>(0x034); } }
			public int _x038 { get { return Read<int>(0x038); } }
			public int _x03C { get { return Read<int>(0x03C); } }
			public int _x040 { get { return Read<int>(0x040); } }
			public int _x044 { get { return Read<int>(0x044); } }
			public int _x048 { get { return Read<int>(0x048); } }
			public int _x04C { get { return Read<int>(0x04C); } }
			public int _x050 { get { return Read<int>(0x050); } }
			public int _x054 { get { return Read<int>(0x054); } }
			public int _x058 { get { return Read<int>(0x058); } }
			public int _x05C { get { return Read<int>(0x05C); } }
			public int _x060 { get { return Read<int>(0x060); } }
			public int _x064 { get { return Read<int>(0x064); } }
			public int _x068 { get { return Read<int>(0x068); } }
			public int _x06C { get { return Read<int>(0x06C); } }
			public int _x070 { get { return Read<int>(0x070); } }
			public int _x074 { get { return Read<int>(0x074); } }
			public int _x078 { get { return Read<int>(0x078); } }
			public int _x07C { get { return Read<int>(0x07C); } }
			public int _x080 { get { return Read<int>(0x080); } }
		}

		public class X088 : MemoryObject
		{
			public const int SizeOf = 0xC8; // 200

			public int x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x04); } }
			public EntityId x08_AccountId { get { return Read<EntityId>(0x08); } }
			//public RefString x18_AccountName { get { return Read<RefString>(0x18); } }
			public int _x24 { get { return Read<int>(0x24); } }
			public EntityId x28_HeroId { get { return Read<EntityId>(0x28); } }
			//public RefString x38_HeroName { get { return Read<RefString>(0x38); } }
			public int _x44 { get { return Read<int>(0x44); } }
			public int _x48 { get { return Read<int>(0x48); } }
			public HeroClass x4C_HeroClass { get { return Read<HeroClass>(0x4C); } }
			public int _x50 { get { return Read<int>(0x50); } }
			public int x54_Level { get { return Read<int>(0x54); } }
			public int x58_Paragon { get { return Read<int>(0x58); } }
			public int _x5C { get { return Read<int>(0x5C); } }
			public int _x60 { get { return Read<int>(0x60); } }
			public int _x64 { get { return Read<int>(0x64); } }
			public int _x68 { get { return Read<int>(0x68); } }
			public int _x6CSeasonChar { get { return Read<int>(0x6C); } }
			public int _x70 { get { return Read<int>(0x70); } }
			public int _x74 { get { return Read<int>(0x74); } }
			public int _x78 { get { return Read<int>(0x78); } }
			public int _x7C { get { return Read<int>(0x7C); } }
			public int _x80 { get { return Read<int>(0x80); } }
			public int _x84 { get { return Read<int>(0x84); } }
			public int _x88 { get { return Read<int>(0x88); } }
			public int _x8C { get { return Read<int>(0x8C); } }
			public int _x90 { get { return Read<int>(0x90); } }
			public int _x94 { get { return Read<int>(0x94); } }
			public int _x98 { get { return Read<int>(0x98); } } // flags? gender?
			public int _x9C { get { return Read<int>(0x9C); } }
			public int _xA0 { get { return Read<int>(0xA0); } }
			public int _xA4 { get { return Read<int>(0xA4); } }
			public int _xA8 { get { return Read<int>(0xA8); } }
			public int xAC_QuestSnoId { get { return Read<int>(0xAC); } }
			public int xB0_QuestStep { get { return Read<int>(0xB0); } }
			public int xB4_PlayTimeInSeconds { get { return Read<int>(0xB4); } }
			//public Timestamp xB8_Created { get { return Read<Timestamp>(0xB8); } }
			//public Timestamp xC0_LastPlayed { get { return Read<Timestamp>(0xC0); } }
		}

		public struct Timestamp
		{
			public long MicrosecondsSinceEpoch;

			public override string ToString()
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(MicrosecondsSinceEpoch / 1000).ToString();
			}
		}
	}
}
