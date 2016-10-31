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
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;
using Enigma.D3.UI.Controls;
using Enigma.D3;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for CMinimap.xaml
    /// </summary>
    public partial class CMinimap : UserControl, IFKControl
    {
        public CMinimap()
        {
            InitializeComponent();
        }

        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public void IUpdate() {}
    }
}