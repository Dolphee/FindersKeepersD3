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
using System.Threading;
using Enigma.D3.UI;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Collections;


namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for ExperienceItems.xaml
    /// </summary>
    public partial class BuffsControl: UserControl, IUpdate
    {
    
       // public static byte[] Buffer = new byte[0];

        public double Value = 5;

        public BuffsControl()
        {
            InitializeComponent();

           // Templates.Buffs.Area = Buffs;
           // Templates.Buffs.DebuffArea = DeBuffs;
        }

        public void Update()
        {
            //if(Buffs.Count == 0)
                GetBuffs();

           // Buffer.Render(Buffs);

            try
            {
                var Container = GameManagerAccountHelper.Current.DiabloIII.BuffManager.x1C_Buffs;
                Container.TakeSnapshot();

                /*foreach(Buff Buff in Container)
                {
                    if (!Buffs.ContainsKey(Buff.Address))// Not added
                    {
                        Buff.TakeSnapshot();
                        Buffs.Add(Buff.Address, Buff);
                        BuffsAdd.Add(Buff.Address);
                    }
                }
                */
                Container.FreeSnapshot();



               // Templates.Buffs.Set();
            }

            catch (Exception e)
            {
              //  MessageBox.Show(e.ToString());
            }

            //Templates.Buffs.SetDebuff();
        }

        public void GetBuffs()
        {
            /*if (Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.BuffManager == null)
                return;

            Buffs = new Dictionary<int, string>();
            int i = 0;

            try
            {
                foreach (Enigma.D3.UI.Buff BuffManager in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.BuffManager.x1C_Buffs)
                {
                    var Diff = (Controller.GameManager.Instance.GameTicks - (Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.x038_Counter_CurrentFrame));

                    var C = GetCooldown(BuffManager) / 60d;
                    var Tic = ((Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.x038_Counter_CurrentFrame + Diff) / 60d) ;

                    double GValue =  C - Tic;

                    if (GValue < 1)
                        GValue = Math.Round(GValue / 0.10) * 0.10;

                    else
                        GValue = Math.Round(GValue, 0);

                        Buffs.Add(i, Format(GValue));
                      /*
                        if (!Buffs.ContainsKey(i))
                        {

                            C = GetCooldown(BuffManager) / 60d;
                            Tic = Controller.GameManager.Instance.GameTicks /60d;

                            GValue = C - Tic;

                           // using (System.IO.StreamWriter fs = new System.IO.StreamWriter("./aaCot.txt", true))
                              //  fs.WriteLine(GetCooldown(BuffManager) + " - " + Controller.GameManager.Instance.GameTicks);


                            Buffs.Add(i, Math.Round(GValue, 2, MidpointRounding.AwayFromZero));

                            try
                            {
                                new BuffEvent().Attach(i);
                            }

                            catch (Exception e)
                            {
                                Extensions.Execute.UIThread(() => MessageBox.Show(e.ToString()));
                                XML.CreateLogFile(e);
                            }
                        }



                        i++;
                    //double CoolDown = GetCooldown(BuffManager);

                    /* if (CoolDown != -1)
                     {
                         if (CoolDown > -1)
                             CoolDown = Math.Round(CoolDown - Controller.GameManager.Instance.GameTicks) / 60d; // 60 "Ticks" per second (60hz)

                        // CoolDown = Math.Round(CoolDown, 0);

                         Buffs.Add(i, CoolDown);
                         i++;
                     }
                }
            }

            catch (Exception ef)
            {
                XML.CreateLogFile(ef);
            }*/
        }

        public static string Format(double Value)
        {
            if (Value == -1 || Value < -1)
                return " ";

            if (Value < 1)
                return (Math.Round(Value / 0.10) * 0.10).ToString();

            TimeSpan Time = TimeSpan.FromSeconds(Value);
            string Minute = "";
            string Seconds = Time.Seconds.ToString();

            if (Time.Minutes > 0)
                Minute = Time.Minutes + ":";
            if (Time.Seconds < 10 && Time.Minutes > 0)
                Seconds = "0" + Seconds;

            return Minute + Seconds;
        }

        public double GetCooldown(Enigma.D3.UI.Buff buff)
        {
            // if (Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0B0_GameBalanceType != Enigma.D3.Enums.GameBalanceType.Heroes)
              //  return -1;

            try
            {
                // for (int i = BuffHelper.BuffIconStart; i <= BuffHelper.BuffIconEnd; i++)
                // {
                // double values = AttributeHelper.GetAttributeValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player,
                //    (Enigma.D3.Enums.AttributeId)i, buff.x000_PowerSnoId);

                double values = Enigma.D3.Helpers.AttributeHelper.GetAttributeValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player,
                (Enigma.D3.Enums.AttributeId)(Enigma.D3.Enums.AttributeId.BuffIconEndTick0 + (buff.x004_Neg1)), buff.x000_PowerSnoId);

                if (values > 0)
                    return values;
                // }

                return -1;
            }

            catch { }

            return -1;
        }
    }

    public class BuffEvent : IDisposable
    {
        public static BuffEvent Instance;
        [ThreadStatic]
        public int Ref = 0;
        [ThreadStatic]
        public AutoResetEvent Reset = new AutoResetEvent(false);

        public void Attach(int Refs)
        {
           Thread Thread = new Thread(() =>
           {
               Ref = Refs;

               Timer f = new Timer(((s) =>
               {
                  /* var val = BuffsControl.Buffs[Ref] - 0.1;
                   if (val < 0)
                       val = 0;

                   BuffsControl.Buffs[Ref] = val;*/

               }), null, Timeout.Infinite, 10);
               f.Change(0, 10);

               Reset.WaitOne();
           });

            Thread.Start();
        }

        public void Fire(object Sender)
        {
           
        }

        public void Dispose() { }
    }

}
