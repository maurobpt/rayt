using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    public class Color
    {
        private double red;
        private double green;
        private double blue;

        public Color()
        {
        }

        public Color(double red, double green, double blue)
        {
            this.red = red;
            this.green = green;            
            this.blue = blue;
        }
        
        public double Red
        {
            get { return red; }
            set { red = value; }
        }

        public double Green
        {
            get { return green; }
            set { green = value; }
        }

        public double Blue
        {
            get { return blue; }
            set { blue = value; }
        }
        //pintar no pixel, convert to ARGB
        public System.Drawing.Color getARGB()
        {
            return System.Drawing.Color.FromArgb(255, (byte)(red * 255.0), (byte)(green * 255.0), (byte)(blue * 255.0));
        }

        public static Color operator +(Color c1, Color c2)
        {
            return new Color(c1.red + c2.red, c1.green + c2.green, c1.blue + c2.blue);
        }

        public static Color operator *(Color c1, Color c2)
        {
            return new Color(c1.red * c2.red, c1.green * c2.green, c1.blue * c2.blue);
        }
        
        public bool isValidColor()
        {
            if (this != null && (this.Red >= 0.0 && this.Red <= 1.0) && (this.Blue >= 0.0 && this.Blue <= 1.0) 
                && (this.Green >= 0.0 && this.Green <= 1.0))
            {
                return true;
            }
            return false;
        }

        public static Color operator *(Color c, double value)
        {
            return new Color(c.red * value, c.green * value, c.blue * value);
        }
    }
}
