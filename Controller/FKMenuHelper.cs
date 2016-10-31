using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace FindersKeepers.Controller
{
    public class FKMenuHelper
    {
        public static List<MenuStruct> TopMenu = new List<MenuStruct>();

        public struct MenuStruct
        {
            public string Title { get; set; }
            public Image Image { get; set; }
            public bool OwnResize { get; set; }
            public MouseEnter MouseEvent { get; set; }
            public object Invoke { get; set; }
            public Action InvokeMethod { get; set; }
            public List<Type> Items { get; set; }

            public struct MouseEnter
            {
                public string Path {get; set;}
                public ImageSource Enter { get; set; }
                public ImageSource Leave { get; set; }
                public Colors Color { get; set; }

                public struct Colors
                {
                    public SolidColorBrush Active { get; set; }
                    public SolidColorBrush Hover { get; set; }
                    public SolidColorBrush Reset { get; set; }
                    public double FadeIn { get; set; }
                    public double FadeOut { get; set; }

                    public SolidColorBrush ResetBorder {
                        get; set; }
                }
            }
        }
    }
}
