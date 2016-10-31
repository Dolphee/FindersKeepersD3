using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Assets;
using Enigma.D3;
using Enigma.D3.Helpers;
using FindersKeepers.Controller.Enums;
using FindersKeepers.Controller;
using Enigma.D3.Enums;

namespace FindersKeepers.Helpers
{
    public class Item
    {   
        public Enigma.D3.ActorCommonData ItemActor;
        public FindersKeepers.Controller.Enums.ItemQuality ItemQuality;
        public FindersKeepers.Controller.Enums.ItemQuality RealQuality;
        public bool ShowAncient { get; set; }
        public SNO.ItemData SNOItem { get { return SNO.TryGet(ItemActor); } }
        public bool AncientItem;

        public Item(Enigma.D3.ActorCommonData Item)
        {
            this.ItemActor = Item;
            this.AncientItem = Item.GetAttribute(AttributeId.AncientRank) == 1;
            ItemQuality = Quality();
        }

        protected FindersKeepers.Controller.Enums.ItemQuality Quality()
        {
            try
            {
                double Quality = ItemActor.GetAttribute(Enigma.D3.Enums.AttributeId.ItemQualityLevel);
                RealQuality = (FindersKeepers.Controller.Enums.ItemQuality)Quality;

                if(Quality < 3)
                    return FindersKeepers.Controller.Enums.ItemQuality.Normal;

                else if (Quality > 2 && Quality < 6)
                    return FindersKeepers.Controller.Enums.ItemQuality.Magic;
                else if(Quality > 5 && Quality < 9)
                    return FindersKeepers.Controller.Enums.ItemQuality.Rare;
                else if(Quality > 9)
                    return FindersKeepers.Controller.Enums.ItemQuality.Legendary;
                return (FindersKeepers.Controller.Enums.ItemQuality)Quality;
            }

            catch{}

            return FindersKeepers.Controller.Enums.ItemQuality.Invalid;
        }
    }

    public struct SNO
    {
        public static Dictionary<int, ItemData> SNOItems;

        public static ItemData TryGet(ActorCommonData Actor)
        {
            return (SNOItems.ContainsKey(Actor.x0B4_GameBalanceId)) ? SNOItems[Actor.x0B4_GameBalanceId] : default(ItemData);
        }

        public static ItemData TryGet(int Id)
        {
            return (SNOItems.ContainsKey(Id)) ? SNOItems[Id] : default(ItemData);
        }

        public struct ItemData
        {
            public string ActorName {get;set;}
            public int ItemType { get; set; }
            public bool SetItem {get;set;}
        }
        
        public static void ItemInformation()
        {
            Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();
            HashSet<int> Allowed = new HashSet<int>() { 19750, 19753, 19754, 170627 };

            foreach (GameBalance Gamebalance in GetContainer<GameBalance>(Enigma.D3.Enums.SnoGroupId.GameBalance))
            {
                if (!Allowed.Contains(Gamebalance.x000_Header.x00_SnoId))
                    continue;

                foreach (GameBalance.Item ItemData in Gamebalance.x028_Items.x08_Items)
                    if (!Items.ContainsKey(ItemData.x100))
                    {
                        Items.Add(ItemData.x100, new ItemData
                        {
                            ActorName = ItemData.x000_Text,
                            ItemType = ItemData.x10C_ItemTypesGameBalanceId,
                            SetItem = ItemData.x170_SetItemBonusesGameBalanceId != -1,
                            //ItemName = ItemStringList.TryGet(ItemData.x000_Text)
                        });
                    }
            }

            SNOItems = Items;
        }

        public static IEnumerable<T> GetContainer<T>(Enigma.D3.Enums.SnoGroupId id) where T : Enigma.D3.Assets.SerializeMemoryObject
        {
            return Enigma.D3.Assets.SnoHelper.Enumerate<T>(id);
        }
    }
}


/*public static void ItemName(string Name)
{
    List<string> Add = new List<string>();

    using (System.IO.StreamWriter fs = new System.IO.StreamWriter("./AAlist.txt", true))
    {
        foreach (StringList Stringlist in GetContainer<StringList>(Enigma.D3.Enums.SnoGroupId.StringList))
        {
            //if (!ItemSNOID.Contains(Stringlist.x00_Header.x00_SnoId))
            //  continue;

            if (Stringlist.x00_Header.x00_SnoId != 52009)
              continue;

            fs.WriteLine("-------------------");
            fs.WriteLine(Stringlist.x00_Header.x00_SnoId);

            foreach (var i in Stringlist.x10_StringTableEntries)
            {
                if (i.x10_Text.ToLower().Contains("[temp]"))
                    continue;
                /*
            if (Add.Contains(i.x00_Text))
            {
                fs.Write("------");
                fs.WriteLine("Duplicate");
                fs.Write("------");
            }

            fs.WriteLine("{\"" + i.x00_Text + "\",\"" + i.x10_Text + "\"},");
            Add.Add(i.x00_Text);

                fs.WriteLine("{ \""+ i.x00_Text +"\", \""+i.x10_Text+"\" },");
            }
        }

    }
}*/
