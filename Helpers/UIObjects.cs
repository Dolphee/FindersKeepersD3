using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Helpers;
using Enigma.D3.UI;
using Enigma.D3.UI.Controls;
using Enigma.Memory;

namespace FindersKeepers.Helpers
{
    public static class UIObjects
    {
        public class UIElementRoot<T> where T : UXControl
        {
            public int Pointer = -1;
            public int Base = -1;
            public string Name { get; set; }
            public int Offset { get; set; }
            public void Reload() { Base = UXHelper.GetControl<T>(Name).Address; Pointer = Base + Offset; }
            public void Dump()
            {
                ObjectDumper.Dump(UXHelper.GetControl<T>(Name));
            }
        }

        public partial class UIElement<T> : UIElementRoot<T> where T : UXControl
        {
            public int Value { get { return Read(); } }
            public bool IsVisible { get { return (Value & 4) != 0; } }
            public bool Cache;
            public int Key;

            public int Read(int offset = -1)
            {
                try
                {
                    if (offset == -1)
                        return Enigma.D3.Engine.Current.Read<int>(Pointer);
                    else
                        return Enigma.D3.Engine.Current.Read<int>(Base + offset);
                }

                catch { }

                return -1;
            }

            public T Read<T>(int offset = -1)
            {
                try
                {
                    if (offset == -1)
                        return Enigma.D3.Engine.Current.Read<T>(Pointer);
                    else
                        return Enigma.D3.Engine.Current.Read<T>(Base + offset);
                }

                catch { }

                return default(T);
            }
        }

        public static bool TryGetValue<T>(this UIElement<T> Element) where T : UXControl
        {
            Element.Cache = Element.IsVisible;

            if (Element.TryGet<T>())
                return Element.IsVisible;

            return false;
        }

        public static int ReadValue<T>(this UIElement<T> Element) where T : UXControl
        {
            if (Element.TryGet<T>())
                return Element.Read(Element.Offset);

            return -1;
        }

        public static E ReadValue<T,E>(this UIElement<T> Element, int offset) where T : UXControl
        {
            if (Element.TryGet<T>())
                return Element.Read<E>(offset);

            return default(E);
        }


        public static bool TryGet<T>(this UIElement<T> Element) where T : UXControl
        {
            if (Element.Pointer == -1)
                Element.Reload();

            return true;
        }

        public static System.Windows.Point TryGetPoint<T>(this UIElement<T> Element, int Offset) where T : UXControl
        {
            return new System.Windows.Point(
                Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.Read<float>(Element.Base + Offset),
                Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.Read<float>(Element.Base + Offset + 4)
            );
        }

        public static void ResetPointer()
        {
            Minimap.Reload();
            LargeMap.Reload();
            Inventory.Reload();
            Stash.Reload();
            Kadala.Reload();
            SkillsBar.Reload();
            InventoryTabs.Tabs.ForEach(x => x.Reload());

        }

        public static UIElement<UXMinimap> Minimap = new UIElement<UXMinimap> { Name = "Root.NormalLayer.minimap_dialog_backgroundScreen.minimap_dialog_pve.minimap_pve_main", Offset = 0x14 };
        public static UIElement<UXMinimap> LargeMap = new UIElement<UXMinimap> { Name = "Root.NormalLayer.map_dialog_mainPage.localmap", Offset = 0x14 };
        public static UIElement<UXLabel> Inventory = new UIElement<UXLabel> { Name = "Root.NormalLayer.inventory_dialog_mainPage", Offset = 0x14 };
        public static UIElement<UXItemsControl> Stash = new UIElement<UXItemsControl> { Name = "Root.NormalLayer.stash_dialog_mainPage", Offset = 0x14 };
        public static UIElement<UXLabel> Kadala = new UIElement<UXLabel> { Name = "Root.NormalLayer.shop_dialog_mainPage.gold_text", Offset = 0x14 };
        public static UIElement<UXControl> SkillsBar = new UIElement<UXControl> { Name = "Root.NormalLayer.SkillPane_main.LayoutRoot", Offset = 0x14 };

        public static UIElement<UXItemsControl> GemUpgrade = new UIElement<UXItemsControl> { Name = "Root.NormalLayer.vendor_dialog_mainPage.riftReward_dialog.LayoutRoot.gemUpgradePane", Offset = 0x14 };

