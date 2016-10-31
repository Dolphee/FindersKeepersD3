using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindersKeepers.Controller.Pickit
{
    [Serializable()]
    public class Attributes
    {
        public class ItemTypes
        {
            [XmlAttribute]
            public ItemType Item  {get;set;}
            [XmlElement]
            public PickitAttributes Pickit {get;set;}
        }

        public class ItemSorted
        {
            public uint PickitLimit {get;set;}
            public string Name {get;set;}
        }

        public class PickitAttributes
        {
            [XmlAttribute]
            public int AllResistance {get ; set;}
            [XmlAttribute]
            public int LightningResistance { get; set; }
            [XmlAttribute]
            public int FireResistance { get; set; }
            [XmlAttribute]
            public int ArcaneResistance { get; set; }
            [XmlAttribute]
            public int PsysicalResistance { get; set; }
            [XmlAttribute]
            public int ColdResistance { get; set; }
            [XmlAttribute]
            public int Armor { get; set; }
            [XmlAttribute]
            public int Vitality { get; set; }
            [XmlAttribute]
            public int Life { get; set; }
            [XmlAttribute]
            public int HealthGlobes { get; set; }
            [XmlAttribute]
            public int LifeOnHit { get; set; }
            [XmlAttribute]
            public int LifeAfterKill { get; set; }
            [XmlAttribute]
            public int Strength { get; set; }
            [XmlAttribute]
            public int Dexterity { get; set; }
            [XmlAttribute]
            public int Intelligence { get; set; }
            [XmlAttribute]
            public double Attackspeed { get; set; }
            [XmlAttribute]
            public double Critchance { get; set; }
            [XmlAttribute]
            public int CritDamage { get; set; }
            [XmlAttribute]
            public int MovementSpeed { get; set; }
            [XmlAttribute]
            public int ExperienceKill { get; set; }
            [XmlAttribute]
            public int PickupRadius { get; set; }
            [XmlAttribute]
            public int GoldFind { get; set; }
            [XmlAttribute]
            public int MagicFind { get; set; }
            [XmlAttribute]
            public double ReduceCost { get; set; }
            [XmlAttribute]
            public double CooldownReduction { get; set; }
        }

        public enum ItemType : int
        {
            Invalid = -1,
            Amulet = -365243096,
            Axe = 109694,
            Axe2H = 119458520,
            Belt = 3635495,
            Gift = 669302080,
            Belt_Barbarian = -479768568,
            Belt_Crusader = -1029603201,
            Belt_DemonHunter = 9087215,
            Belt_Monk = 1555631483,
            Belt_WitchDoctor = 994235600,
            Belt_Wizard = -2044734313,
            Boots = 120334087,
            Boots_Barbarian = -2097752600,
            Boots_Crusader = -1989686689,
            Boots_DemonHunter = -1038932273,
            Boots_Monk = 1931831131,
            Boots_WitchDoctor = -53783888,
            Boots_Wizard = -385210761,
            Bow = 110504,
            Bracers = -1999984446,
            Bracers_Barbarian = 1143143779,
            Bracers_Crusader = 60780154,
            Bracers_DemonHunter = 2129074442,
            Bracers_Monk = 1160852982,
            Bracers_WitchDoctor = -1180744469,
            Bracers_Wizard = 1833174994,
            CeremonialDagger = -199811863,
            ChestArmor = -1028103400,
            ChestArmor_Barbarian = -1289348295,
            ChestArmor_Crusader = -1054135920,
            ChestArmor_DemonHunter = -1154939808,
            ChestArmor_Monk = 1667159564,
            ChestArmor_WitchDoctor = -169791423,
            ChestArmor_Wizard = -849738392,
            Cloak = 121411562,
            CombatStaff = -1620551894,
            Crossbow = -1338851342,
            CrusaderShield = 602099538,
            Dagger = -262576534,
            DemonicKey = -1979915768,
            DemonicOrgan = -49494186,
            EnchantressSpecial = -464307745,
            FistWeapon = -2094596416,
            Flail1H = -1363671135,
            Flail2H = -1363671102,
            FollowerSpecial = 1637769035,
            GenericBelt = -948083356,
            GenericBowWeapon = 395678127,
            GenericChestArmor = 828360981,
            GenericHelm = -947867741,
            GenericOffHand = 344906995,
            GenericRangedWeapon = 165564792,
            GenericSwingWeapon = 1846932879,
            GenericThrustWeapon = 998499313,
            Gloves = -131821392,
            Gloves_Barbarian = 444212945,
            Gloves_Crusader = 299901480,
            Gloves_DemonHunter = 1202607608,
            Gloves_Monk = 922698404,
            Gloves_WitchDoctor = -2107211303,
            Gloves_Wizard = 180877312,
            HandXbow = 763102523,
            HealthGlyph = 1883419642,
            HealthPotion = -1916071921,
            Helm = 3851110,
            Helm_Barbarian = -1587563257,
            Helm_Crusader = -2104376930,
            Helm_DemonHunter = 506481070,
            Helm_Monk = 122656538,
            Helm_WitchDoctor = 1491629455,
            Helm_Wizard = 813646326,
            Legs = 3994699,
            Legs_Barbarian = -1177810900,
            Legs_Crusader = 1031652387,
            Legs_DemonHunter = 50199059,
            Legs_Monk = 1717378847,
            Legs_WitchDoctor = 1035347444,
            Legs_Wizard = -1995514053,
            Mace = 4026134,
            Mace2H = 89494384,
            MightyWeapon1H = -1488678091,
            MightyWeapon2H = -1488678058,
            Mojo = 4041621,
            MysteryWeapon1H = -241705760,
            MysteryWeapon2H = -241705727,
            Orb = 124739,
            Polearm = -1203595600,
            Quiver = 269990204,
            Ring = 4214896,
            ScoundrelSpecial = -953512528,
            Shield = 332825721,
            Shoulders = -940830407,
            Shoulders_Barbarian = 1212065434,
            Shoulders_Crusader = 62868689,
            Shoulders_DemonHunter = -124654591,
            Shoulders_Monk = -821460787,
            Shoulders_WitchDoctor = 860493794,
            Shoulders_Wizard = -832936855,
            Spear = 140519163,
            SpiritStone_Monk = 576647032,
            Staff = 140658708,
            Sword = 140782159,
            Sword2H = -1307049751,
            TemplarSpecial = 129668150,
            Wand = 4385866,
            Wizard_Hat = -1499089042,
            VoodooMask = -333341566
        }
    }
}
