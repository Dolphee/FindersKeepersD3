using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using FindersKeepers.Helpers.Overlay.ActorTypes;
using System.Windows.Media;
using System.Windows;

namespace FindersKeepers.Templates.Mainbar
{
    public class ExperienceHelperObserver : NotifyingObject, IFKControl , IFKWPF, IGameChanged
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public IFKControl Control {get; set;}
        public ExperienceHelper Owner { get; set; }
        public void Empty() { }
        private ObservableCollection<IMapExperienceHelper> _Elements;// _imapItems
        public ObservableCollection<IMapExperienceHelper> Elements { get { return _Elements; } }
        public bool ins = false;

        public ExperienceHelperObserver()
        {
            _Elements = new ObservableCollection<IMapExperienceHelper>();
            Control = (IFKControl)new ExperienceHelper { DataContext = this };
            Owner = Control as ExperienceHelper;

            var xi = (Control as UserControl);
            var xfi = (GameManager.Instance.GManager.GRef.D3OverlayControl.D3RenderControl as UserControl);

            foreach (var x in Config.Get<FKConfig>().General.Experience.Item)
                _Elements.Add(new IMapExperienceHelper(x));

            ((ExperienceHelper)Control).FControls.Loaded += ((s, e) => 
            {
                foreach (var x in Elements)
                {
                    var i = ((ExperienceHelper)Control).FControls.ItemContainerGenerator.ContainerFromItem(x);
                    var f = Extensions.FindVisualChildren<Border>(i).FirstOrDefault();

                    // f.Loaded += ((ss,ee) => {
                    //x.RegisterTooltip(f.PointToScreen(new Point(0,0)) ,f);
                    //});
                }
            });
        }

        public void IUpdate()
        {
            foreach (IMapExperienceHelper Helper in Elements)
                 Helper.Update();


            //Owner.Controls.ForEach(x => x.Update());
        }

        public void IGameChanged()
        {

        }
    }

    public class IMapExperienceHelper : NotifyingObject
    {
        public FKConfig.GeneralSettings.ExperienceBar.Items Item { get;set; }
        public FKStyles.ItemStyle _Style  { get { return Config.Get<FKStyles>().Styles.Style.Single(a => a.Name == Item.StyleName);} }
        public FKStyles.ItemStyle Style { get { return _Style; } }
        public string TextExtension { get { return Item.Extension == "#" ? "" : Item.Extension;} }
        public double _PreviousValue { get; set; }
        public string _Value = "0";
        public string Value { get
            {
                return _Value;
            }

            set
            {
                if(value != _Value)
                {
                    _Value = value;
                    Refresh();
                }
            }
        }
        public string FName { get { return Item.Target.ToString(); } }

        public void Update()
        {
            double val = Helpers.ExperienceHelper.Call(Item.Target);

            if(val != _PreviousValue)
            {
                Value = val.FormatNumber(Item.Extension);
                _PreviousValue = val;
            }
        }

        public void RegisterTooltip(Point point, FrameworkElement r)
        {
            GameManager.Instance.GManager.GRef.D3OverlayControl.RegisterTooltip(point,r);
        }

        public object CreateControl()
        {
       
            FKStyles.ItemStyle Style = Config.Get<FKStyles>().Styles.Style.Single(a => a.Name == Item.StyleName);

            double Position = Style.Background.Position;

            List<GradientStop> Stops = new List<GradientStop>();
            foreach (var x in Style.Background.Background)
                Stops.Add(new GradientStop { Color = Extensions.HexToColor("#" + x.Background).Color, Offset = x.Angle });

            //string PValue = Helpers.ExperienceHelper.Call(Item.Target, true);

            Canvas Container = new Canvas
            {
                Width = Style.Background.Width,
                Height = 27,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(Style.Background.Position, 0,0,0),
                Background = new LinearGradientBrush()
                {
                    StartPoint = new Point(0.5, 0),
                    EndPoint = new Point(0.5, 1),
                    Opacity = Style.Background.Opacity,
                    GradientStops = new GradientStopCollection((IEnumerable<GradientStop>)Stops)
                }
            };

            Border Border = new Border
            {
                Width = Style.Background.Width,
                Height = 27,
                BorderBrush = Extensions.HexToBrush("#" + Style.Background.BorderColor),
                BorderThickness = new Thickness(Style.Background.BorderSize)
            };

            StackPanel Cont = new StackPanel {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center
            };

            /*Value = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Text = "0",
                Opacity = 1,
                FontFamily = new FontFamily(Style.Value.Font),
                Foreground = Extensions.HexToBrush("#" + Style.Value.FontColor),
                FontSize = Style.Value.FontSize,
                HorizontalAlignment = (HorizontalAlignment)Style.Value.HorisontalPosition,
                VerticalAlignment = (VerticalAlignment)Style.Value.HorisontalPosition,
            };*/

            TextBlock ValueExtension = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Text = Item.Extension == "#" ? "" : Item.Extension,
                Opacity = 1,
                FontFamily = new FontFamily(Style.Value.Font),
                Foreground = Extensions.HexToBrush("#eaeaea"),
                FontSize = Style.Value.FontSize,
                HorizontalAlignment = (HorizontalAlignment)Style.Value.HorisontalPosition,
                VerticalAlignment = (VerticalAlignment)Style.Value.HorisontalPosition,
                Margin = Style.Value.Margin,
            };

            //Cont.Children.Add(Value);
            Cont.Children.Add(ValueExtension);

            Container.Children.Add(Border);
            Border.Child = Cont;

            Container.Loaded += (s,e) =>
            {
                var f = Container.PointFromScreen(new Point(0, 0));
                MessageBox.Show(f.ToString());
                // RegisterToolTip(Container, Style.Background.Width, Item.ToolTip);
            };

            return Container;
        }

        public static Canvas RegisterToolTip(Canvas Container, int Width, string ToolTip)
        {
            var f = Container.PointFromScreen(new Point(0, 0));

            Canvas ContainerH = new Canvas { Opacity = 0.95, Margin = new Thickness(-40, -60, 0, 0), Visibility = Visibility.Hidden };

            Border BorderH = new Border
            {
                Width = 140,
                Height = 60,
                BorderThickness = new Thickness(3),
                CornerRadius = new CornerRadius(3),
                Background = Brushes.Black,
                BorderBrush = Extensions.HexToBrush("#333")
            };


            StackPanel xx = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Width = 100 };
            TextBlock fe = new TextBlock
            {
                FontFamily = new FontFamily("DinPro"),
                TextAlignment = TextAlignment.Center,
                Text = ToolTip,
                FontSize = 15,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            xx.Children.Add(fe);
            BorderH.Child = xx;

            ContainerH.Children.Add(BorderH);

            if (Container is Canvas)
                Container.CastVisual<Canvas>().Children.Add(ContainerH);

            // GameManager.Instance.GManager.GRef.Attacher.Search<MainBar>("MainBar").Tooltip.Add(ContainerH,
             //   new Rect(Math.Abs(f.X), Math.Abs(f.Y), Width, 27));

            return Container;
         }

        public IMapExperienceHelper(FKConfig.GeneralSettings.ExperienceBar.Items Item)
        {
            this.Item = Item;
        }

    }

}

