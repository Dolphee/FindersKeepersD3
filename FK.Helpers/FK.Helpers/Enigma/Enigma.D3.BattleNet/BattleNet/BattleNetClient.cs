using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.D3.Collections;
using Enigma.D3.Memory;
using Enigma.D3.Enums;

namespace Enigma.D3.BattleNet
{
	[Version("2.1.0.26451")]
	public class BattleNetClient : MemoryObject
	{
		// TODO: 0x1090
		public const int SizeOf = 0x1078; // 4216 // 2492

        public int x0000_VTable { get { return Read<int>(0x0000); } }
		public Ptr<BattleNetSocial> x0004_Ptr_2888Bytes_BNetSocial { get { return ReadPointer<BattleNetSocial>(0x0004); } }
		public Ptr<BattleNetParty> x0008_Ptr_3040Bytes_BNetParty { get { return ReadPointer<BattleNetParty>(0x0008); } }
		public Ptr<BattleNetChat> x000C_Ptr_1728Bytes_BNetChat { get { return ReadPointer<BattleNetChat>(0x000C); } }
		public Ptr<BattleNetSettings> x0010_Ptr_672Bytes_BNetSettings { get { return ReadPointer<BattleNetSettings>(0x0010); } }
        public int x0014 { get { return Read<int>(0x0014); } }
        public Ptr<BattleNetGames> x0018_Ptr_2064Bytes_BNetGames { get { return ReadPointer<BattleNetGames>(0x0018); } }
        public Ptr x001C_Ptr_176Bytes { get { return ReadPointer(0x001C); } }
        public Ptr<BattleNetCommon> x0020_Ptr_76Bytes_BNetCommon { get { return ReadPointer<BattleNetCommon>(0x0020); } }
        public Ptr x0024_Ptr { get { return ReadPointer(0x0024); } }
        public Ptr x0028_Ptr_1376ytes { get { return ReadPointer(0x0028); } }
        public Ptr<BattleNetGroups> x002C_Ptr_2216Bytes_BNetGroups { get { return ReadPointer<BattleNetGroups>(0x002C); } }
        public int x0030 { get { return Read<int>(0x0030); } }
        public int x0034_Neg1 { get { return Read<int>(0x0034); } }
        public ListPack x0038_ListPack_ItemSize4 { get { return Read<ListPack>(0x0038); } } // 189F114C ListPack { Valid: True, ItemSize: 4, Count: 0}
        public ListPack x0068_ListPack_ItemSize12 { get { return Read<ListPack>(0x0068); } } // 189F117C ListPack { Valid: True, ItemSize: 12, Count: 0}
        public int x0098 { get { return Read<int>(0x0098); } }
        public Ptr<Struct0A0> x009C_Ptr { get { return ReadPointer<Struct0A0>(0x009C); } }
        public int x00A0_Ptr { get { return Read<int>(0x00A0); } }
        public Ptr<GameAccount> x00A4_GameAccount { get { return ReadPointer<GameAccount>(0x00A4); } }
        //GameAccount
        public Ptr<HeroData> x00A8_SelectedHeroData { get { return ReadPointer<HeroData>(0x00A8); } }
        public int x00AC { get { return Read<int>(0x00AC); } }
        public int x00B0 { get { return Read<int>(0x00B0); } }
        public int x00B4 { get { return Read<int>(0x00B4); } }
        public int x00B8 { get { return Read<int>(0x00B8); } }
        public int x00BC { get { return Read<int>(0x00BC); } }
        public int x00C0 { get { return Read<int>(0x00C0); } }
        public int x00C4_Neg1 { get { return Read<int>(0x00C4); } }
        public int x00C8 { get { return Read<int>(0x00C8); } }
        public int x00CC { get { return Read<int>(0x00CC); } }
        public int x00D0 { get { return Read<int>(0x00D0); } }
        public int x00D4 { get { return Read<int>(0x00D4); } }
        public int x00D8_StructStart { get { return Read<int>(0x00D8); } }
        public int x00DC_Ptr_28Bytes { get { return Read<int>(0x00DC); } }
        public int x00E0 { get { return Read<int>(0x00E0); } }
        public int _x00E4 { get { return Read<int>(0x00E4); } }
        public int x00E8 { get { return Read<int>(0x00E8); } }
        public int x00EC_StructStart { get { return Read<int>(0x00EC); } }
        public int x00F0_Ptr_28Bytes { get { return Read<int>(0x00F0); } }
        public int x00F4 { get { return Read<int>(0x00F4); } }
        public int _x00F8 { get { return Read<int>(0x00F8); } }
        public int x00FC { get { return Read<int>(0x00FC); } }
        public int x0100_StructStart { get { return Read<int>(0x0100); } }
        public int x0104_Ptr_28Bytes { get { return Read<int>(0x0104); } }
        public int x0108 { get { return Read<int>(0x0108); } }
        public int _x010C { get { return Read<int>(0x010C); } }
        public int x0110 { get { return Read<int>(0x0110); } }
        public int x0114_StructStart { get { return Read<int>(0x0114); } }
        public int x0118_Ptr_28Bytes { get { return Read<int>(0x0118); } }
        public int x011C { get { return Read<int>(0x011C); } }
        public int _x0120 { get { return Read<int>(0x0120); } }
        public int x0124 { get { return Read<int>(0x0124); } }
        public int x0128_StructStart { get { return Read<int>(0x0128); } }
        public int x012C_Ptr_28Bytes { get { return Read<int>(0x012C); } }
        public int x0130 { get { return Read<int>(0x0130); } }
        public int _x0134 { get { return Read<int>(0x0134); } }
        public int x0138 { get { return Read<int>(0x0138); } }
        public int x013C_StructStart { get { return Read<int>(0x013C); } }
        public int x0140_Ptr_28Bytes { get { return Read<int>(0x0140); } }
        public int x0144 { get { return Read<int>(0x0144); } }
        public int _x0148 { get { return Read<int>(0x0148); } }
        public int x014C { get { return Read<int>(0x014C); } }
        public int x0150_StructStart { get { return Read<int>(0x0150); } }
        public int x0154_Ptr_28Bytes { get { return Read<int>(0x0154); } }
        public int x0158 { get { return Read<int>(0x0158); } }
        public int _x015C { get { return Read<int>(0x015C); } }
        public int x0160 { get { return Read<int>(0x0160); } }
        public int x0164_StructStart { get { return Read<int>(0x0164); } }
        public int x0168_Ptr_28Bytes { get { return Read<int>(0x0168); } }
        public int x016C { get { return Read<int>(0x016C); } }
        public int _x0170 { get { return Read<int>(0x0170); } }
        public int x0174 { get { return Read<int>(0x0174); } }
        public int x0178_StructStart { get { return Read<int>(0x0178); } }
        public int x017C_Ptr_28Bytes { get { return Read<int>(0x017C); } }
        public int x0180 { get { return Read<int>(0x0180); } }
        public int _x0184 { get { return Read<int>(0x0184); } }
        public int x0188 { get { return Read<int>(0x0188); } }
        public int x018C_StructStart { get { return Read<int>(0x018C); } }
        public int x0190_Ptr_28Bytes { get { return Read<int>(0x0190); } }
        public int x0194 { get { return Read<int>(0x0194); } }
        public int _x0198 { get { return Read<int>(0x0198); } }
        public int x019C { get { return Read<int>(0x019C); } }
        public int x01A0_StructStart { get { return Read<int>(0x01A0); } }
        public int x01A4_Ptr_28Bytes { get { return Read<int>(0x01A4); } }
        public int x01A8 { get { return Read<int>(0x01A8); } }
        public int _x01AC { get { return Read<int>(0x01AC); } }
        public int x01B0 { get { return Read<int>(0x01B0); } }
        public int x01B4_StructStart { get { return Read<int>(0x01B4); } }
        public int x01B8_Ptr_28Bytes { get { return Read<int>(0x01B8); } }
        public int x01BC { get { return Read<int>(0x01BC); } }
        public int _x01C0 { get { return Read<int>(0x01C0); } }
        public int x01C4 { get { return Read<int>(0x01C4); } }
        public int x01C8_StructStart { get { return Read<int>(0x01C8); } }
        public int x01CC_Ptr_28Bytes { get { return Read<int>(0x01CC); } }
        public int x01D0 { get { return Read<int>(0x01D0); } }
        public int _x01D4 { get { return Read<int>(0x01D4); } }
        public int x01D8 { get { return Read<int>(0x01D8); } }
        public int x01DC_StructStart { get { return Read<int>(0x01DC); } }
        public int x01E0_Ptr_28Bytes { get { return Read<int>(0x01E0); } }
        public int x01E4 { get { return Read<int>(0x01E4); } }
        public int _x01E8 { get { return Read<int>(0x01E8); } }
        public int x01EC { get { return Read<int>(0x01EC); } }
        public int x01F0_StructStart { get { return Read<int>(0x01F0); } }
        public int x01F4_Ptr_28Bytes { get { return Read<int>(0x01F4); } }
        public int x01F8 { get { return Read<int>(0x01F8); } }
        public int _x01FC { get { return Read<int>(0x01FC); } }
        public int x0200 { get { return Read<int>(0x0200); } }
        public int x0204_StructStart { get { return Read<int>(0x0204); } }
        public int x0208_Ptr_28Bytes { get { return Read<int>(0x0208); } }
        public int x020C { get { return Read<int>(0x020C); } }
        public int _x0210 { get { return Read<int>(0x0210); } }
        public int x0214 { get { return Read<int>(0x0214); } }
        public int x0218_Ptr_552Bytes { get { return Read<int>(0x0218); } }
        public Ptr<BattleNetPlatform> x021C_Ptr_BattleNetPlatform { get { return ReadPointer<BattleNetPlatform>(0x021C); } }
        public int x0220_StructStart_Min_172Bytes { get { return Read<int>(0x0220); } }
        public int _x0224 { get { return Read<int>(0x0224); } }
        public int _x0228 { get { return Read<int>(0x0228); } }
        public ListPack x022C_ListPack_ItemSize4 { get { return Read<ListPack>(0x022C); } } // 189F133C ListPack { Valid: True, ItemSize: 4, Count: 66}
        public int _x025C { get { return Read<int>(0x025C); } } // 0
        public Map x0260_Map { get { return Read<Map>(0x0260); } }
        public int x02D0 { get { return Read<int>(0x02D0); } }
        public int x02D4 { get { return Read<int>(0x02D4); } }
        public int x02D8 { get { return Read<int>(0x02D8); } }
        public int x02DC { get { return Read<int>(0x02DC); } }
        public int x02E0 { get { return Read<int>(0x02E0); } }
        public int x02E4 { get { return Read<int>(0x02E4); } }
        /// public RefString x02E8_RefString_RealID { get { return Read<RefString>(0x02E8); } }
        // public RefString x02F4_RefString_Password { get { return Read<RefString>(0x02F4); } }
        public Ptr x0300_Ptr_ { get { return Read<Ptr>(0x0300); } } // 0x108E9440
        public Ptr x0304_Ptr_ { get { return Read<Ptr>(0x0304); } } // 0x10AF2090
        public int _x0308 { get { return Read<int>(0x0308); } } // 0
        public Ptr x030C_Ptr_ { get { return Read<Ptr>(0x030C); } } // 0x22410000
        public int _x0310 { get { return Read<int>(0x0310); } } // 0
        public int _x0314 { get { return Read<int>(0x0314); } } // 0
        public int _x0318 { get { return Read<int>(0x0318); } } // 0
        public Ptr x031C_Ptr_ { get { return Read<Ptr>(0x031C); } } // 0xCC35CBFF
        public int _x0320 { get { return Read<int>(0x0320); } } // 0
        public float x0324_float { get { return Read<float>(0x0324); } } // -481274.4
        public int _x0328 { get { return Read<int>(0x0328); } } // 0
        public Ptr x032C_Ptr_ { get { return Read<Ptr>(0x032C); } } // 0xF3530A56
        public Ptr x0330_Ptr_ { get { return Read<Ptr>(0x0330); } } // 0x6ED67D59
        public Ptr x0334_Ptr_ { get { return Read<Ptr>(0x0334); } } // 0x000501A1
        public int _x0338 { get { return Read<int>(0x0338); } } // 3
        public int _x033C { get { return Read<int>(0x033C); } } // 0
        public int _x0340 { get { return Read<int>(0x0340); } } // 0
        public int _x0344 { get { return Read<int>(0x0344); } } // 0
        public int _x0348 { get { return Read<int>(0x0348); } } // 0
        public int _x034C { get { return Read<int>(0x034C); } } // 0
        public int _x0350 { get { return Read<int>(0x0350); } } // 0
        public int _x0354 { get { return Read<int>(0x0354); } } // 0
        public int _x0358 { get { return Read<int>(0x0358); } } // 0
        public int _x035C { get { return Read<int>(0x035C); } } // 0
        public int _x0360 { get { return Read<int>(0x0360); } } // 0
        public int _x0364 { get { return Read<int>(0x0364); } } // 0
        public int _x0368 { get { return Read<int>(0x0368); } } // 0
        public int _x036C { get { return Read<int>(0x036C); } } // 0
        public int _x0370 { get { return Read<int>(0x0370); } } // 0
        public int _x0374 { get { return Read<int>(0x0374); } } // 0
        public int _x0378 { get { return Read<int>(0x0378); } } // 0
        public int _x037C { get { return Read<int>(0x037C); } } // 0
        public int _x0380 { get { return Read<int>(0x0380); } } // 0
        public int _x0384 { get { return Read<int>(0x0384); } } // 0
        public int _x0388 { get { return Read<int>(0x0388); } } // 0
        public int _x038C { get { return Read<int>(0x038C); } } // 0
        public int _x0390 { get { return Read<int>(0x0390); } } // 0
        public int _x0394 { get { return Read<int>(0x0394); } } // 0
        public int _x0398 { get { return Read<int>(0x0398); } } // 0
        public int _x039C { get { return Read<int>(0x039C); } } // 0
        public int _x03A0 { get { return Read<int>(0x03A0); } } // 0
        public int _x03A4 { get { return Read<int>(0x03A4); } } // 0
        public int _x03A8 { get { return Read<int>(0x03A8); } } // 0
        public int _x03AC { get { return Read<int>(0x03AC); } } // 0
        public int _x03B0 { get { return Read<int>(0x03B0); } } // 0
        public int _x03B4 { get { return Read<int>(0x03B4); } } // 0
        public int _x03B8 { get { return Read<int>(0x03B8); } } // 0
        public int _x03BC { get { return Read<int>(0x03BC); } } // 0
        public int _x03C0 { get { return Read<int>(0x03C0); } } // 0
        public int _x03C4 { get { return Read<int>(0x03C4); } } // 0
        public int _x03C8 { get { return Read<int>(0x03C8); } } // 0
        public int _x03CC { get { return Read<int>(0x03CC); } } // 0
        public int _x03D0 { get { return Read<int>(0x03D0); } } // 0
        public int _x03D4 { get { return Read<int>(0x03D4); } } // 0
        public int _x03D8 { get { return Read<int>(0x03D8); } } // 0
        public int _x03DC { get { return Read<int>(0x03DC); } } // 0
        public int _x03E0 { get { return Read<int>(0x03E0); } } // 0
        public int _x03E4 { get { return Read<int>(0x03E4); } } // 0
        public int _x03E8 { get { return Read<int>(0x03E8); } } // 0
        public int _x03EC { get { return Read<int>(0x03EC); } } // 0
        public int _x03F0 { get { return Read<int>(0x03F0); } } // 0
        public int _x03F4 { get { return Read<int>(0x03F4); } } // 0
        public int _x03F8 { get { return Read<int>(0x03F8); } } // 0
        public int _x03FC { get { return Read<int>(0x03FC); } } // 0
        public int _x0400 { get { return Read<int>(0x0400); } } // 0
        public int _x0404 { get { return Read<int>(0x0404); } } // 0
        public int _x0408 { get { return Read<int>(0x0408); } } // 0
        public int _x040C { get { return Read<int>(0x040C); } } // 0
        public int _x0410 { get { return Read<int>(0x0410); } } // 0
        public int _x0414 { get { return Read<int>(0x0414); } } // 0
        public int _x0418 { get { return Read<int>(0x0418); } } // 0
        public int _x041C { get { return Read<int>(0x041C); } } // 0
        public int _x0420 { get { return Read<int>(0x0420); } } // 0
        public int _x0424 { get { return Read<int>(0x0424); } } // 0
        public int _x0428 { get { return Read<int>(0x0428); } } // 0
        public int _x042C { get { return Read<int>(0x042C); } } // 0
        public int _x0430 { get { return Read<int>(0x0430); } } // 0
        public int _x0434 { get { return Read<int>(0x0434); } } // 0
        public int _x0438 { get { return Read<int>(0x0438); } } // 0
        public int _x043C { get { return Read<int>(0x043C); } } // 0
        public int _x0440 { get { return Read<int>(0x0440); } } // 0
        public int _x0444 { get { return Read<int>(0x0444); } } // 0
        public int _x0448 { get { return Read<int>(0x0448); } } // 0
        public int _x044C { get { return Read<int>(0x044C); } } // 0
        public int _x0450 { get { return Read<int>(0x0450); } } // 0
        public int _x0454 { get { return Read<int>(0x0454); } } // 0
        public int _x0458 { get { return Read<int>(0x0458); } } // 0
        public int _x045C { get { return Read<int>(0x045C); } } // 0
        public int _x0460 { get { return Read<int>(0x0460); } } // 0
        public int _x0464 { get { return Read<int>(0x0464); } } // 0
        public int _x0468 { get { return Read<int>(0x0468); } } // 0
        public int _x046C { get { return Read<int>(0x046C); } } // 0
        public int _x0470 { get { return Read<int>(0x0470); } } // 0
        public int _x0474 { get { return Read<int>(0x0474); } } // 0
        public int _x0478 { get { return Read<int>(0x0478); } } // 0
        public int _x047C { get { return Read<int>(0x047C); } } // 0
        public int _x0480 { get { return Read<int>(0x0480); } } // 0
        public int _x0484 { get { return Read<int>(0x0484); } } // 0
        public int _x0488 { get { return Read<int>(0x0488); } } // 0
        public int _x048C { get { return Read<int>(0x048C); } } // 0
        public int _x0490 { get { return Read<int>(0x0490); } } // 0
        public int _x0494 { get { return Read<int>(0x0494); } } // 0
        public int _x0498 { get { return Read<int>(0x0498); } } // 0
        public int _x049C { get { return Read<int>(0x049C); } } // 0
        public int _x04A0 { get { return Read<int>(0x04A0); } } // 0
        public int _x04A4 { get { return Read<int>(0x04A4); } } // 0
        public int _x04A8 { get { return Read<int>(0x04A8); } } // 0
        public int _x04AC { get { return Read<int>(0x04AC); } } // 0
        public int _x04B0 { get { return Read<int>(0x04B0); } } // 0
        public int _x04B4 { get { return Read<int>(0x04B4); } } // 0
        public int _x04B8 { get { return Read<int>(0x04B8); } } // 0
        public int _x04BC { get { return Read<int>(0x04BC); } } // 0
        public int _x04C0 { get { return Read<int>(0x04C0); } } // 0
        public int _x04C4 { get { return Read<int>(0x04C4); } } // 0
        public int _x04C8 { get { return Read<int>(0x04C8); } } // 0
        public int _x04CC { get { return Read<int>(0x04CC); } } // 0
        public int _x04D0 { get { return Read<int>(0x04D0); } } // 0
        public int _x04D4 { get { return Read<int>(0x04D4); } } // 0
        public int _x04D8 { get { return Read<int>(0x04D8); } } // 0
        public int _x04DC { get { return Read<int>(0x04DC); } } // 0
        public int _x04E0 { get { return Read<int>(0x04E0); } } // 0
        public int _x04E4 { get { return Read<int>(0x04E4); } } // 0
        public int _x04E8 { get { return Read<int>(0x04E8); } } // 0
        public int _x04EC { get { return Read<int>(0x04EC); } } // 0
        public int _x04F0 { get { return Read<int>(0x04F0); } } // 0
        public int _x04F4 { get { return Read<int>(0x04F4); } } // 0
        public int _x04F8 { get { return Read<int>(0x04F8); } } // 0
        public int _x04FC { get { return Read<int>(0x04FC); } } // 0
        public int _x0500 { get { return Read<int>(0x0500); } } // 0
        public int _x0504 { get { return Read<int>(0x0504); } } // 0
        public int _x0508 { get { return Read<int>(0x0508); } } // 0
        public int _x050C { get { return Read<int>(0x050C); } } // 0
        public int _x0510 { get { return Read<int>(0x0510); } } // 0
        public int _x0514 { get { return Read<int>(0x0514); } } // 0
        public int _x0518 { get { return Read<int>(0x0518); } } // 0
        public int _x051C { get { return Read<int>(0x051C); } } // 0
        public int _x0520 { get { return Read<int>(0x0520); } } // 0
        public int _x0524 { get { return Read<int>(0x0524); } } // 0
        public int _x0528 { get { return Read<int>(0x0528); } } // 0
        public int _x052C { get { return Read<int>(0x052C); } } // 0
        public int _x0530 { get { return Read<int>(0x0530); } } // 0
        public int _x0534 { get { return Read<int>(0x0534); } } // 0
        public int _x0538 { get { return Read<int>(0x0538); } } // 0
        public int _x053C { get { return Read<int>(0x053C); } } // 0
        public int _x0540 { get { return Read<int>(0x0540); } } // 0
        public int _x0544 { get { return Read<int>(0x0544); } } // 0
        public int _x0548 { get { return Read<int>(0x0548); } } // 0
        public int _x054C { get { return Read<int>(0x054C); } } // 0
        public int _x0550 { get { return Read<int>(0x0550); } } // 0
        public int _x0554 { get { return Read<int>(0x0554); } } // 0
        public int _x0558 { get { return Read<int>(0x0558); } } // 0
        public int _x055C { get { return Read<int>(0x055C); } } // 0
        public int _x0560 { get { return Read<int>(0x0560); } } // 0
        public int _x0564 { get { return Read<int>(0x0564); } } // 0
        public int _x0568 { get { return Read<int>(0x0568); } } // 0
        public int _x056C { get { return Read<int>(0x056C); } } // 0
        public int _x0570 { get { return Read<int>(0x0570); } } // 0
        public int _x0574 { get { return Read<int>(0x0574); } } // 0
        public int _x0578 { get { return Read<int>(0x0578); } } // 0
        public int _x057C { get { return Read<int>(0x057C); } } // 0
        public int _x0580 { get { return Read<int>(0x0580); } } // 0
        public int _x0584 { get { return Read<int>(0x0584); } } // 0
        public int _x0588 { get { return Read<int>(0x0588); } } // 0
        public int _x058C { get { return Read<int>(0x058C); } } // 0
        public int _x0590 { get { return Read<int>(0x0590); } } // 0
        public int _x0594 { get { return Read<int>(0x0594); } } // 0
        public int _x0598 { get { return Read<int>(0x0598); } } // 0
        public int _x059C { get { return Read<int>(0x059C); } } // 0
        public int _x05A0 { get { return Read<int>(0x05A0); } } // 0
        public int _x05A4 { get { return Read<int>(0x05A4); } } // 0
        public int _x05A8 { get { return Read<int>(0x05A8); } } // 0
        public int _x05AC { get { return Read<int>(0x05AC); } } // 0
        public int _x05B0 { get { return Read<int>(0x05B0); } } // 0
        public int _x05B4 { get { return Read<int>(0x05B4); } } // 0
        public int _x05B8 { get { return Read<int>(0x05B8); } } // 0
        public int _x05BC { get { return Read<int>(0x05BC); } } // 0
        public int _x05C0 { get { return Read<int>(0x05C0); } } // 0
        public int _x05C4 { get { return Read<int>(0x05C4); } } // 0
        public int _x05C8 { get { return Read<int>(0x05C8); } } // 0
        public int _x05CC { get { return Read<int>(0x05CC); } } // 0
        public int _x05D0 { get { return Read<int>(0x05D0); } } // 0
        public int _x05D4 { get { return Read<int>(0x05D4); } } // 0
        public int _x05D8 { get { return Read<int>(0x05D8); } } // 0
        public int _x05DC { get { return Read<int>(0x05DC); } } // 0
        public int _x05E0 { get { return Read<int>(0x05E0); } } // 0
        public int _x05E4 { get { return Read<int>(0x05E4); } } // 0
        public int _x05E8 { get { return Read<int>(0x05E8); } } // 0
        public int _x05EC { get { return Read<int>(0x05EC); } } // 0
        public int _x05F0 { get { return Read<int>(0x05F0); } } // 0
        public int _x05F4 { get { return Read<int>(0x05F4); } } // 0
        public int _x05F8 { get { return Read<int>(0x05F8); } } // 0
        public int _x05FC { get { return Read<int>(0x05FC); } } // 0
        public int _x0600 { get { return Read<int>(0x0600); } } // 0
        public int _x0604 { get { return Read<int>(0x0604); } } // 0
        public int _x0608 { get { return Read<int>(0x0608); } } // 0
        public int _x060C { get { return Read<int>(0x060C); } } // 0
        public int _x0610 { get { return Read<int>(0x0610); } } // 0
        public int _x0614 { get { return Read<int>(0x0614); } } // 0
        public float x0618_float { get { return Read<float>(0x0618); } } // 0.002343834
        public int _x061C { get { return Read<int>(0x061C); } } // 0
        public int _x0620 { get { return Read<int>(0x0620); } } // 0
        public int _x0624 { get { return Read<int>(0x0624); } } // 0
        public int _x0628 { get { return Read<int>(0x0628); } } // 0
        public int _x062C { get { return Read<int>(0x062C); } } // 0
        public int _x0630 { get { return Read<int>(0x0630); } } // 0
        public int _x0634 { get { return Read<int>(0x0634); } } // 0
        public int _x0638 { get { return Read<int>(0x0638); } } // 0
        public int _x063C { get { return Read<int>(0x063C); } } // 0
        public int _x0640 { get { return Read<int>(0x0640); } } // 0
        public int _x0644 { get { return Read<int>(0x0644); } } // 0
        public int _x0648 { get { return Read<int>(0x0648); } } // 0
        public int _x064C { get { return Read<int>(0x064C); } } // 0
        public int _x0650 { get { return Read<int>(0x0650); } } // 0
        public int _x0654 { get { return Read<int>(0x0654); } } // 0
        public int _x0658 { get { return Read<int>(0x0658); } } // -27
        public int x065C_ActorSnoId { get { return Read<int>(0x065C); } } // 401 (Actor: NPC_Human_Male)
        public int _x0660 { get { return Read<int>(0x0660); } } // 0
        public int _x0664 { get { return Read<int>(0x0664); } } // 0
        public Item[] x0668_Array_Count4_ItemSize260 { get { return Read<Item>(0x0668, 4); } }
        public Ptr x0A78_Ptr_ { get { return Read<Ptr>(0x0A78); } } // 0xD52B61D0
        public Ptr x0A7C_Ptr_ { get { return Read<Ptr>(0x0A7C); } } // 0x066B0DDF
        public int _x0A80 { get { return Read<int>(0x0A80); } } // 0
        public int _x0A84 { get { return Read<int>(0x0A84); } } // 0
        public Ptr x0A88_Ptr_ { get { return Read<Ptr>(0x0A88); } } // 0x10A331A0
        public int _x0A8C { get { return Read<int>(0x0A8C); } } // 0
        public int _x0A90 { get { return Read<int>(0x0A90); } } // 0
        public int _x0A94 { get { return Read<int>(0x0A94); } } // 0
        public int _x0A98 { get { return Read<int>(0x0A98); } } // 0
        public int _x0A9C { get { return Read<int>(0x0A9C); } } // 0
        public Map x0AA0_Map { get { return Read<Map>(0x0AA0); } } // { count = 0, entrySize = 12 }
        public Map x0B10_Map { get { return Read<Map>(0x0B10); } } // { count = 0, entrySize = 12 }
        public Map x0B80_Map { get { return Read<Map>(0x0B80); } } // { count = 0, entrySize = 12 }
        public Map x0BF0_Map { get { return Read<Map>(0x0BF0); } } // { count = 0, entrySize = 12 }
        public Map x0C60_Map { get { return Read<Map>(0x0C60); } } // { count = 0, entrySize = 12 }
        public Map x0CD0_Map { get { return Read<Map>(0x0CD0); } } // { count = 0, entrySize = 12 }
        public Map x0D40_Map { get { return Read<Map>(0x0D40); } } // { count = 0, entrySize = 40 }
        public Map x0DB0_Map { get { return Read<Map>(0x0DB0); } } // { count = 0, entrySize = 40 }
        public Map x0E20_Map { get { return Read<Map>(0x0E20); } } // { count = 0, entrySize = 40 }
        public Map x0E90_Map { get { return Read<Map>(0x0E90); } } // { count = 0, entrySize = 40 }
        public Map x0F00_Map { get { return Read<Map>(0x0F00); } } // { count = 0, entrySize = 32 }
        public Map x0F70_Map { get { return Read<Map>(0x0F70); } } // { count = 0, entrySize = 32 }
        public Ptr x0FE0_Ptr_ { get { return Read<Ptr>(0x0FE0); } } // 0x209AFFFF
        public Ptr x0FE4_Ptr_ { get { return Read<Ptr>(0x0FE4); } } // 0x1022CBD0
        public int _x0FE8 { get { return Read<int>(0x0FE8); } } // 20
        public float x0FEC_float { get { return Read<float>(0x0FEC); } } // 125.881
        public Ptr x0FF0_Ptr_ { get { return Read<Ptr>(0x0FF0); } } // 0x117B8CC0
        public Ptr x0FF4_Ptr_ { get { return Read<Ptr>(0x0FF4); } } // 0x117B8EC0
        public Ptr x0FF8_Ptr_ { get { return Read<Ptr>(0x0FF8); } } // 0x117B8EC0
        public Ptr x0FFC_Ptr_ { get { return Read<Ptr>(0x0FFC); } } // 0x99726DD9
        public int _x1000 { get { return Read<int>(0x1000); } } // 63
        public int _x1004 { get { return Read<int>(0x1004); } } // 64
        public float x1008_float { get { return Read<float>(0x1008); } } // 1
        public Ptr x100C_Ptr_ { get { return Read<Ptr>(0x100C); } } // 0xDDAF3652
        public Map x1010_Map { get { return Read<Map>(0x1010); } } // { count = 15, entrySize = 40 }

