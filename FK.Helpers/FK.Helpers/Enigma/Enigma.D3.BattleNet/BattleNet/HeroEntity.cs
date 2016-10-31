using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Memory;

namespace Enigma.D3.BattleNet
{
	public class HeroEntity : MemoryObject
	{
		public const int SizeOf = 0x160; // 352

		public EntityId x000_HeroId { get { return Read<EntityId>(0x000); } }
		public HeroData x010_HeroData { get { return Read<HeroData>(0x010); } }
	}
}
