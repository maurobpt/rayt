using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace COSIG_RAY_TRACING
{
    public class Triangle
    {
       private Material material;
       private Vector point1;
       private Vector point2;
       private Vector point3;
       private Vector normal;

       public Triangle(Material mat, Vector position_vertex1, Vector position_vertex2, Vector position_vertex3)
       {
           material = mat;
           this.point1 = position_vertex1;
           this.point2 = position_vertex2;
           this.point3 = position_vertex3;
           this.normal = calculateNormal();
       }

       public Triangle(Material mat, string[] vert1, string[] vert2, string[] vert3)
       {
           material = mat;
           var point1X = Double.Parse(vert1[0], CultureInfo.InvariantCulture);
           var point1Y = Double.Parse(vert1[1], CultureInfo.InvariantCulture);
           var point1Z = Double.Parse(vert1[2], CultureInfo.InvariantCulture);

           var point2X = Double.Parse(vert2[0], CultureInfo.InvariantCulture);
           var point2Y = Double.Parse(vert2[1], CultureInfo.InvariantCulture);
           var point2Z = Double.Parse(vert2[2], CultureInfo.InvariantCulture);

           var point3X = Double.Parse(vert3[0], CultureInfo.InvariantCulture);
           var point3Y = Double.Parse(vert3[1], CultureInfo.InvariantCulture);
           var point3Z = Double.Parse(vert3[2], CultureInfo.InvariantCulture);

           this.point1 = new Vector(point1X, point1Y, point1Z);
           this.point2 = new Vector(point2X, point2Y, point2Z);
           this.point3 = new Vector(point3X, point3Y, point3Z);

           this.normal = calculateNormal();
       }

       public Vector Point1
       {
           get { return point1; }
           set { point1 = value; }
       }

       public Vector Point2
       {
           get { return point2; }
           set { point2 = value; }
       }

       public Vector Point3
       {
           get { return point3; }
           set { point3 = value; }
       }

       public Material Material
       {
           get { return material; }
           set { material = value; }
       }

       public Vector Normal
       {
           get { return normal; }
           set { normal = value; }
       }

       private Vector calculateNormal()
       {
           var dir = Vector.Cross_Product(point2 - point1, point3 - point1);
           return Vector.Normalize(dir);
       }

       public Vector getNormal(Vector hitPoint)
       {
           return normal;
       }

       public double RayIntersects(Ray ray)
       {
           Triangle triInt = new Triangle(this.material, this.point1, this.point2, this.point3);
           return MathHelper.RayIntersects(triInt, ray);
       }
    }
}
