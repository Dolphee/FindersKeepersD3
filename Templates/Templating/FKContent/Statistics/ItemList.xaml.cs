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
using System.Windows.Media.Animation;
using FK.UI;

namespace FindersKeepers.Templates.Statistics
{
    /// <summary>
    /// Interaction logic for ItemList.xaml
    /// </summary>
    public partial class ItemList : UserControl
    {
        public List<FKTracker.LegendaryItems> ConfigList = Config.Get<FKTracker>().Item.OrderByDescending(x => x.ID).ToList();
        public List<FKTracker.LegendaryItems> List = Config.Get<FKTracker>().Item.OrderByDescending(x => x.ID).ToList();
        public List<FKTracker.LegendaryItems> Listed = new List<FKTracker.LegendaryItems>();
        public DateTime LastEntered;
        public bool SearchEnabled;
        public bool ResetSearch;

        public ItemList()
        {
            InitializeComponent();
            Entries.Children.Clear();
            //UpdateList("Inna");

            var x = Enum.GetValues(typeof(LegendaryItemsTypes)).Cast<LegendaryItemsTypes>().Select(e => e.ToString()).ToList();
            x.Sort();
            
          //  foreach (var xe in x)
               // ItemsTypes.Items.Add(xe.ToString());

            Scroller.ScrollChanged += (s, e) =>
            {
                if (Scroller.VerticalOffset == Scroller.ScrollableHeight && !SearchEnabled)
                    UpdateList();
            };
        }

