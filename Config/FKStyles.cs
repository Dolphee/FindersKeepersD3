using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;

namespace FindersKeepers
{
    [Serializable]
    public class FKStyles : SetDefault
    {
        [XmlElement]
        public Item Styles = new Item();

        public class Item
        {
            [XmlArray("Style")]
            [XmlArrayItem("Item", typeof(ItemStyle))]
            public List<ItemStyle> Style { get; set; }
        }

        public class ItemStyle 
        {
            [XmlElement]
            public string Name { get; set; }
            [XmlElement]
            public Container Background { get; set; }
            [XmlElement]
            public SkillBar Text { get; set; }
            [XmlElement]
            public SkillBar Value { get; set; }

            [Serializable()]
            public class SkillBar
            {
                [XmlAttribute]
                public bool Enabled { get; set; }
                [XmlElement]
                public string Font { get; set; }
                [XmlElement]
                public double FontSize { get; set; }
                [XmlElement]
                public string FontColor { get; set; }
                [XmlElement]
                public double BorderSize { get; set; }
                [XmlElement]
                public string BorderColor { get; set; }
                [XmlElement]
                public FontWeight ValueThickness { get; set; }
                [XmlElement]
                public Alignment HorisontalPosition { get; set; }
                [XmlElement]
                public Alignment VerticalPosition { get; set; }
                [XmlElement]
                public Thickness Margin { get; set; }

                [Serializable]
                public enum Alignment
                {
                    Left,
                    Center,
                    Right,
                    Bottom
                }
            }

            [Serializable()]
            public class Container
            {
                [XmlArray("Background")]
                [XmlArrayItem("Item", typeof(GradientHelper))]
                public List<GradientHelper> Background { get; set; }
                [XmlAttribute]
                public double Opacity { get; set; }
                [XmlAttribute]
                public string BorderColor { get; set; }
                [XmlAttribute]
                public int BorderSize { get; set; }
                [XmlElement]
                public double Position { get; set; }
                [XmlAttribute]
                public int Width { get; set; }
                [XmlAttribute]
                public bool CalculateWidth { get; set; }

                [Serializable()]
                public class GradientHelper
                {
                    [XmlAttribute]
                    public string Background { get; set; }
                    [XmlAttribute]
                    public double Angle { get; set; }
                }
            }
        }

        public object _DEFAULT()
        {
            return new FKStyles
            {
                Styles = new Item()
                {
                    Style = new List<ItemStyle>()
                    {
                        new ItemStyle {
                            Name = "XPHour",
                            Text = null,

                            Background = new FKStyles.ItemStyle.Container
                            {
                                CalculateWidth = false,
                                BorderColor = "000000",
                                BorderSize = 1,
                                Opacity = 0.6,
                                Position = 0,
                                Width = 62,
                                Background = new List<FKStyles.ItemStyle.Container.GradientHelper>(){
                                    new FKStyles.ItemStyle.Container.GradientHelper
                                    {
                                        Background = "fe5454",
                                        Angle = 0
                                    },

                                    new FKStyles.ItemStyle.Container.GradientHelper
                                    {
                                        Background = "ff4343",
                                        Angle = 0.5
                                    }
                                },
                            },

                            Value = new ItemStyle.SkillBar {
                                HorisontalPosition =  ItemStyle.SkillBar.Alignment.Center,
                                VerticalPosition =ItemStyle.SkillBar.Alignment.Center,
                                FontSize = 14,
                                FontColor = "FFFFFF",
                                Font = "Gautami",
                                Margin = new System.Windows.Thickness(1,0,0,0)
                            }
                        },

                       new ItemStyle {
                            Name = "AverageDamage",
                            Background = new FKStyles.ItemStyle.Container
                            {
                                BorderColor = "000000",
                                BorderSize = 1,
                                Opacity = 0.6,
                                Position = 0,
                                Width = 58,
                                Background = new List<FKStyles.ItemStyle.Container.GradientHelper>(){
                                     new FKStyles.ItemStyle.Container.GradientHelper
                                        {
                                            Background = "000000",
                                            Angle = 1
                                        }
                                },
                            },

                            Value = new ItemStyle.SkillBar {
                                HorisontalPosition =  ItemStyle.SkillBar.Alignment.Center,
                                VerticalPosition =ItemStyle.SkillBar.Alignment.Center,
                                FontSize = 14,
                                FontColor = "FFFFFF",
                                Font = "Gautami",
                                Margin = new System.Windows.Thickness(1,0,0,0)
                            }
                        },

                         new ItemStyle {
                            Name = "AttackPerSecond",
                            Background = new FKStyles.ItemStyle.Container
                            {
                                BorderColor = "000000",
                                BorderSize = 1,
                                Opacity = 0.6,
                                Position = 0,
                                Width = 58,
                                Background = new List<FKStyles.ItemStyle.Container.GradientHelper>(){
                                     new FKStyles.ItemStyle.Container.GradientHelper
                                        {
                                            Background = "2c69e6",
                                            Angle = 1
                                        }
                                },
                            },

                            Value = new ItemStyle.SkillBar {
                                HorisontalPosition =  ItemStyle.SkillBar.Alignment.Center,
                                VerticalPosition =ItemStyle.SkillBar.Alignment.Center,
                                FontSize = 14,
                                FontColor = "FFFFFF",
                                Font = "Gautami",
                                Margin = new System.Windows.Thickness(1,0,0,0)
                            }
                        },

                        new ItemStyle {
                            Name = "CritChance",
                            Background = new FKStyles.ItemStyle.Container
                            {
                                BorderColor = "000000",
                                BorderSize = 1,
                                Opacity = 0.6,
                                Position = 0,
                                Width = 58,
                                Background = new List<FKStyles.ItemStyle.Container.GradientHelper>(){
                                     new FKStyles.ItemStyle.Container.GradientHelper
                                        {
                                            Background = "314c1e",
                                            Angle = 1
                                        }
                                },
                            },

                            Value = new ItemStyle.SkillBar {
                                HorisontalPosition =  ItemStyle.SkillBar.Alignment.Center,
                                VerticalPosition =ItemStyle.SkillBar.Alignment.Center,
                                FontSize = 14,
                                FontColor = "FFFFFF",
                                Font = "Gautami",
                                Margin = new System.Windows.Thickness(1,0,0,0)
                            }
                        },
                    }
                }
            };
        }
    }
}
