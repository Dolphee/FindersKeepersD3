using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Speech.Synthesis;
using FindersKeepers.Controller.Enums;
using FK.UI;

namespace FindersKeepers.Helpers
{
    public class SoundHelper
    {
        public static void Play(FilterItems Filter, int MultiboxID = -1)
        {
           // if (Filter.Sound.Exists(x => x.ProcessID == MultiboxID))
              //  Play(Filter.Sound.Find(x => x.ProcessID == MultiboxID).URI);
        }

        public static void Play(string URI, bool PlaySynced = true, bool NewThread = true)
        {
            if (!System.IO.File.Exists(URI))
                return;

            System.Threading.Thread Thread = new System.Threading.Thread(() => {
                using(SoundPlayer Player = new SoundPlayer(URI))
                {
                    if(PlaySynced)
                        Player.PlaySync();
                    else
                        Player.Play();
                }
            });

            Thread.Start();
        }

        public static System.Threading.Thread SpeechThread;
        public static void PlayVoice(FilterItems Filter, Item Item)
        {
            string NameToRead = Item.ItemActor.x004_Name;

            if (Item.ItemQuality == ItemQuality.Legendary)
            {
                try
                {
                    UniqueItem Legendary = UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower());
                    NameToRead = (Item.AncientItem) ? "Ancient" + Legendary.ItemName : Legendary.ItemName;
                }
                catch
                {
                    // Unknown legendary...
                }
            }

            System.Threading.Thread Thread = new System.Threading.Thread(() =>
            {
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    synthesizer.Volume = 100; 
                    synthesizer.Rate = 0;
                    synthesizer.SetOutputToDefaultAudioDevice();

                    synthesizer.Speak(NameToRead);
                 
                }
            });
         
            Thread.Start();
        }

        public static void OutputToFile(Item Item, string Nickname)
        {
            System.Threading.Thread Thread = new System.Threading.Thread((() =>
            {
                try
                {
                    UniqueItem Legendary = UniqueItemsController.TryGet(Item.SNOItem.ActorName.ToLower());
                    string Name = (Item.AncientItem) ? "Ancient" + Legendary.ItemName : Legendary.ItemName;

                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter("./Legendary.txt", true))
                        fs.WriteLine(Name + "|" + DateTime.Now + "|" + Legendary.LegendaryItemType + "|" + Nickname);
                }
                catch { }
            }));

            Thread.Start();
        }
    }
}
