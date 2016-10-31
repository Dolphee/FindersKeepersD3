using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FK.UI;
using System.Windows.Controls;

namespace FindersKeepers.Templates.Overlay
{
    public class ItemOverlayNotifyer : IFKControl, IFKWPF
    {
        public ObservableCollection<FindersKeepers.OverlayItems> _Items {get;set; }
        public ObservableCollection<OverlayItems> Items { get { return _Items; } }
        public List<OverlayItems> itemsToAdd = new List<OverlayItems>();
        public List<OverlayItems> itemsToRemove = new List<OverlayItems>();
        public HashSet<int> Position = new HashSet<int>();

        public void IUpdate()
        {
 
            foreach(OverlayItems item in Items)
            {
                if (item.Close)
                {
                    item.Dispose();
                    itemsToRemove.Add(item);
                }
            }

            if(itemsToAdd.Count > 0 && Items.Count != 4)
            {
                //OverlayItems Item = itemsToAdd.First();
                Extensions.Execute.UIThread(() => Items.Add(itemsToAdd.FirstOrDefault()));
                itemsToAdd.Remove(itemsToAdd.FirstOrDefault());
            }

            if (itemsToRemove.Count < 1)
                return;

            Extensions.Execute.UIThread(() =>
            {
                itemsToRemove.ForEach(x => {
                    GetObject(x).CastHelper<StackPanel>().AnimateFade<StackPanel>(1, 0, TimeSpan.FromMilliseconds(1000), default(TimeSpan),
                        (() =>
                        {
                            Items.Remove(x);
                            Position.Remove(x.Position);
                        }));
                  });
            });

            itemsToRemove = new List<OverlayItems>();

        }

        public IFKControl Control
        {
            get; set;
        }

        public ItemOverlayNotifyer()
        {
            _Items = new ObservableCollection<OverlayItems>();
            Control = (IFKControl)new ItemOverlay { DataContext = this };
        }

        public System.Windows.DependencyObject GetObject(OverlayItems item)
        {
            var i = ((ItemOverlay)Control).FControls.ItemContainerGenerator.ContainerFromItem(item);
            return Extensions.FindVisualChildren<StackPanel>(i).FirstOrDefault();
        }

        public int GetId()
        {
            for (int i = 4; i >= 1; i--)
            {
                if (!Position.Contains(i))
                {
                    Position.Add(i);
                    return i;
                }
            } 

            return 4;
        }

        public void Add(OverlayItems item)
        {
            string PathValue = "";
            item.Position = GetId();

            OverlayItems.SimpleItem Item = item.Item;
            item._Legendary = new UniqueItem
            {
                ItemName = "Unknown",
                LegendaryItemType = (UniqueItemType)Item.SNOItem.ItemType
            };

            PathValue = item.Legendary.LegendaryItemType.ToString();

            if (Item.ItemQuality == Controller.Enums.ItemQuality.Legendary)
            {
                if (UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower()) != null)
                {
                    item._Legendary = UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower());
                    PathValue = item.Legendary.LegendaryItemType.ToString();

                    if (item.Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith)
                        PathValue = "PlanLegendary_Smith";
                }
            }

            else // Magic, Rare, White
            {
                item._Legendary.ItemName = ItemsTypesConversion.TryGet(((LegendaryItemsTypes)Item.SNOItem.ItemType).ToString());

                if (item._Legendary.LegendaryItemType.ToString().Contains("Generic"))
                    PathValue = item.Legendary.ItemName;
            }

            string Path = UniqueItemsController.GetBaseItemName(Item.SNOItem.ActorName.ToLower());
            string ItemNamePath = string.IsNullOrWhiteSpace(item._Legendary.Override) ? Path : Path + "_" + item._Legendary.Override;
            string ItemNamePather = (item._Legendary.LegendaryItemType == UniqueItemType.PlanLegendary_Smith) ? "SmithPlan" : ItemNamePath;

            item.PathType = Extensions.ItemImage(PathValue + "/" + ItemNamePather);

            Extensions.Execute.UIThread(() => itemsToAdd.Add(item));
        }

    }
}
