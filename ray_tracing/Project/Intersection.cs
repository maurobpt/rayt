using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    class Intersection
    {
        public double Distance { get; set; }
        public Vector intersectionPoint { get; set; }
        public Triangle FirstTriangle { get; set; }

        public Intersection(double d, Vector ip, Triangle ft)
        {
            Distance = d;
            intersectionPoint = ip;
            FirstTriangle = ft;
        }

        public static Intersection GetFirstIntersection(Ray ray, List<Triangle> listTriangles)
        {
            double distance = -1;
            Triangle first = null;

            foreach (Triangle triangle in listTriangles)
            {
                double t = triangle.RayIntersects(ray);
                if (t >= 0)
                {
                    if (distance == -1)
                    {
                        distance = t;
                        first = triangle;
                    }
                    else if (t < distance)
                    {
                        distance = t;
                        first = triangle;
                    }
                }
            }

            Vector intersectionPoint = ray.Origin + (ray.Direction * distance);

            return new Intersection(distance, intersectionPoint, first);
        }
    }
}
