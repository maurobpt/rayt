using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    public class Light
    {
        private Transformation transformation;
        private Color color;
        private Vector position;

        public Light()
        {
            transformation = new Transformation();
            color = new Color();
        }

        public Light(Transformation transformation, Color color)
        {
            this.transformation = transformation;
            this.color = color;
        }

        public Light(Vector position, Color color)
        {
            this.color = color;
            this.position = position;
        }

        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }

        public Transformation Transformation
        {
            get { return transformation; }
            set { transformation = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }    
    }
}
