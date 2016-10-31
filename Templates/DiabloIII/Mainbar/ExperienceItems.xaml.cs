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
    /// Interaction logic for ExperienceItems.xaml
    /// </summary>
    public partial class ExperienceItems : UserControl, IFKControl
    {
        public ulong TotalGained = 0;
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public int i = 0;

        public ExperienceItems()
        {
            InitializeComponent();
        }

        public void IUpdate()
        {
            ulong Total = Helpers.ExperienceHelper.TotalGained;

            if (TotalGained != Total)
            {
                TotalGained = Total;
                Extensions.Execute.UIThread(() =>
                {
                    ExperienceBarValue.Text = Total.ToString("#,##0") + " / " + Helpers.ExperienceHelper.NextLevelExperience.ToString("#,##0");
                });
            }
        }
    }
}
