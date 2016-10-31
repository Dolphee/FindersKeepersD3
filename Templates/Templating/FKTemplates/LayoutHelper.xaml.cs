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
    /// Interaction logic for LayoutHelper.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class LayoutHelper : UserControl
    {
        public Header HeaderControl { get { if (_HeaderControl == null) _HeaderControl = Activator.CreateInstance<Header>(); return _HeaderControl; } }
        public IMainUpdate MainControl { get { if (_MainContent == null) _MainContent = (IMainUpdate)Extensions.Create<MainContent>(this); return _MainContent; } set { _MainContent = value; } }
        public SideControl SideControl { get { if(_SideControl == null) _SideControl = Extensions.Create<SideControl>(this); return _SideControl; }}
        public SideControl _SideControl { get; set; }
        public IMainUpdate _MainContent { get; set; }
        public Header _HeaderControl { get; set; }

        public LayoutHelper()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void GetPage(UIElement Control)
        {
            MainControl.UpdateMain(Control);

            if (this is IDesign && Control is IDesignUpdate)
                (this as IDesign).CurrentTarget = (IDesignUpdate)Control;

            if (Control is IDesignUpdate)
                ((IDesignUpdate)Control).CollectionChanged();
        }

        public void GetPage<T>() where T : UIElement
        {
            GetPage(Extensions.Create<T>(this));
        }

    }
}
