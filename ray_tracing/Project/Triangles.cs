using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    public class Triangles
    {
        private List<Triangle> listOfTriangles;

        private Transformation transformation;

        public List<Triangle> ListOfTriangles
        {
            get { return listOfTriangles; }
            set { listOfTriangles = value; }
        }

        public void addTriangleToListOfTriangles(Triangle triangle)
        {
            listOfTriangles.Add(triangle);
        }

        public Transformation Transformation
        {
            get { return transformation; }
            set { transformation = value; }
        }

       public Triangles()
       {
           listOfTriangles = new List<Triangle>();
       }

    }
}
