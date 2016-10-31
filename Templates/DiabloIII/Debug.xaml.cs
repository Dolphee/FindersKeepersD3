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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Enigma.D3.Helpers;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for Debug.xaml
    /// </summary>

    public partial class Debug : UserControl, IFKControl
    {
        public bool DynamicSize { get; set; }
        public bool DynamicSizeChanged { get; set; }

        public struct Dump
        {
            public string Text;
            public Action<object> Value;
        }

        public List<Dump> Items = new List<Dump>();

        public Debug()
        {
            InitializeComponent();
            DynamicSize = false;
            return;
            var I = UXHelper.GetControl<Enigma.D3.UI.Controls.UXItemsControl>("Root.NormalLayer.minimap_dialog_backgroundScreen.minimap_dialog_pve.minimap_pve_main");

            this.Width = I.x468_UIRect.Width;
            this.Height = I.x468_UIRect.Height;
            Canvas.SetLeft(this, I.x468_UIRect.Left);
            Canvas.SetTop(this, I.x468_UIRect.Top);

           // ObjectDumper.Dump(I);

            //T.Rect.Width;

            /*Enigma.D3.UI.UIRect Rect = Control.x4BC_UIRect_1600x1200;

             this.Width = Rect.Width;
             this.Height = Rect.Height;
             Canvas.SetTop(this, Rect.Top);
             ObjectDumper.Dump(Control);
             Canvas.SetLeft(this, (Rect.Width * 0.9) + Rect.Left);*/

        }

        public void IUpdate()
        {
            Extensions.Execute.UIThread(() =>
            {

               // if (!Control.IsVisible())
                 //   return;


                /*
                Container.Children.Clear();

               var ia = GameManager.Instance.GManager.GRef.Attacher.Search<Templates.ActorHelper.ActorHelperObserver>("ActorHelper", true);

                StackPanel fxe = new StackPanel { Orientation = Orientation.Vertical };

                TextBlock a2e = new TextBlock { Foreground = Brushes.White, Text = "Count " + ia.MinimapMarkers.Count, FontSize = 12, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                fxe.Children.Add(a2e);
                // TextBlock f1 = new TextBlock { Foreground = Brushes.White, Text = "Power", FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                // f.Children.Add(f1);
                /*
                List<string> Test = new List<string>();
                Test.Add("Root.NormalLayer.stash_dialog_mainPage.tab_highlight");


                for (int ix = 1; ix <= 5; ix++)
                {
                    var fe = ((UXHelper.GetControl<Enigma.D3.UI.Controls.UXItemButton>("Root.NormalLayer.stash_dialog_mainPage.tab_" + ix).xB84_Neg1) % 3) == 1;
                    TextBlock a2e = new TextBlock { Foreground = Brushes.White, Text = "Stash " + ix + " is " + Helpers.UIObjects.InventoryTabs.ActiveTab().ToString(), FontSize = 12, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                    fxe.Children.Add(a2e);
                }       

                
                Container.Children.Add(fxe);

                return;

                var Reflect = GameManagerAccountHelper.Current.RiftHelper.CurrentRift;

                Container.Children.Clear();

                var i = ((Templates.MainBar)GameManager.Instance.GManager.GRef.Attacher.MainBar).Controls.Where(x => x.Key == "Buffs").FirstOrDefault().Value.Item2.CastVisual<Templates.Mainbar.BuffController>();

                StackPanel f = new StackPanel { Orientation = Orientation.Vertical };
               // TextBlock f1 = new TextBlock { Foreground = Brushes.White, Text = "Power", FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
               // f.Children.Add(f1);

                foreach (var x in i.Powers)
                {
                    break;
                    TextBlock a2 = new TextBlock { Foreground = Brushes.White, Text = ((SnoPower)x).ToString(), FontSize = 12, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                    f.Children.Add(a2);
                }

                Container.Children.Add(f);

                TextBlock f12 = new TextBlock { Foreground = Brushes.White, Text = "Buff Container", FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                f.Children.Add(f12);

                foreach (var x in i.Buffs)
                {
                    TextBlock a2 = new TextBlock { Foreground = Brushes.White,
                        Text = ((SnoPower)x.Value.PowerSNO) +" ( " + x.Value.pos + " )" , 
                        FontSize = 12, Height = 15, Margin = new Thickness(20, 10, 0, 0) };

                    TextBlock a3 = new TextBlock
                    {
                        Foreground = Brushes.Gray,
                        Text =  "" + x.Value.CurrentValue+ " )" + " ( " + (SnoPower)x.Value.Buff.x000_PowerSnoId + " )" + "(" + (SnoPower)x.Value.EndTick + ")",
                        FontSize = 12,
                        Height = 15,
                        Margin = new Thickness(20, 00, 0, 0)
                    };

                    f.Children.Add(a2);
                    f.Children.Add(a3);
                }


                /*
                if (Reflect == null)
                    return;

                StackPanel fu = new StackPanel { Orientation = Orientation.Horizontal };

                TextBlock fx = new TextBlock { Foreground = Brushes.White, Text = "GameTick", FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                TextBlock afx = new TextBlock { Foreground = Brushes.White, Text = GameManager.Instance.GameTicks.ToString(), FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };

                fu.Children.Add(fx);
                fu.Children.Add(afx);
                Container.Children.Add(fu);

                foreach (var i in Reflect.GetType().GetProperties())
                {
                    StackPanel f = new StackPanel { Orientation = Orientation.Horizontal };

                    TextBlock x = new TextBlock { Foreground = Brushes.White, Text =  i.Name , FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                    TextBlock ax = new TextBlock { Foreground = Brushes.White, Text = i.GetValue(Reflect, null).ToString() , FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };

                    f.Children.Add(x);
                    f.Children.Add(ax);

                    Container.Children.Add(f);


                }*/
            });

            /*

            Extensions.Execute.UIThread(() =>
            {
                Container.Children.Clear();

                try
                {

                    TextBlock xa = new TextBlock { Foreground = Brushes.White, Text = "C COUNT : " + Helpers.Affix.AffixList[100].Owners.Count, FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                    Container.Children.Add(xa);
                }

                catch{}

                TextBlock x = new TextBlock { Foreground = Brushes.White, Text = "Accounts ", FontSize = 16, Height = 20, Margin = new Thickness(20, 20, 0, 0) };
                Container.Children.Add(x);

           
                    foreach (var xf in Controller.GameManager.Instance.Accounts)
                    {
                        TextBlock xax = new TextBlock
                        {
                            Foreground = Brushes.Red,
                            Text = xf.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary().Where(xee => xee.Value.x00_WorldPosition.x0C_WorldId == xf.DiabloIII.ObjectManager.x790.x038_WorldId_).Count().ToString(),
                            Height = 25,
                            FontSize = 21,
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        /*TextBlock xa = new TextBlock
                        {
                            Foreground = Brushes.Red,
                            Text = xf.GameData().Multibox.Nickname,
                            Height = 15,
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        TextBlock xfa1 = new TextBlock
                        {
                            Foreground = Brushes.Gray,
                            Text = "Main/UI Account " + xf.GameData().Multibox.MainAccount,
                            Height = 20,
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        TextBlock xfa = new TextBlock
                        {
                            Foreground = Brushes.Gray,
                            Text = "Levelarea " + xf.DiabloIII.LevelArea.x044_SnoId,
                            Height = 20,
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        TextBlock xfa2 = new TextBlock
                        {
                            Foreground = Brushes.Gray,
                            Text = "PlayerID " + Helpers.DiabloIII.PlayerData(xf.DiabloIII).x0004_AcdId,
                            Height = 20,
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        TextBlock xfa21 = new TextBlock
                        {
                            Foreground = Brushes.Gray,
                            Text = "PlayerName " + Helpers.DiabloIII.PlayerData(xf.DiabloIII).x9188_HeroName,
                            Height = 20,
                            Margin = new Thickness(20, 0, 0, 0)
                        };
                        Container.Children.Add(xax);

                        Container.Children.Add(xa);
                        Container.Children.Add(xfa1);
                        Container.Children.Add(xfa2);
                        Container.Children.Add(xfa21);
                        Container.Children.Add(xax);

                        //Container.Children.Add(xfa);
                }

                    TextBlock xfa211 = new TextBlock
                    {
                        Foreground = Brushes.Gray,
                        Text = "",
                        Height = 20,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    Container.Children.Add(xfa211);

            });*/

        }
    }
}
