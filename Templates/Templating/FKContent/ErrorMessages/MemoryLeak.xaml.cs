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

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for ErrorMessage.xaml
    /// </summary>
    public partial class MemoryLeak : UserControl
    {
        public MemoryLeak()
        {
            InitializeComponent();
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            Extensions.TryInvoke(() => MainWindow.Window.DragMove());
        }

        private void CloseFK(object sender, MouseButtonEventArgs e)
        {
        }
        private void MiniMize(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("#eeeeee");
            ((Border)sender).BorderThickness = new Thickness(1, 0, 0, 1);
        }

        private void MiniMizeOut(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("transparent");
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
        }
        private void MinimizeClick(object sender, MouseButtonEventArgs e)
        {
            //MainWindow.Window.Minimize();
        }

        private void ClickBtn(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
           // MainWindow.Window.LoadPageIntoMain(typeof(Templates.GameInit));
        }

        private void AttachE(object sender, MouseEventArgs e)
        {
            var Tag = new FKMethods.TagHelper
            {
                Transition =
                       new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                       {
                           Active = Extensions.HexToColor("#274b6c"),
                           Hover = Extensions.HexToColor("#274b6d"),
                           Reset = Extensions.HexToColor("#f4f4f2"),
                           FadeIn = 500,
                           FadeOut = 200
                       }
            };


            AttachText.AnimateForeground(new FKMethods.TagHelper
            {
                Transition = new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                {
                    Active = Extensions.HexToColor("#6f6f6f"),
                    Hover = Extensions.HexToColor("#fff"),
                    Reset = Extensions.HexToColor("#6f6f6f"),
                    ResetBorder = Extensions.HexToColor("#fff"),
                    FadeIn = 500,
                    FadeOut = 200
                }
            });

            sender.CastVisual<Border>().AnimateBackground(Tag, false, true);
        }

        private void AttachEF(object sender, MouseEventArgs e)
        {
            var Tag = new FKMethods.TagHelper
            {
                Transition =
                       new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                       {
                           Active = Extensions.HexToColor("#274b6c"),
                           Hover = Extensions.HexToColor("#274b6d"),
                           Reset = Extensions.HexToColor("#f4f4f2"),
                           ResetBorder = Extensions.HexToColor("#fff"),
                           FadeIn = 300,
                           FadeOut = 200
                       }
            };

            AttachText.AnimateForeground(new FKMethods.TagHelper
            {
                Transition = new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                {
                    Active = Extensions.HexToColor("#6f6f6f"),
                    Hover = Extensions.HexToColor("#fff"),
                    Reset = Extensions.HexToColor("#6f6f6f"),
                    FadeIn = 300,
                    FadeOut = 200
                }
            }, true, true);

            sender.CastVisual<Border>().AnimateBackground(Tag, true, true);
        }

    }
}
