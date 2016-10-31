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
    public partial class ErrorMessage : UserControl
    {
        public RunController RunController {get;set; }

        public ErrorMessage(object p)
        {
            InitializeComponent();
            RunController = (RunController)p;
        }

        private void ClickBtn(object sender, MouseButtonEventArgs e)
        {
            RunController.TryGet("GameInit");
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
