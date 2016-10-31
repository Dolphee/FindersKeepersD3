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
using FK.UI;

namespace FindersKeepers.Templates.DebugHelper
{
    /// <summary>
    /// Interaction logic for ItemViewer.xaml
    /// </summary>
    public partial class ItemViewer : UserControl
    {
        public BitmapImage Image { get; set; }
        public string Text { get; set; }
        public string Formatted { get; set; }

        public ItemViewer()
        {
            InitializeComponent();
            DataContext = this;

            Text = "x1_amulet_norm_unique_25";

            try
            {
                var Legendary = UniqueItemsController.TryGet(Text);
                    
                string Path = UniqueItemsController.GetBaseItemName(Text);
                string ItemNamePath = string.IsNullOrWhiteSpace(Legendary.Override) ? Path : Path + "_" + Legendary.Override;
                string ItemNamePather = (Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith) ? "SmithPlan" : ItemNamePath;

                // MessageBox.Show(Legendary.LegendaryItemType.ToString() + "/" + ItemNamePather);
                Formatted = Legendary.LegendaryItemType.ToString() + "/" + ItemNamePather + ".png";

                var value = Extensions.ItemImage(Legendary.LegendaryItemType.ToString() + "/" + ItemNamePather);

                Image = value;


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
