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
using System.Windows.Media.Animation;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    public partial class Information : IFKControl
    {
        public bool DynamicSize { get;set; }
        public bool DynamicSizeChanged { get; set; }
        public void IUpdate() { }

        public Information()
        {
            InitializeComponent();
            FK.Content = "FindersKeepers - "+ FindersKeepers.MainWindow.Version;
         
            try
            {
                FK.AnimateFade<Label>(0,1, new Duration(TimeSpan.FromSeconds(4)), TimeSpan.FromSeconds(2), (() => 
                {
                     FK.AnimateFade<Label>(1,0, new Duration(TimeSpan.FromSeconds(3)), TimeSpan.FromSeconds(3));
                }));

                FKLogo.AnimateFade<Image>(0, 1, new Duration(TimeSpan.FromSeconds(4)), TimeSpan.FromSeconds(2), (() =>
                {
                    FKLogo.AnimateFade<Image>(1, 0, new Duration(TimeSpan.FromSeconds(3)), TimeSpan.FromSeconds(3), (() =>
                    {
                        if (GameManager.Instance.GManager.GRef.Attacher != null)
                            GameManager.Instance.GManager.GRef.Attacher.CloseInformation();
                    }));
                }));
            }

            catch {
                // Closed before exit
            }
        }
    }
}
