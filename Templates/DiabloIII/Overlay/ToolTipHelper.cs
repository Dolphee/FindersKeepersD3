using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;

namespace FindersKeepers
{
    public class ToolTipHelper : NotifyingObject
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string BorderBrush { get; set; }
        public double BorderSize { get; set; }
        public double CornerRadius { get; set; }
        public double Opacity { get; set; }
        public string Background { get; set; }
        public string Text { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public object Owner { get; set; }
    }
}
