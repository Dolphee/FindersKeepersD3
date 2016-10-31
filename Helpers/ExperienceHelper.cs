using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.Helpers;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Controller;

namespace FindersKeepers.Helpers
{
    public static class ExperienceHelper
    {
        public static Controller.GameManagerData.GameManagerData AccountData;
        public static Controller.GameManagerData.GameManagerData.InGameData Set { get; set; }
        public static ushort HeroLevel { get { return (ushort)Attributes.Level.GetValue(Player); } }
        public static ushort Paragon { get { return (ushort)Attributes.AltLevel.GetValue(Player); } }
        public static Enigma.D3.ActorCommonData Player { get { return GameManager.Instance.GManager.GList.MainAccount.Player; } }
        public static ulong NextLevelExperience { get; set; }
        public static ulong ExperienceNeeded { get; set; }
        public static ulong TotalGained { get; set; }

        public static void StartNew()
        {
            if (Player.x120_FastAttribGroupId == -1 || Player.x0B0_GameBalanceType != Enigma.D3.Enums.GameBalanceType.Heroes)
                return;

            ExperienceNeeded = (HeroLevel == 70) ? Attributes.AltExperienceNext.GetValue(Player) : Attributes.ExperienceNext.GetValue(Player);
            Calculate();
        }

        public static void Calculate()
        {
            if (Set.Experience.XPNeeded == 0) // First, when all items are not set
            {
                NextLevelExperience = (HeroLevel == 70) ? (ulong)ExperienceTracker.GetValue(Paragon, true) : (ulong)ExperienceTracker.GetValue(HeroLevel);
                TotalGained = NextLevelExperience - ExperienceNeeded;
                Set.Experience.XPNeeded = ExperienceNeeded;
            }

            if (Set.Experience.XPNeeded < ExperienceNeeded) // New level
            {
                Set.Experience.XPNeeded = ExperienceNeeded;
                NextLevelExperience = (HeroLevel == 70) ? (ulong)ExperienceTracker.GetValue(Paragon, true) : (ulong)ExperienceTracker.GetValue(HeroLevel);
            }

            if (Set.Experience.XPNeeded == ExperienceNeeded)
                return;

            Set.Experience.XPGained += Set.Experience.XPNeeded - ExperienceNeeded;
            Set.Experience.XPNeeded = ExperienceNeeded;
            TotalGained = NextLevelExperience - Set.Experience.XPNeeded;
        }

        public static double GetXPHour()
        {
            double c;
            return Math.Round( double.IsNaN((c = (double)Set.Experience.XPGained / Set.PlayTime)) ? 0 : c * 3600 , 0); // .FormatNumber("/h")
        }

        public static double AreaXPHour()
        {
            double c;
            return Math.Round(double.IsNaN((c = (double)GameManager.Instance.GManager.GList.MainAccount.LevelAreaContainer.ExperienceData.XPGained / GameManager.Instance.GManager.GList.MainAccount.LevelAreaContainer.Timer)) ? 0 : c * 3600, 0); // .FormatNumber("/h")
        }

        public static string GetETA()
        {
            if (Set == null)
                return "n/a";

            ulong ExpLeft = Set.Experience.XPNeeded;
            double c = 0.0;
            double TimeLeft = 0.0;
            double XPHour = GetXPHour();

            if (XPHour != 0)
                TimeLeft = Math.Round(double.IsNaN((c = (double)XPHour / 3600)) ? 0 : ExpLeft / c, 0);

            return ConvertToTimeSmall((double)TimeLeft); 
        }

        public static string GetEXPGear()
        {
            return Math.Round(Attributes.ExperienceBonusPercentTotal.GetValue(GameManager.Instance.GManager.GList.MainAccount.Player) *100 ) + "%";
        }

        public static double XPGained()
        {
            return 0;
        }

