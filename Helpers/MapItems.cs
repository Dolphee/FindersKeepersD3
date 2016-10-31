using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using System.Windows.Controls;
using System.Windows.Media;
using Enigma.D3.Helpers;
using System.Windows.Shapes;
using Enigma.D3.Enums;
using FindersKeepers.Controller.Enums;
using System.Windows.Media.Media3D;
using FindersKeepers.Controller;
using FindersKeepers.Helpers.Overlay.ActorTypes;
using System.Windows.Media.Imaging;
using FindersKeepers.Helpers.Overlay;
using System.Windows;

namespace FindersKeepers.Helpers
{
    public static class MapItems
    {
        public static HashSet<int> Goblins = new HashSet<int>(new int[] { 5984, 5985, 5986, 5987, 5988, 394196, 391593, 403549, 405186, 408655, 413289, 408989, 408354, 410572, 410574, 429161, 428663, 429161 });
        public static HashSet<int> GreaterRiftSouls = new HashSet<int>(new int[] { 401751});
        public static HashSet<int> XPPool = new HashSet<int>() { 373463 };
        public static HashSet<int> Keywardens = new HashSet<int>() { 255704, 256022, 256040, 256054 };
        public static HashSet<int> RiftSouls = new HashSet<int>(new int[] { 436807});
        public static HashSet<int> Shrines = null;
        public static HashSet<int> PowerPylon = null;

        public static Dictionary<int, IMap> Monsters = new Dictionary<int, IMap>();
        public static Dictionary<int, IMap> Scenes = new Dictionary<int, IMap>();
        public static Dictionary<int, IMap> ScenesLarge = new Dictionary<int, IMap>();

        public static List<IMap> MonstersAdd = new List<IMap>();
        public static List<IMap> ScenesAdd = new List<IMap>();
        public static List<IMap> ScenesLargeAdd = new List<IMap>();

        public static bool TryCreateResplendent(this ActorCommonData acd)
        {
            if ((acd.x004_Name.ToLower().IndexOf("chest") != -1) && (acd.x004_Name.ToLower().IndexOf("rare") != -1))
                if (acd.IsValidGizmoChest())
                    return true;

            return false;
        }

        public static bool TryCreateChest(this ActorCommonData acd, bool CheckForRacks = false)
        {
            if (!CheckForRacks)
            {
                if ((acd.IsValidGizmoChest()) && acd.x004_Name.ToLower().IndexOf("chest") != -1)
                    return true;
            }

            else
            {
                if (acd.IsValidGizmoChest())
                    return true;
            }
           

           /* else if (acd.x180_GizmoType == GizmoType.Switch)
            {
                switch (acd.x090_ActorSnoId)
                {
                    case 0x0005900F: // x1_Global_Chest_CursedChest
                    case 0x00059229: // x1_Global_Chest_CursedChest_B
                        return true;
                }
            }*/
            return false;
        }

        public static bool TryCreateLootable(this ActorCommonData acd)
        {
            if (acd.IsValidGizmoLoreChest())
                    return true;

            return false;
        }


