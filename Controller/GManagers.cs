using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Helpers;
using System.Threading;
using FindersKeepers.Templates.Overlay;

namespace FindersKeepers.Controller
{
    public class GManagers
    {
        public GLists GList = new GLists();
        public GCaches GCache = new GCaches();
        public GThreads GThread = new GThreads();
        public GRefs GRef = new GRefs();

        public class GLists
        {
            public Dictionary<short, GameManagerData.GameManagerData> GameManagerData = new Dictionary<short, GameManagerData.GameManagerData>();
            public List<GameManagerAccounts> Accounts = new List<GameManagerAccounts>();
            public List<OverlayItems> ItemOverlay = new List<OverlayItems>();
            public GameManagerAccounts MainAccount;
            public GameManagerData.GameManagerData MainAccountData;
        }

        public class GRefs
        {
            public D3Overlay D3Overlay = null;
            public Attach Attacher = null;
            public Attacher<D3OverlayControl> D3OverlayControl = null;
            public Actors Actors;
            public List<OverlayItems> ItemOverlay = new List<OverlayItems>();
        }

        public class GCaches
        {
            public bool MapReveal;
            public bool Debug = false;
            public bool Available;
            public int PollRate = 10;
            public bool Multiboxing;
        }

        public class GThreads
        {
            public bool HasThreads;
            public List<AccountWatcherThread> Threads = new List<AccountWatcherThread>();
            public AutoResetEvent ExitThread = new AutoResetEvent(false);
        }
    }
}
