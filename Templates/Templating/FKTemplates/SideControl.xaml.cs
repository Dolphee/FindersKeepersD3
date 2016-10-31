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

namespace FindersKeepers.Templates.Templating.FKTemplates
{
    /// <summary>
    /// Interaction logic for SideControl.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class SideControl : UserControl
    {
        public IDesignHelper DesignHelper {get;set; }

        public SideControl(IDesign p)
        {
            InitializeComponent();
            this.DataContext = this;
            DesignHelper = p.IDesignHelper;
        }

        private void ChangeItem(object sender, MouseButtonEventArgs e)
        {
            IDesignHelper.ISideMenu Entry = (sender as FrameworkElement).Tag as IDesignHelper.ISideMenu;

            foreach (var x in DesignHelper.SideMenu)
                x.isActive = x.EntryId == Entry.EntryId;

            if(Entry.Target != null)
                Entry.Target.Invoke();

            (sender as FrameworkElement).Create().Register("From", "064c6c").Register("To", "5085a0")
                .Animate(SolidColorBrush.ColorProperty, 500, Extensions.FadeDirection.FadeIn);
        }

        private void ChangeItemEnter(object sender, MouseEventArgs e)
        {

            IDesignHelper.ISideMenu Value = (IDesignHelper.ISideMenu)(sender as FrameworkElement).Tag;

            if (Value.isActive)
                return;

            (sender as FrameworkElement).Create().Register("From", "fafafa").Register("To", "f2eeee")
            .Animate(SolidColorBrush.ColorProperty, 500, Extensions.FadeDirection.FadeIn);
        }

        private void ChangeItemLeave(object sender, MouseEventArgs e)
        {
            IDesignHelper.ISideMenu Value = (IDesignHelper.ISideMenu)(sender as FrameworkElement).Tag;

            if (Value.isActive)
                return;

            (sender as FrameworkElement).Create().Register("From", "f2eeee").Register("To", "fafafa")
           .Animate(SolidColorBrush.ColorProperty, 200, Extensions.FadeDirection.FadeIn);
        }
    }
}
