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
using Enigma.D3;
using FindersKeepers.Helpers;
using Enigma.D3.UI.Controls;
using FindersKeepers.Controller.Enums;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for LargeMap.xaml
    /// </summary>
    public partial class LargeMap : UserControl, IFKControl
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public void IUpdate() { }

        public LargeMap()
        {
            InitializeComponent();
        }
    }
}
