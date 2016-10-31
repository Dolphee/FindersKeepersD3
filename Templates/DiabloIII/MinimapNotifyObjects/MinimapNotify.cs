using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using System.Collections.ObjectModel;
using FindersKeepers.Helpers;
using FindersKeepers.Helpers.Overlay.ActorTypes;
using System.Collections.Specialized;
using System.Windows.Controls;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3;
using Enigma.D3.UI.Controls;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.MinimapNotifyObjects
{
    public class MinimapNotify : IFKControl, IFKWPF
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public IFKControl Control { get; set;}
        public bool IsVisible = true;
        private ObservableCollection<IMap> Elements;// _imapItems
        public ObservableCollection<IMap> MinimapMarkers { get { return Elements; } }

        public MinimapNotify(){
            Elements = new ObservableCollection<IMap>();
            Control = (IFKControl)new CMinimap { DataContext = this };

        }

        public void IUpdate()
        {
            try
            {
                if (UIObjects.LargeMap.TryGetValue<Enigma.D3.UI.Controls.UXMinimap>() || UIObjects.Inventory.TryGetValue<Enigma.D3.UI.Controls.UXLabel>()) // Largemap is Visible
                {
                    if (IsVisible)
                    {
                        Extensions.Execute.UIThread(() => Control.CastHelper<UserControl>().Visibility = System.Windows.Visibility.Hidden);
                        IsVisible = false;

                        var itemsToRemove = new List<IMap>();

                        foreach (var mapItem in Elements)
                        {
                            if (!(mapItem is IMapScenes))
                                itemsToRemove.Add(mapItem);
                        }

                        Extensions.Execute.UIThread(() =>
                        {
                            itemsToRemove.ForEach(a => Elements.Remove(a));
                        });
                    }

                    IsVisible = false;
                    return;
                }

                else
                {
                    if (!IsVisible)
                    {
                        Extensions.Execute.UIThread(() => Control.CastHelper<UserControl>().Visibility = System.Windows.Visibility.Visible);
                        IsVisible = true;
                    }
                }

                try
                {
                    var itemsToRemove = new List<IMap>();
                    System.Windows.Point Player = new System.Windows.Point(GameManagerAccountHelper.Current.Player.x0D0_WorldPosX,
                    GameManagerAccountHelper.Current.Player.x0D4_WorldPosY);

                    foreach (var mapItem in Elements.Concat(MapItems.ScenesAdd.Concat(MapItems.MonstersAdd)))
                    {
                        if (!mapItem.Update(Player))
                        {
                            itemsToRemove.Add(mapItem);

                            if (mapItem is IMapScenes)
                                MapItems.Scenes.Remove(mapItem.Id);
                            else
                                MapItems.Monsters.Remove(mapItem.Id);
                        }
                    }

                    IEnumerable<IMap> Add = MapItems.MonstersAdd.Concat(MapItems.ScenesAdd);

                    Extensions.Execute.UIThread(() =>
                    {

                        if (itemsToRemove.Count > 0 || Add.Count() > 0)
                        {
                                try
                                {
                                    //Elements.AddRange(Add);
                                   // Elements.RemoveRange(itemsToRemove);

                                    foreach (var Item in Add)
                                        Elements.Add(Item);

                                    itemsToRemove.ForEach(a => Elements.Remove(a));
                                }

                                catch (Exception ef)
                                {
                                DebugWriter.Write(ef);
                            }
                        }
                    }, true);

                    MapItems.MonstersAdd = new List<IMap>();
                    MapItems.ScenesAdd = new List<IMap>();
                }

                catch (Exception f)
                {
                    DebugWriter.Write(f);
                }
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }
        
        public void Empty()
        {
            MapItems.Scenes = new Dictionary<int, IMap>();
            MapItems.ScenesAdd = new List<IMap>();
            MapItems.Monsters = new Dictionary<int, IMap>();
            MapItems.MonstersAdd = new List<IMap>();

            if (Elements != null)
            {
                Extensions.Execute.UIThread(() =>
                {
                    Elements.Clear();
                }, true);
            }
        }
    }

    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification = false;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
                base.OnCollectionChanged(e);
        }

        public void AddRange(IEnumerable<T> list, IEnumerable<T> Removeable)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            _suppressNotification = true;

            foreach (T item in list)
            {
                Add(item);
            }

            foreach (T remove in Removeable)
            {
                Remove(remove);
            }

            _suppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }
    }
}
