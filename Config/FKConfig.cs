using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows;
using PropertyChanged;

namespace FindersKeepers
{
    [Serializable(), ImplementPropertyChanged]
    public class FKConfig : SetDefault
    {
        [XmlElement]
        public string FindersKeepersVersion = FindersKeepers.MainWindow.Version;
        [XmlElement]
        public GeneralSettings General {get;set;}

        [Serializable()]
        public class GeneralSettings
        {
            [XmlElement]
            public Minimap MiniMapSettings { get;set; }
            [XmlElement]
            public MultiBoxMacro Macros{ get; set; }
            [XmlElement]
            public Settings FKSettings { get; set; }
            [XmlElement]
            public Skillbar Skills { get; set; }
            [XmlElement]
            public ExperienceBar Experience { get; set; }
            [XmlElement]
            public MiscBar Misc { get; set; }
            [XmlElement]
            public DamageNumbers DamageNumber { get; set; }

            public class DamageNumbers
            {
                [XmlElement]
                public bool Enabled { get; set; }
                [XmlElement]
                public ushort UpdateRate { get; set; }
                [XmlElement]
                public bool ResetLimit { get; set; }

                [XmlArray("DamageTypes")]
                [XmlArrayItem("Type", typeof(Helpers.DamageNumbers.DamageTypes))]
                public HashSet<Helpers.DamageNumbers.DamageTypes> Types { get; set; }
            }

            [ImplementPropertyChanged]
            public class Minimap
            {
                [XmlElement]
                public bool Enabled { get; set; }
                [XmlElement]
                public bool LargeMap { get; set; }
                [XmlElement]
                public MarkerSettings RevealMapMinimap { get; set; }
                [XmlElement]
                public MarkerSettings RevealMapLargemap { get; set; }
                [XmlElement]
                public bool QuestMarker { get; set; }

                [ImplementPropertyChanged]
                public class MarkerSettings
                {
                    [XmlAttribute]
                    public bool Reveal { get; set; }
                    [XmlElement]
                    public string Background { get; set; }
                    [XmlElement]
                    public float Opacity { get; set; }
                }
            }

            [ImplementPropertyChanged]
            public class Skillbar
            {
                [XmlElement]
                public bool Skill { get; set; }
                [XmlElement]
                public bool Buffs { get; set; }
                [XmlElement]
                public bool Resource { get; set; }
                [XmlElement]
                public bool Life { get; set; }
                [XmlElement]
                public bool SkillDamage { get; set; }
            }

            [ImplementPropertyChanged]
            public class MiscBar
            {
                [XmlElement]
                public bool Inventory { get; set; }
                [XmlElement]
                public bool GreaterRifts { get; set; }
            }

            [ImplementPropertyChanged]
            public class ExperienceBar
            {
                [XmlElement]
                public bool Enabled { get; set; }
                [XmlElement]
                public bool ShowNumbers { get; set; }

                [XmlArray("ExperienceItems")]
                [XmlArrayItem("Items", typeof(Items))]
                public List<Items> Item { get; set; }

                [ImplementPropertyChanged]
                public class Items
                {
                    [XmlAttribute]
                    public TargetType Target { get; set; }
                    [XmlAttribute]
                    public string Text { get; set; }
                    [XmlElement]
                    public string StyleName { get; set; }
                    [XmlElement]
                    public string ToolTip { get; set; }
                    [XmlAttribute]
                    public string Extension { get; set; }

                    [Serializable]
                    public enum TargetType
                    {
                        XPHour,
                        IncreasedEXPGearProcent,
                        NextLevelETA,
                        LevelTarget,
                        XPGained,
                        AreaXPHour,
                        TotalRift,
                        AverageDamage,
                        RiftExpHour,
                        LegendaryHour,
                        AncientRate,
                        CriticalHitChance,
                        AttackSpeed,
                        LegendaryOnMap
                    }
                }
            }

            [ImplementPropertyChanged]
            public class MultiBoxMacro
            {
                [XmlElement]
                public bool UseHotkey { get; set; }
                [XmlElement]
                public int Hotkey { get; set; }

                [XmlElement]
                public bool UseForceMove { get; set; }

                [XmlElement]
                public int ForceMoveHotkey { get; set; }
            }

            [ImplementPropertyChanged]
            public class Settings
            {
                [XmlElement]
                public bool UseOverlayInGame { get; set; }

