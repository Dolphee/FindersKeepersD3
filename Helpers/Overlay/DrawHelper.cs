using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace FindersKeepers.Helpers.Overlay
{
    public class DrawHelper
    {

        public static PensHelper RevealMinimap = new PensHelper
        {
            Enabled = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Reveal,
            Opacity = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Opacity,
            Pen = Extensions.HexToBrush("#" + Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Background)
        };

        public static PensHelper RevealLargemap = new PensHelper
        {
            Enabled = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Reveal,
            Opacity = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Opacity,
            Pen = Extensions.HexToBrush("#" + Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Background)
        };

        public static BitmapSource CreateBitmap(int width, int height, double dpi, Action<DrawingContext> render)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                render(drawingContext);
            }

            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                width, height, dpi, dpi, PixelFormats.Default);
            bitmap.Render(drawingVisual);

            //bitmap.Freeze();
            return bitmap;
        }
        public class PensHelper
        {
            public bool Enabled;
            public float Opacity;
            public Brush Pen;
        }
    }
}
