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
using Enigma.D3;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for ActorHelper.xaml
    /// </summary>
    public partial class ActorHelperDebug : UserControl, IPerform
    {
        public ActorHelperDebug()
        {
            InitializeComponent();


            Extensions.Execute.UIThread(() =>
            {
                Ta.Children.Clear();

                ActorCommonData d = Helpers.DiabloIII.Player();

                foreach (var x in Engine.Current.ObjectManager.RActors.Dereference())
                {
                    if (x.x088_AcdId == -1)
                        continue;

                    ActorCommonData Actor = x.GetACDData();

                    Extensions.Execute.UIThread(() =>
                    {
                       // if (Actor.x004_Name.Split('-')[0] == "g_Portal_Rectangle_Orange_IconDoor")
                         //   FindersKeepers.Debug.GetValues.GetAttributes(Actor);

                        var xy = Actor.x0D0_WorldPosX - d.x0D0_WorldPosX;
                        var y = Actor.x0D4_WorldPosY - d.x0D4_WorldPosY;

                        var xa = Helpers.DiabloIII.FromD3toScreenCoords(
                            new System.Windows.Media.Media3D.Point3D(d.x0D0_WorldPosX, d.x0D4_WorldPosY, d.x0D8_WorldPosZ),
                            new System.Windows.Media.Media3D.Point3D(Actor.x0D0_WorldPosX, Actor.x0D4_WorldPosY, Actor.x0D8_WorldPosZ)
                         );

                        var T = new System.Windows.Controls.Label
                        {
                            Content = x.x004_Name.Split('-')[0],
                            Foreground = System.Windows.Media.Brushes.White
                        };

                        
                        Ta.Children.Add(T);

                        Canvas.SetLeft(T, xa.X);
                        Canvas.SetTop(T, xa.Y);
                    });

                }
            });

        }

        public void Set()
        {

        }
    }
}