        [Version("2.1.0.26451")]
		public class Item : MemoryObject
		{
			public const int SizeOf = 0x104; // 260

			public int x000 { get { return Read<int>(0x000); } }
			public int x004 { get { return Read<int>(0x004); } }
			public int x008 { get { return Read<int>(0x008); } }
			public int x00C { get { return Read<int>(0x00C); } }
			public int x010 { get { return Read<int>(0x010); } }
			public int x014 { get { return Read<int>(0x014); } }
			public int x018 { get { return Read<int>(0x018); } }
			public int x01C { get { return Read<int>(0x01C); } }
			public int x020 { get { return Read<int>(0x020); } }
			public int x024 { get { return Read<int>(0x024); } }
			public int x028 { get { return Read<int>(0x028); } }
			public int x02C { get { return Read<int>(0x02C); } }
			public int x030 { get { return Read<int>(0x030); } }
			public int x034 { get { return Read<int>(0x034); } }
			public int x038 { get { return Read<int>(0x038); } }
			public int x03C { get { return Read<int>(0x03C); } }
			public int x040 { get { return Read<int>(0x040); } }
			public int x044 { get { return Read<int>(0x044); } }
			public int x048 { get { return Read<int>(0x048); } }
			public int x04C { get { return Read<int>(0x04C); } }
			public int x050 { get { return Read<int>(0x050); } }
			public int x054 { get { return Read<int>(0x054); } }
			public int x058 { get { return Read<int>(0x058); } }
			public int x05C { get { return Read<int>(0x05C); } }
			public int x060 { get { return Read<int>(0x060); } }
			public int x064 { get { return Read<int>(0x064); } }
			public int x068_StructStart_72Bytes { get { return Read<int>(0x068); } }
			public int _x06C { get { return Read<int>(0x06C); } }
			public int _x070 { get { return Read<int>(0x070); } }
			public int _x074 { get { return Read<int>(0x074); } }
			public int _x078 { get { return Read<int>(0x078); } }
			public int _x07C { get { return Read<int>(0x07C); } }
			public int _x080 { get { return Read<int>(0x080); } }
			public int _x084 { get { return Read<int>(0x084); } }
			public int _x088 { get { return Read<int>(0x088); } }
			public int _x08C { get { return Read<int>(0x08C); } }
			public int _x090 { get { return Read<int>(0x090); } }
			public int _x094 { get { return Read<int>(0x094); } }
			public int _x098 { get { return Read<int>(0x098); } }
			public int _x09C { get { return Read<int>(0x09C); } }
			public int _x0A0 { get { return Read<int>(0x0A0); } }
			public int _x0A4 { get { return Read<int>(0x0A4); } }
			public int _x0A8 { get { return Read<int>(0x0A8); } }
			public int _x0AC { get { return Read<int>(0x0AC); } }
			public int x0B0_StructStart_72Bytes { get { return Read<int>(0x0B0); } }
			public int _x0B4 { get { return Read<int>(0x0B4); } }
			public int _x0B8 { get { return Read<int>(0x0B8); } }
			public int _x0BC { get { return Read<int>(0x0BC); } }
			public int _x0C0 { get { return Read<int>(0x0C0); } }
			public int _x0C4 { get { return Read<int>(0x0C4); } }
			public int _x0C8 { get { return Read<int>(0x0C8); } }
			public int _x0CC { get { return Read<int>(0x0CC); } }
			public int _x0D0 { get { return Read<int>(0x0D0); } }
			public int _x0D4 { get { return Read<int>(0x0D4); } }
			public int _x0D8 { get { return Read<int>(0x0D8); } }
			public int _x0DC { get { return Read<int>(0x0DC); } }
			public int _x0E0 { get { return Read<int>(0x0E0); } }
			public int _x0E4 { get { return Read<int>(0x0E4); } }
			public int _x0E8 { get { return Read<int>(0x0E8); } }
			public int _x0EC { get { return Read<int>(0x0EC); } }
			public int _x0F0 { get { return Read<int>(0x0F0); } }
			public int _x0F4 { get { return Read<int>(0x0F4); } }
			public int x0F8 { get { return Read<int>(0x0F8); } }
			public int x0FC { get { return Read<int>(0x0FC); } }
			public int x100 { get { return Read<int>(0x100); } }
		}

