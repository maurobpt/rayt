using System;
namespace COSIG_RAY_TRACING
{

    public class Ray
    {
        private Vector origin;
        private Vector direction;

        public Ray()
        {
            origin = new Vector();
            direction = new Vector();
        }

        public Ray(Vector Origin, Vector Direction)
        {
            this.origin = Origin;
            this.direction = Direction;
        }

        public Vector Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Vector Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}