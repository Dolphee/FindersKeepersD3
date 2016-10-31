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
using System.Collections.ObjectModel;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;

namespace FindersKeepers.Templates.Filters
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class ItemFilter
    {
        public ObservableCollection<int> Active { get; set; }
        public ObservableCollection<int> NonActive {get;set; }

        public ItemFilter(object filters) : base(filters)
        {
            InitializeComponent();
            CollectionChanged();
        }

        public override void CollectionChanged()
        {
            List<int> ActiveList = TryGetDataObject<FilterController>().Filter.AttributesFilter.OrderBy(x => ((LegendaryItemsTypes)x).ToString()).ToList();
            List<int> NonActiveList = Enum.GetValues(typeof(LegendaryItemsTypes)).Cast<int>().ToList().Except(
                    TryGetDataObject<FilterController>().Filter.AttributesFilter).OrderBy(x => ((LegendaryItemsTypes)x).ToString()).ToList();

            Active = new ObservableCollection<int>(ActiveList);
            NonActive = new ObservableCollection<int>(NonActiveList);
         }

        private void IconHover(object sender, MouseEventArgs e)
        {
             ((Border)sender).Background = Extensions.HexToBrush("#f0f0f0", false);
        }

        private void IconLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = Extensions.HexToBrush("#ffffff", false);
        }

        private void ChangeCollection(object sender, Direction direction)
        {
            ListBox s = sender as ListBox;

            if (s.SelectedIndex == -1)
                return;

            int Id = (int)s.SelectedItem;

            if (direction == Direction.Add)
                TryGetDataObject<FilterController>().Filter.AttributesFilter.Add(Id);

            else
                TryGetDataObject<FilterController>().Filter.AttributesFilter.Remove(Id);

            CollectionChanged();
        }

        private void MoveToDisabled(object sender, MouseButtonEventArgs e) { ChangeCollection(sender, Direction.Remove); }
        private void MoveToEnable(object sender, MouseButtonEventArgs e) { ChangeCollection(sender, Direction.Add); }

        private void MoveSingleDisable(object sender, MouseButtonEventArgs e) { ChangeCollection(ActiveBox, Direction.Remove); }
        private void MoveSingleEnable(object sender, MouseButtonEventArgs e) { ChangeCollection(notactiveBox, Direction.Add); }

        private void MoveAllEnabled(object sender, MouseButtonEventArgs e)
        {
            TryGetDataObject<FilterController>().Filter.AttributesFilter = new HashSet<int>(
                Enum.GetValues(typeof(LegendaryItemsTypes)).Cast<int>().ToList()
            );

            CollectionChanged();
        }

        private void MoveAllDisabled(object sender, MouseButtonEventArgs e)
        {
            TryGetDataObject<FilterController>().Filter.AttributesFilter = new HashSet<int>();
            CollectionChanged();
        }

        public enum Direction : uint
        {
            Add,
            Remove
        }
    }
}