		public class Struct0A0 : MemoryObject
		{
			// 2.0.6.24641
			public const int SizeOf = 0x78;

			public int x00_VTable { get { return Read<int>(0x00); } }
			public int x04_UISnoId { get { return Read<int>(0x04); } }
			public int x08 { get { return Read<int>(0x08); } }
			public int _x0C { get { return Read<int>(0x0C); } }
			public int x10 { get { return Read<int>(0x10); } }
			public int _x14 { get { return Read<int>(0x14); } }
			public RefString x18_Name { get { return Read<RefString>(0x18); } }
			public RefString x24_RefString { get { return Read<RefString>(0x24); } }
			public RefString x30_RID { get { return Read<RefString>(0x30); } }
			public Ptr<Vector> x3C_Ptr_Vector { get { return ReadPointer<Vector>(0x3C); } }
			public int x40_StructStart_Min8Bytes { get { return Read<int>(0x40); } }
			public int _x44 { get { return Read<int>(0x44); } }
			public int _x48 { get { return Read<int>(0x48); } }
			public int x4C_TextureSnoId_Logo { get { return Read<int>(0x4C); } }
			public RefString x50_Country { get { return Read<RefString>(0x50); } }
			public int x5C_TextureSnoId_Footer { get { return Read<int>(0x5C); } }
			public int x60 { get { return Read<int>(0x60); } }
			public int x64 { get { return Read<int>(0x64); } }
			public int x68 { get { return Read<int>(0x68); } }
			public int x6C { get { return Read<int>(0x6C); } }
			public int x70 { get { return Read<int>(0x70); } }
			public int _x74 { get { return Read<int>(0x74); } }


