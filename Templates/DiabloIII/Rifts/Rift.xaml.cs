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
using Enigma.D3.UI.Controls;
using Enigma.D3.UI;
using FindersKeepers.Controller;
using Enigma.D3.Helpers;
using FindersKeepers.Helpers;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.Rifts
{
    /// <summary>
    /// Interaction logic for GreaterRift.xaml
    /// </summary>
    public partial class Rift : UserControl, IFKControl
    {
        public Point _TempPoint;

        public Rift()
        {
            InitializeComponent();
            Helpers.UIObjects.Rift.Reload();

            _TempPoint = Helpers.UIObjects.Rift.TryGetPoint<UXItemsControl>(Enigma.D3.OffsetConversion.UXControlRect);
            UpdateRect();
        }

        public void UpdateRect()
        {
            Extensions.Execute.UIThread(() =>
            {
                Canvas.SetLeft(this, _TempPoint.X);
                Canvas.SetTop(this, _TempPoint.Y);
            });
        }

        public void IUpdate()
        {
            try
            {
                if (!Helpers.UIObjects.Rift.TryGetValue<UXItemsControl>())
                {
                    GameManager.Instance.GManager.GRef.D3OverlayControl.Delete<Templates.Rifts.Rift>();
                    return;
                }

                var Temp = Helpers.UIObjects.Rift.TryGetPoint<UXItemsControl>(Enigma.D3.OffsetConversion.UXControlRect);

                if (!Temp.Equals(_TempPoint))
                {
                    _TempPoint = Temp;
                    UpdateRect();
                }

                SoulsCaptured();
                GetTimeLeft();
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }

        public void SoulsCaptured()
        {
            double Total = Math.Round((Enigma.D3.Engine.Current.ObjectManager.x790.Dereference().x0F4_RiftProgress ), 0);
            Extensions.Execute.UIThread(() =>
            {
                Souls.Text = Total.ToString();
            });
        }

        public void GetTimeLeft()
        {
            double TicksLeft = Enigma.D3.Engine.Current.ObjectManager.xA04_Ptr_TimedEvents.Dereference().x08_Count == 0 ? 0 :
                (GameManager.Instance.GameTicks- Enigma.D3.Engine.Current.ObjectManager.xA04_Ptr_TimedEvents.Dereference().First()._x04) / 60d; // x.08 = stop x.04 = startime

            TimeSpan Time = TimeSpan.FromSeconds(TicksLeft);

            string TimeFormatted = String.Format("{0} {1}",
              Time.Minutes == 0 ? "" : Time.Minutes + " MIN",
              Time.Seconds == 0 ? "" : Time.Seconds + " SEC");

            Extensions.Execute.UIThread(() =>
            {
                TimeLeft.Text = TimeFormatted;
            });
        }
    }
 
}
