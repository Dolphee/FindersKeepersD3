using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows;
using System.Linq;
using PropertyChanged;

namespace FindersKeepers
{
    [Serializable(), ImplementPropertyChanged]
    public class FKAffixes : SetDefault, ICache
    {
        [XmlElement]
        public FKAffixHelper Settings {get;set; }

        [XmlArray("AffixesList")]
        [XmlArrayItem("Affix")]
        public List<FKAffix> Affixes = new List<FKAffix>();

        [XmlIgnore]
        public HashSet<int> _Affixs;

        [XmlIgnore]
        public HashSet<int> Affixs { 
            get {
                if (_Affixs == null)
                    _Affixs = new HashSet<int>(Affixes.Where(x => x.Enabled).Select(x => x.Identifier));
                        
                return _Affixs;
            }
        }

        [Serializable(), ImplementPropertyChanged]
        public class FKAffixHelper {
            [XmlElement]
            public bool Enabled {get; set;}
            [XmlElement]
            public bool DrawLine { get; set; }
            [XmlElement]
            public bool InRift { get; set; }
            [XmlElement]
            public bool InGreaterRifts { get; set; }
            [XmlElement]
            public Controller.Enums.Difficulty RiftCondition { get; set; }
            [XmlElement]
            public int GreaterRiftCondition { get; set; }
        }

        [Serializable(), ImplementPropertyChanged]
        public class FKAffix : HasName
        {
            [XmlAttribute]
            public string Name { get; set; }
            [XmlElement]
            public int Identifier { get; set; }
            [XmlElement]
            public FKAffix.Style Styles { get; set; }
            [XmlAttribute]
            public bool Enabled { get; set; }

            [ImplementPropertyChanged]
            public class Style
            {
                [XmlElement]
                public string Background {get;set;}
                [XmlElement]
                public string Foreground { get; set; }
                [XmlElement]
                public int BorderSize { get; set; }
                [XmlElement]
                public string BorderColor { get; set; }

                [XmlIgnore]
                public System.Windows.Media.Brush BackgroundBrush;
                [XmlIgnore]
                public System.Windows.Media.Brush ForegroundBrush;
                [XmlIgnore]
                public System.Windows.Media.Brush BorderBrush;
            }
        }

        public void OnStartup()
        {
            foreach (var x in Affixes)
            {
                x.Styles.BackgroundBrush = Extensions.HexToBrush("#"+ x.Styles.Background);
                x.Styles.ForegroundBrush = Extensions.HexToBrush("#" + x.Styles.Foreground);
                x.Styles.BorderBrush = Extensions.HexToBrush("#" + x.Styles.BorderColor);
            }
        }

        public object _DEFAULT()
        {
            List<FKAffix> Affixes = new List<FKAffix>(){
                    new FKAffix { Identifier = -1669589516, Enabled = true, Name = "Arcane Enchanted", Styles = new FKAffix.Style{ Background = "c415ba", BorderColor = "420735",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1929212066,Enabled = true,  Name = "Poison Enchanted", Styles = new FKAffix.Style{ Background = "468448", BorderColor = "032f09",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1165197192, Enabled = true,  Name = "Avenger", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -121983956, Enabled = true, Name = "Desecrator", Styles = new FKAffix.Style{ Background = "ff3030", BorderColor = "3c0808",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1752429632, Enabled = true, Name = "Electrified", Styles = new FKAffix.Style{ Background = "387cf7", BorderColor = "0e283e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1512481702, Enabled = true, Name = "Extra Health", Styles = new FKAffix.Style{ Background = "5f241c", BorderColor = "41180b",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 3775118, Enabled = true, Name = "Fast", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -163836908,Enabled = true,  Name = "Frozen", Styles = new FKAffix.Style{ Background = "6ea7bf", BorderColor = "0e1e25",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1799201764,Enabled = true,  Name = "Health Link", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 127452338,Enabled = true,  Name = "Horde", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 394214687, Enabled = true, Name = "Illusionist", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -27686857, Enabled = true, Name = "Jailer", Styles = new FKAffix.Style{ Background = "974ca2", BorderColor = "48124d",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -2088540441,Enabled = true,  Name = "Knockback", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1886876669, Enabled = true, Name = "Frozen Pulse", Styles = new FKAffix.Style{ Background = "6ea7bf", BorderColor = "0e1e25",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -439707236, Enabled = true, Name = "Fire Chains", Styles = new FKAffix.Style{ Background = "ac0303", BorderColor = "4c0e0e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1412750743,Enabled = true,  Name = "Missile Dampening", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 106438735,Enabled = true,  Name = "Molten", Styles = new FKAffix.Style{ Background = "ac0303", BorderColor = "4c0e0e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 106654229, Enabled = true, Name = "Mortar", Styles = new FKAffix.Style{ Background = "ac0303", BorderColor = "4c0e0e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1245918914,Enabled = true,  Name = "Nightmarish", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1905614711, Enabled = true, Name = "Orbiter", Styles = new FKAffix.Style{ Background = "387cf7", BorderColor = "0e283e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1333953694,Enabled = true,  Name = "Plagued", Styles = new FKAffix.Style{ Background = "468448", BorderColor = "032f09",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -1374592233,Enabled = true,  Name = "Reflects Damage", Styles = new FKAffix.Style{ Background = "151515", BorderColor = "0c0c0c",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -725865705,Enabled = true,  Name = "Shielding", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -507706394,Enabled = true,  Name = "Teleporter", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 1156956365,Enabled = true,  Name = "Wormhole", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = -50556465,Enabled = true,  Name = "Thunderstorm", Styles = new FKAffix.Style{ Background = "387cf7", BorderColor = "0e283e",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 458872904,Enabled = true,  Name = "Vortex", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 481181063, Enabled = true, Name = "Waller", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}},
                    new FKAffix { Identifier = 395423867,Enabled = true,  Name = "Vampiric", Styles = new FKAffix.Style{ Background = "202020", BorderColor = "1a1a1a",  Foreground = "ffffff", BorderSize = 1}}
                };

            return new FKAffixes
            {
                 Settings = new FKAffixHelper
                 {
                      DrawLine = false,
                      Enabled = true,
                      InGreaterRifts = true,
                      InRift = true,
                      GreaterRiftCondition = 0,
                      RiftCondition = Controller.Enums.Difficulty.Normal
                 },
                 Affixes = Affixes
            };
        }

    }


}