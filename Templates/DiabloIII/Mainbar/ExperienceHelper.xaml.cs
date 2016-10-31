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
using FindersKeepers.Controller;
using System.Windows.Shapes;

namespace FindersKeepers.Templates.Mainbar
{
    /// <summary>
    /// Interaction logic for ExperienceHelper.xaml
    /// </summary>
    public partial class ExperienceHelper : UserControl, IFKControl
    {
        public List<ExperienceTick> Controls = new List<ExperienceTick>();
        public void IUpdate() { }
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }

        public class ExperienceTick
        {
            public double _value;
            public Canvas Control;
            public FKConfig.GeneralSettings.ExperienceBar.Items.TargetType Type;

            public void Update()
            {
                double val = Helpers.ExperienceHelper.Call(Type);

                if(val != _value)
                {
                    _value = val;

                    Extensions.Execute.UIThread(() =>
                    {
                        FKMethods.TagHelper Tag = Control.Tag.CastHelper<FKMethods.TagHelper>();
                        Tag.Handler.CastHelper<Border>().AnimateBackground(Tag, true, true);

                        this.Control.FindChild<TextBlock>("").Text = "+"+_value;

                    });
                }

            }

        }

        public ExperienceHelper()
        {
            InitializeComponent();

            // LootTracker.Ani

            /*LootTracker.Loaded += ((s,e) => IMapExperienceHelper.RegisterToolTip(LootTracker, 35, "Markers on Minimap"));
            AvgLegHour.Loaded += ((s, e) => IMapExperienceHelper.RegisterToolTip(AvgLegHour, 35, "Legendaries per hour"));
            AncientRate.Loaded += ((s, e) => IMapExperienceHelper.RegisterToolTip(AncientRate, 35, "Item Ancient %"));
            */
            LootTracker.Tag =  new FKMethods.TagHelper
                {
                    Handler = LootTracker.Children.OfType<Border>().FirstOrDefault(),
                    Transition =
                        new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                        {
                            Active = Extensions.HexToColor("#274b6c"),
                            Hover = Extensions.HexToColor("#274b6d"),
                            Reset = Extensions.HexToColor("#2c0913"),
                            FadeIn = 500,
                            FadeOut = 500
                        }
                };

            Controls.Add(new ExperienceTick { Control = LootTracker, Type = FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LegendaryOnMap });
        }
    }
}
