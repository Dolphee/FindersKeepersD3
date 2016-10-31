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
using System.Globalization;

namespace FindersKeepers.Templates.Overlay
{
    /// <summary>
    /// Interaction logic for ItemOverlay.xaml
    /// </summary>
    public partial class ItemOverlay : UserControl, IFKControl
    {
        public void IUpdate() { }
        public ItemOverlay()
        {
            InitializeComponent();
        }
    }


    public class BorderColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            OverlayItems.SimpleItem Item = value as OverlayItems.SimpleItem;

            if (Item.AncientItem)
                return TextColors.TryGet((Item.SNOItem.SetItem) ? "Set" : "Legendary").Color;
            else
                return "#141414".ToColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            OverlayItems.SimpleItem Item = value as OverlayItems.SimpleItem;

            return TextColors.TryGet((Item.SNOItem.SetItem) ? "Set" : "Legendary");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
