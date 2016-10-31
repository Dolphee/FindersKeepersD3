using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;
using Enigma.D3.UI.Controls;
using System.Globalization;

namespace FindersKeepers.Helpers.Styles
{
    class BuffRender : FrameworkElement
    {
        private VisualCollection visuals;
        private readonly DrawingVisual _visual = new DrawingVisual();

        public BuffRender()
        {
            SnapsToDevicePixels = true;
            visuals = new VisualCollection(this);

            visuals.Add(_visual);
        }

        public void Render(Dictionary<int, string> value)
        {
            return;
            Extensions.Execute.UIThread(() =>
            {
                using (DrawingContext drawingContext = _visual.RenderOpen())
                {
                    int values = 1;
                    foreach (var x in value)
                    {
                        if (values >= 8)
                            break;

                      /*  Point p = new Point(
                        (55 * x.Key) + (3 * x.Key) + ((55 - x.Value.Length) / 2) // Left
                         , 21);

                        Point p2 = new Point(
                       ((55 * x.Key) + (3 * x.Key) + ((55 - x.Value.Length) / 2)) - (2) // Left
                       , 22);

                        drawingContext.DrawGlyphRun(Brushes.Black, CreateGlyphRun(x.Value, 22, p2));
                        drawingContext.DrawGlyphRun(Brushes.White, CreateGlyphRun(x.Value , 19, p));
                        */

                         FormattedText Text = new FormattedText(x.Value.ToString(), CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                             new Typeface(Extensions.GetFont("Helvetica Rounded LT Black"), new FontStyle(), new FontWeight(), new FontStretch()),
                             20, Brushes.White);

                         Point p = new Point(
                            (55 * x.Key) + (3 * x.Key) + ((55 - Text.Width )/2) // Left
                             , 11);

                          Geometry textGeometry = Text.BuildGeometry(p);
                         drawingContext.DrawText(Text, p);
                         drawingContext.DrawGeometry(null, new Pen(new SolidColorBrush(Brushes.Black.Color), 1), textGeometry);
                         values++;
                    }
                }
            });
        }

        public GlyphRun CreateGlyphRun(string text, double size, Point origin)
        {
            Typeface typeface = new Typeface(Extensions.GetFont("Helvetica Rounded LT Black"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            GlyphTypeface glyphTypeface;
            if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
                throw new InvalidOperationException("No glyphtypeface found");

            ushort[] glyphIndexes = new ushort[text.Length];
            double[] advanceWidths = new double[text.Length];

            for (int n = 0; n < text.Length; n++)
            {
                ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];
                glyphIndexes[n] = glyphIndex;
                advanceWidths[n] = glyphTypeface.AdvanceWidths[glyphIndex] * size;
            }

            //P//oint origin = new Point(0, 0);

            GlyphRun glyphRun = new GlyphRun(glyphTypeface, 0, false, size, glyphIndexes, origin, advanceWidths, null, null, null,
                                             null, null, null);
            return glyphRun;
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
