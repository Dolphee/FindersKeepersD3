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

namespace FindersKeepers.Templates.Mainbar
{
    /// <summary>
    /// Interaction logic for MainBarControl.xaml
    /// </summary>
    public partial class MainBarControl : UserControl, IFKControl
    {
        /*
            UI Control
            Root.NormalLayer.game_dialog_backgroundScreenPC.game_window_hud_overlay
            1600 x 200 - L: 0, T: 1000, R: 1600, B: 1200
        */
        public void IUpdate() { }
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }

        public MainBarControl()
        {
            InitializeComponent();
        }

    }
}
