using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows;
using System.Linq;

namespace FindersKeepers
{
    public abstract class AvaiableSounds
    {
        [XmlIgnore]
        public List<FKSounds.FKSound> AvailableSounds { get { return Config.Get<FKSounds>().Soundmap; } }
    }

    [Serializable()]
    public class FKSounds : SetDefault
    {
        [XmlArray("SoundsMap")]
        [XmlArrayItem("SoundId")]
        public List<FKSound> Soundmap = new List<FKSound>();

        [XmlIgnore]
        public int GetAutoIncrementId { get { return Soundmap.Count + 1; } } // + 1 to counter the default no sound available

        [Serializable()]
        public class FKSound
        {
            [XmlElement]
            public string Name { get; set; }
            [XmlElement]
            public string URI { get; set; }
            [XmlAttribute]
            public int Identifier { get; set; }
            [XmlElement]
            public bool InFolder { get; set; }
        }
             
        public object _DEFAULT()
        {
            return new FKSounds
            {
                Soundmap = new List<FKSound>() {
                    new FKSound {
                        Name = "No sound",
                        URI = "" ,
                        InFolder = true,
                        Identifier = -1
                    },

                     new FKSound {
                        Name = "Default sound",
                        URI = "default.wav" ,
                        InFolder = true,
                        Identifier = 1
                    },

                   new FKSound {
                        Name = "Gobba sound",
                        URI = "gobba.wav" ,
                        InFolder = true,
                        Identifier = 2
                    }
                },
            };
        }

    }


}