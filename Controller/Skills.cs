 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.UI.Controls;
using Enigma.D3;
using Enigma.D3.Helpers;
using FindersKeepers.Controller;

namespace FindersKeepers
{
    class Skills
    {
        public struct BuffHelper
        {
            public static int Count = 31;
            public static int BuffIconStart = 609;
            public static int BuffIconEnd = 640;
        }

        public static HashSet<int> ActiveAndPassiveSkills = new HashSet<int>(){
            86991,
            375089, // Mantra Conviction
            375083, // Mantra Retrubution
            375050, // Mantra Evasion,
            373154, // Mantra Healing
            2
        };

        public static HashSet<int> Skips = new HashSet<int>()
        {
            258199, // Community buff multiplayer
             // Anniversary buff
        };

        public static Dictionary<int, string> UIElements = new Dictionary<int, string>(){
            { 0, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_1" },
            { 1, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_2" },
            { 2, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_3" },
            { 3, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_4" },
            { 4, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_activeSkillLeft" },
            { 5, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_activeSkillRight" },
        };

        public static Dictionary<int, string> UITimer = new Dictionary<int, string>(){
            { 0, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_1.timer" },
            { 1, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_2.timer" },
            { 2, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_3.timer" },
            { 3, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_skill_hotbar_4.timer" },
            { 4, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_activeSkillLeft.timer" },
            { 5, "Root.NormalLayer.game_dialog_backgroundScreenPC.game_activeSkillRight.timer" },
        };

        public static UXIcon GetPowers(int Pos)
        {
            return UXControl.Get<UXIcon>(UIElements[Pos]);
        }

        public static UXTimer GetTimer(int Pos)
        {
            return UXControl.Get<UXTimer>(UITimer[Pos]);
        }

        public static List<FindersKeepers.Controller.SkillbarHelper> GetSkillSNO()
        {
            List<SkillbarHelper> Skills = new List<SkillbarHelper>();

            int i = 0;
            foreach (var x in Helpers.DiabloIII.PlayerData().x00A0_ServerData.x000C_PlayerSavedData.x0000_HotbarButtonData)
            {
                Skills.Add(new Controller.SkillbarHelper {
                    SlotPostion = (SkillPosition)i,
                    SnoId = x.x00_PowerSnoId,
                    SnoPower = (SnoPower)x.x00_PowerSnoId,
                    Value = -1
                });
                i++;
            }

            Skills.Add(new Controller.SkillbarHelper
            {
                SlotPostion = SkillPosition.Potion,
                SnoId = (int)SnoPower.DrinkHealthPotion,
                SnoPower = SnoPower.DrinkHealthPotion,
                Value = -1
            });

            return Skills;
        }

        public static double Cooldown(int PowerSnoID)
        {
            if (Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0B0_GameBalanceType != Enigma.D3.Enums.GameBalanceType.Heroes)
                return -1;

            int GameTicks = Controller.GameManager.Instance.GameTicks;
            int Cooldown = GameTicks;

            if (PowerSnoID == (int)SnoPower.X1_Monk_DashingStrike || PowerSnoID == (int)SnoPower.Barbarian_FuriousCharge || PowerSnoID == (int)SnoPower.DemonHunter_Sentry)
                Cooldown = Attributes.NextChargeGainedtime.GetValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player, PowerSnoID);

            else
                Cooldown = Attributes.PowerCooldown.GetValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player, PowerSnoID);

            if(Cooldown > GameTicks)
            {
                double CurrentTick = (Cooldown - GameTicks) / 60d;

                if (CurrentTick < 1)
                    return Math.Round(CurrentTick, 1);
                else
                    return Math.Round(CurrentTick, 0);
            }   

            return -1;
        }
        
        public static double GetCoolDown(double Value) // Returns Seconds
        {            
            if(Value > -1)
                return (Value - Controller.GameManager.Instance.GameTicks) / 60d; // 60 "Ticks" per second (60hz)
              
            return -1;
        }

        public static string Format(double Value)
        {
            if (Value == -1 || Value < -1)
                return " ";

            if(Value < 1)
                return (Math.Round(Value / 0.10) * 0.10).ToString();

            TimeSpan Time = TimeSpan.FromSeconds(Value);
            string Minute = "";
            string Seconds = Time.Seconds.ToString();

            if(Time.Minutes > 0)
                Minute = Time.Minutes + ":";
            if (Time.Seconds < 10 && Time.Minutes > 0)
                Seconds = "0" + Seconds;

            return Minute + Seconds;
        }

        public static Dictionary<int, double> Debuffs()
        {
            Dictionary<int, double> Buffs = new Dictionary<int, double>(); // Dictionary<SNOID, ToCooldown>()

            foreach (Enigma.D3.UI.Buff BuffManager in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.BuffManager.x30_Debuffs)
            {
                double value = Container(BuffManager);
                
                Buffs.Add(BuffManager.x000_PowerSnoId, GetCoolDown(value));
            }

            return Buffs;

        }
        
        public static Dictionary<int, double> Buffs()
        {
            Dictionary<int, double> Buffs = new Dictionary<int, double>(); // Dictionary<SNOID, ToCooldown>()
            //HashSet<int> Active = new HashSet<int>();
            int i = 0;

            if (Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.BuffManager == null)
               return Buffs;

            foreach (Enigma.D3.UI.Buff BuffManager in Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.BuffManager.x1C_Buffs)
            {
                /*double valuex = Container(BuffManager.x000_PowerSnoId,1);

                using (System.IO.StreamWriter fs = new System.IO.StreamWriter("./aaaTick.txt", true))
                    fs.WriteLine(valuex);

                UXButton x = UXHelper.GetControl<UXButton>(BuffManager.x228_DlgIcon.x008_Name);*/

                double value = Container(BuffManager);

                if (Buffs.ContainsKey(BuffManager.x000_PowerSnoId)) // Same skill, mantra?
                    Buffs.Add(BuffManager.x000_PowerSnoId+1, GetCoolDown(value));

                else
                    Buffs.Add(BuffManager.x000_PowerSnoId, GetCoolDown(value));


                i++;
                /*
                List<double> GetContainer = Container(BuffManager.x000_PowerSnoId);
                int Count = GetContainer.Count;

                if (Skips.Contains(BuffManager.x000_PowerSnoId))
                {
                    Buffs.Add(BuffManager.x000_PowerSnoId, -1);
                    continue;
                }

                if (Count == 2) 
                {
                    if (Active.Contains(BuffManager.x000_PowerSnoId)) // Active already added (-1)
                        Buffs.Add(BuffManager.x000_PowerSnoId + 1, GetCoolDown(GetContainer.FirstOrDefault()));

                    else
                    {
                        if(!Buffs.ContainsKey(BuffManager.x000_PowerSnoId))
                            Buffs.Add(BuffManager.x000_PowerSnoId, GetCoolDown(-1));

                        Active.Add(BuffManager.x000_PowerSnoId);
                    }
                }

                else
                {
                    if (ActiveAndPassiveSkills.Contains(BuffManager.x000_PowerSnoId))
                        if (!Active.Contains(BuffManager.x000_PowerSnoId))
                            Buffs.Add(BuffManager.x000_PowerSnoId, GetCoolDown(-1));
                        else
                            continue;
                    else
                        Buffs.Add(BuffManager.x000_PowerSnoId, GetCoolDown(GetContainer.FirstOrDefault()));
                }*/
            }

            return Buffs;
        }

   
        public static double Container(Enigma.D3.UI.Buff buff, int Skip = 0)
        {
            if (Controller.GameManager.Instance.GManager.GList.MainAccount.Player.x0B0_GameBalanceType != Enigma.D3.Enums.GameBalanceType.Heroes)
                return -1;

            try
            {
               // for (int i = BuffHelper.BuffIconStart; i <= BuffHelper.BuffIconEnd; i++)
               // {
                   // double values = AttributeHelper.GetAttributeValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player,
                    //    (Enigma.D3.Enums.AttributeId)i, buff.x000_PowerSnoId);

                    double values = Enigma.D3.Helpers.AttributeHelper.GetAttributeValue(Controller.GameManager.Instance.GManager.GList.MainAccount.Player,
                    (Enigma.D3.Enums.AttributeId)(BuffHelper.BuffIconStart + (buff.x004_Neg1)), buff.x000_PowerSnoId);

                    if (values > 0)
                        return values;
               // }

                return -1;
            }

            catch { }

            return -1;
        }
    }
}
