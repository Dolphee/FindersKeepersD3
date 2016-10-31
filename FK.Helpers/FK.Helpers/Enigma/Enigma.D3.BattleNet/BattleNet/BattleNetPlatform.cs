using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.D3.Collections;
using Enigma.D3.Memory;

namespace Enigma.D3.BattleNet
{
	public class BattleNetPlatform : MemoryObject
	{
		// 2.0.5.24017
		public const int SizeOf = 0x238;

		public int x000_VTable { get { return Read<int>(0x000); } }
		public int _x004 { get { return Read<int>(0x004); } }
		public int x008 { get { return Read<int>(0x008); } }
		public int x00C_Ptr { get { return Read<int>(0x00C); } }
		public int x010_Unk_Authentication { get { return Read<int>(0x010); } }
		public int x014_Unk_Account { get { return Read<int>(0x014); } }
		public int x018_Unk_Presence { get { return Read<int>(0x018); } }
		public int x01C_Unk_Friends { get { return Read<int>(0x01C); } }
		public int x020_Unk_Party { get { return Read<int>(0x020); } }
		public int x024_Unk_Chat { get { return Read<int>(0x024); } }
		public int x028_Unk_Whisper { get { return Read<int>(0x028); } }
		public int x02C_Unk_UserManager { get { return Read<int>(0x02C); } }
		public int x030_Unk_Resources { get { return Read<int>(0x030); } }
		public int x034 { get { return Read<int>(0x034); } }
		public int x038 { get { return Read<int>(0x038); } }
		public Ptr<X3C> x03C_Unk_Games { get { return ReadPointer<X3C>(0x03C); } }
		public int x040_Unk_LocalStorage { get { return Read<int>(0x040); } }
		public int x044_Unk_Exchange { get { return Read<int>(0x044); } }
		public int x048 { get { return Read<int>(0x048); } }
		public int x04C_Unk_Search { get { return Read<int>(0x04C); } }
		public int x050 { get { return Read<int>(0x050); } }
		public int x054_Unk_Achievements { get { return Read<int>(0x054); } }
		public int x058_Unk_ProfanityFilter { get { return Read<int>(0x058); } }
		public int x05C_Unk_Challenge { get { return Read<int>(0x05C); } }
		public int x060_Unk_Report { get { return Read<int>(0x060); } }
		public int x064_Unk_Broadcast { get { return Read<int>(0x064); } }
		public int x068_Unk_Notification { get { return Read<int>(0x068); } }
		public int x06C_StructStart_Min28Bytes { get { return Read<int>(0x06C); } }
		public int _x070 { get { return Read<int>(0x070); } }
		public int _x074 { get { return Read<int>(0x074); } }
		public int _x078 { get { return Read<int>(0x078); } }
		public int _x07C { get { return Read<int>(0x07C); } }
		public int _x080 { get { return Read<int>(0x080); } }
		public int _x084 { get { return Read<int>(0x084); } }
		public int _x088 { get { return Read<int>(0x088); } }
		public int x08C { get { return Read<int>(0x08C); } }
		public int x090 { get { return Read<int>(0x090); } }
		public int x094 { get { return Read<int>(0x094); } }
		public int x098 { get { return Read<int>(0x098); } }
		public ListPack x09C_ListPack_ItemSize32 { get { return Read<ListPack>(0x09C); } }
		public Win32.CriticalSection x0CC_CriticalSection { get { return Read<Win32.CriticalSection>(0x0CC); } }
		public int _x0E4 { get { return Read<int>(0x0E4); } }
		public Map x0E8_MapEx { get { return Read<Map>(0x0E8); } }
		public int x158 { get { return Read<int>(0x158); } }
		public Ptr<X15C> x15C_Ptr_8Bytes_Account { get { return ReadPointer<X15C>(0x15C); } }
		public Ptr<X160> x160_Ptr_8Bytes_Authentication { get { return ReadPointer<X160>(0x160); } }
		public Ptr<X164> x164_Ptr_8Bytes_Presence { get { return ReadPointer<X164>(0x164); } }
		public Ptr<X168> x168_Ptr_8Bytes_Friends { get { return ReadPointer<X168>(0x168); } }
		public Ptr<X16C> x16C_Ptr_32Bytes_Party { get { return ReadPointer<X16C>(0x16C); } }
		public Ptr<X170> x170_Ptr_8Bytes_Chat { get { return ReadPointer<X170>(0x170); } }
		public Ptr<X174> x174_Ptr_8Bytes_Whisper { get { return ReadPointer<X174>(0x174); } }
		public Ptr<X178> x178_Ptr_8Bytes_UserManager { get { return ReadPointer<X178>(0x178); } }
		public int x17C { get { return Read<int>(0x17C); } }
		public int x180 { get { return Read<int>(0x180); } }
		public int x184_Ptr_24Bytes_Games { get { return Read<int>(0x184); } }
		public int x188 { get { return Read<int>(0x188); } }
		public int x18C_Ptr_8Bytes_Exchange { get { return Read<int>(0x18C); } }
		public int x190_Ptr_8Bytes_Achievements { get { return Read<int>(0x190); } }
		public int x194_Ptr_8Bytes_Challenge { get { return Read<int>(0x194); } }
		public int x198_Ptr_8Bytes_Broadcast { get { return Read<int>(0x198); } }
		public int x19C_Ptr_8Bytes_Notification { get { return Read<int>(0x19C); } }
		public int x1A0_IsConnected_State { get { return Read<int>(0x1A0); } }
		public int x1A4_IsDisconnecting { get { return Read<int>(0x1A4); } }
		public int x1A8 { get { return Read<int>(0x1A8); } }
		public int x1AC { get { return Read<int>(0x1AC); } }
		public int _x1B0 { get { return Read<int>(0x1B0); } }
		public int _x1B4 { get { return Read<int>(0x1B4); } }
		public Map x1B8_MapEx { get { return Read<Map>(0x1B8); } }
		public int x228 { get { return Read<int>(0x228); } }
		public int x22C { get { return Read<int>(0x22C); } }
		public int x230_Ptr { get { return Read<int>(0x230); } }
		public int _x234 { get { return Read<int>(0x234); } }

