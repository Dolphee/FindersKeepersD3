using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.D3
{
	public static class EngineExtensions
	{
		public static ScreenManager GetScreenManager(this Engine engine)
		{
			return engine.ReadPointer<ScreenManager>(StaticAddress.PtrScreenManager).Dereference();
		}
	}
}
