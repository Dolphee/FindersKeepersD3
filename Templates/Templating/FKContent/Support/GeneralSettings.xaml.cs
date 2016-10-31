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
using System.Reflection;
using PropertyChanged;

namespace FindersKeepers.Templates.Support
{
    /// <summary>
    /// Interaction logic for GeneralSettings.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class GeneralSettings
    {
        public FKConfig.GeneralSettings.Settings Settings { get { return Config.Get<FKConfig>().General.FKSettings; } }

        public GeneralSettings(object support) : base(support)
        {
            InitializeComponent();

            (support as SupportController).IsTemplate = true;
            (support as SupportController).IDesignHelper.Settings.UseMainMenu = false;

        }
    }
}
