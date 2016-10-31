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
using Enigma.D3.Helpers;

namespace FindersKeepers.Templates.Mainbar
{
    /// <summary>
    /// Interaction logic for Health.xaml
    /// </summary>
    public partial class Health : UserControl, IFKControl, IGameStatus
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public double _Life;
        public double _Regen;
        public void OnCreation() { }

        public Health()
        {
            InitializeComponent();
        }

        public void IUpdate()
        {
            double HitPoints = Math.Round((GameManagerAccountHelper.Current.Player.x188_Hitpoints / Attributes.HitpointsMaxTotal.GetValue(GameManagerAccountHelper.Current.Player)) * 100, 0);
            double Regens = Math.Round(Attributes.HitpointsRegenPerSecondTotal.GetValue(GameManagerAccountHelper.Current.Player), 0);

            if (_Life != HitPoints)
            {
                Extensions.Execute.UIThread(() => LifeCurrent.Text = HitPoints.ToString());
                _Life = HitPoints;
            }

            if (_Regen != Regens)
            {
                Extensions.Execute.UIThread(() => Regen.Text = ToNumber(Regens));
                _Regen = Regens;
            }
        }

        public string ToNumber(double Number)
        {
            if (Number > 1000000)
                return Number.ToString("#,#0,,M", System.Globalization.CultureInfo.InvariantCulture);
            else if (Number > 100000)
                return Number.ToString("#,#0,K", System.Globalization.CultureInfo.InvariantCulture);

            return Number.ToString();
        }

        public void OnExit()
        {

        }

        public void OnJoin()
        {

        }

        public void OnDestroy()
        {

        }
    }
}
