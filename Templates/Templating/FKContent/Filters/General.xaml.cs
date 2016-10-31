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
using FindersKeepers.Controller.Enums;
using FindersKeepers.Templates.Templating.FKTemplates;
using System.Collections.ObjectModel;
using FindersKeepers.Controller;
using PropertyChanged;

namespace FindersKeepers.Templates.Filters
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    /// 
    public partial class General
    {
        public List<Border> QualityBorder = new List<Border>();
        public Brush Active;
        public Dictionary<int, GradientStop[]> ColorList = new Dictionary<int, GradientStop[]>();

        public override void CollectionChanged()
        {
            ItemQualityShort Quality = (ItemQualityShort)Enum.Parse(typeof(ItemQualityShort), TryGetDataObject<FilterController>().Filter.Quality.ToString());

            int c = 1;
            foreach (Border i in SetQualities.Children.OfType<Border>())
            {
                QualityBorder.Add(i);
                i.Opacity = 0.3;
                if ((short)Quality == c)
                {
                    Active = i.Background;
                    i.Opacity = 1;
                }
                c++;
            }
        }

        public General(object f) : base(f)
        {
            InitializeComponent();
        }

        private void SetQuality(object sender, MouseEventArgs e)
        {
            foreach(Border x in QualityBorder )
            {
                if (x.Background == sender.CastVisual<Border>().Background)
                {
                    x.Opacity = 1;
                    continue;
                }

                x.Opacity = 0.3;
            }
        }

        private void ResetQuality(object sender, MouseEventArgs e)
        {
            foreach (Border x in QualityBorder)
            {
                if (x.Background == Active)
                {
                    x.Opacity = 1;
                    continue;
                }

                x.Opacity = 0.3;
            }
        }

        private void ChangeQuality(object sender, MouseButtonEventArgs e)
        {
            int i = 1;
            foreach (Border x in QualityBorder)
            {
                if (x.Background == sender.CastVisual<Border>().Background)
                    break;
                i++;
            }

            ItemQualityShort Quality =  (ItemQualityShort)i;
            ItemQuality RealQuality = (ItemQuality)Enum.Parse(typeof(ItemQuality), Quality.ToString());
            TryGetDataObject<FilterController>().Filter.Quality = RealQuality;

            Active = sender.CastVisual<Border>().Background;
            ResetQuality(sender, null);
        }
    }
}
