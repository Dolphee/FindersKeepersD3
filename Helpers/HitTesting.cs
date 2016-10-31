using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Helpers;
using System.Windows;

namespace FindersKeepers.Helpers
{
    public class HitTesting
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public Point _mouse { get { return PInvokers.GetMousePosition(); } }
        public static Point Mouse { get { return PInvokers.GetMousePosition(); } }
        public List<Vector2D> Children = new List<Vector2D>();
        public int _count;
        public bool Open;

        public class Vector2D
        {
            public Rect Rect;
            public System.Windows.Controls.Canvas control;
            public bool Open;

            public bool InBounds()
            {
                if (Mouse.X >= Rect.X && Mouse.X < Rect.X + Rect.Width && Mouse.Y >= Rect.Y && Mouse.Y < Rect.Y + Rect.Height)
                {
                    if (!Open)
                    {
                        Extensions.Execute.UIThread(() =>
                        {
                            control.Visibility = Visibility.Visible;
                        });

                        Open = true;
                    }
                }

                else
                {
                    if (Open)
                    {
                        Extensions.Execute.UIThread(() =>
                        {
                            control.Visibility = Visibility.Hidden;
                        });
                        Open = false;
                    }
                }

                return true;
            }
        }

        public void Add(System.Windows.Controls.Canvas C, Rect ChildPoint)
        {
            Rect temp = new Rect(ChildPoint.X * Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale, ChildPoint.Y * Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale, 
                ChildPoint.Width * Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale, ChildPoint.Height * Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale);

            Children.Add(
                new Vector2D {
                    Rect = temp,
                    control = C
                });

            _count++;
        }

        public void HitTest()
        {

            if (Mouse.X >= X && Mouse.X < X + W && Mouse.Y >= Y && Mouse.Y < Y + H)
            {
                Open = true;
                if (_count != 0)
                {
                    foreach (var x in Children)
                        x.InBounds();
                }
            }

            else
            {
                if (Open)
                {
                    foreach (var x in Children)
                        x.InBounds();

                    Open = false;
                }
            }
        }
    }
}
