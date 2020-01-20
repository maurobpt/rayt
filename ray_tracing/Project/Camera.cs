using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    public class Camera
    {
        private Transformation transformation;
        private double distance;
        private double fieldOfView;

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public double Field_of_view
        {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }

        public Transformation Transformation
        {
            get { return transformation; }
            set { transformation = value; }
        }

        public Camera()
        {
            transformation = new Transformation();
            distance = 0.0;
            fieldOfView = 0.0;
        }

        public Camera(double distance, double fieldOfView)
        {
            this.distance = distance;
            this.fieldOfView = fieldOfView;
        }

        public Camera(Transformation t, double distance, double fieldOfView)
        {
            transformation = t;
            this.distance = distance;
            this.fieldOfView = fieldOfView;
        }
    }
}
