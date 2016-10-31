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
using System.Windows.Interop;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers.Styles
{
    class MinimapContainer : FrameworkElement
    {
        
        private VisualCollection visuals;
        private readonly DrawingVisual _visual = new DrawingVisual();
        private HashSet<int> Scenes = new HashSet<int>();
        public PensHelper RevealMinimap;
        public PensHelper RevealLargemap;
        public Point LargeMapOffset = new Point(0,0);
        private bool LargeMinimap = false;
        public bool QuestMarker = Config.Get<FKConfig>().General.MiniMapSettings.QuestMarker;
        public bool Drawline = Config.Get<FKAffixes>().Settings.DrawLine;
        public bool Rifts = Config.Get<FKAffixes>().Settings.DrawLine;
        public System.Windows.Media.Imaging.BitmapSource Door = Extensions.RotateImage("doors");
        public Dictionary<int, System.Drawing.Bitmap> CacheScenes = new Dictionary<int, System.Drawing.Bitmap>();
        //public bool Drawline = Config.Get<FKAffixes>().Settings.DrawLine;

        // protected readonly Brush Pens = Extensions.HexToBrush("#311c13");

        public struct PensHelper
        {
            public bool Enabled;
            public float Opacity;
            public Brush Pen;
        }

        public struct DrawHelper
        {
            public Size Size;
            public Point Point;
            public Brush Brush;
            public Pen Pen;

            public DrawHelper(Size s, Point p, Brush b, Pen pen)
            {
                Size = s;
                Point = p;
                Brush = b;
                Pen = pen;
            }

        }

        public MinimapContainer()
        {
            SnapsToDevicePixels = true;
            visuals = new VisualCollection(this);
            
            visuals.Add(_visual);

            RevealMinimap = new PensHelper
            {
                Enabled = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Reveal,
                Opacity = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Opacity,
                Pen = Extensions.HexToBrush("#" + Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Background)
            };

            RevealLargemap = new PensHelper
            {
                Enabled = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Reveal,
                Opacity = Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Opacity,
                Pen = Extensions.HexToBrush("#" + Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Background)
            };
        }

        public void DrawEmpty()
        {
            using (DrawingContext drawingContext = _visual.RenderOpen())
            {
                drawingContext.Close();
            }
        }

        public Point RotatePoint(float angle, Point pt)
        {
            var a = angle * System.Math.PI / 180.0;
            float cosa = (float)Math.Cos(a), sina = (float)Math.Sin(a);
            return new Point(pt.X * cosa - pt.Y * sina, pt.X * sina + pt.Y * cosa);
        }

        public double UIScale(double value)
        {
            return (double)(value * Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale);
        }

        public void DrawRectangle(DrawHelper Data, DrawingContext drawingContext)
        {
            DrawHelper Rect = GetRect(Data);

            drawingContext.DrawRectangle(Rect.Brush, Rect.Pen, new System.Windows.Rect
            {
                Width = Rect.Size.Width,
                Height = Rect.Size.Height,
                X = Rect.Point.X,
                Y = Rect.Point.Y
            });
        }

        public void DrawEllipse(DrawHelper Data, DrawingContext drawingContext)
        {
            DrawHelper Rect = GetRect(Data);

            drawingContext.DrawEllipse(Rect.Brush, Rect.Pen,
                Rect.Point, Rect.Size.Width, Rect.Size.Height);
        }

        public DrawHelper GetRect(DrawHelper Helper)
        {
            Helper.Size = new Size(UIScale(Helper.Size.Width), UIScale(Helper.Size.Height));
            Point Position = new Point(Helper.Point.X - (Helper.Size.Width / 2),
                        Helper.Point.Y - (Helper.Size.Height / 2));

            Helper.Point = NewPosition(Position);

            return Helper;
        }

        public void DrawScene(IEnumerable<SceneHelper.NavHelper> IEnumerable, DrawingContext drawingContext)
        {
            PensHelper Pen = (LargeMinimap) ? RevealLargemap : RevealMinimap;

            drawingContext.PushOpacity(Pen.Opacity);

            foreach (SceneHelper.NavHelper Scene in IEnumerable)
            {
                
                    Point Position = NewPosition(Scene.NavContainer.Position);
                   drawingContext.PushTransform(new TranslateTransform(Position.X, Position.Y));

                   foreach (var SceneData in Scene.NavContainer.NavigationCells)
                       drawingContext.DrawRectangle(Pen.Pen, null, new Rect(
                           new Point(SceneData.Min.X, SceneData.Min.Y),
                           new Point(SceneData.Max.X, SceneData.Max.Y)));
                   drawingContext.Pop(); 

                /* Point Position = NewPosition(Scene.NavContainer.Position);
                 drawingContext.PushTransform(new TranslateTransform(Position.X, Position.Y));

                 foreach (var SceneData in Scene.NavContainer.NavigationCells)
                     drawingContext.DrawRectangle(Pen.Pen, null, new Rect(
                         new Point(SceneData.Min.X, SceneData.Min.Y),
                         new Point(SceneData.Max.X, SceneData.Max.Y)));
                 drawingContext.Pop(); */
            }

            drawingContext.Pop();


            /*  // if (CacheScenes.ContainsKey(Scene.NavContainer.InternalID))
                //   continue;

               using (System.Drawing.Bitmap Bit = new System.Drawing.Bitmap(240, 240))
               {
                   using (System.Drawing.Graphics e = System.Drawing.Graphics.FromImage(Bit))
                   {
                       foreach (var SceneData in Scene.NavContainer.NavigationCells)

                           e.DrawRectangle(System.Drawing.Pens.White,
                               (int)SceneData.Min.X,
                               (int)SceneData.Min.Y,
                               (int)SceneData.Max.X,
                               (int)SceneData.Max.Y
                               );


                       var f = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                           Bit.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,

                          System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                       drawingContext.DrawImage(f, new Rect(0, 0, Bit.Width, Bit.Height));

                   }
               }

               break;*/


        }

        public bool OutofBounds(Point f, int Max)
        {
            if (f.X >= Max || f.X <= -Max || f.Y >= Max || f.Y <= -Max)
                return true;
       
            return false;
        }

        public System.Windows.Point NewPosition(System.Windows.Point f)
        {
            if (!LargeMinimap)
                return f;

            return new System.Windows.Point(
                (f.X < 0) ? f.X + -LargeMapOffset.X : f.X - LargeMapOffset.X,
                (f.Y < 0) ? f.Y + -LargeMapOffset.Y : f.Y - LargeMapOffset.Y);
        }

        public void Draw(bool LargeMap = false)
        {
            this.LargeMinimap = LargeMap;

            try
            {
                if (LargeMinimap)
                    LargeMapOffset = UIObjects.LargeMap.TryGetPoint<UXMinimap>(Enigma.D3.OffsetConversion.UXControlMinimapRect);

                using (DrawingContext drawingContext = _visual.RenderOpen())
                {
                    if ((LargeMinimap && RevealLargemap.Enabled) || (!LargeMinimap && RevealMinimap.Enabled))
                    {
                        if (!TownHelper.InTown)
                        {
                            IEnumerable<SceneHelper.NavHelper> Scenes = SceneHelper.NavHelpers.Values
                                    .Where(x => !x.NavContainer.Skip || (!GameManager.Instance.GManager.GList.MainAccount.RiftHelper.InRiftNow && x.LevelAreaSnoID == GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea.x044_SnoId));

                            DrawScene(LargeMinimap ? Scenes : Scenes.Where(x => !this.OutofBounds(x.NavContainer.Position, 450)), drawingContext);
                        }
                    }


                    MapItemElement Current = MapItemElement.AT_Unsigned;
                    MapItemShape Shape = null;
                    drawingContext.PushOpacity(1);

                    /*foreach (IMap.IMapActor Actor in Controller.GameManager.Instance.GManager.GList.MainAccount.LevelAreaContainer.Monsters.OrderBy(x => x.Type))
                    {
                        if (Actor.Type == MapItemElement.AT_Portal)
                        {
                            drawingContext.DrawImage(Door, new Rect(Actor.Point.X - UIScale(25 / 2), Actor.Point.Y - UIScale(26 / 2), UIScale(25), UIScale(26)));
                        }
                        else
                        {
                            if (Actor.Type != Current)
                            {
                                Shape = Config.MiniMapItems(Actor.Type, Actor.Identifier).Shape;
                                Current = Actor.Type;
                                drawingContext.Pop();
                                drawingContext.PushOpacity(Shape.Opacity);
                            }

                            if (Shape.Shape == ItemShape.Ellipse)
                                DrawEllipse(new DrawHelper(new Size(Shape.Width, Shape.Height), new Point(Actor.Point.X, Actor.Point.Y), Shape.FillBrush, Shape.StrokeBrush), drawingContext);

                            else if (Shape.Shape == ItemShape.Rectangle)
                                DrawRectangle(new DrawHelper(new Size(Shape.Width, Shape.Height), new Point(Actor.Point.X, Actor.Point.Y), Shape.FillBrush, Shape.StrokeBrush), drawingContext);

                            if (Actor.EliteOffset != -1 && Drawline)
                                drawingContext.DrawLine(Shape.StrokeBrush, new Point(Actor.Point.X - (Shape.Width / 2), Actor.Point.Y - (Shape.Height) / 2), RotatePoint(-45, new Point(175, -105 + (40 * Actor.EliteOffset))));
                            // drawingContext.Pop();
                        }
                    }*

                    drawingContext.Pop();*/


                   /* foreach (var x in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.TrickleManager.x04_Ptr_Items.Dereference())
                    {
                
                        double UI = Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale;
                        MapItemShape Shapes = Config.MiniMapItems(MapItemElement.AT_Objective, -1).Shape;
                        Size Controll = new Size(Shapes.Width * UI, Shapes.Height * UI);

                        Point Point = new System.Windows.Point(
                             x.x08_WorldPosX - Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0D0_WorldPosX,
                               x.x0C_WorldPosY - Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0D4_WorldPosY);

                        drawingContext.DrawEllipse(Shapes.FillBrush, Shapes.StrokeBrush,
                              Point, Controll.Width, Controll.Height);

                    }*/

                    if (Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea == null)
                        return;
                    
                    foreach (var marker in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary().Where(x => x.Value.x00_WorldPosition.x0C_WorldId == Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.x790.Dereference().x038_WorldId_))
                    {
                        double UI = Controller.GameManager.Instance.GManager.GRef.Attacher.UIScale;
                        MapItemShape Shapes = Config.MiniMapItems(MapItemElement.AT_Objective, -1).Shape;
                        Size Controll = new Size(Shapes.Width * UI, Shapes.Height * UI);

                        Point Point = new System.Windows.Point(
                                marker.Value.x00_WorldPosition.x00_X - Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0D0_WorldPosX,
                                marker.Value.x00_WorldPosition.x04_Y - Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0D4_WorldPosY);

                        int Scale = (int)((LargeMap) ? (1200 * UI) / 2 : (350 * UI) / 2);
                        Point offset = new Point((this.VisualOffset.X) * UI, (this.VisualOffset.Y) * UI);

                        if (LargeMap)
                            Point = new Point(
                                (LargeMapOffset.X < 0) ? Point.X + -LargeMapOffset.X : Point.X - LargeMapOffset.X,
                                    (LargeMapOffset.Y < 0) ? Point.Y + -LargeMapOffset.Y : Point.Y - LargeMapOffset.Y
                            );

                        if (Point.X > offset.X)
                            Point.X = offset.X;
                        if (Point.X < -offset.X)
                            Point.X = -offset.X;
                        if (Point.Y > offset.Y)
                            Point.Y = offset.Y;
                        if (Point.Y < -offset.Y)
                            Point.Y = -offset.Y;

                        Point.X = Point.X - (Controll.Width / 2) + 5;
                        Point.Y = Point.Y - (Controll.Height / 2) + 5;

                      //  drawingContext.DrawEllipse(Shapes.FillBrush, Shapes.StrokeBrush,
                            //    Point, Controll.Width, Controll.Height);
                    }
                }
            }

            catch(Exception e) {
                DebugWriter.Write(e);
            }
        }

        /* public void Draw (bool LargeMap = false)
         {
             try
             {
                 using (DrawingContext drawingContext = _visual.RenderOpen())
                 {
                     if (LargeMap)
                         LargeMapOffset = UIObjects.LargeMap.TryGetPoint<UXMinimap>(0x0C50);

                     if (GameManager.MapReveal)
                     {
                         if ((LargeMap && RevealLargemap.Enabled) || (!LargeMap && RevealMinimap.Enabled))
                         {
                             try
                             {
                                 PensHelper Pen = (LargeMap) ? RevealLargemap : RevealMinimap;

                                 drawingContext.PushOpacity(Pen.Opacity);

                                 foreach (var Scene in SceneHelper.NavHelpers.Where(x => !x.Value.NavContainer.Skip))
                                 {
                                     if (Controller.Enums.Rift.InRiftNow && Scene.Value.LevelAreaSnoID != GameManager.MainAccount.DiabloIII.LevelArea.x044_SnoId)
                                         continue;

                                     Point Position = Scene.Value.NavContainer.Position;

                                     if (!LargeMap)
                                     {
                                         if (Position.X > 0) // Positive
                                         {
                                             if (Position.X >= 450)
                                                 continue;
                                         }

                                         else
                                         {
                                             if (Position.X <= -450)
                                                 continue;
                                         }

                                         if (Position.Y > 0) // Positive
                                         {
                                             if (Position.Y >= 450)
                                                 continue;
                                         }

                                         else
                                         {
                                             if (Position.Y <= -450)
                                                 continue;
                                         }
                                     }

                                     if (LargeMap)
                                     {
                                         Position.X = (LargeMapOffset.X < 0) ? Position.X + -LargeMapOffset.X : Position.X - LargeMapOffset.X;
                                         Position.Y = (LargeMapOffset.Y < 0) ? Position.Y + -LargeMapOffset.Y : Position.Y - LargeMapOffset.Y;
                                     }

                                     drawingContext.PushTransform(new TranslateTransform(Position.X, Position.Y));

                                     foreach (var SceneData in Scene.Value.NavContainer.NavigationCells)
                                         drawingContext.DrawRectangle(Pen.Pen, null, new Rect(
                                             new Point(SceneData.Min.X, SceneData.Min.Y),
                                             new Point(SceneData.Max.X, SceneData.Max.Y)));

                                     drawingContext.Pop();
                                 }

                                 drawingContext.Pop();
                             }

                             catch (Exception e)
                             {
                                 if (GameManager.Debug)
                                     Extensions.Execute.UIThread(() => { MessageBox.Show(e.ToString()); });
                             }
                         }
                     }

                      foreach (IMap.IMapActor Actor in Controller.GameManager.MainAccount.ActorList.Monsters)
                     {
                         MapItemShape Shape = (Actor.Type != MapItemElement.AT_Custom) ?
                             Config.MiniMapItems(Actor.Type).Shape :
                            Config._.FKMinimap.DefaultMapItem.CustomActors[Actor.Identifier].Shape;

                         drawingContext.PushOpacity(Shape.Opacity);

                         Size Size = new Size(UIScale(Shape.Width), UIScale(Shape.Height));
                         Point Position = new Point(Actor.Point.X - (Size.Width / 2), Actor.Point.Y - (Size.Height / 2));

                         if (LargeMap)
                             Position = new Point(
                                 (LargeMapOffset.X < 0) ? Position.X + -LargeMapOffset.X : Position.X - LargeMapOffset.X,
                                  (LargeMapOffset.Y < 0) ? Position.Y + -LargeMapOffset.Y : Position.Y - LargeMapOffset.Y
                             );


                         if (Shape.Shape == ItemShape.Ellipse)
                         {
                             drawingContext.DrawEllipse(Shape.FillBrush, Shape.StrokeBrush,
                                 Position, Size.Width, Size.Height);
                         }
                         else if (Shape.Shape == ItemShape.Rectangle)
                         {
                             drawingContext.DrawRectangle(Shape.FillBrush, Shape.StrokeBrush,
                                 new Rect
                                 {
                                     Width = Size.Width,
                                     Height = Size.Height,
                                     X = Position.X,
                                     Y = Position.Y
                                 });
                         }

                         if (Actor.EliteOffset != -1)
                         {
                             Point EliteOffset = RotatePoint(-45, new Point(175, -105 + (40 * Actor.EliteOffset)));
                             drawingContext.DrawLine(Shape.StrokeBrush, new Point(Actor.Point.X - (Size.Width / 2), Actor.Point.Y - (Size.Height) / 2), EliteOffset);
                         }


                         drawingContext.Pop();
                     }

                     try
                     {
                         if (GameManager.MainAccount.DiabloIII.LevelArea == null)
                             return;

                         foreach (var marker in GameManager.MainAccount.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary().Where(x => x.Value.x00_WorldPosition.x0C_WorldId == GameManager.MainAccount.DiabloIII.ObjectManager.x790.x038_WorldId_))
                         {
                             double UI = Controller.GameManager.Attacher.UIScale;
                             MapItemShape Shape = Config.MiniMapItems(MapItemElement.AT_Objective).Shape;
                             Size Controll = new Size(Shape.Width * UI, Shape.Height * UI);
                             Point Point = new System.Windows.Point(
                                     marker.Value.x00_WorldPosition.x00_X - Controller.GameManager.MainAccount.Player.x0D0_WorldPosX,
                                     marker.Value.x00_WorldPosition.x04_Y - Controller.GameManager.MainAccount.Player.x0D4_WorldPosY);

                             int Scale = (int)((LargeMap) ? (1200 * UI) / 2 : (350 * UI) / 2);
                             Point offset = new Point((this.VisualOffset.X) * UI, (this.VisualOffset.Y) * UI);

                             if(LargeMap)
                                 Point = new Point(
                                     (LargeMapOffset.X < 0) ? Point.X + -LargeMapOffset.X : Point.X - LargeMapOffset.X,
                                         (LargeMapOffset.Y < 0) ? Point.Y + -LargeMapOffset.Y : Point.Y - LargeMapOffset.Y
                                 );

                             if (Point.X > offset.X)
                                 Point.X = offset.X;
                             if (Point.X < -offset.X)
                                 Point.X = -offset.X;
                             if (Point.Y > offset.Y)
                                 Point.Y = offset.Y;
                             if (Point.Y < -offset.Y)
                                 Point.Y = -offset.Y;

                             Point.X = Point.X - (Controll.Width / 2) + 5;
                             Point.Y = Point.Y - (Controll.Height / 2) + 5;

                             drawingContext.DrawEllipse(Shape.FillBrush, Shape.StrokeBrush,
                                    Point, Controll.Width, Controll.Height);
                         }
                     }

                     catch (Exception e)
                     {
                         if (GameManager.Debug)
                             Extensions.Execute.UIThread(() =>
                             {
                                 MessageBox.Show(e.ToString());
                             });
                     }
                     drawingContext.Close();
                 }
             }

             catch (Exception e)
             {
                 if (GameManager.Debug)
                     Extensions.Execute.UIThread(() =>
                     {
                         MessageBox.Show(e.ToString());
                     });
             }
         }*/

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



/*  Brush xa = Brushes.LightGray;
  if (xaf.Where(x => x.x00_SceneSnoId == Scene.Value.NavContainer.SNOID) != null)
  {
      Enigma.D3.SceneRevealInfo Info = xaf.Where(x => x.x00_SceneSnoId == Scene.Value.NavContainer.SNOID).FirstOrDefault();

      if(Info != null)
          if(Info.x24_OpacityMask.Count() < 1)
              xa = Brushes.Orange;


      //if(xaf.Where(f => f.x00_SceneSnoId == Scene.Value.NavContainer.SceneID).First().x34_IsFullyVisible == 1)
        //  xa = Brushes.Orange;
  }
*/

/* Brush xa;
 if (Scene.Value.NavContainer.SNOID == 18842)
     xa = Brushes.Brown;
 else
     xa = (SceneHelper.NewScenes.Contains(Scene.Value.NavContainer.InternalID)) ? Brushes.LightGray : Brushes.Orange;
 */


/*  drawingContext.DrawRectangle(xa, null, new Rect(new Point((Scene.Value.NavContainer.Min.X - Controller.GameManager.MainAccount.Player.x0D0_WorldPosX),
        (Scene.Value.NavContainer.Min.Y - Controller.GameManager.MainAccount.Player.x0D4_WorldPosY)),
        new Point((Scene.Value.NavContainer.Max.X - Controller.GameManager.MainAccount.Player.x0D0_WorldPosX),
            (Scene.Value.NavContainer.Max.Y - Controller.GameManager.MainAccount.Player.x0D4_WorldPosY))));*/
/* drawingContext.DrawText(new FormattedText("X:" + Scene.Value.NavContainer.NavigationCells.Count.ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-us"),
System.Windows.FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.White, new NumberSubstitution()), new Point(0, 0));

*/