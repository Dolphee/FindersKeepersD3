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

namespace FindersKeepers.Templates.Inventory
{
    /// <summary>
    /// Interaction logic for Stash.xaml
    /// </summary>
    public partial class Stash : UserControl, IPerform
    {
        public Dictionary<int, BorderHelper> Controls = new Dictionary<int, BorderHelper>();

        public class BorderHelper
        {
            public Border Control;
            public Point Position;
        }

        public Stash()
        {
            InitializeComponent();
        }

        public void Wipe()
        {
            Extensions.Execute.UIThread(() =>
            {
                Container.Children.Clear();
                Controls = new Dictionary<int, BorderHelper>();
            });
        }

        public void Set()
        {
            foreach (var Item in Helpers.UIObjects.InventoryTabs.InventoryActors)
            {
                if(Controls.ContainsKey(Item.Key)) // Validate
                {
                    if (Item.Value.Position.X != Controls[Item.Key].Position.X || Item.Value.Position.Y != Controls[Item.Key].Position.Y)
                    {
                        Extensions.Execute.UIThread(() =>
                        {
                            var _Control = Controls[Item.Key].Control;
                            Point Point = new Point(64 * Item.Value.Position.X, 64 * Item.Value.Position.Y);
                            Canvas.SetLeft(_Control, Point.X);
                            Canvas.SetTop(_Control, Point.Y);
                            Controls[Item.Key].Position = new Point(Item.Value.Position.X, Item.Value.Position.Y);
                        });
                    }
                }

                else
                {
                    Extensions.Execute.UIThread(() =>
                    {
                        Border x = new Border { Width = 63, Height = 63};
                        Grid Grid = new Grid {Width = 50, Height = 50, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        FindersKeepers.Helpers.OutlinedTextBlock Text = new Helpers.OutlinedTextBlock
                        {
                           FontFamily = FKMethods.HelveticaFont,
                           Fill = Extensions.HexToBrush("#fff"),
                           Stroke = Extensions.HexToBrush("#000"),
                           StrokeThickness = 1,
                           FontWeight = FontWeights.Bold,
                           FontSize = 15,
                           Margin = new Thickness(0,0,0,0),
                           HorizontalAlignment = HorizontalAlignment.Right,
                           VerticalAlignment = VerticalAlignment.Bottom,
                           Text = Item.Value.Level.ToString()
                        };
           
                        Point Point = new Point(64 * Item.Value.Position.X, 64 * Item.Value.Position.Y);
                        Canvas.SetLeft(x, Point.X);
                        Canvas.SetTop(x,  Point.Y);

                        Grid.Children.Add(Text);
                        x.Child = Grid;
                        Container.Children.Add(x);
                        Controls.Add(Item.Key, new BorderHelper { Position = new Point(Item.Value.Position.X, Item.Value.Position.Y), Control = x });
                    });
                }
            }

                var TempDic = Controls.Keys.ToList();

                foreach (var x in TempDic.Except(Helpers.UIObjects.InventoryTabs.InventoryActors.Keys))
                {
                    Extensions.Execute.UIThread(() =>
                    {
                        Container.Children.Remove(Controls[x].Control);
                        Controls.Remove(x);
                    });
                }
        }
    }
}
