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
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates.Elites
{
    /// <summary>
    /// Interaction logic for EliteAffixes.xaml
    /// </summary>
    public partial class EliteAffixes : UserControl, IFKControl
    {
        public int BaseHeight = 40; // Right & Top
        public Point Base = new Point(372, 110); // Right & Top

        public EliteAffixes(Affix.AffixHelper Helper)
        {
            InitializeComponent();
            Canvas.SetTop(this, (110 + (BaseHeight * Helper.ElementKey)));

            int count = 0;
            int AffixCount = Helper.Affixes.Count;
            FontFamily Font = Extensions.GetFont("DINPro Regular");

            foreach (var x in Helper.Affixes)
            {
                var i = Config._.FKAffixes.Affixes.Find(b => b.Identifier == x);

                if (i.Styles == default(FKAffixes.FKAffix.Style))
                    continue;

                Thickness BorderThickness = new Thickness((count == 0) ? 1 : 0, i.Styles.BorderSize, 1, i.Styles.BorderSize);

                Border Border = new System.Windows.Controls.Border {
                    BorderThickness = BorderThickness, 
                    BorderBrush = i.Styles.BorderBrush,
                    Background = i.Styles.BackgroundBrush,
                    Padding = new Thickness(5,0,5,0)
                };

                Label Affix = new Label { 
                    FontFamily = Font, 
                    Content = i.Name, 
                    Foreground = i.Styles.ForegroundBrush,
                    FontSize = 14,
                };

                Border.Child = Affix;
                AffixesContainer.Children.Add(Border);
                count++;
            }
        }

        public void IUpdate() { }
    }
}
