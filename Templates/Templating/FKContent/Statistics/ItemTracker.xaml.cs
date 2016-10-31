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

namespace FindersKeepers.Templates.Statistics
{
    /// <summary>
    /// Interaction logic for ItemTracker.xaml
    /// </summary>
    public partial class ItemTracker : UserControl
    {
        public ItemTracker()
        {
            InitializeComponent();

            AncientRate.AnimateDependency<UIElement>(AncientRate.Per, 0, 50, Duration.Automatic);

            //AncientRate.AnimateSize<UIElement>(0, 100, Duration.Automatic);

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

        public void BackgroundEventWhite(object sender, EventArgs e, bool Out = false)
        {
            if (Out)
                ((Border)sender).Background = Extensions.HexToBrush("#ffffff");
            else
                ((Border)sender).Background = Extensions.HexToBrush("#f0f0f0");
        }

        public void BackgroundEventGrey(object sender, EventArgs e, bool Out = false)
        {
            if (Out)
                ((Border)sender).Background = Extensions.HexToBrush("#f0f0f0");
            else
                ((Border)sender).Background = Extensions.HexToBrush("#ffffff");
        }

        private void CloseFK(object sender, MouseButtonEventArgs e)
        {
        }

        private void MiniMize(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("#eeeeee");
            ((Border)sender).BorderThickness = new Thickness(1, 0, 0, 1);
        }

        private void MiniMizeOut(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("transparent");
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void LoadPage(object sender, MouseButtonEventArgs e)
        {
            Container.Children.Clear();
            Container.Children.Add((UIElement)Activator.CreateInstance(typeof(ItemList)));
        }   
    }
}
