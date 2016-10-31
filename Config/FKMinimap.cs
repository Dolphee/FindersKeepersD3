using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;
using PropertyChanged;

namespace FindersKeepers
{
    [Serializable]
    public class FKMinimap : SetDefault, ICache
    {
        [XmlElement]
        public KnownActors DefaultMapItem = new KnownActors();

        [XmlIgnore]
        public Dictionary<int, int> OverrideLayout = new Dictionary<int,int>(); // First ActorSNOID, sec Identifier

        [XmlIgnore]
        public HashSet<int> Allowed;
        
        public void OnStartup()
        {
            try
            {
                Allowed = new HashSet<int>(DefaultMapItem.MapItemsx.Where(x => x.Enabled).Select(x => (int)x.ItemElement));
                DefaultMapItem.DefaultActors = DefaultMapItem.MapItemsx.ToDictionary(x => (int)x.ItemElement, x => x);
                DefaultMapItem.CustomActors = DefaultMapItem.CustomItems.ToDictionary(x => x.Identifier, x => x);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            foreach (var key in DefaultMapItem.CustomItems.Where(x => x.Enabled))
            {
                if (key.SNOID == null || key.SNOID.Count < 1)
                    continue;

                foreach(int keys in key.SNOID)
                    OverrideLayout.Add(keys, key.Identifier);
            }

            foreach (var i in DefaultMapItem.CustomActors.Values)
            {
                if (i.Shape == null)
                    continue;

                i.Shape.FillBrush = i.Shape.Fill.ToBrush();
                i.Shape.StrokeBrush = i.Shape.Stroke.ToBrush();
            }

            foreach (var i in DefaultMapItem.DefaultActors.Values)
            {
                if (i.Shape == null)
                    continue;

                i.Shape.FillBrush = i.Shape.Fill.ToBrush();
                i.Shape.StrokeBrush = i.Shape.Stroke.ToBrush();
            }
        }

        public object _DEFAULT()
        { 

            return new FKMinimap
            {
                DefaultMapItem = new KnownActors
                {
                    #region Default Values
                    Enabled = true,
                    CustomItems = new List<MapItem>
                    {
                        new MapItem {
                            Identifier = 1,
                            SNOID = new HashSet<int>() { 408989, 429619} ,
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Custom,
                            Name = "Blood Theif",
                            Shape = new MapItemShape
                            {
                                Width = 12,
                                Height = 12,
                                Fill = "FF23FC",
                                Opacity = 0.7,
                                Stroke = "842A83",
                                StrokeThickness = 2,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                         new MapItem {
                            Identifier = 2,
                            SNOID = new HashSet<int>() { 408354, 410572 , 410574 } ,
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Custom,
                            Name = "Gelatinous Sire",
                            Shape = new MapItemShape
                            {
                                Width = 12,
                                Height = 12,
                                Fill = "659DF2",
                                Opacity = 0.7,
                                Stroke = "659DF2",
                                StrokeThickness = 2,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem {
                            Identifier = 3,
                            SNOID = new HashSet<int>() { 269349 } ,
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Custom,
                            Name = "Bandit Shrine",
                            Shape = new MapItemShape
                            {
                                Width = 12,
                                Height = 12,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "FF87BA",
                                StrokeThickness = 2,
                                Shape = ItemShape.Ellipse,
                            }
                        },
                         /*new MapItem {
                            Identifier = 2,
                            SNOID = new HashSet<int>() { 4541, 221291, 304460, 241288},
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Custom,
                            Name = "Jump & Charge",
                            Shape = new MapItemShape
                            {
                                Width = 5,
                                Height = 5,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "FF0000",
                                StrokeThickness = 4,
                                Shape = ItemShape.Rectangle,
                            }
                        },*/
                    },
                    MapItemsx = new List<MapItem>()
                    {
                        new MapItem {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Goblin,
                            Name = "Treasure Goblins",
                            Shape = new MapItemShape
                            {
                                Width = 12,
                                Height = 12,
                                Fill = "ff7f21",
                                Opacity = 0.7,
                                Stroke = "Transparent",
                                StrokeThickness = 0,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Shrines,
                            Name = "Shrine",
                            Shape = new MapItemShape
                            {
                                Width = 12,
                                Height = 12,
                                Stroke = "256331",
                                StrokeThickness = 3,
                                Fill = "a4ff9d",
                                Opacity = 0.7,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Normal,
                            Name = "Normal Monsters",
                            Shape = new MapItemShape
                            {
                                Width = 7,
                                Height = 7,
                                Fill = "FFADD8E6",
                                Stroke = "Transparent",
                                StrokeThickness = 0,
                                Opacity = 0.7,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Champions,
                            Name = "Champion Packs",
                            Shape = new MapItemShape
                            {
                                Width = 14,
                                Height = 14,
                                Fill = "Transparent",
                                Opacity = 0.8,
                                Stroke = "477bff",
                                StrokeThickness = 4,
                                Shape = ItemShape.Ellipse,
                            }
                        },
                        
                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Elite,
                            Name = "Elite Pack",
                            Shape = new MapItemShape
                            {
                                Width = 14,
                                Height = 14,
                                Fill = "Transparent",
                                Opacity = 0.8,
                                Stroke = "ffd803",
                                StrokeThickness = 4,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        { 
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Minion,
                            Name = "Minons",
                            Shape = new MapItemShape
                            {
                                Width = 10,
                                Height = 10,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "EEEEEE",
                                StrokeThickness = 5,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        { 
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Chests,
                            Name = "Chests",
                            Shape = new MapItemShape
                            {
                                Width = 10,
                                Height = 10,
                                Opacity = 0.7,
                                Fill = "Transparent",
                                Stroke = "FF3B3B",
                                StrokeThickness = 3,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        { 
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_LoreBooks, // Books
                            Name = "Lootable Containers",
                            Shape = new MapItemShape
                            {
                                Width = 7,
                                Height = 7,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "5fb2ff",
                                StrokeThickness = 4,
                                Shape = ItemShape.Ellipse,
                                
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Unique,
                            Name = "Unique Monsters",
                            Shape = new MapItemShape
                            {
                                Width = 14,
                                Height = 14,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "ff1cd2",
                                StrokeThickness = 4,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_Boss,
                            Name = "Boss",
                            Shape = new MapItemShape
                            {
                                Width = 14,
                                Height = 14,
                                Fill = "Transparent",
                                Opacity = 0.7,
                                Stroke = "ff3f3f",
                                StrokeThickness = 4,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                            Enabled = true,
                            UseOverlay = true,
                            ItemElement = MapItemElement.AT_PowerPylons,
                            Name = "Power Pylon",
                            Shape = new MapItemShape
                            {
                                Width = 14,
                                Height = 14, 
                                Stroke = "8f8322",
                                StrokeThickness = 2,
                                Fill = "fff557",
                                Opacity = 0.7,
                                Shape = ItemShape.Ellipse,
                            }
                        },

                        new MapItem
                        {
                           Enabled = true, 
                           UseOverlay = true,
                           ItemElement = MapItemElement.AT_Objective, 
                           Name = "Objectives", 
                           Shape = new MapItemShape
                           {
                               Width = 14,
                               Height = 14,
                               Stroke = "835014",
                               StrokeThickness = 2,
                               Fill = "ffa842",
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse,
                           }
                        },

                        new MapItem
                        {
                           Enabled = true, 
                           UseOverlay = true,
                           ItemElement = MapItemElement.AT_PoolOfReflection, 
                           Name = "Pool of Reflection", 
                           Shape = new MapItemShape
                           {
                               Width = 12,
                               Height = 12,
                               Stroke = "ffffff",
                               StrokeThickness = 2,
                               Fill = "ffffff",
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse,
                           }
                        },

                        new MapItem
                        {
                           Enabled = true, UseOverlay = true, ItemElement = MapItemElement.AT_GreaterRiftSoul, Name = "Greater Rift Souls", Shape = new MapItemShape
                           {
                               Width = 10,
                               Height = 10,
                               Stroke = "d49cff",
                               StrokeThickness = 2,
                               Fill = "ad5dff",
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse,
                           }
                        },

                        new MapItem
                        {
                           Enabled = true, UseOverlay = true, ItemElement = MapItemElement.AT_RiftSoul, Name = "Rift Souls", Shape = new MapItemShape
                           {
                               Width = 10,
                               Height = 10,
                               Stroke = "fdac73",
                               StrokeThickness = 2,
                               Fill = "d55b06",
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse,
                           }
                        },

                        new MapItem
                        {
                           Enabled = true, UseOverlay = true, ItemElement = MapItemElement.AT_ResplendentChest, Name = "Resplended Chest", Shape = new MapItemShape
                           {
                               Width = 14,
                               Height = 14,
                               Stroke = "311C13",
                               StrokeThickness = 2,
                               Fill = "ffd801",
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse,
                           }
                        },

                        new MapItem
                        {
                            Enabled = false, UseOverlay = false, ItemElement = MapItemElement.AT_Keywardens, Name = "Keywardens", Shape = new MapItemShape
                           {
                               Width = 12,
                               Height = 12,
                               Stroke = "FFFFFF",
                               StrokeThickness = 2,
                               Fill = "Transparent", 
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse
                           }
                        },

                        new MapItem
                        {
                            Enabled = false, UseOverlay = false, ItemElement = MapItemElement.AT_Player, Name = "Player",  Shape = new MapItemShape
                           {
                               Width = 1,
                               Height = 1,
                               Stroke = "Transparent",
                               StrokeThickness = 0,
                               Fill = "Transparent", 
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse
                           }
                        },

                        new MapItem
                        {
                           Enabled = false,  UseOverlay = false, ItemElement = MapItemElement.AT_PlayerPets, Name = "Player Pets",  Shape = new MapItemShape
                           {
                               Width = 1,
                               Height = 1,
                               Stroke = "Transparent",
                               StrokeThickness = 0,
                               Fill = "Transparent", 
                               Opacity = 0.7,
                               Shape = ItemShape.Ellipse
                           }  
                        }
                    }
                    #endregion
                }
            };
        }
    }
    
    [Serializable]
    public class KnownActors
    {
        [XmlAttribute]
        public bool Enabled;

        [XmlArray("ActorType")]
        [XmlArrayItem("Actor", typeof(MapItem))]
        public List<MapItem> MapItemsx = new List<MapItem>();

        [XmlArray("CustomActorType")]
        [XmlArrayItem("Actor", typeof(MapItem))]
        public List<MapItem> CustomItems = new List<MapItem>();

        [XmlIgnore]
        public Dictionary<int, MapItem> DefaultActors;// { get { return MapItemsx.ToDictionary(x => (int)x.ItemElement, x => x);}}

        [XmlIgnore]
        public Dictionary<int, MapItem> CustomActors;// { get { return MapItemsx.ToDictionary(x => (int)x.ItemElement, x => x);}}

    }

    [Serializable, ImplementPropertyChanged]
    public class MapItem : AvaiableSounds
    {
        [XmlAttribute]
        public int Identifier = -1;
        [XmlAttribute]
        public MapItemElement ItemElement { get; set; }
        [XmlAttribute]
        public string Name{ get; set;}
        [XmlAttribute]
        public bool Enabled { get; set; }
        [XmlAttribute]
        public bool SoundEnabled { get; set; }
        [XmlAttribute]
        public int SoundId { get; set; }
        [XmlAttribute]
        public bool UseOverlay { get; set; }
        [XmlElement]
        public MapItemShape Shape { get; set; }
        [XmlArray("ActorSNO")]
        [XmlArrayItem("SNOID", typeof(int))]
        public HashSet<int> SNOID { get; set; }
    }

    [Serializable, ImplementPropertyChanged]
    public class MapItemShape
    {
        [XmlAttribute]
        public string Stroke {get;set; }
        [XmlAttribute]
        public int StrokeThickness { get; set; }
        [XmlAttribute]
        public string Fill { get; set; }
        [XmlAttribute]
        public double Opacity { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        [XmlAttribute]
        public int Width {get;set; }
        [XmlAttribute]
        public ItemShape Shape { get;set; }
        [XmlElement]
        public EffectItem Effect = new EffectItem();
        
        [XmlIgnore]
        public Brush FillBrush { get; set; }
        [XmlIgnore]
        public Brush StrokeBrush { get; set; }

        public Shape ToShape()
        {

            if(Shape == ItemShape.Ellipse)
                return new Ellipse
                {
                    Width = Width,
                    Height = Height,
                    Opacity = Opacity,
                    Fill = Fill.ToBrush(),
                    StrokeThickness = StrokeThickness,
                    Stroke = Stroke.ToBrush()
                };


             else if (Shape == ItemShape.Rectangle)
                 return new Rectangle
                 {
                     Width = Width,
                     Height = Height,
                     Opacity = Opacity,
                     Fill = Fill.ToBrush(),
                     StrokeThickness = StrokeThickness,
                     Stroke = Stroke.ToBrush()
                 };

            return null;

                /*if(Shape == "Ellipse")
                    Container = new Ellipse { Width = Width, Height = Height, Fill = Extensions.HexToBrush(Fill), Opacity = Opacity, Stroke = Extensions.HexToBrush(Stroke), StrokeThickness = StrokeThickness };
                if (Shape == "Rectangle")
                    Container = new Rectangle { Width = Width, Height = Height, Fill = Extensions.HexToBrush(Fill), Opacity = Opacity, Stroke = Extensions.HexToBrush(Stroke), StrokeThickness = StrokeThickness  };
            
                if(Effect != null)
                    if (Effect.AnimateName == EffectItemName.AnimateOpacity)
                        Container.AnimateFade<Shape>(Effect.fromValue, Effect.toValue, new Duration(TimeSpan.FromMilliseconds(Effect.Duration)));
                    if(Effect.AnimateName == EffectItemName.AnimateSize)
                        Container.AnimateSize<Shape>(Effect.fromValue, Effect.toValue, new Duration(TimeSpan.FromMilliseconds(Effect.Duration)));*/

        }
    }

    [Serializable]
    public class EffectItem
    {
        [XmlAttribute]
        public double Duration;
        [XmlAttribute]
        public double toValue;
        [XmlAttribute]
        public double fromValue;
        [XmlAttribute]
        public EffectItemName AnimateName;
    }

    public enum ItemShape
    {
        Ellipse,
        Rectangle
    }

    [Serializable]
    public enum EffectItemName
    {
        AnimateOpacity,
        AnimateSize,
    }

    [Serializable]
    public enum MapItemElement
    {
        AT_Unsigned = -1,
        AT_Normal = 0,
        AT_Champions,
        AT_Elite,
        AT_Minion, 
        AT_Unique,
        AT_Keywardens,
        AT_Goblin,
        AT_Boss,
        AT_Shrines, 
        AT_PowerPylons,
        AT_Objective,
        AT_Player,
        AT_PlayerPets,
        AT_Chests,
        AT_ResplendentChest,
        AT_LootableContainers,
        AT_GreaterRiftSoul,
        AT_RiftSoul,
        AT_HasBeenOperated,
        AT_LoreBooks,
        AT_PoolOfReflection,
        AT_Custom,
        AT_Portal
    }

  
}