        public void UpdateList(string Search = "")
        {
            try
            {
                SearchEnabled = true;

                if (this.ResetSearch)
                {
                    ResetSearch = false;
                    List = ConfigList.OrderByDescending(x => x.ID).ToList();
                    Entries.Children.Clear();
                    SearchHint.AnimateFade<StackPanel>(1, 0, new Duration(TimeSpan.FromMilliseconds(1000)));
                }

                else if (Search != "")
                {
                    ResetSearch = false;
                    Entries.Children.Clear();
                    List = ConfigList.Where(x => x.LegendaryItemName.ToLower().Contains(Search.ToLower())).OrderByDescending(x => x.ID).ToList();
                    SearchHint.AnimateFade<StackPanel>(0, 1, new Duration(TimeSpan.FromMilliseconds(1000)));
                    SearchCount.Text = List.Count.ToString();
                }

                IEnumerable<FKTracker.LegendaryItems> Temp = List.Take(10).ToList();
                Listed.AddRange(Temp);

                foreach (var x in Temp.ToList())
                    List.Remove(x);

                foreach (FKTracker.LegendaryItems Item in Temp)
                    Paint(Item);

                SearchEnabled = false;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        public string TranslateGameType(FKTracker.GameType Type)
        {
            switch (Type)
            {
                case FKTracker.GameType.GreaterRift:
                    return "Greater Rift";
                case FKTracker.GameType.Rift:
                    return "Nephalem Rift";
                case FKTracker.GameType.KadalaGamble:
                    return "Kadala";
                case FKTracker.GameType.KanaiCube:
                    return "Kanai Cube";
                default:
                    return "Open World";
           }
        }

        public string GetTime(DateTime d)
        {
            TimeSpan ts = DateTime.Now.Subtract(d);

            if (ts.Days != 0)
                return ts.Days + " day" + (ts.Days == 1 ? "" : "s") + " ago";

            else if (ts.Hours != 0)
                return ts.Hours + " hour" + (ts.Hours == 1 ? "" : "s") + " ago";

            else if (ts.Minutes != 0)
                return ts.Minutes + " minute" + (ts.Minutes == 1 ? "" : "s") + " ago";

            else if (ts.Seconds != 0)
                return ts.Seconds + " second" + (ts.Seconds == 1 ? "" : "s") + " ago";

            return "";
        }

        public string TranslateDifficulty(int Diff)
        {
            Controller.Enums.Difficulty Difficulty = (Controller.Enums.Difficulty)Diff;
            return Difficulty.ToString().Replace("_", " ");
        }

        public void Paint(FKTracker.LegendaryItems Item)
        {
            //  StackPanel Container = new StackPanel { Orientation = Orientation.Horizontal };
            //   StackPanel MainContent = new StackPanel { Width = 634, Height = 300, Background = Extensions.HexToBrush("#ffffff") };

            UniqueItem Legendary = new UniqueItem
            {
                ItemName = "Unknown"
            };

            string ImagePath = "";
            Brush Color = TextColors.TryGet(Item.SetItem ? "Set2" : "Legendary");
            string Diff = Item.GameType == FKTracker.GameType.GreaterRift ? Item.Difficulty.ToString() : TranslateDifficulty(Item.Difficulty);

            if (UniqueItemsController.TryGet(Item.Name.ToLower()) != null)
            {
                Legendary = UniqueItemsController.TryGet(Item.Name.ToLower());

                string PathType = Legendary.LegendaryItemType.ToString();

                if (Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith)
                    PathType = "PlanLegendary_Smith";

                string Path = UniqueItemsController.GetBaseItemName(Item.Name.ToLower());
                string ItemNamePath = string.IsNullOrWhiteSpace(Legendary.Override) ? Path : Path + "_" + Legendary.Override;
                string ItemNamePather = (Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith) ? "SmithPlan" : ItemNamePath;

                ImagePath = PathType + "/" + ItemNamePather;
            }

            StackPanel Container = new StackPanel { Cursor = Cursors.Hand, Height = 60, HorizontalAlignment = HorizontalAlignment.Left, Orientation = Orientation.Horizontal, Width = 634, Background = Extensions.HexToBrush("#ffffff") };

                StackPanel ImageHolder = new StackPanel { Width = 60, Height = 60, Margin = new Thickness(1, 0, 0, 0), Background = Extensions.HexToBrush("#fbfbfb") };
                    Grid Alignment = new Grid { VerticalAlignment = VerticalAlignment.Center, Height = 60 };
                        Image Image = new Image { Source = Extensions.ItemImage(ImagePath), Height = 40, VerticalAlignment = VerticalAlignment.Center};

            StackPanel TextContainer = new StackPanel { Margin = new Thickness(15, 0, 0, 0), Width = 200, VerticalAlignment = VerticalAlignment.Center, Orientation = Orientation.Horizontal };
                TextBlock Text = new TextBlock { Text = Legendary.ItemName, FontFamily = Extensions.GetFont("DINPro Regular"), FontSize = 15, Foreground = Color };

            StackPanel CContainer = new StackPanel { Orientation = Orientation.Horizontal};
                 StackPanel AccountContainer = new StackPanel { Margin = new Thickness(0, 16, 0, 0),  Width = 45};
                    TextBlock ATextHeader = new TextBlock { Text = "Account", FontFamily = new FontFamily("Segoe UI"), Height = 15, FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#bfbfbf") };
                    //TextBlock AText = new TextBlock { Text = Config.Get<FKAccounts>().GetAccount((int)Item.AccountID).Nickname, FontFamily = new FontFamily("Segoe UI"), FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#2a2627") };
            TextBlock AText = new TextBlock { Text = Item.ID.ToString(), FontFamily = new FontFamily("Segoe UI"), FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#2a2627") };

            StackPanel LevelContainer = new StackPanel { Margin = new Thickness(25, 16, 0, 0), Width = 70, };
                    TextBlock LTextHeader = new TextBlock { Text = TranslateGameType(Item.GameType), FontFamily = new FontFamily("Segoe UI"), Height = 15, FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#bfbfbf") };
                    TextBlock LText = new TextBlock { Text = Diff, FontFamily = new FontFamily("Segoe UI"), FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#2a2627") };

                StackPanel AncientContainer = new StackPanel { Margin = new Thickness(25, 16, 0, 0),Width = 45, };
                   TextBlock AnTextHeader = new TextBlock { Text = "Ancient", FontFamily = new FontFamily("Segoe UI"), Height = 15, FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#bfbfbf") };
                   TextBlock AnText = new TextBlock { Text = Item.AncientItem ? "Yes" : "No", FontFamily = new FontFamily("Segoe UI"), FontSize = 10, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#2a2627") };

                StackPanel TimeContainer = new StackPanel { Margin = new Thickness(25, 4, 0, 0), Orientation = Orientation.Horizontal, Width = 100};
                        Image TImage = new Image { Source = Extensions.FKImage("clock2"), Width = 15, Height = 15, Margin = new Thickness(0,0,10,0) };
                        TextBlock TText = new TextBlock { Text = GetTime(Item.Found), FontFamily = new FontFamily("Segoe UI"), VerticalAlignment = VerticalAlignment.Center, FontSize = 11, FontWeight = FontWeights.Regular, TextAlignment = TextAlignment.Center, Foreground = Extensions.HexToBrush("#ff6686") };

            Alignment.Children.Add(Image);
            ImageHolder.Children.Add(Alignment);
            Container.Children.Add(ImageHolder);
                
            TextContainer.Children.Add(Text);

            AccountContainer.Children.Add(ATextHeader);
            AccountContainer.Children.Add(AText);
            CContainer.Children.Add(AccountContainer);

            LevelContainer.Children.Add(LTextHeader);
            LevelContainer.Children.Add(LText);
            CContainer.Children.Add(LevelContainer);

            AncientContainer.Children.Add(AnTextHeader);
            AncientContainer.Children.Add(AnText);
            CContainer.Children.Add(AncientContainer);

            TimeContainer.Children.Add(TImage);
            TimeContainer.Children.Add(TText);
            CContainer.Children.Add(TimeContainer);

            Container.Children.Add(TextContainer);
            Container.Children.Add(CContainer);

            Container.ToolTip = new TextBlock { Text = "Double click to show more information" };

            Container.MouseEnter += (s, e) =>
            {
                Container.Cursor = Cursors.Hand;
                Container.Background = Extensions.HexToBrush("#efefef");
                ImageHolder.Background = Extensions.HexToBrush("#e2e2e2");

            };
            Container.MouseLeave += (s, e) =>
            {
                Container.Background = Brushes.Transparent;
                ImageHolder.Background = Extensions.HexToBrush("#fbfbfb");
            };

            Entries.Children.Add(Container);

            /* < StackPanel Margin = "25,4,0,0" Orientation = "Horizontal" >
                           < Image Source = "./../../Images/FK/clock2.png" Width = "15" Height = "15" Margin = "0,0,10,0" />
                              < TextBlock Foreground = "#ff6686" FontFamily = "Segoe UI" FontSize = "11" VerticalAlignment = "Center" FontWeight = "Regular" TextAlignment = "Center" > 5 minutes ago</ TextBlock >
                               </ StackPanel >

                               < StackPanel Margin = "20,4,0,0" Orientation = "Horizontal" >
                                     < Border Width = "20" Height = "20" BorderBrush = "#dedede" BorderThickness = "1" CornerRadius = "3" >
                                             < TextBlock Foreground = "#2a2627" FontWeight = "Bold" FontFamily = "DinPro" FontSize = "10" VerticalAlignment = "Center" TextAlignment = "Center" > X </ TextBlock >
                                              </ Border >*/


        }

        private void Search(object sender, KeyEventArgs e)
        {
            if (sender.CastVisual<TextBox>().Text.Length > 2)
            {
                UpdateList(sender.CastVisual<TextBox>().Text);
            }

            else if (sender.CastVisual<TextBox>().Text.Length == 0)
            {
                ResetSearch = true;
                UpdateList("");
            }
        }

        private void ToggleMenu(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Menu.Visibility = Visibility.Visible;
                Menu.Animate<Canvas>(new List<DependencyProperty>() { FrameworkElement.WidthProperty}, 0, 187, (() =>
                {
                    BMenu.Visibility = Visibility.Visible;

                }));
            }

            catch (Exception ef)
            {
                MessageBox.Show(ef.ToString());
            }
        }


        /* 
        <StackPanel Orientation = "Horizontal" >
            < StackPanel Width="634" Height="300" Background="#fff">
                <StackPanel Height = "60" HorizontalAlignment="Left" Orientation="Horizontal" Background="White" Width="634">
                    <StackPanel Width = "60" Height="60" Margin="1,0,0,0" Background="#fbfbfb">
                        <Grid VerticalAlignment = "Center" Height="60">
                            <Image Source = "/Config/unique_gloves_002.png" Height="40" VerticalAlignment="Center"  />
                        </Grid>
                    </StackPanel>

                    <StackPanel Margin = "15,0,0,0" Width="210" VerticalAlignment="Center" Orientation="Horizontal">
                        <TextBlock FontFamily = "DinPro" FontSize="16" Foreground="#ff9b25">Elements of elements</TextBlock>
                    </StackPanel>

                    <StackPanel Orientation = "Horizontal" >
                        < StackPanel Margin="0,16,0,0">
                            <TextBlock Foreground = "#bfbfbf" FontFamily="Segoe UI" Height="15"  FontSize="10" FontWeight="Regular" TextAlignment="Center">Account</TextBlock>
                            <TextBlock Foreground = "#2a2627" FontFamily="Segoe UI" FontSize="10" FontWeight="Regular" TextAlignment="Center">Dolphelius</TextBlock>
                        </StackPanel>

                        <StackPanel Margin = "25,16,0,0" >
                            < TextBlock Foreground="#bfbfbf" FontFamily="Segoe UI" Height="15" FontSize="10" FontWeight="Regular" TextAlignment="Center">Greater Rift</TextBlock>
                            <TextBlock Foreground = "#2a2627" FontFamily= "Segoe UI" FontSize= "10" FontWeight= "Regular" TextAlignment= "Center" > 45 </ TextBlock >
                         </ StackPanel >
 
                         < StackPanel Margin= "25,16,0,0" >
                             < TextBlock Foreground= "#bfbfbf" FontFamily= "Segoe UI" Height= "15" FontSize= "10" FontWeight= "Regular" TextAlignment= "Center" > Ancient </ TextBlock >
                             < TextBlock Foreground= "#2a2627" FontFamily= "Segoe UI" FontSize= "10" FontWeight= "Regular" TextAlignment= "Center" > Yes </ TextBlock >
                         </ StackPanel >
 
                         < StackPanel Margin= "25,4,0,0" Orientation= "Horizontal" >
                             < Image Source= "./../../Images/FK/clock2.png" Width= "15" Height= "15" Margin= "0,0,10,0" />
                             < TextBlock Foreground= "#ff6686" FontFamily= "Segoe UI" FontSize= "11" VerticalAlignment= "Center" FontWeight= "Regular" TextAlignment= "Center" > 5 minutes ago</TextBlock>
                        </StackPanel>

                        <StackPanel Margin = "20,4,0,0" Orientation= "Horizontal" >
                             < Border Width= "20" Height= "20" BorderBrush= "#dedede" BorderThickness= "1" CornerRadius= "3" >
                                 < TextBlock Foreground= "#2a2627" FontWeight= "Bold" FontFamily= "DinPro" FontSize= "10" VerticalAlignment= "Center" TextAlignment= "Center" > X </ TextBlock >
                             </ Border >
                         </ StackPanel >
                     </ StackPanel >
                 </ StackPanel >*/

    }
}
