using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FK.UI
{
    public static class UniqueItemsController 
    {
        public static Dictionary<string, UniqueItem> LegendaryItems { get; set; }

        public static void Add(string ActorName, string DisplayName, UniqueItemType LegendaryType, string overide = "")
        {
            LegendaryItems = LegendaryItems ?? new Dictionary<string, UniqueItem>();

            if (LegendaryItems.ContainsKey(ActorName))
                return;

            LegendaryItems.Add(ActorName, new UniqueItem
            {
                ItemName = DisplayName,
                LegendaryItemType = LegendaryType,
                Override = overide
            });
        }

        public static UniqueItem TryGet(string Key)
        {
            if (LegendaryItems.ContainsKey(Key))
                return LegendaryItems[Key];

            return null;
        }

        public static void Remove(string Key)
        {
            if (LegendaryItems.ContainsKey(Key))
                LegendaryItems.Remove(Key);
        }

        public static string GetBaseItemName(string name)
        {
            /*##
             * 104 = First patch 2012 (oct)
             * x1 = RoS
             * 1xx = RoS Hotfix
             * p_1 || p_2 || p_3 || p_4 = Season 1 | 2 | 3 | 4
             * x1_210 No idea ( patch 2.1? )
             */
            string[] Keys = new string[] { "x1", "1xx", "p_1", "p_2", "x1_210", "p1", "p2", "p3", "p4", "p41", "104" };
            string[] Starting = new string[] { "p1", "p2", "p3", "p4", "p41", "x1" };

            string[] NameParts = name.Split('_');
            string Ending = NameParts.Last();
            string Start = NameParts.First();

            int startIndex = Start.Length + 1;
            int endIndex = Ending.Length + 1;

            if (Starting.Contains(Start))
                name = name.Substring(startIndex, name.Length - startIndex);

            if (name.Contains("x1_210"))
                name = name.Substring(0, name.Length - 7);

            if (Keys.Contains(Ending))
            {
                if (Ending == "104")
                {
                    string[] All = name.Split('_');
                    int Count = All.Length;

                    if (Regex.IsMatch(All[Count - 2], @"^\d+$")) // Digit before, replace _104
                        name = name.Substring(0, name.Length - endIndex);
                }

                else
                    name = name.Substring(0, name.Length - endIndex);
            }

            return name;
        }

    }
}
