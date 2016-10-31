using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace FindersKeepers.Helpers.Styles
{
    public class MapReveal : FrameworkElement
    {
        private VisualCollection visuals;
        private readonly DrawingVisual _visual = new DrawingVisual();
        protected readonly Brush Pens = Extensions.HexToBrush("#000000");
    
        public MapReveal()
        {
            SnapsToDevicePixels = true;
            visuals = new VisualCollection(this);
            visuals.Add(_visual);
        }

        public Image Load(NavContainer Container)
        {
            using (DrawingContext drawingContext = _visual.RenderOpen())
            {
                drawingContext.PushOpacity(0.5);

                foreach (var SceneData in Container.SNOScene.x180_NavZoneDefinition.x08_NavCells.Where(x => (x.x18 & 1) != 0))
                    drawingContext.DrawRectangle(Pens, null, new Rect(
                        new Point(Convert.ToDouble(SceneData.x00.X), Convert.ToDouble(SceneData.x00.Y)),
                        new Point(Convert.ToDouble(SceneData.x0C.X), SceneData.x0C.Y)));

                drawingContext.Pop();
            }

            Image myImage = new Image();
            RenderTargetBitmap bmp = new RenderTargetBitmap(340, 340, 120, 96, PixelFormats.Pbgra32);
            bmp.Render(_visual);
            myImage.Source = bmp;
            myImage.Tag = Container.SceneID;
            DrawEmpty();
            return myImage;
        }
        public void DrawEmpty()
        {
            using (DrawingContext drawingContext = _visual.RenderOpen())
            {
                drawingContext.Close();
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visuals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }
    }
}
