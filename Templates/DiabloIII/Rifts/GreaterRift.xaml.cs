using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Enigma.D3.UI.Controls;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.Rifts
{
    /// <summary>
    /// Interaction logic for GreaterRift.xaml
    /// </summary>
    public partial class GreaterRift : UserControl, IFKControl
    {
        public Point _TempPoint;

        public GreaterRift()
        {
            InitializeComponent();
            Helpers.UIObjects.GreaterRift.Reload();

            _TempPoint = Helpers.UIObjects.GreaterRift.TryGetPoint<UXItemsControl>(Enigma.D3.OffsetConversion.UXControlRect);
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
                if (!Helpers.UIObjects.GreaterRift.TryGetValue<UXItemsControl>())
                {
                    GameManager.Instance.GManager.GRef.D3OverlayControl.Delete<Templates.Rifts.GreaterRift>();
                    return;
                }

                var Temp = Helpers.UIObjects.GreaterRift.TryGetPoint<UXItemsControl>(Enigma.D3.OffsetConversion.UXControlRect);

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
                (Enigma.D3.Engine.Current.ObjectManager.xA04_Ptr_TimedEvents.Dereference().First()._x08 - GameManager.Instance.GameTicks) / 60d; // x.08 = stop x.04 = startime
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
