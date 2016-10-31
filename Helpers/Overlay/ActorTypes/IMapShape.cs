using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers.Overlay.ActorTypes
{
    public class IMapShape : IMapBase, IMap
    {
        public object _control;
        public bool Validated { get; set; }
        public int Id { get; }
        public Enigma.D3.Actor Actor;
        public readonly int _AcdID;
        public double height;

        public IMapShape(Enigma.D3.Actor A)
        {
            Actor = A;
            _AcdID = A.x000_Id;
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
            //height =( Actor.x0C4_CollisionRadius ) * 2;
            height = 20;

            Canvas x = new Canvas {  };
            //Label e = new Label { Content = PInvokers.GetMousePosition().X + " - " + PInvokers.GetMousePosition().Y.ToString() };
            //Label e = new Label { Content = Actor.x004_Name };

            Ellipse f = new Ellipse { Width = height, Height = height /2 , Stroke = Brushes.Pink, StrokeThickness = 2 };
           // x.Children.Add(e);
            x.Children.Add(f);

            return x;
        }

        public bool Update(System.Windows.Point Player, System.Windows.Point Additional = default(System.Windows.Point))
        {
            if (_AcdID != Actor.x000_Id)
                return false;

            try
            {
                var x = DiabloIII.FromD3toScreenCoords(
                    GameManagerAccountHelper.Current.Player.Point(),
                                        Actor.Point());
                                        
                X = x.X ;
                Y = x.Y ;
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
            return true;
        }

        //        public void calculatePoints_H(Vector3 c1p, Vector3 c2p, double c1r, double c2r, out Vector3 startLine, out Vector3 endLine)
        public void calculatePoints_H(Vector3 c1p, Vector3 c2p, double c1r, double c2r)
        {
            //c1p = circle one position
            //c1r = circle one radius

            Vector3 P0 = c1p;
            Vector3 P1 = c2p;

            double d, a, h;

            // d is the Distance Between the Center of the Spheres
            d = P0.Distance(P1);//  Vector3.Distance(P0, P1);
            
            // 'a' is the distance from the first sphere to the center of the
            //  circle resulting from the intersection of the spheres
            a = (c1r * c1r - c2r * c2r + d * d) / (2 * d);

            // h is the radius of the resulting circle
            h = Math.Sqrt(c1r * c1r - a * a);

            // P2 is the center of the resulting circle
            Vector3 P2 = (P1 - P0);
            /* P2 = (P2 * (a / d));
             P2 = (P2 + P0);

             float x3, y3, x4, y4 = 0;

             x3 = (double)(P2.X + h * (P1.Y - P0.Y) / d);
             y3 = (double)(P2.Y - h * (P1.X - P0.X) / d);

             x4 = (double)(P2.X - h * (P1.Y - P0.Y) / d);
             y4 = (double)(P2.Y + h * (P1.X - P0.X) / d);

             //draw visual to screen (unity 3D engine)
             //Debug.DrawLine(new Vector3(x3, 0, y3), new Vector3(x4, 0, y4), Color.green);
             */
            //out parameters for*/ a line renderer
           // startLine = new Vector3(x3, 0, y3);
            //endLine = new Vector3(x4, 0, y4);
        }

    }
}
