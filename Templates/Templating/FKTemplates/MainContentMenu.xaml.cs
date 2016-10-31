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
using PropertyChanged;

namespace FindersKeepers.Templates.Templating.FKTemplates
{
    /// <summary>
    /// Interaction logic for MainContentMenu.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class MainContentMenu : UserControl
    {
        public IDesignHelper DesignHelper { get; set; }

        public MainContentMenu(IDesign p)
        {
            InitializeComponent();
            DataContext = this;
            DesignHelper = p.IDesignHelper;
        }

        private void Change(object sender, MouseButtonEventArgs e)
        {
            IDesignHelper.IMenu Entry = (sender as FrameworkElement).Tag as IDesignHelper.IMenu;

            foreach (IDesignHelper.IMenu es in DesignHelper.Menu)
                es.isActive = Entry.Equals(es);

            if (Entry.Target != null)
                Entry.Target.Invoke();
        }
    }
}
