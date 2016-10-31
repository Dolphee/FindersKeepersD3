using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Enigma.D3;
using Enigma.D3.Enums;

namespace FindersKeepers
{
    [Serializable()]
    public class Heroes : SetDefault
    {
        public object _DEFAULT(){
            return new object();
        }

        public string HeroName { get; set; }
        public Enigma.D3.Enums.HeroClass HeroClass { get; set; }
        public HeroGenders HeroGender { get; set; }
        public Arena PlayArena { get; set; }
        public Season Seasonal { get; set; }
        public int ACDID { get; set; }

        public enum Season
        {
            NonSeason,
            Season
        }

        public enum Arena
        {
            Softcore,
            Hardcore
        }

        public enum HeroGenders
        {
            Male,
            Female
        }
    }

    public static class HeroReader 
    {
        public static Heroes.HeroGenders GetGender
        {
            get
            {
                return (Heroes.HeroGenders)((Engine.Current.Memory.Reader.ReadChain<int>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x98) >> 1) & 1);
            }
        }

        public static HeroClass GetHeroClass
        {
            get
            {
                return Engine.Current.Memory.Reader.ReadChain<Enigma.D3.Enums.HeroClass>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x4C);
            }
        }

        public static Heroes.Arena GetHeroArena
        {
            get
            {
                return (Heroes.Arena)(Engine.Current.Memory.Reader.ReadChain<int>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x98) & 1);
            }
        }

        public static Heroes.Season GetSeasonal
        {
            get
            {
                return (Engine.Current.Memory.Reader.ReadChain<int>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x6C) != OffsetConversion.ActiveSeason) ? Heroes.Season.NonSeason : Heroes.Season.Season;
            }
        }

        public static RefString GetHeroName
        {
            get
            {
                return Engine.Current.Memory.Reader.ReadChain<RefString>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x38);
            }
        }

        public static EntityId GetHeroId
        {
            get
            {
                return Engine.Current.Memory.Reader.ReadChain<EntityId>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x28);
            }
        }


        public static RefString GetBattleTag
        {
            get
            {
                return Engine.Current.Memory.Reader.ReadChain<RefString>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes, OffsetConversion.HeroesData + 0x18);
            }
        }

    }

}
