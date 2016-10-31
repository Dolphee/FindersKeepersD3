using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using System.Windows.Controls;


namespace FindersKeepers.Helpers
{
    public static class SceneHelper
    {
        public static Dictionary<int, SceneNavContainer> SNOScenes = new Dictionary<int, SceneNavContainer>();
        public static Dictionary<int, NavHelper> NavHelpers = new Dictionary<int, NavHelper>();
        public static HashSet<int> LoadedScenes = new HashSet<int>();
        public static HashSet<int> NewScenes = new HashSet<int>();
        public static IEnumerable<int> ToHide = new HashSet<int>();
        public static bool ForceRefresh = false;

        public class NavHelper
        {
            public NavContainer NavContainer;
            public int LevelAreaSnoID;
            public Image RenderedImage;
            public bool ShouldShow = false;
        }

        public class SceneNavContainer
        {
            public int ID;
            public List<NavCell> NavCells = new List<NavCell>();

            public SceneNavContainer(Enigma.D3.Assets.Scene.NavCell[] Cell)
            {
                foreach(Enigma.D3.Assets.Scene.NavCell NavCell in Cell.Where(x => (x.x18 & 1) != 0 ))
                {
                    NavCells.Add(new SceneNavContainer.NavCell {
                        Min = new System.Windows.Point((double)NavCell.x00.X,(double)NavCell.x00.Y),
                        Max = new System.Windows.Point((double)NavCell.x0C.X, (double)NavCell.x0C.Y)
                    });
                }
            }

            public struct NavCell
            {
                public System.Windows.Point Min;
                public System.Windows.Point Max;
            }

        }

        public static void Load()
        {
            foreach (Enigma.D3.Assets.Scene Scene in SNO.GetContainer<Enigma.D3.Assets.Scene>(Enigma.D3.Enums.SnoGroupId.Scene))
                SNOScenes.Add(Scene.x000_Header.x00_SnoId, new SceneNavContainer(Scene.x180_NavZoneDefinition.x08_NavCells));
        }

        public static bool Contains(int WorldID)
        {
            return SNOScenes.ContainsKey(WorldID);
        }

        public static SceneNavContainer Getter(int WorldId)
        {
            return SNOScenes[WorldId];
        }

        public static bool TryAdd(int SceneID)
        {
            foreach (Enigma.D3.Assets.Scene Scene in SNO.GetContainer<Enigma.D3.Assets.Scene>(Enigma.D3.Enums.SnoGroupId.Scene))
                if (Scene.x000_Header.x00_SnoId == SceneID)
                {
                    SNOScenes.Add(Scene.x000_Header.x00_SnoId, new SceneNavContainer(Scene.x180_NavZoneDefinition.x08_NavCells));
                    return true;
                }

            return false;
        }

        public static System.Windows.Point GetPosition(this NavContainer c)
        {
            return new System.Windows.Point(
                 c.Min.X - GameManager.Instance.GManager.GList.MainAccount.Player.x0D0_WorldPosX, 
                c.Min.Y - GameManager.Instance.GManager.GList.MainAccount.Player.x0D4_WorldPosY);
        }

        public static void LoadScenes()
        {
            NewScenes = new HashSet<int>();

            IEnumerable<Enigma.D3.Scene> Scenes = (GameManager.Instance.GManager.GList.MainAccount.RiftHelper.InRiftNow) ? 
                    Enigma.D3.Engine.Current.ObjectManager.Scenes.Dereference().Where(x => x.x050_WorldsSnoId == -1):  
                    Enigma.D3.Engine.Current.ObjectManager.Scenes.Dereference();

            foreach (Enigma.D3.Scene Scene in Scenes.Where(x => x.x000_Id != 0 && x.x000_Id != -1))
            {
                NewScenes.Add(Scene.x004_StructStart_Internal_SceneId);
                if (!LoadedScenes.Contains(Scene.x004_StructStart_Internal_SceneId)) // Add
                    AddScene(Scene);

                //  else
                // Add that same SceneID can appear twice, maybe sort on Point?
            }
        }

        public static void AddScene(Enigma.D3.Scene Scene)
        {
            if (SceneHelper.Contains(Scene.x0E8_SceneSnoId)) // Has SnoID
            {
                NavHelper Nav = new NavHelper { NavContainer = new NavContainer(Scene), ShouldShow = true, LevelAreaSnoID = Enigma.D3.Engine.Current.LevelArea.x044_SnoId };

                if (!Nav.NavContainer.Error)
                {
                    NavHelpers.Add(Nav.NavContainer.InternalID, Nav);
                    LoadedScenes.Add(Nav.NavContainer.InternalID);
                }

                else
                    Remove(Nav);
            }

            else
            {
                SceneHelper.TryAdd(Scene.x0E8_SceneSnoId);
            }
        }

        public static void Remove(NavHelper nav)
        {
            NavHelpers.Remove(nav.NavContainer.InternalID);
        }

    }

    public class NavContainer
    {
        public int SceneID;
        public int SNOID;
        public int AreaID;
        public Vector3 Min;
        public Vector3 Max;
        public Enigma.D3.Assets.Scene SNOScene;
        public bool Skip = false;
        public List<Navigation> NavigationCells = new List<Navigation>();
        public bool Disposable = false;
        public int InternalID;
        public bool Error = false;

        public System.Windows.Point Position
        {
            get
            {
                return new System.Windows.Point(
                    Min.X - GameManager.Instance.GManager.GList.MainAccount.Player.x0D0_WorldPosX,
                    Min.Y - GameManager.Instance.GManager.GList.MainAccount.Player.x0D4_WorldPosY);
            }
        }

        public struct Navigation
        {
            public System.Windows.Point Min; // Position
            public System.Windows.Point Max; // Size
        }

        public NavContainer(Enigma.D3.Scene Scene)
        {
            SceneID = Scene.x000_Id;
            SNOID = Scene.x0E8_SceneSnoId;
            AreaID = Scene.x018_LevelAreaSnoId;
            Min = new Vector3(Scene.x0FC, Scene.x100, Scene.x104);
            Max = new Vector3(Scene.x174, Scene.x178, Scene.x104);
            InternalID = Scene.x004_StructStart_Internal_SceneId;

            try
            {
                foreach (var x in SceneHelper.Getter(SNOID).NavCells)
                {
                    NavigationCells.Add(new Navigation
                    {
                        Min =  x.Min,
                        Max = x.Max,
                    });
                }
            }

            catch
            {
                Error = true;
            }

            Skip = NavigationCells.Count < 1;
        }
    }
}


