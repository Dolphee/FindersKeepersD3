using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using FindersKeepers.Helpers.Overlay.ActorTypes;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Templates.ActorHelper
{
    public class ActorHelperObserver : NotifyingObject, IPerform, IWPF
    {
        public UserControl Control { get; set; }

        private ObservableCollection<IMap> Elements;// _imapItems
        public ObservableCollection<IMap> MinimapMarkers { get { return Elements; } }

        public HashSet<int> Added = new HashSet<int>();

        public ActorHelperObserver()
        {
            Elements = new ObservableCollection<IMap>();
            Control = new ActorHelper { DataContext = this };
        }

        public void Set() {

            foreach (var Actor in Enigma.D3.Actor.Container)
            {
                if (Actor.x08C_ActorSnoId != 74706)// != 433966) // Ring Oculus
                    continue;

                Enigma.D3.ActorCommonData e = Actor.GetACDData();

                if (Added.Contains(Actor.Address))
                {
                    foreach (var x in MinimapMarkers)
                    {
                        if (!x.Update(new System.Windows.Point(0, 0)))
                        {
                            Extensions.Execute.UIThread(() => MinimapMarkers.Remove(x));
                            Added.Remove(Actor.Address);
                       }
                    }
                }

                else
                {
                    Extensions.Execute.UIThread(() =>
                    {
                        MinimapMarkers.Add(new IMapShape(Actor));
                    });

                    Added.Add(Actor.Address);
                }
            }
        }
        public void Empty() { }
    }
}
