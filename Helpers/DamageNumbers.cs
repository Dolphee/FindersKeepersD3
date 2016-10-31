using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using FindersKeepers.DebugWorker;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers
{
    public class DamageNumbers
    {
        private int _tickPrevious;
        private int _listTickPrevious;
        private Enigma.D3.Player Player { get { return GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.Player.Dereference(); } }
        private HashSet<float> Numbers = new HashSet<float>();
        private HashSet<int> Allowed = new HashSet<int>(Config.Get<FKConfig>().General.DamageNumber.Types.Cast<int>());
        private ushort Max = Config.Get<FKConfig>().General.DamageNumber.UpdateRate;
        private bool Reset = Config.Get<FKConfig>().General.DamageNumber.ResetLimit;
        public float AverageDamage = 0;
        public bool Enabled = Config.Get<FKConfig>().General.DamageNumber.Enabled;

        public void Update()
        {
            int tickCurrent = GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.x038_Counter_CurrentFrame;

            if (tickCurrent > _tickPrevious)
            {
                OnTick(_tickPrevious, tickCurrent);
            }
            _tickPrevious = tickCurrent;
        }

        public void OnTick(int from, int to)
        {
            try
            {
                var list = Player.xA018_FloatingNumbers;
                list.TakeSnapshot();

                var node = list.x00_First;
                var listTick = _listTickPrevious;
                while (node != null)
                {
                    node.TakeSnapshot();

                    if (node.x00_Value.x30_StartTick < to && node.x00_Value.x30_StartTick > _listTickPrevious)
                    {
                        if (node.x00_Value.x2C_Tick == 0)
                            break;

                        if (Allowed.Contains(node.x00_Value.x48_Type))
                        {
                            if (Numbers.Count >= Max)
                                CalculateDamage();

                            Numbers.Add((float)Math.Round(node.x00_Value.x4C_Value));
                        }

                        listTick = Math.Max(listTick, node.x00_Value.x30_StartTick);
                    }
                    var next = node.x08_Next;
                    node.FreeSnapshot();
                    node = next;
                }
                _listTickPrevious = listTick;
                list.FreeSnapshot();
            }

            catch(Exception e)
            {
                DebugWriter.Write(e);
            }
        }

        public void CalculateDamage()
        {
            float Damage = (Numbers.Sum()) / Max;
            Numbers = new HashSet<float>();

            if (Reset)
            {

            }

            else
            {
                if (AverageDamage == 0)
                    AverageDamage = Damage;
                else
                    AverageDamage = (AverageDamage + Damage) / 2;
            }
        }

        public enum DamageTypes
        {
            Low = 0,
            Normal = 1,
            CriticalHits = 2,
            DamageTaken = 3,
            Unknown5 = 5,
            Healing = 10,
            CriticalDamageTaken = 28
        }
    }
}
