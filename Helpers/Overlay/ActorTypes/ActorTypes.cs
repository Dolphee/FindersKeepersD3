using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;
using Enigma.D3;

namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public class ActorTypes
    {
        public Action<ActorCommonData> Monsters = new Action<ActorCommonData>((e) =>
        {
            //if ((Actor.IsTreasureGoblin()) && Config._.FKMinimap.Allowed.Contains((int)MapItemElement.AT_Goblin))
                //GameManager.Instance.GManager.GList.MainAccount.LevelAreaContainer.Monsters.Add(new IMap.IMapActor { Point = IMap.GetPoint(Actor), Type = MapItemElement.AT_Goblin });

        });


        public void CheckForMosnters()
        {

            Monsters.Invoke(new ActorCommonData());
        }

    }
}
