using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Enigma.D3.Helpers;
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates
{
    public partial class MainBar : UserControl, IPerform
    {
        public Dictionary<FKConfig.GeneralSettings.ExperienceBar.Items.TargetType, TextBlock> ExperienceItems;
        //public bool Skillbar = Config.Get<FKConfig>().General.Skills.Skill;
        public Dictionary<string, Tuple<UIElement, object>> Controls = new Dictionary<string, Tuple<UIElement, object>>();
        public List<IGameStatus> Notify = new List<IGameStatus>();
        public List<object> UpdateControls = new List<object>();
        public HitTesting Tooltip = new HitTesting { Y = 915, X = 0, H = 250, W = 1600 };
       
        public MainBar()
        {
            InitializeComponent();

            if (Config.Get<FKConfig>().General.Experience.ShowNumbers)
                Add<Templates.ExperienceItems>("ShowNumbers");
            
             //if (Config.Get<FKConfig>().General.Experience.Enabled)
                // ExperiencePaint(Config.Get<FKConfig>().General.Experience.ShowNumbers);
            
            if (Config.Get<FKConfig>().General.Skills.Skill)
                Add<Templates.Skillbar>("Skill");

            if (Config.Get<FKConfig>().General.Skills.Buffs)
                AddWPF<Templates.Mainbar.BuffController>("Buffs");

            if(Config.Get<FKConfig>().General.Skills.Life)
                Add<Templates.Mainbar.Health>("Health");

            if (Config.Get<FKConfig>().General.Skills.Resource)
                Add<Templates.Mainbar.Resource>("Resource");

            //if (Config.Get<FKConfig>().General.Experience.Enabled)
                //AddWPF<Templates.Mainbar.ExperienceHelperObserver>("ExperienceHelper");

            //MessageBox.Show(ToolTip.HitTest().ToString());

            //824

            // Slots.Add(Potion);
        }

        public void Add<T>(string Name, bool OverlayCaller = false) where T : UserControl
        {
            T Control = (T)Extensions.CreatePage(typeof(T));

            if (!Controls.ContainsKey(Name))
            {
                Controls.Add(Name, new Tuple<UIElement, object>(Control, Control));
                MainBarControls.Children.Add(Control);

                if (OverlayCaller)
                {
                    if (Control is IGameStatus)
                        ((IGameStatus)Control).OnCreation();
                }
            }

            UpdateList();
        }

        public T AddWPF<T>(string Name) where T : IWPF
        {
            //T Control = (T)Extensions.CreatePage(typeof(T));
            IWPF ClassControl = default(T);

            if (!Controls.ContainsKey(Name))
            {
                ClassControl = (IWPF)Extensions.CreatePage(typeof(T));
                Controls.Add(Name, new Tuple<UIElement, object>(ClassControl.Control, ClassControl));
                MainBarControls.Children.Add(ClassControl.Control);
            }

            UpdateList();
            return (T)ClassControl;
        }


        public T GetControl<T>(string Name) where T : UserControl
        {
            if (!Controls.ContainsKey(Name))
                return null;

            return (T)Controls[Name].Item1;
        }

        public T GetWPFControl<T>(string Name) where T : IWPF
        {
            if (!Controls.ContainsKey(Name))
                return default(T);

            return (T)Controls[Name].Item2;
        }


        public void Remove(string Name)
        {
            if (!Controls.ContainsKey(Name))
                return;

            MainBarControls.Children.Remove(Controls[Name].Item1);
            Controls.Remove(Name);

            UpdateList();
        }

        public void UpdateList()
        {
            Notify = Controls.Select(x => x.Value.Item2).Where(x => x is IGameStatus).Cast<IGameStatus>().ToList();
            UpdateControls = Controls.Select(x => x.Value.Item2).Where(x => x is IUpdate).ToList();
        }

        public void ExperienceTick(bool Numbers = true)
        {
            if (ExperienceItems == null)
                return; // ExperiencePaint();

            foreach(KeyValuePair<FKConfig.GeneralSettings.ExperienceBar.Items.TargetType, TextBlock> Item in ExperienceItems)
            {
               /* if (Helpers.ExperienceHelper.HeroLevel == 70 && Item.Key == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LevelTarget)
                    continue;*/

                Extensions.Execute.UIThread(() =>
                {
                     //Item.Value.Text = Helpers.ExperienceHelper.Call(Item.Key, false);
                });
            }
        }

        public void ExperienceDelete()
        {
           // ExperienceAttribs.Children.Clear();
            ExperienceItems = new Dictionary<FKConfig.GeneralSettings.ExperienceBar.Items.TargetType, TextBlock>();
        }

        public void ExperiencePaint(bool Numbers = false)
        {
            return;
            ExperienceItems = new Dictionary<FKConfig.GeneralSettings.ExperienceBar.Items.TargetType, TextBlock>();
            List<FKStyles.ItemStyle> IStyle = new List<FKStyles.ItemStyle>();
            try
            {
                foreach (FKConfig.GeneralSettings.ExperienceBar.Items Item in Config.Get<FKConfig>().General.Experience.Item)
                {
                    if (Config.Get<FKStyles>().Styles.Style.SingleOrDefault(x => x.Name == Item.StyleName) == null)
                        continue;

                    //FKStyles.ItemStyle Style = Config.GetNested<FKStyles, FKStyles.ItemStyle>(x => x.Styles.Style.Single(a => a.Name == Item.StyleName));
                    FKStyles.ItemStyle Style = null;
                    double Position = Style.Background.Position;
                        
                    int count = IStyle.Count(x => x == Style);
                    IStyle.Add(Style);

                    if (count > 0) // Using more styles
                        Position = Position + (Style.Background.Width * count);
                    
                    List<GradientStop> Stops = new List<GradientStop>();
                    foreach (var x in Style.Background.Background)
                        Stops.Add(new GradientStop { Color = Extensions.HexToColor("#" + x.Background).Color, Offset = x.Angle });

                    //(IEnumerable<GradientStop>)Stops

                    //string PValue = Helpers.ExperienceHelper.Call(Item.Target, true);

                    Canvas Container = new Canvas
                    {
                        Width = Style.Background.Width,
                        Height = 27,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
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

                    TextBlock Value = new TextBlock
                    {
                        TextAlignment = TextAlignment.Center,
                        Text = "0",
                        Opacity = 1,
                        FontFamily = Extensions.GetFont("Gautami"),
                        Foreground = Extensions.HexToBrush("#" + Style.Value.FontColor),
                        FontSize = Style.Value.FontSize,
                        HorizontalAlignment = (HorizontalAlignment)Style.Value.HorisontalPosition,
                        VerticalAlignment = (VerticalAlignment)Style.Value.HorisontalPosition,
                        Margin = Style.Value.Margin,
                    };

                    if ((Item.Text != null) && Item.Text.Length > 0)
                    {
                        StackPanel P = new StackPanel { Width = Style.Background.Width, Orientation = Orientation.Horizontal, Height = 27 };

                        TextBlock Header = new TextBlock
                        {
                            TextAlignment = TextAlignment.Center,
                            Text = Item.Text,
                            FontFamily = Extensions.GetFont("Gautami"),
                            Foreground = Extensions.HexToBrush("#" + Style.Text.FontColor),
                            FontSize = Style.Text.FontSize,
                            HorizontalAlignment = (HorizontalAlignment)Style.Text.HorisontalPosition,
                            VerticalAlignment = (VerticalAlignment)Style.Text.HorisontalPosition,
                            Margin = Style.Text.Margin,
                        };

                        P.Children.Add(Header);
                        P.Children.Add(Value);
                        Container.Children.Add(P);
                    }

                    else
                    {
                        Grid b = new Grid
                        {
                            Width = Style.Background.Width,
                            Height = 27,
                        };

                        b.Children.Add(Value);
                        Container.Children.Add(b);
                    }

                    var xa = new FindersKeepers.Helpers.OutlinedTextBlock
                    {
                        Width = Style.Background.Width,
                        Height = 27,
                        Text = "",
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        StrokeThickness = 0,
                        Fill = Brushes.White,
                        FontFamily = Extensions.GetFont("Helvetica Rounded LT Black"),
                        FontSize = 25,
                        FontWeight = Style.Value.ValueThickness,
                        TextAlignment = TextAlignment.Center
                    };

                    Canvas.SetLeft(Container, Position);
                    Container.Children.Add(Border);
                    ExperienceItems.Add(Item.Target, Value);
                    //ExperienceAttribs.Children.Add(Container);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void IGameStatus()
        {
            Extensions.Execute.UIThread(() =>
            {
              //  ((IGameChanged)GetWPFControl<Templates.Mainbar.ExperienceHelperObserver>("ExperienceHelper")).IGameChanged();
            });
        }

        public int i = 1;
        public void Set()
        {
            Tooltip.HitTest();

            if (Controller.GameManager.Instance.GManager.GList.MainAccount.Gamestate.PlayerLoaded)
            {
                Helpers.ExperienceHelper.Set = Controller.GameManager.Instance.GManager.GList.MainAccountData.GameData;
                Helpers.ExperienceHelper.StartNew();

                ExperienceTick();
            }

            foreach (object Control in UpdateControls)
            {
                try
                {
                    ((IUpdate)Control).Update();
                }

                catch (Exception e)
                {

                }
            }
        }
    }
}
