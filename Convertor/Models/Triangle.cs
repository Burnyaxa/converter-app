using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Converter.Models
{
    public struct Triangle
    {
        public Vector3 A { get; set; }
        public Vector3 B { get; set; }
        public Vector3 C { get; set; }

        public Vector3 Center => new Vector3((A.X + B.X + C.X) / 3, (A.Y + B.Y + C.Y) / 3, (A.Z + B.Z + C.Z) / 3);

        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Vector3 NormVector()
        {
            Vector3 first = B - A;
            Vector3 second = C - A;
            Vector3 cross = Vector3.Cross(first, second);
            return Vector3.Normalize(cross);
        }

        public static bool operator ==(Triangle first, Triangle second)
        {
            return first.A == second.A && first.B == second.B && first.C == second.C;
        }

        public static bool operator !=(Triangle first, Triangle second)
        {
            return !(first == second);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Center.GetHashCode();
        }
    }
}
