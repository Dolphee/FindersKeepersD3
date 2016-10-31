using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using Enigma.D3.UI;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using FindersKeepers.Helpers.Overlay.Buffs;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.Mainbar
{
    public class BuffController : IUpdate, IWPF
    {
        private ObservableCollection<IBuff> Elements;// _imapItems
        public ObservableCollection<IBuff> BuffMarkers { get { return Elements; } }
        public UserControl Control { get; set; }
        public Dictionary<int, IBuff> Buffs = new Dictionary<int, IBuff>();

        public List<IBuff> BuffsAdd = new List<IBuff>();
        public List<IBuff> BuffsRemove = new List<IBuff>();
        public HashSet<int> Powers = new HashSet<int>();

        public BuffController()
        {
            Elements = new ObservableCollection<IBuff>();
            Control = new BuffsControl { DataContext = this };
        }

        public void Empty()
        {
        }

        public void Update()
        {
            return;
            try
            {
                var Container = GameManagerAccountHelper.Current.DiabloIII.BuffManager.x1C_Buffs;

                if (Container == null)
                    return;

                Container.TakeSnapshot();

                int Position = -1;
            
                foreach (Buff Buff in Container)
                {
                    Position++;

                    if (!Powers.Add(Buff.x000_PowerSnoId))
                    {
                        if (Buffs.ContainsKey(Buff.x000_PowerSnoId))
                        {
                            Buffs[Buff.x000_PowerSnoId].pos = Position;
                        }
                         
                        continue;
                    }

                    IBuff IBuff = new IBuff(Buff, Position);
                    BuffsAdd.Add(IBuff);
                    Buffs.Add(IBuff.PowerSNO, IBuff);
                }


                foreach (var x in BuffMarkers)
                {
                    if (!x.Update())
                    {
                        Powers.Remove(x.PowerSNO);
                        BuffsRemove.Add(x);
                        Buffs.Remove(x.PowerSNO);
                    }
                }

                Extensions.Execute.UIThread(() =>
                {
                    BuffsAdd.ForEach(x => Elements.Add(x));
                    BuffsRemove.ForEach(x => Elements.Remove(x));

                    BuffsAdd = new List<IBuff>();
                    BuffsRemove = new List<IBuff>();
                });

                Container.FreeSnapshot();
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }
    }

}
