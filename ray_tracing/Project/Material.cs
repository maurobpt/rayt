using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
   public class Material
    {

        private Color color;
        private double ambientCoe;
        private double diffuseCoe;
        private double reflectionCoe;
        private double refractionCoe;
        private double indexRefraction;

  
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public double AmbientCoe
        {
            get { return ambientCoe; }
            set { ambientCoe = value; }
        }

        public double DiffuseCoe
        {
            get { return diffuseCoe; }
            set { diffuseCoe = value; }
        }

        public double ReflectionCoe
        {
            get { return reflectionCoe; }
            set { reflectionCoe = value; }
        }

        public double RefractionCoe
        {
            get { return refractionCoe; }
            set { refractionCoe = value; }
        }

        public double IndexRefraction
        {
            get { return indexRefraction; }
            set { indexRefraction = value; }
        }
    

        public Material()
        {
        }

        public Material(Color color, double ambientCoe, double diffuseCoe, double reflectionCoefficient, double refractionCoefficient, double indexRefraction)
        {
            this.color = color;
            this.ambientCoe = ambientCoe;
            this.diffuseCoe = diffuseCoe;
            this.reflectionCoe = reflectionCoefficient;
            this.refractionCoe = refractionCoefficient;
            this.indexRefraction = indexRefraction;
        }
    }
}
