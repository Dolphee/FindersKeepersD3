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

namespace FindersKeepers.Templates.Application.ElitePacks
{
    /// <summary>
    /// Interaction logic for GeneralSettings.xaml
    /// </summary>
    ///
    [ImplementPropertyChanged]
    public partial class GeneralSettings
    {
        public FKAffixes.FKAffixHelper Settings { get { return Config.Get<FKAffixes>().Settings; } }
        public IEnumerable<Controller.Enums.Difficulty> Difficultys { get {
                return Enum.GetValues(typeof(Controller.Enums.Difficulty)).Cast<Controller.Enums.Difficulty>().Where(x => (int)x != 35); }
        }

        public GeneralSettings(object p) : base(p)
        {
            InitializeComponent();
        }
    }
}
