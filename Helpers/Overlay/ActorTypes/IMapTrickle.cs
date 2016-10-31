using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using Enigma.D3;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public class IMapTrickle : IMapBase, IMap
    {
        public int Id { get; }
        public object _control;
        public bool Validated { get; set; }
        public Trickle Marker;
        public Size Size;

        public IMapTrickle(Trickle Mark)
        {
            Id = Mark.Address;
            Marker = Mark;
        }

        public object Control
        {
            get
            {
                return (_control = _control ?? CreateControl());
            }
        }

        public object CreateControl()
        {
            MapItemShape Shape = Config.MiniMapItems(MapItemElement.AT_Objective, -1).Shape;
            Size = new System.Windows.Size(Shape.Width / 2, Shape.Height / 2);

            if (Shape.Shape == ItemShape.Ellipse)
                return new Ellipse
                {
                    Width = Shape.Width,
                    Height = Shape.Height,
                    Opacity = Shape.Opacity,
                    Fill = Shape.FillBrush,
                    StrokeThickness = Shape.StrokeThickness,
                    Stroke = Shape.StrokeBrush
                };

            else if (Shape.Shape == ItemShape.Rectangle)
                return new Rectangle
                {
                    Width = Shape.Width,
                    Height = Shape.Height,
                    Opacity = Shape.Opacity,
                    Fill = Shape.FillBrush,
                    StrokeThickness = Shape.StrokeThickness,
                    Stroke = Shape.StrokeBrush
                };

            return null;
        }

        public bool Update(System.Windows.Point Player, Point Additional = default(Point))
        {
            if (!IsValid())
                return false;

            var Temp = new Point(Marker.x08_WorldPosX - Player.X, Marker.x0C_WorldPosY - Player.Y);

            if (Additional != default(Point))
            {
                Temp.X = (Additional.X < 0) ? Temp.X + -Additional.X : Temp.X - Additional.X;
                Temp.Y = (Additional.Y < 0) ? Temp.Y + -Additional.Y : Temp.Y - Additional.Y;
            }

            X = Temp.X - Size.Width;
            Y = Temp.Y - Size.Height;

            return true;
        }

        public bool IsValid()
        {
            return true;
           
        }
    }
}
