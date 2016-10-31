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
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates.SkillScreen
{
    /// <summary>
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl, IPerform
    {
        public Enigma.D3.Actor RActor;

        public Screen(Enigma.D3.Actor RActor)
        {
            InitializeComponent();
            this.RActor = RActor;
        }

        public void Set()
        {
            Extensions.Execute.UIThread(() =>
            {
                /*if (Percentage.Percentage < 0)
                {
                    Controller.GameManager.Attacher.Delete(this);
                    return;
                }*/

                //Percentage.Percentage -= 0.5;

                Point P = Helpers.DiabloIII.FromD3toScreenCoords(Helpers.DiabloIII.Player().Point(), new System.Windows.Media.Media3D.Point3D(RActor.x0A8_WorldPosX, RActor.x0AC_WorldPosY, RActor.x0B0_WorldPosZ));

                Canvas.SetTop(this, P.Y);
                Canvas.SetLeft(this, P.X);
            });
        }
    }
}
