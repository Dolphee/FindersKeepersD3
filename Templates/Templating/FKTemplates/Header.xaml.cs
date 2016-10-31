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

namespace FindersKeepers.Templates.Templating.FKTemplates
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : BasicEvents
    {
        public Header()
        {
            InitializeComponent();


           // t.Backg<Border>(SolidColorBrush.ColorProperty, Extensions.HexToColor("#fff"), Extensions.HexToColor("#000"));
        }

        private void MiniMize(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("#eeeeee", false);
            ((Border)sender).BorderThickness = new Thickness(1, 0, 0, 1);
        }

        private void MiniMizeOut(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("transparent", false);
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
        }

    }
}
