using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using Enigma.D3;
using System.Windows.Shapes;
using System.Windows.Media;

namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public class IMapActor : IMapBase, IMap
    {
        public int Id { get; set; }
        public ActorCommonData Actor;
        public MapItemElement Type;
        public int EliteOffset = -1;
        public int ACDID;
        public int Identifier;
        public object _control;
        public bool Validated { get; set; }
        public bool IsMonster;
        public System.Windows.Size Size;
        public bool IsItem = false;
   

        public IMapActor(ActorCommonData A, MapItemElement E, int EliteOff = -1, int I = -1, bool Monster = false)
        {
            Actor = A;
            Type = E;
            EliteOffset = EliteOff;
            Identifier = I;
            ACDID = Actor.x000_Id;
            Id = Actor.Address;
            IsMonster = Monster;
            IsItem = A.IsItem();
        }

        public object Control
        {
            get
            {
                return (_control = _control ?? CreateControl());
            }
        }

        public object CreateControl()
        {
            if (Type == MapItemElement.AT_HasBeenOperated)
            {
                 Size = new System.Windows.Size(10, 10);
                 Ellipse Ellipse = new Ellipse { Width = 20, Height = 20 };
                 Ellipse.Fill = new ImageBrush { ImageSource = Extensions.FKImage("Icons/operated")};
                 Ellipse.Fill.Freeze();
                 Ellipse.Opacity = 0.5;
                Ellipse.RenderTransform = new RotateTransform(0);

                 return Ellipse;

                //return new Ellipse { Width = 10, Height = 10, Stroke = System.Windows.Media.Brushes.Red, StrokeThickness = 2 };
            }

            else
            {
                MapItemShape Shape = Config.MiniMapItems(Type, Identifier).Shape;
                Size = new System.Windows.Size(Shape.Width / 2, Shape.Height /2);

                if (Shape.Shape == ItemShape.Ellipse)
                    return new Ellipse
                    {
                        Width = Shape.Width,
                        Height = Shape.Height,
                        Opacity = Shape.Opacity,
                        Fill = Shape.FillBrush,
                        StrokeThickness = Shape.StrokeThickness,
                        Stroke = Shape.StrokeBrush
                    };

                else if (Shape.Shape == ItemShape.Rectangle)
                    return new Rectangle
                    {
                        Width = Shape.Width,
                        Height = Shape.Height,
                        Opacity = Shape.Opacity,
                        Fill = Shape.FillBrush,
                        StrokeThickness = Shape.StrokeThickness,
                        Stroke = Shape.StrokeBrush
                    };
            }
            return null;
        }

        public bool Update(System.Windows.Point Player, System.Windows.Point Additional = default(System.Windows.Point))
        {
            if (!IsValid())
                return false;

            var Temp = new System.Windows.Point(Actor.x0D0_WorldPosX - Player.X, Actor.x0D4_WorldPosY - Player.Y);

            if (Additional != default(System.Windows.Point))
            {
                Temp.X = (Additional.X < 0) ? Temp.X + -Additional.X : Temp.X - Additional.X;
                Temp.Y = (Additional.Y < 0) ? Temp.Y + -Additional.Y : Temp.Y - Additional.Y;
            }

            X = (Temp.X - Size.Width) - (Size.Width /2);
            Y = (Temp.Y - Size.Height) - (Size.Height /2);

            return true;
        }

        public bool IsValid()
        {
            int ActorId = Actor.x000_Id;

            if (ActorId == -1 || ActorId != ACDID) //|| Actor.Address != Id)
                return false;

            if (IsMonster)
                return Actor.x188_Hitpoints != 0;

            if (IsItem)
                return (int)Actor.x114_ItemLocation == -1;

            else
            {
                if (Type == MapItemElement.AT_Chests || Type == MapItemElement.AT_ResplendentChest)
                {
                    if (Type == MapItemElement.AT_Chests)
                        return Actor.IsValidGizmoChest();
                    else
                        return Actor.TryCreateResplendent();
                }

                else if (Type == MapItemElement.AT_Shrines || Type == MapItemElement.AT_PowerPylons)
                {
                    return Actor.GetAttribute(Enigma.D3.Enums.AttributeId.GizmoHasBeenOperated) != 1;
                }

                else if (Type == MapItemElement.AT_PoolOfReflection)
                {
                    return Actor.GetAttribute(Enigma.D3.Enums.AttributeId.GizmoHasBeenOperated) != 1;
                }
            }
            return true;
        }

    }
}
