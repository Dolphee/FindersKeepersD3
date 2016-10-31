using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FindersKeepers.Helpers;
using Enigma.D3;
using System.Windows;

namespace FindersKeepers.Templates
{
    public class Buffs
    {
        public static Dictionary<int, Buffs.Helpers> Items = new Dictionary<int,Helpers>();
        public static Dictionary<int, double> PowerSNOID = new Dictionary<int, double>();

        public static Dictionary<int, Buffs.Helpers> ItemsDebuff = new Dictionary<int, Helpers>();
        public static Dictionary<int, double> PowerSNOIDDebuff = new Dictionary<int, double>();

        public static StackPanel Area = null;
        public static StackPanel DebuffArea = null;


        public static void Reset()
        {
            Items = new Dictionary<int, Helpers>();
            PowerSNOID = new Dictionary<int, double>();
            PowerSNOIDDebuff = new Dictionary<int, double>();
            ItemsDebuff = new Dictionary<int, Helpers>();
        }

        public static void Delete(int Key)
        {
            Extensions.Execute.UIThread(() =>
            {
                Area.Children.Remove(Items.Single(x => x.Key == Key).Value.Border);
            });

            Items.Remove(Key);
        }

        public static void Add(int Key, double Timeleft)
        {
            Extensions.Execute.UIThread(() => 
            {
                Border Border = new Border { Width = 55, Height = 54, Margin = new System.Windows.Thickness(0,0,3,0)};
                OutlinedTextBlock Text = new OutlinedTextBlock
                {
                    Text = Skills.Format(Timeleft),
                    FontSize = 20,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    FontWeight = System.Windows.FontWeights.Bold,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    Margin = new System.Windows.Thickness(2,0,0,3),
                    StrokeThickness = 1,
                    Fill = Extensions.HexToBrush("#ffffff"),
                    Stroke = Extensions.HexToBrush("#000000"),
                };

                Border.Child = Text;
                Items.Add(Key, new Helpers { Border = Border, Text = Text});

                if (Area != null)
                    Area.Children.Add(Border);
            });
            // Hydra 15 Sec
        }

        public static void DeleteDebuff(int Key)
        {
            Extensions.Execute.UIThread(() =>
            {
                DebuffArea.Children.Remove(ItemsDebuff.Single(x => x.Key == Key).Value.Border);
            });

            ItemsDebuff.Remove(Key);
        }

        public static void AddDebuff(int Key, double Timeleft)
        {
            Extensions.Execute.UIThread(() =>
            {
                Border Border = new Border { Width = 55, Height = 54, Margin = new System.Windows.Thickness(0, 0, 3, 0), HorizontalAlignment = HorizontalAlignment.Right};
                OutlinedTextBlock Text = new OutlinedTextBlock
                {
                    Text = Skills.Format(Timeleft),
                    FontSize = 20,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    FontWeight = System.Windows.FontWeights.Bold,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    Margin = new System.Windows.Thickness(2, 0, 0, 3),
                    StrokeThickness = 1,
                    Fill = Extensions.HexToBrush("#ffffff"),
                    Stroke = Extensions.HexToBrush("#000000"),
                    FlowDirection = FlowDirection.LeftToRight
                };

                Border.Child = Text;
                ItemsDebuff.Add(Key, new Helpers { Border = Border, Text = Text });

                if (Area != null)
                    DebuffArea.Children.Add(Border);
            });

            // Hydra 15 Sec
        }


        public static void Set()
        {
            PowerSNOID = Skills.Buffs();
            HashSet<int> Values = new HashSet<int>(Items.Keys.Except(PowerSNOID.Keys).Concat(PowerSNOID.Keys.Except(Items.Keys)));
                
            foreach (int Key in Values) // Both delete and add keys
                if (Items.ContainsKey(Key)) // Delete
                    Delete(Key);
                else // Add new 
                    Add(Key, PowerSNOID.Single(x => x.Key == Key).Value);

            // ## Update
            Extensions.Execute.UIThread(() =>
            {
                foreach (KeyValuePair<int, Helpers> Key in Items)
                    Key.Value.Text.Text = Skills.Format(PowerSNOID.Single(x => x.Key == Key.Key).Value);
            });
        }

        public static void SetDebuff()
        {
            PowerSNOIDDebuff = Skills.Debuffs();

            if (PowerSNOIDDebuff.Count < 1 && ItemsDebuff.Count == 0)
                return;

            HashSet<int> Values = new HashSet<int>(ItemsDebuff.Keys.Except(PowerSNOIDDebuff.Keys).Concat(PowerSNOIDDebuff.Keys.Except(ItemsDebuff.Keys)));

            foreach (int Key in Values) // Both delete and add keys
                if (ItemsDebuff.ContainsKey(Key)) // Delete
                    DeleteDebuff(Key);
                else // Add new 
                    AddDebuff(Key, PowerSNOIDDebuff.Single(x => x.Key == Key).Value);

            // ## Update
            Extensions.Execute.UIThread(() =>
            {
                foreach (KeyValuePair<int, Helpers> Key in ItemsDebuff)
                    Key.Value.Text.Text = Skills.Format(PowerSNOIDDebuff.Single(x => x.Key == Key.Key).Value);
            });
        }

        public struct Helpers
        {
            public Border Border;
            public OutlinedTextBlock Text;
            public int Position;
        }
    }
}


/* foreach(var x in Enigma.D3.Engine.Current.ObjectManager.x930_RActors)
 {
     /*if(x.GetACDData().x184_ActorType == Enigma.D3.Enums.ActorType.Item)
     {
         Debug.GetValues.GetAttributes(x.x004_Name.Split('-')[0], true);
     }

     if(x.x004_Name.Split('-')[0] == "Wizard_HydraHead_Big") // 900
     //if (x.x004_Name.Split('-')[0] == "Wizard_Familiar_Regen")
     {
         Debug.GetValues.GetAttributes("Wizard_HydraHead_Big", true);
         i++;
         double CurrentTick = (((x.GetACDData().x0A4_GameTick + 36000) - Enigma.D3.Engine.Current.ObjectManager.x798_Storage.x0F0_GameTick) / 60d);
         Extensions.Execute.UIThread(() =>
         {
                Items.First().Value.Text.Text = Math.Round(CurrentTick,0).ToString();
         });
     }
 }*/
