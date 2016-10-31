using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Helpers
{
    public static class FKTracker
    {

        public static void Add(Helpers.Item Item, bool Gambling)
        {
            Controller.Enums.ItemQuality Quality = Item.ItemQuality;

            if (Quality == Controller.Enums.ItemQuality.Normal)
                GameManagerAccountHelper.CurrentData.ItemHelper.White += 1;

            else if (Quality == Controller.Enums.ItemQuality.Magic)
                GameManagerAccountHelper.CurrentData.ItemHelper.Magic += 1;

            else if (Quality == Controller.Enums.ItemQuality.Rare)
                GameManagerAccountHelper.CurrentData.ItemHelper.Rare += 1;

            else if (Quality == Controller.Enums.ItemQuality.Legendary)
                AddLegendary(Item, Gambling);

            //if(Item.ItemQuality == Controller.Enums.ItemQuality.Legendary || Item.ItemQuality.
        }

        public static void AddLegendary(Helpers.Item Item, bool Gambling)
        {
            if(Item.SNOItem.SetItem)
                GameManagerAccountHelper.CurrentData.ItemHelper.Set += 1;
            else
                GameManagerAccountHelper.CurrentData.ItemHelper.Legendary += 1;

            FindersKeepers.FKTracker.GameType GameType = FindersKeepers.FKTracker.GameType.OpenWorld;
            int Difficulty = (int)GameManagerAccountHelper.Current.LevelAreaHelper.GameDifficulty;

            if (Gambling)
            {
                GameType = FindersKeepers.FKTracker.GameType.KadalaGamble;
            }

            else if (Controller.GameManager.Instance.GManager.GList.MainAccount.RiftHelper.InRiftNow)
            {
                if (Controller.GameManager.Instance.GManager.GList.MainAccount.RiftHelper.CurrentRift.RiftLevel != -1)
                {
                    GameType = FindersKeepers.FKTracker.GameType.GreaterRift;
                    Difficulty = Controller.GameManager.Instance.GManager.GList.MainAccount.RiftHelper.CurrentRift.RiftLevel;
                }
                else
                    GameType = FindersKeepers.FKTracker.GameType.Rift;
            }

            FKTrackerList.Add(new FindersKeepers.FKTracker.LegendaryItems
            {
                Name = Item.SNOItem.ActorName,
                Found = DateTime.Now,
                SetItem = Item.SNOItem.SetItem,
                AncientItem = Item.AncientItem,
                GameType = GameType,
                Difficulty = Difficulty,
                AccountID = (uint)GameManagerAccountHelper.CurrentData.Multibox.MultiboxID,
             });
        }

    }
}