        public static bool IsValidGizmoChest(this ActorCommonData Actor)
        {
            try
            {
                return (Actor.x248_CollisionFlags & 0x400) == 0 &&
                    Attributes.ChestOpen.GetValue(Actor) != 1;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidGizmoLoreChest(this ActorCommonData Actor)
        {
            try
            {
                return Attributes.ChestOpen.GetValue(Actor, 0xA0000) != 1;
            }

            catch
            {
                return false;
            }
        }

        public static bool IsKeyWarden(this ActorCommonData Actor)
        {
            if (Keywardens.Contains(Actor.x090_ActorSnoId))
                return true;

            return false;
        }

        public static bool IsShrine(this ActorCommonData Actor)
        {
            if(Shrines == null)
                Shrines = new HashSet<int>(new int[] { 176074, 176075, 176076, 176077, 260346, 260347, 260948, 225025, 225027, 225028,
                    225030, 225261 , 225262 , 225263 , 225269, 225270 , 225271, 225272,
                    260342, 260344, 260345 ,269349 
                }); // 260948 = A4 well

            return Shrines.Contains(Actor.x090_ActorSnoId);

            /*
             *  176074	Actor	Shrine_Global_Blessed
                176075	Actor	Shrine_Global_Enlightened
                176076	Actor	Shrine_Global_Fortune
                176077	Actor	Shrine_Global_Frenzied
                260346	Actor	Shrine_Global_Hoarder
                260347	Actor	Shrine_Global_Reloaded
                269349	Actor   BanditShrine
             */

        }

        public static bool IsPoolOfReflection(this ActorCommonData Actor)
        {
            return XPPool.Contains(Actor.x090_ActorSnoId);
        }

        public static bool IsGreaterRiftSoul(this ActorCommonData Actor)
        {
            return GreaterRiftSouls.Contains(Actor.x090_ActorSnoId);
        }

        public static bool IsRiftSoul(this ActorCommonData Actor)
        {
            return RiftSouls.Contains(Actor.x090_ActorSnoId);
        }

        public static bool IsPowerPylon(this ActorCommonData Actor)
        {
            if (PowerPylon == null)
                PowerPylon = new HashSet<int>(new int[] { 330695, 330696, 330697, 330698, 330699, 398654 });

            return PowerPylon.Contains(Actor.x090_ActorSnoId);
            // 176074 // blessed Shrine Protection

        }

        public static bool IsTreasureGoblin(this ActorCommonData Actor)
        {
            return Goblins.Contains(Actor.x090_ActorSnoId);

            /*
             * treasureGoblin_J-29178 - 429161 / Guld Gobban
             * treasureGoblin_D_Splitter_03-107452 - 410574
             * treasureGoblin_D_Splitter_02-107345 - 410572
             * treasureGoblin_D_Splitter-107234 - 408354
             * treasureGoblin_F-5334 - 408989
             * treasureGoblin_C-10052 - 5987
             *  treasureGoblin_H-6857 - 413289
             * treasureGoblin_E-1765 - 408655
                 391593  Actor   Blood Theif?
             * 394196 ?? No idea p1_treasureGoblin_inBackpack_A
             * 405186 RainBow Goblin
             * 
             */
        }

        public static void Search(ActorCommonData Actor)
        {
            if (Monsters.ContainsKey(Actor.Address))
                return;

        }

        public static void Add(this ActorCommonData Actor, MapItemElement Ele, int I = -1, int EliteOff = -1, bool IsMonster = false)
        {
            IMapActor Monster = new IMapActor(Actor, Ele, EliteOff, I, IsMonster);
            Monsters.Add(Actor.Address, Monster);
            MonstersAdd.Add(Monster);
        }

        public static bool OutofBounds(Point f, int Max)
        {
            if (f.X >= Max || f.X <= -Max || f.Y >= Max || f.Y <= -Max)
                return true;

            return false;
        }

        public static void Scene()
        {
           // if(Config._.FKConfig.General.MiniMapSettings.QuestMarker)
                GetQuestMarkers();
            GetTrickle();

            //if (TownHelper.InTown)
            //return;

            IEnumerable<SceneHelper.NavHelper> Scenese = SceneHelper.NavHelpers.Values;
           //  .Where(x => !x.NavContainer.Skip || (!GameManager.Instance.GManager.GList.MainAccount.RiftHelper.InRiftNow && 
           // x.LevelAreaSnoID == GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea.x044_SnoId));

            foreach (SceneHelper.NavHelper Scene in Scenese)
            {
                if (!Scenes.ContainsKey(Scene.NavContainer.InternalID))
                {
                    IMapScenes Item = new IMapScenes(Scene.NavContainer);
                    //IMapScenes Item = new IMapScenes(Scene.NavContainer.InternalID, Scene.NavContainer.ZIndex, Scene.NavContainer.NavigationCells, new Point(Scene.NavContainer.Min.X, Scene.NavContainer.Min.Y));
                    Scenes.Add(Scene.NavContainer.InternalID, Item);
                    ScenesAdd.Add(Item);
                }

                else if (!ScenesLarge.ContainsKey(Scene.NavContainer.InternalID))
                {
                    IMapScenesLargeMap ItemLarge = new IMapScenesLargeMap(Scene.NavContainer);
                    ScenesLarge.Add(Scene.NavContainer.InternalID, ItemLarge);
                    ScenesLargeAdd.Add(ItemLarge);
                }
            }
        }

        public static void GetQuestMarkers()
        {
            foreach (var marker in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary().Where(x => x.Value.x00_WorldPosition.x0C_WorldId == Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.x790.Dereference().x038_WorldId_))
            {
                if (Monsters.ContainsKey(marker.Value.Address))
                    continue;

                IMapQuest Monster = new IMapQuest(marker);
                Monsters.Add(marker.Value.Address, Monster);
                MonstersAdd.Add(Monster);
            }
        }

        public static void GetTrickle()
        {
            return;
            foreach (var marker in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.TrickleManager.x04_Ptr_Items.Dereference())
            {
                if (Monsters.ContainsKey(marker.Address) || marker.x1C_LevelArea != LevelArea.Instance.x044_SnoId)
                    continue;

                IMapTrickle Monster = new IMapTrickle(marker);
                Monsters.Add(marker.Address, Monster);
                MonstersAdd.Add(Monster);
            }
        }

        public static void DetermineActor(this ActorCommonData Actor)
        {
            if (Monsters.ContainsKey(Actor.Address))
            {
                if (Actor.IsElite(true))
                    Actor.EliteAffixes();
                return;
            }

            if (Config._.FKMinimap.OverrideLayout.ContainsKey(Actor.x090_ActorSnoId) && Actor.x08C_ActorId != -1)
            {
                Actor.Add(MapItemElement.AT_Custom, Config._.FKMinimap.OverrideLayout[Actor.x090_ActorSnoId]);
            }

            else
            {
                if (Actor.IsMonster()) // Monster, Goblins, Bosses etc
                {
                    int EliteOffset = -1;

                    if ((Actor.IsTreasureGoblin()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_Goblin))
                        Actor.Add(MapItemElement.AT_Goblin, -1, -1, true);

                    else
                    {
                        if (!Config._.FKMinimap.Allowed.Contains((int)(MapItemElement)Actor.x0B8_MonsterQuality))
                            return;

                        MapItemElement Quality = (MapItemElement)Actor.x0B8_MonsterQuality;

                        if (Controller.GameManagerData.GameManagerAccountHelper.Current.LevelAreaHelper.EliteAffixesEnabled && (Actor.IsElite(true)))
                            EliteOffset = Actor.EliteAffixes();

                        if (Actor.IsKeyWarden())
                            Quality = MapItemElement.AT_Keywardens;

                        Actor.Add(Quality, -1, EliteOffset, true);
                    }
                }

                else if (Actor.IsItem())
                {
                    if ((Actor.IsGreaterRiftSoul()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_GreaterRiftSoul)) // GR Stuff
                        Actor.Add(MapItemElement.AT_GreaterRiftSoul);

                    else if ((Actor.IsRiftSoul()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_RiftSoul))
                        Actor.Add(MapItemElement.AT_RiftSoul);
                }

                else // Dunno yet ??????
                {
                    if ((Actor.IsShrine()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_Shrines))
                    {
                        if (Actor.GetAttribute(AttributeId.GizmoHasBeenOperated) == 1)
                            Actor.Add(MapItemElement.AT_HasBeenOperated);
                        else
                            Actor.Add(MapItemElement.AT_Shrines);
                    }

                    else if ((Actor.IsPowerPylon()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_PowerPylons))
                    {
                        if (Actor.GetAttribute(AttributeId.GizmoHasBeenOperated) == 1)
                            Actor.Add(MapItemElement.AT_HasBeenOperated);
                        else
                            Actor.Add(MapItemElement.AT_PowerPylons);
                    }

                    else if (Actor.x180_GizmoType == GizmoType.Chest)
                    {
                        if ((Actor.TryCreateResplendent()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_ResplendentChest))
                            Actor.Add(MapItemElement.AT_ResplendentChest);
                        else if ((Actor.TryCreateChest()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_Chests))
                            Actor.Add(MapItemElement.AT_Chests);
                        ///  else if (Actor.TryCreateChest(true))
                        //   GameManager.MainAccount.ActorList.Monsters.Add(new IMap.IMapActor { Actor = Actor, Type = MapItemElement.AT_LootableContainers });
                        //  else if (Actor.TryCreateLootable())
                        // GameManager.MainAccount.ActorList.Monsters.Add(new IMap.IMapActor { Actor = Actor, Type = MapItemElement.AT_LoreBooks });
                    }

                    else if ((Actor.IsPoolOfReflection()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_PoolOfReflection))
                    {
                        if (Actor.GetAttribute(AttributeId.GizmoHasBeenOperated) == 1)
                            Actor.Add(MapItemElement.AT_HasBeenOperated);
                        else
                            Actor.Add(MapItemElement.AT_PoolOfReflection);
                    }
                }
            }
        }

        public static bool IsElite(this ActorCommonData acd, bool IncludeUnique = false)
        {
            if (IncludeUnique)
                return acd.x0B8_MonsterQuality == MonsterQuality.Champion || acd.x0B8_MonsterQuality == MonsterQuality.Rare || acd.x0B8_MonsterQuality == MonsterQuality.Boss || acd.x0B8_MonsterQuality == MonsterQuality.Unique;

            return acd.x0B8_MonsterQuality == MonsterQuality.Champion || acd.x0B8_MonsterQuality == MonsterQuality.Rare;
        }
    }

   /* public class IMap
    {
        public static HashSet<int> _Added = new HashSet<int>(); // Alla nya id
        public static System.Windows.Point GetPoint(ActorCommonData Actor)
        {
            return new System.Windows.Point
                {
                    X = Actor.x0D0_WorldPosX - GameManager.Instance.GManager.GList.MainAccount.Player.x0D0_WorldPosX,
                    Y = Actor.x0D4_WorldPosY - GameManager.Instance.GManager.GList.MainAccount.Player.x0D4_WorldPosY
                };
        }

        public class IMapActor
        {
            public ActorCommonData Actor;
            public MapItemElement Type;
            public int EliteOffset = -1;
            public int ACDID;
            public int Identifier;
            public System.Windows.Point Point;
        }
    }*/
}
