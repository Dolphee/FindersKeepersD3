using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Media;
using FindersKeepers.Controller.Enums;
using FindersKeepers.Controller.Pickit;
using System.Runtime.Serialization;
using System.Windows;
using FindersKeepers.Controller;
using Enigma.D3;
using Enigma.D3.Helpers;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Helpers;
using PropertyChanged;

namespace FindersKeepers
{
    // [XmlInclude(typeof(ItemFilter))]
    [Serializable(), ImplementPropertyChanged]
    public class FKFilters : SetDefault
    {
        [XmlArray("FilterItem")]
        [XmlArrayItem("FilterName")]
        public List<FilterItems> Filters = new List<FilterItems>();
        public object _DEFAULT()
        {
            return new FKFilters
            {
                Filters = new List<FilterItems>(){
                new FilterItems {
                    Name = "Legendary",
                     Enabled = true,
                     ID = 1,
                      Quality = ItemQuality.Legendary, OverlayIngame = true, ShowIfAncient = true,
                       SoundId = 0, 
                       AttributesFilter = new HashSet<int>((Enum.GetValues(typeof(LegendaryItemsTypes)).Cast<LegendaryItemsTypes>()).Cast<int>()),
                       SoundEnabled = true,
                       Sound = new List<FilterItems.Sounds>() {
                           new FilterItems.Sounds { AccountId = 0, SoundId = 0 },
                           new FilterItems.Sounds { AccountId = 1, SoundId = 0 },
                           new FilterItems.Sounds { AccountId = 2, SoundId = 0 },
                           new FilterItems.Sounds { AccountId = 3, SoundId = 0 } },
                         OverlayTimer = 10000,
                       FilterType = FilterItems.Type.Item
                }
            }
            };

        }
    }

    [Serializable(), ImplementPropertyChanged]
    public class FilterItems 
    {
        [Serializable()]
        public class Sounds : AvaiableSounds
        {
            [XmlAttribute]
            public int AccountId { get; set; }
            [XmlAttribute]
            public int SoundId { get; set; }
            [XmlIgnore]
            public FKAccounts.Multibox GetAccount { get { return Config.Get<FKAccounts>().GetAccount(AccountId); } }
        }

        [XmlAttribute]
        public int ID { get; set; }
        [XmlAttribute]
        public Type FilterType { get; set; }
        [XmlAttribute]
        public bool Enabled {get;set;}
        [XmlAttribute]
        public ItemQuality Quality { get; set; }
        [XmlAttribute]
        public bool SoundEnabled {get;set;}
        [XmlAttribute]
        public ushort OverlayTimer { get; set; }
        [XmlAttribute]
        public bool UseVoice { get; set; }
        [XmlAttribute]
        public bool ShowIfAncient{ get; set; }
        [XmlAttribute]
        public bool OverrideFilter{ get; set; }
        [XmlAttribute]
        public bool OverlayIngame { get; set; }
        [XmlAttribute]
        public bool OnlyAncient { get; set; }
        [XmlAttribute]
        public bool MultiboxSound { get; set; }
        [XmlAttribute]
        public bool DisableForKadala { get; set; }
        [XmlAttribute]
        public int SoundId {get;set;}
        [XmlElement]
        public string Name {get;set; }

        [XmlArray("Sounds")]
        [XmlArrayItem("Account", typeof(Sounds))]
        public List<Sounds> Sound { get; set; }

        [XmlArray("Pickit")]
        [XmlArrayItem("Item", typeof(int))]
        public HashSet<int> AttributesFilter {get;set;}

        public enum Type
        {
            Item,
            Gems,
            CraftingMaterials
        }
    }

    class ItemFilter
    {
        public static HashSet<int> Ignored = new HashSet<int>() { -1962741247 /*PowerGlobe*/, 126259833 /*Gold*/, 126259833 /*Gold*/, 126259834, 126259835  };

        public static ItemQuality OverrideQuality(Helpers.Item Item, LegendaryItemsTypes ItemType)
        {
            if (ItemType == LegendaryItemsTypes.PlanLegendary_Smith)
                Item.ItemQuality = ItemQuality.Legendary;

            /*
			        ItemQuality result;
			        if (ItemType == LegendaryItemsTypes.Demonic_Key || ItemType == LegendaryItemsTypes.Demonic_Key || ItemType == LegendaryItemsTypes.Gift)
			        {
				        result = ItemQuality.Legendary;
			        }
			        else
			        {
				        result = Item.ItemQuality;
			        }
			        return result;
		        */

            return Item.ItemQuality;
        }

