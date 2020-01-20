using System;
using System.Globalization;
namespace COSIG_RAY_TRACING
{
    public class Vector
    {
        private double x;
        private double y;
        private double z;

        public Vector()
        {
        }

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector(Vector v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double Magnitude()
        {
            return Math.Sqrt((double)((x * x) + (y * y) + (z * z)));
        }

        // normalizar o vector
        public void Normalize()
        {
            double length = Magnitude();
            x = x / length;
            y = y / length;
            z = z / length;
        }

        public static Vector Normalize(Vector vec)
        {
            vec.Normalize();
            return vec;
        }

        public bool IsNormalized
        {
            get { return MathHelper.IsOne((x * x) + (y * y) + (z * z)); }
        }

        /*
         * 
         *   http://onlinemschool.com/math/assistance/vector/multiply1/
             a × b = i(y1z2 - z1y2) - j(x1z2 - z1x2) + k(x1y2 - y1x2)
         * 
         * */

        public static void Cross_Product(ref Vector v1, ref Vector v2, out Vector result)
        {
            result = new Vector(
                (v1.y * v2.z) - (v1.z * v2.y),
                (v1.z * v2.x) - (v1.x * v2.z),
                (v1.x * v2.y) - (v1.y * v2.x));
        }

        // produto de vectores v1 * v2
        public static Vector Cross_Product(Vector v1, Vector v2)
        {
            Vector result;
            Cross_Product(ref v1, ref v2, out result);
            return result;
        }

        public static Vector operator *(Vector v1, double value)
        {
            return new Vector(v1.X * value, v1.Y * value, v1.Z * value);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector operator /(Vector v1, Vector v2)
        {
            return new Vector(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator /(Vector v1, double value)
        {
            return new Vector(v1.X / value, v1.Y / value, v1.Z / value);
        }


        //a · b = ax × bx + ay × by
        //https://www.mathsisfun.com/algebra/vectors-dot-product.html
        // produto escalar
        public static double DotProduct(Vector v1, Vector v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        public static Vector Neg(Vector v)
        {
            return new Vector(-v.x, -v.y, -v.z);
        }
    }
}