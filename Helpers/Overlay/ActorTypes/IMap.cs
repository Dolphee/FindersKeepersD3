using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;
using Enigma.D3;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.ComponentModel;

namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public interface IMap : INotifyPropertyChanged
    {
        double X { get; }
        double Y { get; }
        object CreateControl();
        bool Update(System.Windows.Point Player, Point Additional = default(Point));
        int Id { get; }
        bool Validated { get; set;}

        /*public readonly int Id;
        public ActorCommonData Actor;
        public MapItemElement Type;
        public int EliteOffset = -1;
        public int ACDID;
        public int Identifier;
        private double _x;
        private double _y;
        public object _control;
        public bool Scene = false;
        public Image SceneMesh;
        public Point ScenePoint;
        public bool ValidScene = true;
        List<NavContainer.Navigation> Cells;

        public double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    Refresh("X");
                }
            }
        }
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    Refresh("Y");
                }
            }
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
            if (!Scene)
            {
                MapItemShape Shape = Config.MiniMapItems(Type, Identifier).Shape;

                if (Shape.Shape == ItemShape.Ellipse)
                    return new Ellipse
                    {
                        Width = Shape.Width,
                        Height = Shape.Height,
                        Opacity = Shape.Opacity,
                        Fill = Shape.FillBrush,
                        StrokeThickness = Shape.StrokeThickness,
                        Stroke = Extensions.HexToBrush(Shape.Stroke)
                    };

                else if (Shape.Shape == ItemShape.Rectangle)
                    return new Rectangle
                    {
                        Width = Shape.Width,
                        Height = Shape.Height,
                        Opacity = Shape.Opacity,
                        Fill = Shape.FillBrush,
                        StrokeThickness = Shape.StrokeThickness,
                        Stroke = Extensions.HexToBrush(Shape.Stroke)
                    };
            }

            else
            {
                System.Windows.Media.Imaging.BitmapSource Img = DrawHelper.CreateBitmap(240, 240, 96, ((e) =>
                {
                    foreach (var SceneData in Cells)
                        e.DrawRectangle(Brushes.Black, null, new Rect(
                            new Point(SceneData.Min.X, SceneData.Min.Y),
                            new Point(SceneData.Max.X, SceneData.Max.Y)));
                }));

                Img.Freeze();
                Image Bit = new Image() { Source = Img, Opacity = 0.5 };
                return Bit;
            }
            return null;
        }

        public IMap(int Id, List<NavContainer.Navigation> Navs,  Point ScenePoint)
        {
            this.Id = Id;
            Scene = true;
            Cells = Navs;
            this.ScenePoint = ScenePoint;
        }

        public IMap(ActorCommonData A, MapItemElement E, int EliteOff = -1, int I = -1)
        {
            Actor = A;
            Type = E;
            EliteOffset = EliteOff;
            Identifier = I;
            ACDID = Actor.x000_Id;
            Id = Actor.Address;
        }

        public bool Update()
        {
            if (!Scene)
            {
                if (!IsValid())
                    return false;

                X = Actor.x0D0_WorldPosX - Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D0_WorldPosX;
                Y = Actor.x0D4_WorldPosY - Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D4_WorldPosY;
                return true;
            }

            if (!ValidScene)
                



            X = ScenePoint.X - Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D0_WorldPosX;
            Y = ScenePoint.Y - Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D4_WorldPosY;
            return true;
        }

        public bool IsValid()
        {
            return Actor.x000_Id != -1 && Actor.x000_Id == ACDID && Actor.x188_Hitpoints != 0;
        }*/
    }   
}
