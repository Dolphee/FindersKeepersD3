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
using Enigma.D3.UI.Controls;
using Enigma.D3.UI;
using FindersKeepers.Controller;
using Enigma.D3.Helpers;
using Enigma.D3;
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates.Rifts
{
    /// <summary>
    /// Interaction logic for GreaterRift.xaml
    /// </summary>
    public partial class GemUpgrade : UserControl, IPerform
    {
        public List<GemUpgradeHelper> _Controls = new List<GemUpgradeHelper>();
        public UIObjects.UIElement<UXItemsControl> Scrollbar = new UIObjects.UIElement<UXItemsControl> { Name = "Root.NormalLayer.vendor_dialog_mainPage.riftReward_dialog.LayoutRoot.gemUpgradePane.items_list._content._stackpanel" };
        public int TopRect = 702;
        public float LastOffset = 0;
        public Dictionary<int, ActorCommonData> Data;

        public class GemUpgradeHelper
        {
            public ActorCommonData Actor;
            public UIObjects.UIElement<UXItemButton> UIHandler;
            public Canvas Control;
            public Point Position;
            public int JewelerLevel;
            public OutlinedTextBlock Text;
        }

        public GemUpgrade()
        {
            InitializeComponent();
            Data = GetItems();
            Scrollbar.Reload();

            int Row = 0;
            for (int i = 0; i <= 58; i++) // 58 is max UI Control count
            {
                if (i % 6 == 5)
                {
                    Row++;
                    continue;
                }

                try
                {
                    var UIHandler = new UIObjects.UIElement<UXItemButton>
                    {
                        Name = "Root.NormalLayer.vendor_dialog_mainPage.riftReward_dialog.LayoutRoot.gemUpgradePane.items_list._content._stackpanel._tilerow" + Row + "._item" + i + ".Item",
                        Offset = 0x0E50
                    };

                    UIHandler.Reload();

                    _Controls.Add(new GemUpgradeHelper
                    {
                        UIHandler = UIHandler,
                        Position = new Point(i % 6, Row)
                    });

                    CreateControl(_Controls.Last());                 
                }

                catch (Exception e)
                {

                }
            }
        }

        public void CreateControl(GemUpgradeHelper Helper)
        {

            int ActorSnoId = Helper.UIHandler.ReadValue<UXItemButton>();
            int Value = Data.ContainsKey(ActorSnoId) ? Attributes.JewelRank.GetValue(Data[ActorSnoId]) : 0;

            Canvas Control = new Canvas();
            Grid c = new Grid { Tag = "Container", Width = 63, Height = 63, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };

            FindersKeepers.Helpers.OutlinedTextBlock Text = new Helpers.OutlinedTextBlock
            {
                FontFamily = FKMethods.HelveticaFont,
                Fill = Extensions.HexToBrush("#fff"),
                Stroke = Extensions.HexToBrush("#000"),
                StrokeThickness = 1,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 0, 3, 3),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Text = Value.ToString(),
                Tag = "TextContainer",
            };

            c.Children.Add(Text);
            Control.Children.Add(c);

            Canvas.SetLeft(Control, 10+ (Helper.Position.X * 64) + (14.5 * Helper.Position.X));
            Canvas.SetTop(Control, 10 + (Helper.Position.Y * 64) + (14.5 * Helper.Position.Y));

            Items.Children.Add(Control);
            Helper.Control = Control;
            Helper.Actor = Data[ActorSnoId];
            Helper.JewelerLevel = Value;
            Helper.Text = Text;
        }

        public Dictionary<int, ActorCommonData> GetItems()
        {
            Dictionary<int, ActorCommonData> Data = new Dictionary<int, ActorCommonData>();

            foreach (var x in GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData)
                if (!Data.ContainsKey(x.x000_Id))
                    Data.Add(x.x000_Id, x);

            return Data;
        }


        public void Set()
        {
            if (!UIObjects.GemUpgrade.TryGetValue<Enigma.D3.UI.Controls.UXItemsControl>())
            {
                GameManager.Instance.GManager.GRef.Attacher.Remove("GemUpgrade");
                return;
            }

            float Rec = Scrollbar.ReadValue<UXItemsControl, float>(0x468 + 0x04);

            if(LastOffset != Scrollbar.ReadValue<UXItemsControl, float>(0x468 + 0x04))
            {
                Extensions.Execute.UIThread(() => Canvas.SetTop(Items, Rec - 670));
                LastOffset = Rec;
            }

            foreach(GemUpgradeHelper Item in _Controls)
            {
                int value = Attributes.JewelRank.GetValue(Item.Actor);

                if(value != Item.JewelerLevel)
                {
                    Extensions.Execute.UIThread(() =>
                    {
                        Item.Text.Text = value.ToString();
                    });

                    Item.JewelerLevel = value;
                }
            }
       }

    }

}
