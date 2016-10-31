using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindersKeepers.Helpers
{
        public class Vector3 : IEquatable<Vector3>
        {
            public float X, Y, Z;

            public static readonly Vector3 Empty = new Vector3();

            public bool IsEmpty { get; private set; }

            public Vector3(float x, float y, float z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                IsEmpty = false;
            }

            public Vector3(Vector3 v)
            {
                X = v.X;
                Y = v.Y;
                Z = v.Z;
                IsEmpty = v.IsEmpty;
            }

            private Vector3()
            {
                X = Y = Z = 0;
                IsEmpty = true;
            }

            // http://en.wikipedia.org/wiki/File:3D_Spherical.svg
            // theta is the angle between the positive Z-axis and the vector in question (0 ≤ θ ≤ π)
            // phi is the angle between the projection of the vector onto the X-Y-plane and the positive X-axis (0 ≤ φ < 2π)        
            public static Vector3 FromSpherical(float theta, float phi)
            {
                return new Vector3((float)(Math.Sin(theta) * Math.Cos(phi)), (float)(Math.Sin(theta) * Math.Sin(phi)), (float)Math.Cos(theta));
            }

            public static Vector3 FromSpherical2D(float phi)
            {
                return new Vector3((float)Math.Cos(phi), (float)Math.Sin(phi), 0);
            }

            public override bool Equals(Object obj)
            {
                if (obj == null)
                    return false;

                Vector3 v = obj as Vector3;

                return Equals(v);
            }

            public bool Equals(Vector3 v)
            {
                return X.Equals(v.X) && Y.Equals(v.Y) && Z.Equals(v.Z);
            }

            public bool Equals(Vector3 v, float epsilon)
            {
                return Math.Abs(X - v.X) < epsilon &&
                       Math.Abs(Y - v.Y) < epsilon &&
                       Math.Abs(Z - v.Z) < epsilon;
            }

            public override int GetHashCode()
            {
                // i know this is bullshit!
                return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
            }

            public static Vector3 operator +(Vector3 LHS, Vector3 RHS)
            {
                return new Vector3(LHS.X + RHS.X,
                                LHS.Y + RHS.Y,
                                LHS.Z + RHS.Z);
            }

            public static Vector3 operator -(Vector3 LHS, Vector3 RHS)
            {
                return new Vector3(LHS.X - RHS.X,
                                LHS.Y - RHS.Y,
                                LHS.Z - RHS.Z);
            }

            public static Vector3 operator -(Vector3 RHS)
            {
                return new Vector3(-RHS.X,
                                -RHS.Y,
                                -RHS.Z);
            }

            public static Vector3 operator *(Vector3 LHS, float RHS)
            {
                return new Vector3(LHS.X * RHS,
                                LHS.Y * RHS,
                                LHS.Z * RHS);
            }

            public static Vector3 operator *(Vector3 LHS, Vector3 RHS)
            {
                return new Vector3(LHS.X * RHS.X,
                                LHS.Y * RHS.Y,
                                LHS.Z * RHS.Z);
            }

            public static Vector3 operator /(Vector3 LHS, float RHS)
            {
                return new Vector3(LHS.X / RHS,
                                LHS.Y / RHS,
                                LHS.Z / RHS);
            }

            public static Vector3 Rotate(Vector3 v, float angle, Vector3 axis)
            {
                if (angle.Equals(0))
                    return new Vector3(v);

                double DEG2RAD = Math.PI / 180;

                double c = Math.Cos(angle * DEG2RAD);
                double s = Math.Sin(angle * DEG2RAD);
                double C = 1.0 - c;

                double[,] Q = new double[3, 3];
                Q[0, 0] = axis.X * axis.X * C + c;
                Q[0, 1] = axis.Y * axis.X * C + axis.Z * s;
                Q[0, 2] = axis.Z * axis.X * C - axis.Y * s;

                Q[1, 0] = axis.Y * axis.X * C - axis.Z * s;
                Q[1, 1] = axis.Y * axis.Y * C + c;
                Q[1, 2] = axis.Z * axis.Y * C + axis.X * s;

                Q[2, 0] = axis.X * axis.Z * C + axis.Y * s;
                Q[2, 1] = axis.Z * axis.Y * C - axis.X * s;
                Q[2, 2] = axis.Z * axis.Z * C + c;

                return new Vector3((float)(v.X * Q[0, 0] + v.Y * Q[1, 0] + v.Z * Q[2, 0]),
                                (float)(v.X * Q[0, 1] + v.Y * Q[1, 1] + v.Z * Q[2, 1]),
                                (float)(v.X * Q[0, 2] + v.Y * Q[1, 2] + v.Z * Q[2, 2]));
            }

            public float Distance(Vector3 v)
            {
                Vector3 diff = this - v;
                return (float)Math.Sqrt((double)(diff.X * diff.X + diff.Y * diff.Y + diff.Z * diff.Z));
            }

            public float DistanceSqr(Vector3 v)
            {
                Vector3 diff = this - v;
                return diff.X * diff.X + diff.Y * diff.Y + diff.Z * diff.Z;
            }

            public float Distance2D(Vector3 v)
            {
                Vector3 diff = this - v;
                return (float)Math.Sqrt((double)(diff.X * diff.X + diff.Y * diff.Y));
            }

            public float Distance2DSqr(Vector3 v)
            {
                Vector3 diff = this - v;
                return diff.X * diff.X + diff.Y * diff.Y;
            }

            public float Length()
            {
                return (float)Math.Sqrt((double)(X * X + Y * Y + Z * Z));
            }

            public float LengthSqr()
            {
                return X * X + Y * Y + Z * Z;
            }

            public float Length2D()
            {
                return (float)Math.Sqrt((double)(X * X + Y * Y));
            }

            public float Length2DSqr()
            {
                return X * X + Y * Y;
            }

            public void Normalize()
            {
                float len = Length();
                X /= len;
                Y /= len;
                Z /= len;
            }

            public void Normalize2D()
            {
                float len = Length2D();
                X /= len;
                Y /= len;
                Z = 0;
            }

            public Vector3 Cross(Vector3 RHS)
            {
                return new Vector3(Y * RHS.Z - Z * RHS.Y,
                                Z * RHS.X - X * RHS.Z,
                                X * RHS.Y - Y * RHS.X);
            }

            public float Dot(Vector3 RHS)
            {
                return (this.X * RHS.X + this.Y * RHS.Y + this.Z * RHS.Z);
            }

            public float DotNorm(Vector3 RHS)
            {
                return (this.X * RHS.X + this.Y * RHS.Y + this.Z * RHS.Z) / (Length() * RHS.Length());
            }

            public float Dot2D(Vector3 RHS)
            {
                return (this.X * RHS.X + this.Y * RHS.Y);
            }

            public float Dot2DNorm(Vector3 RHS)
            {
                return (this.X * RHS.X + this.Y * RHS.Y) / (Length2D() * RHS.Length2D());
            }

            public Vector3 Blend(Vector3 RHS, float ratio)
            {
                float ratio2 = 1.0f - ratio;
                return new Vector3(X * ratio2 + RHS.X * ratio,
                                Y * ratio2 + RHS.Y * ratio,
                                Z * ratio2 + RHS.Z * ratio);
            }

            public static Vector3 Max(Vector3 LHS, Vector3 RHS)
            {
                return new Vector3(Math.Max(LHS.X, RHS.X), Math.Max(LHS.Y, RHS.Y), Math.Max(LHS.Z, RHS.Z));
            }

            public static Vector3 Min(Vector3 LHS, Vector3 RHS)
            {
                return new Vector3(Math.Min(LHS.X, RHS.X), Math.Min(LHS.Y, RHS.Y), Math.Min(LHS.Z, RHS.Z));
            }

            public override string ToString() { return "[" + Math.Round(X, 2) + " " + Math.Round(Y, 2) + " " + Math.Round(Z, 2) + "]"; }
        }
}
