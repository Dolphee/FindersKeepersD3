using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using Enigma.D3.Enums;
using FindersKeepers.Controller;
using Enigma.D3.UI.Controls;

namespace FindersKeepers.Helpers
{
    public static class TownHelper
    {
        public static bool StashOpen = false;
        public static bool KadalaOpen = false;
        public static HashSet<int> Stash = new HashSet<int>(GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Items.Keys.ToList());
        public static HashSet<int> Towns = new HashSet<int>() { 332339, 168314, 92945, 270011 }; // Ordered in A1-A5, A3 and A4 has the same SNOID
        public static bool InTown;
        public static Dictionary<int, List<InventoryHelper>> Inventory = new Dictionary<int, List<InventoryHelper>>();

        public class InventoryHelper
        {
            public int ID;
            public string Name;
            public System.Windows.Point InventoryPosition; 
        }

        public static void GetStash()
        {
            if ((Helpers.UIObjects.Stash.TryGetValue<UXItemsControl>()) || !GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Loaded)
            {
                StashOpen = true;
                GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Loaded = true;

                //UIObjects.InventoryTabs.GetItems();
            }

            else if ((StashOpen) && !Helpers.UIObjects.Stash.TryGetValue<UXItemsControl>())
            {
                GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.ResetList();

                foreach (ActorCommonData ACD in GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData.Where(x => x.x114_ItemLocation == ItemLocation.Stash))
                {
                    if (!Stash.Contains(ACD.x090_ActorSnoId))
                        continue;

                    GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Items[ACD.x090_ActorSnoId].Quantity += (ushort)ACD.GetAttribute(AttributeId.ItemStackQuantityLo);
                }

                GameManager.Instance.GManager.GRef.Attacher.TryUpdate();
                StashOpen = false;
            }
        }

        public static void UpdateStash()
        {
            GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.ResetList();

            foreach (ActorCommonData ACD in GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData.Where(x => x.x114_ItemLocation == ItemLocation.Stash))
            {
                if (!Stash.Contains(ACD.x090_ActorSnoId))
                    continue;

                GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Items[ACD.x090_ActorSnoId].Quantity += (ushort)ACD.GetAttribute(AttributeId.ItemStackQuantityLo);
            }

            GameManager.Instance.GManager.GRef.D3OverlayControl.TryRefreshControl<Templates.Inventory.Inventory>();

           // GameManager.Instance.GManager.GRef.Attacher.TryUpdate();
        }

        public static void Gambling()
        {
            int i = 0;

            if (Helpers.UIObjects.Kadala.TryGetValue<UXLabel>()) // Is gambling
            {
                if (!KadalaOpen) // First Check, havent gambled yet
                {
                    if (!Inventory.ContainsKey(i))
                        Inventory.Add(i, new List<InventoryHelper>());

                    KadalaGambling(i, true);
                    KadalaOpen = true;
                }

                else
                {
                    KadalaGambling(i, false);
                }
            }

            else
            {
                if (KadalaOpen) // Stopped gambling UI window closed
                {
                    KadalaOpen = false;
                    Inventory.Remove(i);
                }
            }
        }

        public static void KadalaGambling(int Collection, bool FirstRun = false)
        {
            foreach (ActorCommonData ACD in GameManager.Instance.GManager.GList.MainAccount.DiabloIII.GetContainer().Where(x => x.x114_ItemLocation == ItemLocation.PlayerBackpack))
            {
                if (Inventory[Collection].Exists(x => x.ID == ACD.x000_Id))
                    continue;

                if (!FirstRun)
                    ItemFilter.Enumerate(ACD, true);

                Inventory[Collection].Add(new InventoryHelper { ID = ACD.x000_Id, Name = ACD.x004_Name });
            }
        }
    }
}