        public static int x = 0;
        public static void Enumerate(ActorCommonData Actor, bool Gambling = false)
        {
            if (Ignored.Contains(Actor.x0B4_GameBalanceId))
                return;

            int key = Enigma.D3.Helpers.Attributes.Seed.GetValue(Actor);
            
            if (!GameManagerAccountHelper.Current.LevelAreaContainer.Items.Contains(key))
            {
                // Prevent leg in inventory / chat
                if(Actor.x090_ActorSnoId != -1 && Actor.x27C_WorldId != 0 && Actor.x0D0_WorldPosX != 0 && (Actor.x0B0_GameBalanceType == Enigma.D3.Enums.GameBalanceType.Items))
                {
                    Helpers.Item Item = new Helpers.Item(Actor);
                    bool ValidItem = (!Config.Get<FKConfig>().General.FKSettings.DebugMode);

                    if (!Gambling && ValidItem)
                        ValidItem = ((Enigma.D3.Helpers.Attributes.ItemAssignedHeroLow.GetValue(Item.ItemActor) > 1));

                    if (ValidItem)
                        FindersKeepers.Helpers.FKTracker.Add(Item, Gambling);

                    foreach (FilterItems Filter in Config._.FKFilters.Filters.Where(x => x.Enabled))
                    {
                        if (Filter.FilterType == FilterItems.Type.Item)
                        {
                            if (OverrideQuality(Item, (LegendaryItemsTypes)Item.SNOItem.ItemType) == Filter.Quality)
                            {
                                if (Gambling && Filter.DisableForKadala)
                                    continue;

                                if (Filter.AttributesFilter.Contains(Item.SNOItem.ItemType))
                                    SetAllowedItem(Item, Filter, true); // ValidItem
                            }
                        }
                    }
                    GameManagerAccountHelper.Current.LevelAreaContainer.Items.Add(key);
                }
            }
        }

        public static void SetAllowedItem(FindersKeepers.Helpers.Item Item, FilterItems Filter,bool ValidItem)
        {
            if ((!Config.Get<FKConfig>().General.FKSettings.DebugMode && (Filter.OnlyAncient && !Item.AncientItem)))
                return;

            //if (!ValidItem) // Check this!! Not triggering in debug mode
              //  return;

            FKAccounts.Multibox Box = null;

            if (Filter.UseVoice)
                SoundHelper.PlayVoice(Filter, Item);

            else if (Filter.MultiboxSound)
                SoundHelper.Play(Filter, GameManagerAccountHelper.CurrentData.Multibox.MultiboxID);

            else if (Filter.SoundEnabled)
               // SoundHelper.Play(Filter.SoundPath);
              
            if (Config.Get<FKConfig>().General.FKSettings.WriteToFile && Item.ItemQuality == ItemQuality.Legendary)
                    SoundHelper.OutputToFile(Item, GameManagerAccountHelper.CurrentData.Multibox.Nickname);

            Item.ShowAncient = Filter.ShowIfAncient;

            if (GameManager.Instance.GManager.GCache.Multiboxing)
                if ((GameManagerAccountHelper.CurrentData.Multibox.Foreground != null && GameManagerAccountHelper.CurrentData.Multibox.TextColor != null) && GameManagerAccountHelper.CurrentData.Multibox.TextColor.Length > 0)
                    Box = GameManagerAccountHelper.CurrentData.Multibox;

            if (Filter.OverlayIngame)
                GameManager.Instance.GManager.GRef.D3OverlayControl.ItemOverlay.Add(
                    new OverlayItems {
                        Account = Box,
                        Item = new OverlayItems.SimpleItem(Item),
                        ItemType = OverlayItems.Type.Item,
                        Transition = Filter.OverlayTimer
                    });
                 //GameManager.Instance.GManager.GRef.ItemOverlay.Add(new OverlayItems { Account = Box, Item = new OverlayItems.SimpleItem(Item), ItemType = OverlayItems.Type.Item, Transition = Filter.OverlayTimer });
        }
    }

}