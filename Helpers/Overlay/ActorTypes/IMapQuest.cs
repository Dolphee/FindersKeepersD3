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
    public class IMapQuest : IMapBase, IMap
    {
        public int Id { get; }
        public object _control;
        public bool Validated { get; set; }
        public Marker Marker;
        public Size Size;

        public IMapQuest(KeyValuePair<int, Marker> Mark)
        {
            Id = Mark.Key;
            Marker = Mark.Value;
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

            if (Marker.x1C_StringListSnoId == ItemQualityMinimap.KeyWarden)
            {
                return new Ellipse
                {
                    Width = Shape.Width,
                    Height = Shape.Height,
                    Opacity = Shape.Opacity,
                    Fill = Extensions.HexToBrush("#ae2af8"),
                };
            }

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

            var Temp = new Point(Marker.x00_WorldPosition.x00_X - Player.X, Marker.x00_WorldPosition.x04_Y - Player.Y);

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
            var Items = Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary();

            if (!Items.ContainsKey(Id))
                return false;

            return true;
        }
    }



}