                [XmlElement]
                public bool GroupRActors { get; set; }
                [XmlElement]
                public bool WriteToFile { get; set; }
                [XmlElement]
                public bool AutomaticStartFK { get; set; }
                [XmlElement]
                public bool MinimizeToTray { get; set; }
                [XmlElement]
                public bool AllowUpdates { get; set; }
                [XmlElement]
                public bool SendErrorLogs { get; set; }
                [XmlElement]
                public bool DebugMode { get; set; }
                [XmlElement]
                public bool CompressXMLFiles { get; set; }
                [XmlElement]
                public bool UseAlternativeOverlay {get;set; }
                [XmlElement("ReadableXML")]
                public bool ReadableXML { get; set; }
            }        
        }

        /*
        /*
         * General Settings */
        public object _DEFAULT()
        {
            return new FKConfig
            {
                General = new GeneralSettings
                {
                    FKSettings = new GeneralSettings.Settings
                    {
                        AllowUpdates = true,
                        AutomaticStartFK = true,
                        CompressXMLFiles = true,
                        DebugMode = false,
                        GroupRActors = false,
                        MinimizeToTray = false,
                        ReadableXML = true,
                        SendErrorLogs = true,
                        UseOverlayInGame = true,
                        WriteToFile = false,
                    },
                   
                    DamageNumber = new GeneralSettings.DamageNumbers
                    {
                        Enabled = true,
                        UpdateRate = 20,
                        ResetLimit = false,
                        Types  = new HashSet<Helpers.DamageNumbers.DamageTypes>() {
                            Helpers.DamageNumbers.DamageTypes.CriticalHits, Helpers.DamageNumbers.DamageTypes.Low, Helpers.DamageNumbers.DamageTypes.Normal
                        }
                     },

                     Misc = new GeneralSettings.MiscBar {
                          GreaterRifts = true,
                          Inventory = true
                     },

                    Macros = new GeneralSettings.MultiBoxMacro
                    {
                        UseHotkey = false,
                        Hotkey = 96 ,
                        ForceMoveHotkey = 96,
                        UseForceMove = false
                    }, 
                    MiniMapSettings = new GeneralSettings.Minimap
                    {
                        Enabled = true,
                        LargeMap = true,
                        QuestMarker = true,
                        RevealMapLargemap = new GeneralSettings.Minimap.MarkerSettings { Reveal = true, Background = "735245", Opacity = 0.4F },
                        RevealMapMinimap = new GeneralSettings.Minimap.MarkerSettings { Reveal = true, Background = "735245", Opacity = 0.4F }
                    },
                    Skills = new GeneralSettings.Skillbar
                    {
                        Buffs = true,
                        Life = true,
                        Resource = true,
                        Skill = true,
                        SkillDamage = true
                    },

                    Experience = new GeneralSettings.ExperienceBar
                    {
                        Enabled = true,
                        ShowNumbers = true,
                        Item = new List<GeneralSettings.ExperienceBar.Items>()
                        {
                            new GeneralSettings.ExperienceBar.Items { StyleName = "XPHour",Extension = "/h", ToolTip = "Experience per hour", Target = GeneralSettings.ExperienceBar.Items.TargetType.XPHour, Text = "" },
                            new GeneralSettings.ExperienceBar.Items { StyleName = "AverageDamage", Extension = "#", ToolTip = "Average Damage", Target = GeneralSettings.ExperienceBar.Items.TargetType.AverageDamage, Text = "" },
                            new GeneralSettings.ExperienceBar.Items { StyleName = "AverageDamage",Extension = "/h", ToolTip = "Total Area XP", Target = GeneralSettings.ExperienceBar.Items.TargetType.AreaXPHour, Text = "" },
                            new GeneralSettings.ExperienceBar.Items { StyleName = "AverageDamage",Extension = "/h", ToolTip = "Total Rift XP", Target = GeneralSettings.ExperienceBar.Items.TargetType.TotalRift, Text = "" },
                            new GeneralSettings.ExperienceBar.Items { StyleName = "AttackPerSecond",Extension = "/s", ToolTip = "Attacks per Second", Target = GeneralSettings.ExperienceBar.Items.TargetType.AttackSpeed, Text = "" },
                            new GeneralSettings.ExperienceBar.Items { StyleName = "CritChance", ToolTip = "Critical Hit Chance", Target = GeneralSettings.ExperienceBar.Items.TargetType.CriticalHitChance, Extension = "%", Text = "" },
                        }
                    }
                },
            };
         
        }
    }
}