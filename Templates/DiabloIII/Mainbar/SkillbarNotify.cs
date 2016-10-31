using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates.Mainbar
{
    public class SkillbarNotify : IFKControl, IFKWPF, IGameStatus
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public IFKControl Control {
            get; set; }

        public ObservableCollection<Controller.SkillbarHelper> _SkillItem;
        public ObservableCollection<Controller.SkillbarHelper> SkillItems { get { return _SkillItem; } }

        public SkillbarNotify()
        {
            _SkillItem = new ObservableCollection<Controller.SkillbarHelper>();
            Control = (IFKControl)new Skillbar { DataContext = this };
        }

        public void OnCreation() { }
        public void OnDestroy() { }
        public void OnExit() {
            Extensions.Execute.UIThread(() => _SkillItem.Clear());
        }
        public void OnJoin() {
           
            List<Controller.SkillbarHelper> Helper = Skills.GetSkillSNO();

            Extensions.Execute.UIThread(() =>
            {
                //_SkillItem = new ObservableCollection<SkillbarHelper>();

                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.Slot_1).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.Slot_2).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.Slot_3).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.Slot_4).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.LeftMouse).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.RightMouse).SingleOrDefault());
                _SkillItem.Add(Helper.Where(x => x.SlotPostion == Controller.SkillPosition.Potion).SingleOrDefault());
            });
        }

        public void IUpdate()
        {
            foreach (SkillbarHelper Helper in SkillItems)
                Helper.Value = Skills.Cooldown(Helper.SnoId);
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
               // double values = double.Parse(value.ToString());
                return ((double)value >= 0) ? Visibility.Visible : Visibility.Hidden;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden;
        }
    }
}