			public string x00_ContentLevel
			{
				get
				{
					uint result = 0;
					int start = x40_StructStart_Min8Bytes;
					int stop = _x44;
					int count = (stop - start) / 4;
					if (count > 0)
					{
						int[] buffer = Memory.Reader.Read<int>(start, count);
						for (int i = 0; i < buffer.Length; i++)
						{
							switch (buffer[i])
							{
								case 0x106:
									result |= 0x10u;
									break;
								case 0x107:
									result |= 2u;
									break;
								case 0x108:
									result |= 0xAu;
									break;
								case 0xA7:
									result |= 0x20u;
									break;
								case 0xA8:
									result |= 1u;
									break;
								case 0xA9:
									result |= 5u;
									break;
								case 0xDC:
									result |= 0x40u;
									break;
								default:
									break;
							}
						}
					}

					string level = "StarterEdition"; // 0
					if ((result & 1) != 0)
						level = "1.0 Vanilla"; // 1
					if ((result & 0x10) != 0 && Memory.Reader.Read<int>(0x01CD2550 + 0x50) > 0)
						level = "2.0 X1"; // 2
					if ((result & 2) != 0 && Memory.Reader.Read<int>(0x01CD2550 + 0x50) == 0)
						level = "2.0 X1"; // 2
					return level;
				}
			}
		}
	}
}
