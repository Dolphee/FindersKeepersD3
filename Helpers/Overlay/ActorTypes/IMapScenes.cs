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
    public class IMapScenes : IMapBase, IMap
    {
        public int Id { get; }
        public object _control;
        public Image SceneMesh;
        public Point ScenePoint;
        public bool ValidScene = true;
        public List<NavContainer.Navigation> Cells;
        public DrawHelper.PensHelper PenHelper = DrawHelper.RevealMinimap;
        public bool Validated { get; set; }
        public NavContainer ef;

        public IMapScenes(NavContainer e)
        {
            this.Id = e.InternalID;
            ef = e;
            Cells = e.NavigationCells;
            this.ScenePoint = new Point(e.Min.X, e.Min.Y);
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
            System.Windows.Media.Imaging.BitmapSource Img = DrawHelper.CreateBitmap(240, 240, 96, ((e) =>
  
            //System.Windows.Media.Imaging.BitmapSource Img = DrawHelper.CreateBitmap((int)ef.Max.X, (int)ef.Max.Y, 96, ((e) =>
            {
                foreach (var SceneData in Cells)
                    e.DrawRectangle(PenHelper.Pen, null, new Rect(
                        new Point(SceneData.Min.X, SceneData.Min.Y),
                        new Point(SceneData.Max.X, SceneData.Max.Y)));
            }));

            Img.Freeze();
           
            Image Bit = new Image() { Opacity = PenHelper.Opacity };
            Bit.Stretch = Stretch.None;
            RenderOptions.SetBitmapScalingMode(Bit, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(Bit, EdgeMode.Aliased);
            Bit.Source = Img;

            return Bit;
        }

        public bool Update(System.Windows.Point Player, Point Additional = default(Point))
        {
            if(!IsValid(Player))
                return false;

            X = ScenePoint.X - Player.X;
            Y = ScenePoint.Y - Player.Y;
            return true;
        }

        public bool IsValid(System.Windows.Point Player)
        {
            Point f = new Point(ScenePoint.X - Player.X, ScenePoint.Y - Player.Y);

            if (MapItems.OutofBounds(f, 450))
                return false;
       
            return true;
        }
    }
}
