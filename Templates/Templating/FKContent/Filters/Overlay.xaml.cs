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
using FindersKeepers.Controller.Enums;
using FindersKeepers.Templates.Templating.FKTemplates;
using PropertyChanged;

namespace FindersKeepers.Templates.Filters
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    /// 

    public partial class Overlay 
    {
        public Overlay(object Data) : base(Data)
        {
            InitializeComponent();
        }
    }
}
