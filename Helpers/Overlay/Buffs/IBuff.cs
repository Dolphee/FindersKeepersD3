using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using Enigma.D3.UI;
using System.Windows.Controls;
using System.Windows.Shapes;
using FindersKeepers.Helpers.Styles;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Helpers.Overlay.Buffs
{
    public class IBuff : NotifyingObject
    {
        public object _control;
        public Buff Buff { get; set; }
        public bool NoCooldown { get; set; }
        public double EndTick { get; set; }
        private OutlinedTextBlock Text { get; set; }
        public double _x = 0;
        public int pos { get; set; }
        public double CurrentValue { get; set; }
        public int T { get; set; }
        public int Identifier { get; set; }
        public int Address { get; set; }
        public  int PowerSNO { get; set; }

        public double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    Refresh("X");
                }
            }
        }

        public IBuff(Buff b, int Position)
        {
            Buff = b;
            // NoCooldown = b.x010_DurationInTicks == -1 || Buff.x000_PowerSnoId == (int)SnoPower.Convetions_Of_Elements;
             NoCooldown = b.x010_DurationInTicks == -1;
            pos = Position;
            PowerSNO = b.x000_PowerSnoId;
            Address = b.Address;
            ReloadTick();
        }

        public void ReloadTick()
        {
            //if (!NoCooldown)
            //{
                EndTick = (GameManagerAccountHelper.Current.Player.GetAttribute(
                    ((Enigma.D3.Enums.AttributeId)((int)Enigma.D3.Enums.AttributeId.BuffIconEndTick0) + Buff.x004_Neg1), Buff.x000_PowerSnoId));
                Identifier = Buff.x004_Neg1;
            //}
        }

        public object Control
        {
            get
            {
                return (_control = _control ?? CreateControl());
            }
        }

        public object CreateControl()
        {
            if (NoCooldown)
                return null;

            Text = new OutlinedTextBlock
            {
                Text = "0.0",
                FontFamily = FKMethods.HelveticaFont,
                FontSize = 20,
                Fill = Extensions.HexToBrush("#fff"),
                TextAlignment = System.Windows.TextAlignment.Center,
                Margin = new System.Windows.Thickness(0, -2, 0, 0)
            };

            return Text;
        }

        public bool Update()
        {
             X = (pos == 0) ? 0 : (pos * 55) + (3.2 * pos);

            if (Address != Buff.Address)
                return false;

            if (PowerSNO != Buff.x000_PowerSnoId) // Changed Memory Address or Expired
                return false;

            ReloadTick();
           // if (Position > 8)
           //  return true;

            if (Text != null)
            {
                double Value = (EndTick - GameManager.Instance.GameTicks) / 60d;

                double FValue = Math.Round(Value, (Value < 1) ? 1 : 0);
                Extensions.Execute.UIThread(() => Text.Text = FValue.ToString());
                CurrentValue = Value;
            }

            return true;
        }
    }
}
