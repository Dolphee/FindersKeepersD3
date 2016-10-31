using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindersKeepers.Controller
{
    public enum SkillPosition : short
    {
        LeftMouse = 0,
        RightMouse,
        Slot_1,
        Slot_2,
        Slot_3,
        Slot_4,
        Potion
    }

    public class SkillbarHelper : NotifyingObject
    {
        public SnoPower SnoPower { get; set; }
        public int SnoId { get; set; }
        public SkillPosition SlotPostion { get; set; }
        public double _Value;
        public double Value {
            get {
                return _Value; }

            set {
                if(_Value != value)
                {
                    _Value = value;
                    Refresh("Value");
                }
            }
        }

    }

}
