using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using System.Windows.Media.Media3D;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.DebugWorker;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers
{
    public static class DiabloIII
    {
        public static Point3D Point(this ActorCommonData RActor)
        {
            return new Point3D{ X = RActor.x0D0_WorldPosX, Y = RActor.x0D4_WorldPosY, Z = RActor.x0D8_WorldPosZ };
        }

        public static Point3D Point(this Actor RActor)
        {
            return new Point3D { X = RActor.x0B8_CollisionX, Y = RActor.x0BC_CollisionY, Z = RActor.x0C0_CollisionZ };
        }

        public static System.Windows.Point GetCord(this ActorCommonData Player, double x, double y, double width, double height)
        {
            return new System.Windows.Point { X = Player.x0D0_WorldPosX - ((x - width) / 2), Y = Player.x0D4_WorldPosY - ((y - height) / 2) };
        }

        public static ActorCommonData Player(Engine Engine = null)
        {
            try
            {
                Engine = (Engine != null) ? Engine : Engine.Current;

                int PlayerIndex = Engine.ObjectManager.Player.Dereference().x00000_LocalDataIndex;
                PlayerData PlayerData = Engine.ObjectManager.Storage.PlayerDataManager.Dereference().x0038_Items[PlayerIndex];
                
                return Engine.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData[(short)PlayerData.x0004_AcdId];
            }

            catch { }

            return null;
        }

        public static ActorCommonData GetPlayer(this Enigma.D3.Collections.ExpandableContainer<ActorCommonData> Container)
        {
            try
            {
                int Index = GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Player.Dereference().x00000_LocalDataIndex;
                return Container[(short)GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Storage.PlayerDataManager.Dereference().x0038_Items[Index].x0004_AcdId];

            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }

            return default(ActorCommonData);
        }

        public static ActorCommonData GetPlayerFromIndex(short index)
        {
            if (index == -1)
                return null;

            try
            {
               return Engine.Current.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData[(short)index];
            }

            catch { }

            return null;
        }

        public static short GetPlayerIndex(Engine Engine)
        {
            int PlayerIndex = Engine.ObjectManager.Player.Dereference().x00000_LocalDataIndex;
            return (short)Engine.ObjectManager.Storage.PlayerDataManager.Dereference().x0038_Items[PlayerIndex].x0004_AcdId;
        }

        public static PlayerData PlayerData()
        {
            int PlayerIndex = Engine.Current.ObjectManager.Player.Dereference().x00000_LocalDataIndex;
            return Engine.Current.ObjectManager.Storage.PlayerDataManager.Dereference().x0038_Items[PlayerIndex];
        }


        public static double Width = 1920;
        public static double Height = 1080;

        public static System.Windows.Point FromD3toScreenCoords(System.Windows.Media.Media3D.Point3D currentCharGameLoc, System.Windows.Media.Media3D.Point3D objectGameLocation)
        {
            double xd = objectGameLocation.X - currentCharGameLoc.X;
            double yd = objectGameLocation.Y - currentCharGameLoc.Y;
            double zd = objectGameLocation.Z - currentCharGameLoc.Z;

            double w = -0.515 * xd + -0.514 * yd + -0.686 * zd + 97.985;
            double X = (-1.682 * xd + 1.683 * yd + 0 * zd + 7.045e-3) / w;
            double Y = (-1.54 * xd + -1.539 * yd + 2.307 * zd + 6.161) / w;
            //   float num7 = ((((-0.515f * num) + (-0.514f * num2)) + (-0.686f * num3)) + 97.002f) / num4;

            double aspectChange = (double)((double)Width / (double)Height) / (double)(4.0f / 3.0f); // 4:3 = default aspect ratio

            X /= aspectChange;

            float rX = (float)((X + 1) / 2 * Width);
            float rY = (float)((1 - Y) / 2 * Height);

            return new System.Windows.Point((int)rX, (int)rY);
        }
    }
}