        public static string LevelTarget()
        {
            ulong XPLeft = 0; //HeroLevel.XPToLevel(Data.HeroLevel) + Data.XPNeeded;
            double c = 0.0;
            double TimeLeft = 0.0;
            double XPHour = GetXPHour();

            if (XPHour != 0)
                TimeLeft = Math.Round(double.IsNaN((c = (double)XPHour / 3600)) ? 0 : XPLeft / c, 0);

            return ConvertToTimeSmall((double)TimeLeft); 
        }

        public static string ConvertToTimeSmall(double seconds, string outString = "")
        {
            seconds = Convert.ToInt32(seconds);

            TimeSpan time = TimeSpan.FromSeconds(seconds);
            if (seconds <= 60)
            {
                outString = seconds + "s";
            }

            else if (seconds >= 61 && seconds < 3600)
            {
                outString = time.Minutes + "m " + time.Seconds + "s";
            }

            else if (seconds >= 3600 && seconds < 86400)
            {
                outString = time.Hours + "h " + time.Minutes + "m";
            }

            else if (seconds >= 86400)
            {
                outString = time.Days + "d " + time.Hours + "h";
            }

            return outString;
        }

        public static double CountMarkers()
        {
            double count = 0;

             foreach (var Account in GameManager.Instance.Accounts.NotMainAccount())
                 count += Account.DiabloIII.LevelArea.x008_PtrQuestMarkersMap.Dereference().ToDictionary().Count(x => x.Value.x00_WorldPosition.x0C_WorldId == Account.DiabloIII.ObjectManager.x790.Dereference().x038_WorldId_ && (x.Value.x10_TextureSnoId == 275968 || x.Value.x10_TextureSnoId == 404424));
                 
            return count;
        }

        public static double Call(FKConfig.GeneralSettings.ExperienceBar.Items.TargetType Target)
        {

            switch (Target)
            {
                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.IncreasedEXPGearProcent:
                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.NextLevelETA:
                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LevelTarget:
                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AncientRate:
                    return 0;

                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.XPHour:
                    return GetXPHour();

                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AverageDamage:
                    return GameManager.Instance.GManager.GRef.Actors.DamangeNumbers.AverageDamage;

                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AttackSpeed:
                    return Math.Round(Attributes.AttacksPerSecondTotal.GetValue(GameManagerAccountHelper.Current.Player), 2);

                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.CriticalHitChance:
                    return Math.Round(Attributes.CritPercentBonusCapped.GetValue(GameManagerAccountHelper.Current.Player) *100, 2) + 5;

                case FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LegendaryOnMap:
                    return CountMarkers();

                default:
                    return 0;
            }


            //if (Init && Target != FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.IncreasedEXPGearProcent)
            //   return 0;// return "n/a";

            /*if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.XPHour)
                return GetXPHour();// return 0;//return GetXPHour().FormatNumber("/h");

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.NextLevelETA)
                return 0;// return GetETA();

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.IncreasedEXPGearProcent)
                return 0;// return GetEXPGear();

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LevelTarget)
                return 0;//return LevelTarget();

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AreaXPHour)
                return 0;// return AreaXPHour().FormatNumber("/h");

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.LegendaryHour)
                return 0;//return "20/h";

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AncientRate)
                return 0;//return "32%";

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AttackSpeed)
                return Math.Round(Attributes.AttacksPerSecondTotal.GetValue(GameManagerAccountHelper.Current.Player),2);

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.CriticalHitChance)
                return Math.Round((Attributes.CritPercentBonusCapped.GetValue(GameManagerAccountHelper.Current.Player) *100) + 5, 2);

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.XPGained)
            {
                double val = XPGained();
                TimeSpan b = TimeSpan.FromSeconds(val);
                return 0;// return b.Minutes + ":" + b.Seconds;
            }

            if (Target == FKConfig.GeneralSettings.ExperienceBar.Items.TargetType.AverageDamage)
                return 0;//return GameManager.Instance.GManager.GRef.Actors.DamangeNumbers.AverageDamage.FormatNumber("");

            return 0;//return "~250m";

    */
        }

    }
}
