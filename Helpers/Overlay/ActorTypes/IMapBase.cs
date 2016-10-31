using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;

namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public abstract class IMapBase : NotifyingObject
    {
        public double _x;
        public double _y;

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
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    Refresh("Y");
                }
            }
        }
    }
}
