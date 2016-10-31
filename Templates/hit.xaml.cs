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

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for hit.xaml
    /// </summary>
    public partial class hit : UserControl
    {
        public hit(Rect f)
        {
            InitializeComponent();

            Width = f.Width;
            Height = f.Height;
            Canvas.SetLeft(this, f.X);
            Canvas.SetTop(this, f.Y);
            Background = Brushes.Yellow;

        }
    }
}
