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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NeedAdmin : UserControl
    {
        public NeedAdmin()
        {
            InitializeComponent();
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.Window.DragMove();
            }
            catch
            {
            }
        }

        private void CloseFK(object sender, MouseButtonEventArgs e)
        {
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

        private void MinimizeClick(object sender, MouseButtonEventArgs e)
        {
            //MainWindow.Window.Minimize();
        }

    }
}
