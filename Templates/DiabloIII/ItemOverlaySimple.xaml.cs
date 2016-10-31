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
using FindersKeepers.Controller;
using System.Threading;
using FK.UI;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for ItemOverlaySimple.xaml
    /// </summary>
    public partial class ItemOverlaySimple : UserControl, IPerform
    {
        protected HashSet<Canvas> Active = new HashSet<Canvas>();
        protected HashSet<ushort> Position = new HashSet<ushort>();
        protected int NumberofItems = 4;
        public static LinearGradientBrush BackgroundGradient;

        public static class Transition
        {
            public static Brush BorderBrush = Extensions.HexToBrush("#141414"); // 
            public static Brush ItemName = Extensions.HexToBrush("#ff9b25");
            public static FontFamily FontFamily = Extensions.GetFont("DINPro Regular");
        }

        public ItemOverlaySimple()
        {
            InitializeComponent();

            BackgroundGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5),
                GradientStops = new GradientStopCollection()
                    {
                        new GradientStop(Colors.Black, 0.0),
                        new GradientStop(Colors.Black, 1.0)
                    }
            };

            BackgroundGradient.Freeze();

        }

        public void Reset()
        {
            Active = new HashSet<Canvas>();
            Position = new HashSet<ushort>();
        }
        
        public void Set()
        {
            if(GameManager.Instance.GManager.GRef.ItemOverlay.Count == 0)
                return;

            if (Active.Count >= NumberofItems)
                return;

            _Paint(GameManager.Instance.GManager.GRef.ItemOverlay.First());
        }
        
        public ushort GetID()
        {
            for(ushort i = 4; i >= 1; i--)
                if (!Position.Contains(i))
                    return i;

            // No overlay present...

            return 4;
        }

        public void DeleteTimer(Canvas p, ushort shutdown, ushort ID)
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            shutdown = (shutdown != 0) ? shutdown : (ushort)10000;

            dispatcherTimer.Tick += new EventHandler((s, e) =>
            {
                Extensions.Execute.UIThread(() =>
                {
                    p.AnimateFade<Canvas>(1, 0, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0), () => { Elements.Children.Remove(p); Active.Remove(p); Position.Remove(ID); });
                });
                dispatcherTimer.Stop();
            });

            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(shutdown); //  new TimeSpan(0,0,10)
            dispatcherTimer.Start();
        }

        public void _Paint(OverlayItems Item)
        {
            ushort ID = GetID();
            Position.Add(ID);

            GameManager.Instance.GManager.GRef.ItemOverlay.Remove((OverlayItems)Item);

            if (Item.ItemType == OverlayItems.Type.Item)
                PaintItem(Item, ID);
            // else
            // Dunno yet
        }

        public void PaintItem(OverlayItems Items, ushort ID)
        {
            OverlayItems.SimpleItem Item = Items.Item;

            Brush Color = TextColors.TryGet(Item.ItemQuality.ToString());

            UniqueItem Legendary = new UniqueItem {
                ItemName = "Unknown",
                LegendaryItemType = (UniqueItemType)Item.SNOItem.ItemType
            };

            string PathType = Legendary.LegendaryItemType.ToString();

            if (Item.ItemQuality == Controller.Enums.ItemQuality.Legendary)
            {
                if (Item.SNOItem.SetItem)
                    Color = TextColors.TryGet("Set");

                if (UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower()) != null)
                {
                    Legendary = UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower());
                    PathType = Legendary.LegendaryItemType.ToString();

                    if (Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith)
                        PathType = "PlanLegendary_Smith";
                } 
            }

            else // Magic, Rare, White
            {
                Legendary.ItemName = ItemsTypesConversion.TryGet(((LegendaryItemsTypes)Item.SNOItem.ItemType).ToString());

                if (Legendary.LegendaryItemType.ToString().Contains("Generic"))
                    PathType = Legendary.ItemName;
            }

            Extensions.Execute.UIThread(() =>
            {
                Canvas xContainer = new Canvas { Width = 370 };
                Grid Container = new Grid { Width = 370 };

                Border Border = new Border { Height = 64, BorderBrush = Transition.BorderBrush, BorderThickness = new Thickness(2), Opacity = 0.7 };

                Grid NameContainerGrid = new Grid { Height = 90, VerticalAlignment = VerticalAlignment.Center };

                StackPanel NameContainer = new StackPanel { VerticalAlignment = VerticalAlignment.Center};
                StackPanel ItemContainer = new StackPanel { Height = 90, Orientation = Orientation.Horizontal };

                Canvas Background = new Canvas { Background = BackgroundGradient, Opacity = 1, Height = 59, Margin = new Thickness(1,0,0,0) };

                StackPanel Image = new StackPanel { Width = 85 };
                StackPanel Name = new StackPanel();
                TextBlock ItemName = new TextBlock { FontFamily = Transition.FontFamily,  VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Normal,
                    Text = Legendary.ItemName, Height = 25, FontSize = 18, Foreground = Color };

                StackPanel Ancient = new StackPanel { Width = 4, Height = 60, Background = Brushes.Orange, Opacity = 0.5};
                string Path = UniqueItemsController.GetBaseItemName(Item.SNOItem.ActorName.ToLower());
                string ItemNamePath = string.IsNullOrWhiteSpace(Legendary.Override) ? Path : Path+ "_"+Legendary.Override;
                string ItemNamePather = (Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith) ? "SmithPlan" : ItemNamePath;
                
                Image.Children.Add(new Image { Source = Extensions.ItemImage(PathType + "/" + ItemNamePather), Width = 75, Height = 90 });

                NameContainer.Children.Add(ItemName);

                if (Items.Account !=  null)
                {
                    TextBlock ItemNames = new TextBlock
                    {
                        FontFamily = Transition.FontFamily,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontWeight = FontWeights.Normal,
                        Text = Items.Account.Nickname,
                        Height = 20,
                        FontSize = 12.1,
                        Foreground = Items.Account.Foreground
                    };
                    NameContainer.Children.Add(ItemNames);
                }

                NameContainerGrid.Children.Add(NameContainer);
                Name.Children.Add(NameContainerGrid);

                if (Item.ShowAncient && Item.AncientItem)
                {
                    Border.BorderBrush = Color;
                    Ancient.Height = 64;
                }
             
                ItemContainer.Children.Add(Image);
                ItemContainer.Children.Add(Name);

                Border.Child = Background;

                Container.Children.Add(Border);
                Container.Children.Add(ItemContainer);

                xContainer.Children.Add(Container);

                Elements.Children.Add(xContainer);

                xContainer.AnimateFade<Canvas>(0, 1, TimeSpan.FromSeconds(1));
                Active.Add(xContainer);
                Canvas.SetTop(xContainer, ID * 90);

                DeleteTimer(xContainer, Items.Transition , ID);
            });

        }

    }
}
