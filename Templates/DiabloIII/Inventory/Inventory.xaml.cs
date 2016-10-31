using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FindersKeepers.Helpers;
using Enigma.D3.UI.Controls;
using FindersKeepers.Controller;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Templates.Inventory
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl, IFKControl
    {
        public bool DynamicSize {get; set; }
        public bool DynamicSizeChanged { get; set; }
        public Inventory()
        {
            InitializeComponent();
            TownHelper.UpdateStash();
            Set();
        }

        public void IUpdate()
        {
            if (!UIObjects.Inventory.TryGetValue<UXLabel>())
            {
                if (this.Visibility != System.Windows.Visibility.Collapsed)
                    Extensions.Execute.UIThread(() =>
                    {
                        this.Visibility = System.Windows.Visibility.Collapsed;
                    });
            }

            else // Visible
            {
                if (this.Visibility == Visibility.Collapsed)
                    Extensions.Execute.UIThread(() =>
                    {
                        this.Visibility = System.Windows.Visibility.Visible;
                        Set();
                    });
            }
        }

        public void Set()
        {
            Dictionary<int, GameManagerData.InGameData.Stash.StashItem> Item = GameManager.Instance.GManager.GList.MainAccountData.GameData.StashItems.Items;

            Extensions.Execute.UIThread(() =>
            {
                DeathBreaths.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Death_Breath).Value.Quantity.ToString("N0");
                Crystals.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Veiled_Crystal).Value.Quantity.ToString("N0");
                ForgottenSouls.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Forgotten_Soul).Value.Quantity.ToString("N0");
                Arcane.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Arcane_Dust).Value.Quantity.ToString("N0");
                Reusable.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Reusable_Parts).Value.Quantity.ToString("N0");

                A1Craft.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Khanduran_Rune).Value.Quantity.ToString("N0");
                A2Craft.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Caldeum_Nightshade).Value.Quantity.ToString("N0");
                A3Craft.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Arreat_War_Tapestry).Value.Quantity.ToString("N0");
                A4Craft.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Corrupted_Angel_Flesh).Value.Quantity.ToString("N0");
                A5Craft.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Westmarch_Holy_Water).Value.Quantity.ToString("N0");
                TiredLootKeys.Text = Item.SingleOrDefault(x => x.Key == (int)GameManagerData.InGameData.InventoryItems.Tired_Loot_Key).Value.Quantity.ToString("N0");

            });
        }
    }
}
