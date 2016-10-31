using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma.D3.BattleNet
{
	public class GameId : MemoryObject
	{
		public const int SizeOf = 0x14;

		public long x00_FactoryId { get { return Read<long>(0x00); } }
		public long x08_High { get { return Read<long>(0x08); } }
		public long x10_Low { get { return Read<long>(0x10); } }
	}
}