        public static UIElement<UXItemsControl> GreaterRift = new UIElement<UXItemsControl> { Name = "Root.NormalLayer.eventtext_bkgrnd.eventtext_region.stackpanel.rift_wrapper.greater_rift_container.rift_progress_bar", Offset = 0x14 };
        public static UIElement<UXItemsControl> Rift = new UIElement<UXItemsControl> { Name = "Root.NormalLayer.eventtext_bkgrnd.eventtext_region.stackpanel.rift_wrapper.rift_container.rift_progress_bar", Offset = 0x14 };
        public static InventoryTab InventoryTabs = new InventoryTab();

        public class InventoryTab
        {
            public Dictionary<int, InventoryHelper> InventoryActors = new Dictionary<int, InventoryHelper>();
            public int TabsY = 10; // zero based index (Count = 10)
            public static int _ActiveTab = -1;
            public Templates.Inventory.Stash UIControl;

            public List<UIElement<UXItemButton>> Tabs = new List<UIElement<UXItemButton>>() {
                new UIElement<UXItemButton> { Name = "Root.NormalLayer.stash_dialog_mainPage.tab_1", Offset = 0xB84, Key = 0 },
                new UIElement<UXItemButton> { Name = "Root.NormalLayer.stash_dialog_mainPage.tab_2", Offset = 0xB84, Key = 1  },
                new UIElement<UXItemButton> { Name = "Root.NormalLayer.stash_dialog_mainPage.tab_3", Offset = 0xB84, Key = 2  },
                new UIElement<UXItemButton> { Name = "Root.NormalLayer.stash_dialog_mainPage.tab_4", Offset = 0xB84, Key = 3  },
                new UIElement<UXItemButton> { Name = "Root.NormalLayer.stash_dialog_mainPage.tab_5", Offset = 0xB84, Key = 4  },
            };
 
            public class InventoryHelper
            {
                public System.Windows.Point Position;
                public Enigma.D3.ActorCommonData Actor;
                public int YIndex = 0;
                public int Level;

                public InventoryHelper(Enigma.D3.ActorCommonData A)
                {
                    Position = new System.Windows.Point((A.x118_ItemSlotX), (A.x11C_ItemSlotY) - (_ActiveTab * 10));
                    Actor = A;
                    YIndex = A.x11C_ItemSlotY;
                    Level = Enigma.D3.Helpers.Attributes.JewelRank.GetValue(A);
                }

                public void ValidatePosition()
                {
                    if (Actor.x118_ItemSlotX != Position.X || Actor.x11C_ItemSlotY != YIndex)
                    {
                        Position = new System.Windows.Point((Actor.x118_ItemSlotX), (Actor.x11C_ItemSlotY) - (_ActiveTab * 10));
                        YIndex = Actor.x11C_ItemSlotY;
                    }
                }
            }

            public void CreateUIControl()
            {
                UIControl = Controller.GameManager.Instance.GManager.GRef.Attacher.Add<Templates.Inventory.Stash>("Stash");
            }

            public void WipeUIControl()
            {
                Controller.GameManager.Instance.GManager.GRef.Attacher.Remove("Stash");
                UIControl = null;
                InventoryActors.Clear();
            }

            public int ActiveTab()
            {
                foreach (var Tab in Tabs)
                    if (Tab.ReadValue<UXItemButton>() % 3 == 1)
                        return Tab.Key;

                return -1;
            }

            public void GetItems()
            {
                if (UIControl == null)
                    CreateUIControl();

                int Tab = ActiveTab();

                if (_ActiveTab != Tab)
                {
                    UIControl.Wipe();
                    InventoryActors.Clear();
                }

                _ActiveTab = Tab;

                foreach (var Item in Controller.GameManagerData.GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData)
                {
                    if (!Item.IsItem())
                        continue;

                    int Seed = Attributes.Seed.GetValue(Item);

                    if (InventoryActors.ContainsKey(Seed))
                    {
                        if(InventoryActors[Seed].Actor.x114_ItemLocation != Enigma.D3.Enums.ItemLocation.Stash)
                        {
                            InventoryActors.Remove(Seed);
                            continue;
                        }

                        InventoryActors[Seed].ValidatePosition();
                        continue;
                    }

                    else if(Item.x114_ItemLocation == Enigma.D3.Enums.ItemLocation.Stash)
                    {
                        var Sno = SNO.TryGet(Item);

                        if (!EqualityComparer<SNO.ItemData>.Default.Equals(Sno, default(SNO.ItemData)) && ((LegendaryItemsTypes)Sno.ItemType != LegendaryItemsTypes.LegendaryGem))
                            continue;

                        if (Item.x11C_ItemSlotY >= (TabsY * Tab) && Item.x11C_ItemSlotY < (TabsY * Tab) + TabsY)
                            InventoryActors.Add(Seed, new InventoryHelper(Item));
                    }
                }
            }
        }
    }
}
