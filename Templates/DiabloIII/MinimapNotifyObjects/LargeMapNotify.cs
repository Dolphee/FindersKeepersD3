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
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.MinimapNotifyObjects
{
    public class LargeMapNotify : NotifyingObject, IFKControl , IFKWPF
    {
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public IFKControl Control { get; set;}
        public bool IsVisible = true;
        private ObservableCollection<IMap> Elements;// _imapItems
        public ObservableCollection<IMap> MinimapMarkers { get { return Elements; } }
        public List<IMap> Temp = new List<IMap>();

        public LargeMapNotify(){
            Elements = new ObservableCollection<IMap>();
            Control = (IFKControl)new LargeMap { DataContext = this };
        }

        public void IUpdate()
        {
            try
            {
                if (!UIObjects.LargeMap.IsVisible) // Largemap not open
                {
                    if (IsVisible)
                    {
                        Extensions.Execute.UIThread(() => Control.CastHelper<UserControl>().Visibility = System.Windows.Visibility.Hidden);
                        IsVisible = false;

                        MapItems.Monsters = new Dictionary<int, IMap>();
                        MapItems.MonstersAdd = new List<IMap>();
                        var itemsToRemove = new List<IMap>();

                        foreach (var mapItem in Elements)
                        {
                            if (!(mapItem is IMapScenesLargeMap))
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
                    if (!IsVisible) // Just opened Largemap
                    {
                        Extensions.Execute.UIThread(() => Control.CastHelper<UserControl>().Visibility = System.Windows.Visibility.Visible);
                        IsVisible = true;

                        MapItems.Monsters = new Dictionary<int, IMap>();
                        MapItems.MonstersAdd = new List<IMap>();
                    }
                }

                try
                {
                    var itemsToRemove = new List<IMap>();

                    System.Windows.Point Player = new System.Windows.Point(Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D0_WorldPosX,
                        Controller.GameManagerData.GameManagerAccountHelper.Current.Player.x0D4_WorldPosY);
                    System.Windows.Point Offset = UIObjects.LargeMap.TryGetPoint<Enigma.D3.UI.Controls.UXMinimap>(Enigma.D3.OffsetConversion.UXControlMinimapRect);

                    foreach (var mapItem in Elements.Concat(MapItems.ScenesLargeAdd.Concat(MapItems.MonstersAdd)))
                    {
                        if (!mapItem.Update(Player, Offset))
                        {
                            itemsToRemove.Add(mapItem);

                            // if(mapItem is IMapActor)
                            //MapItems.Monsters.Remove(mapItem.Id);
                        }
                    }

                    Extensions.Execute.UIThread(() =>
                    {
                        MapItems.MonstersAdd.ForEach(a => Elements.Add(a));
                        MapItems.ScenesLargeAdd.ForEach(a => Elements.Add(a));
                        itemsToRemove.ForEach(a => Elements.Remove(a));
                    });

                    MapItems.MonstersAdd = new List<IMap>();
                    MapItems.ScenesLargeAdd = new List<IMap>();
                    Temp = new List<IMap>();
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
            MapItems.ScenesLarge = new Dictionary<int, IMap>();
            MapItems.ScenesLargeAdd = new List<IMap>();
            //MapItems.MonstersLarge = new Dictionary<int, IMap>();
            // MapItems.MonstersLargeAdd = new List<IMap>();

            if (Elements != null)
            {
                Extensions.Execute.UIThread(() =>
                {
                    Elements.Clear();
                });
            }
        }
    }
}
