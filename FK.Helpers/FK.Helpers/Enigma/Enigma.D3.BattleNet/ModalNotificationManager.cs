using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Collections;
using Enigma.D3.UI;
using Enigma.Memory;

namespace Enigma.D3
{
	public class ModalNotificationManager : MemoryObject
	{
		// 2.0.6.24641
		public const int SizeOf = 0x240; // 576

		public int x000_VTable { get { return Read<int>(0x000); } }
		public int _x004 { get { return Read<int>(0x004); } }
		public ListPack<ListItem> x008_ListPack88 { get { return Read<ListPack<ListItem>>(0x008); } }
		public UIReference x038_UIReference { get { return Read<UIReference>(0x038); } }

		public class ListItem : MemoryObject
		{
			public const int SizeOf = 8;

			public int x00 { get { return Read<int>(0x00); } }
			public Struct_x04 x04_ScreenManager_UIRefs { get { return Read<Struct_x04>(0x04); } }



			public class Struct_x04 : MemoryObject
			{
				public const int SizeOf = 0x54;

				public int _x00 { get { return Read<int>(0x00); } }
				public int _x04 { get { return Read<int>(0x04); } }
				public RefString x08_RefString { get { return Read<RefString>(0x08); } }
				public RefString x14_RefString { get { return Read<RefString>(0x14); } }
				public RefString x20_RefString { get { return Read<RefString>(0x20); } }
				public RefString x2C_RefString { get { return Read<RefString>(0x2C); } }
				public int x38 { get { return Read<int>(0x38); } }
				public int x3C { get { return Read<int>(0x3C); } }
				public int x40_StructStart_Min16Bytes { get { return Read<int>(0x40); } }
				public int _x44 { get { return Read<int>(0x44); } }
				public int _x48 { get { return Read<int>(0x48); } }
				public int _x4C { get { return Read<int>(0x4C); } }
				public int x50 { get { return Read<int>(0x50); } }
			}
		}
	}

}
