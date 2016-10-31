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
    /// Interaction logic for MainContent.xaml
    /// </summary>
    /// 

    [ImplementPropertyChanged]
    public partial class MainContent : UserControl, IMainUpdate
    {
        public MainContentMenu MenuBar { get { return Extensions.Create<MainContentMenu>(Params); } }
        public IDesign Params {get; set; }
        public bool GetWidth { get { return Extensions.GetNestedReflection<bool>("IDesignHelper.Settings.UseSideControl", Params); } }

        public MainContent(object p)
        {
            InitializeComponent();
            DataContext = this;
            Params = (IDesign)p;
        }

        public void UpdateMain(UIElement Control) {
            PageContent.Children.Clear();
            PageContent.Children.Add(Control);
        }

    }
}