		public class X3C : MemoryObject
		{
			public const int SizeOf = 0x60; // 96: Min 4

			public Ptr<X00> x00 { get { return ReadPointer<X00>(0x00); } }
			public int _x04 { get { return Read<int>(0x04); } }
			public int _x08 { get { return Read<int>(0x08); } }
			public int _x0C { get { return Read<int>(0x0C); } }
			public int _x10 { get { return Read<int>(0x10); } }
			public int _x14 { get { return Read<int>(0x14); } }
			public int _x18 { get { return Read<int>(0x18); } }
			public int _x1C { get { return Read<int>(0x1C); } }
			public int _x20 { get { return Read<int>(0x20); } }
			public int _x24 { get { return Read<int>(0x24); } }
			public int _x28 { get { return Read<int>(0x28); } }
			public int _x2C { get { return Read<int>(0x2C); } }
			public int _x30 { get { return Read<int>(0x30); } }
			public int _x34 { get { return Read<int>(0x34); } }
			public int _x38 { get { return Read<int>(0x38); } }
			public int _x3C { get { return Read<int>(0x3C); } }
			public int _x40 { get { return Read<int>(0x40); } }
			public int _x44 { get { return Read<int>(0x44); } }
			public int _x48 { get { return Read<int>(0x48); } }
			public int _x4C { get { return Read<int>(0x4C); } }
			public int _x50 { get { return Read<int>(0x50); } }
			public int _x54 { get { return Read<int>(0x54); } }
			public int _x58 { get { return Read<int>(0x58); } }
			public int _x5C { get { return Read<int>(0x5C); } }

			public class X00 : MemoryObject
			{
				public const int SizeOf = 0x60; // 96: Min 96

				public int _x00 { get { return Read<int>(0x00); } }
				public int _x04 { get { return Read<int>(0x04); } }
				public int _x08 { get { return Read<int>(0x08); } }
				public int _x0C { get { return Read<int>(0x0C); } }
				public int _x10 { get { return Read<int>(0x10); } }
				public int _x14 { get { return Read<int>(0x14); } }
				public int _x18 { get { return Read<int>(0x18); } }
				public int _x1C { get { return Read<int>(0x1C); } }
				public int _x20 { get { return Read<int>(0x20); } }
				public int _x24 { get { return Read<int>(0x24); } }
				public int _x28 { get { return Read<int>(0x28); } }
				public int _x2C { get { return Read<int>(0x2C); } }
				public int _x30 { get { return Read<int>(0x30); } }
				public int _x34 { get { return Read<int>(0x34); } }
				public int _x38 { get { return Read<int>(0x38); } }
				public int _x3C { get { return Read<int>(0x3C); } }
				public int _x40 { get { return Read<int>(0x40); } }
				public int _x44 { get { return Read<int>(0x44); } }
				public int _x48 { get { return Read<int>(0x48); } }
				public int _x4C { get { return Read<int>(0x4C); } }
				public int _x50 { get { return Read<int>(0x50); } }
				public int _x54 { get { return Read<int>(0x54); } }
				public int _x58 { get { return Read<int>(0x58); } }
				public int _x5C { get { return Read<int>(0x5C); } }
			}
		}

		public class X15C : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X160 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X164 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X168 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X16C : MemoryObject
		{
			public const int SizeOf = 32;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x04); } }
			public int _x08 { get { return Read<int>(0x08); } }
			public int _x0C { get { return Read<int>(0x0C); } }
			public int _x10 { get { return Read<int>(0x10); } }
			public int _x14 { get { return Read<int>(0x14); } }
			public int _x18 { get { return Read<int>(0x18); } }
			public int _x1C { get { return Read<int>(0x1C); } }
		}

		public class X170 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X174 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}

		public class X178 : MemoryObject
		{
			public const int SizeOf = 8;

			public int _x00 { get { return Read<int>(0x00); } }
			public int _x04 { get { return Read<int>(0x00); } }
		}
	}
}
