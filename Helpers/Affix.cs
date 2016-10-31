using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using System.Windows.Controls;

namespace FindersKeepers.Helpers
{
    public static class Affix
    {
        public static Dictionary<int, AffixHelper> AffixList = new Dictionary<int, AffixHelper>();
        public static AffixHelper Current = null;
        public static Dictionary<int, int> ActorID = new Dictionary<int, int>();
        public static Dictionary<int, int> NewActorID = new Dictionary<int, int>();
        public static int Max = 5;
        public static int Count = 0;

        public class AffixHelper
        {
            public string Name;
            public int Key;
            public int ElementKey;
            public HashSet<int> Affixes = new HashSet<int>();
            public HashSet<int> Owners = new HashSet<int>();
            public System.Windows.Controls.UserControl UserControl;
        }

        public static void NewLoop()
        {
            if (ActorID.Count == 0 && NewActorID.Count == 0)
            {
                Count = 0;
                if (AffixList.Count != 0) // Buggished
                {
                    foreach (var value in AffixList)
                       // Controller.GameManager.Instance.GManager.GRef.Attacher.Remove(value.Value.Name, value.Value.UserControl);
                    Controller.GameManager.Instance.GManager.GRef.D3OverlayControl.Delete<Templates.Elites.EliteAffixes>(value.Value.Name);
                }

                return;
            }

            IEnumerable<int> ToDelete = NewActorID.Keys.Except(ActorID.Keys).Concat(ActorID.Keys.Except(NewActorID.Keys));

            foreach (int value in ToDelete)
            {
                if (!ActorID.Keys.Contains(value) && NewActorID.Keys.Contains(value)) // newly added
                    continue;

                if (ActorID.Keys.Contains(value) && !NewActorID.Keys.Contains(value))
                {
                    if (ActorID[value] == 1) // Blue pack Champion
                    {
                        AffixHelper List = LookForOwner(ToDelete, value);

                        if (List != null)
                        {
                            //Controller.GameManager.Instance.GManager.GRef.Attacher.Remove(List.Name, List.UserControl);
                            Controller.GameManager.Instance.GManager.GRef.D3OverlayControl.Delete<Templates.Elites.EliteAffixes>(List.Name);
                            AffixList.Remove(List.Key);
                        }
                    }

                    else
                    {
                        if (AffixList.ContainsKey(value)) // Havent been deleted
                        {
                            //Controller.GameManager.Instance.GManager.GRef.Attacher.Remove(AffixList[value].Name, AffixList[value].UserControl);
                             Controller.GameManager.Instance.GManager.GRef.D3OverlayControl.Delete<Templates.Elites.EliteAffixes>(AffixList[value].Name);
                            AffixList.Remove(value);
                        }
                    }
                }
            }

            ActorID = new Dictionary<int, int>(NewActorID);
            NewActorID = new Dictionary<int, int>();
        }

        public static HashSet<int> TryAdd(ActorCommonData ACD)
        {
            HashSet<int> Affix = new HashSet<int>();
            List<int> Values = new List<int>() { ACD.x1A8_Neg1_MonsterAffixId, ACD.x1A8_Neg1_MonsterAffixId, ACD.x1AC_Neg1_MonsterAffixId, 
                    ACD.x1B0_Neg1_MonsterAffixId, ACD.x1B4_Neg1_MonsterAffixId, ACD.x1B8_Neg1_MonsterAffixId,ACD.x1BC_Neg1_MonsterAffixId, 
                    ACD.x1C0_Neg1_MonsterAffixId,ACD.x1C4_Neg1_MonsterAffixId,ACD.x1C8_Neg1_MonsterAffixId };

            foreach (int Value in Values.Where(x => x != -1))
                if (Config._.FKAffixes.Affixs.Contains(Value))
                    Affix.Add(Value);

            return Affix;
        }

        public static AffixHelper Exist(this HashSet<int> List)
        {
            foreach (var x in AffixList.Values)
                if (x.Affixes.SequenceEqual(List))
                    return x;

            return null;
        }

        public static int GetKey()
        {
            HashSet<int> Keys = new HashSet<int>(AffixList.Select(x => x.Value.ElementKey));

            for (int i = 0; i < Max; i++)
                if(!Keys.Contains(i))
                    return i;

            return -1;
        }

        public static AffixHelper LookForOwner(IEnumerable<int> List, int Key)
        {
            AffixHelper Found = null;
            foreach (var x in AffixList.Values)
                if (x.Owners.Contains(Key))
                    Found = x;

            if (Found != null)
            {
                int i = Found.Owners.Count;

                foreach (int Keys in Found.Owners)
                    if (List.Contains(Keys))
                        i--;

                if (i == 0)
                    return Found;

                else
                    Found.Owners.Remove(Key);
            }

            return null;
        }

        public static int EliteAffixes(this ActorCommonData ACD)
        {
            HashSet<int> Affix = TryAdd(ACD);

            if (AffixList.Count >= Max || Affix.Count == 0)
            {
                NewActorID.Add(ACD.x000_Id, ((int)ACD.x0B8_MonsterQuality == 1) ? 1 : 0);
                return -1;
            }

            AffixHelper Current = null;

            if (ACD.x0B8_MonsterQuality == Enigma.D3.Enums.MonsterQuality.Champion) // Blue
            {
                Current = Affix.Exist();

                if (Current != null) // Already have same affixes, link with that instead!
                {
                    if (!Current.Owners.Contains(ACD.x000_Id))
                    {
                        Current.Owners.Add(ACD.x000_Id);
                        return Current.ElementKey;
                    }

                    NewActorID.Add(ACD.x000_Id, 0);
                    return Current.ElementKey;
                }
            }

            else
            {
                if (AffixList.ContainsKey(ACD.x000_Id))
                {
                    NewActorID.Add(ACD.x000_Id, 1);
                    return AffixList[ACD.x000_Id].ElementKey;
                }
            }

            Current = new AffixHelper
            {
                Key = ACD.x000_Id,
                Name = "ElitePack" + ACD.x000_Id,
                Affixes = Affix,
                ElementKey = GetKey(),
                Owners = new HashSet<int>() { ACD.x000_Id },
            };

            //UserControl T = Controller.GameManager.Instance.GManager.GRef.Attacher.Add<Templates.Elites.EliteAffixes>(Current.Name, Current);
            UserControl T = Controller.GameManager.Instance.GManager.GRef.D3OverlayControl.CreateControl<Templates.Elites.EliteAffixes>(Current.Name, false, Current);
            Current.UserControl = T;
            AffixList.Add(ACD.x000_Id, Current);
            NewActorID.Add(Current.Key, ((int)ACD.x0B8_MonsterQuality == 1) ? 1 : 0);
            return Current.ElementKey;
        }
    }

}
