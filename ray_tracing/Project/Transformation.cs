using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    public class Transformation
    {
        private bool identity; 
        private Vector translation;
        private Vector rotation; 
        private Vector scale;
        private double[,] transformMatrix;

      
        public bool Identity
        {
            get { return identity; }
            set { identity = value; }
        }
        public Vector Translation
        {
            get { return translation; }
            set { translation = value; }
        }
        public Vector Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public double[,] TransformMatrix
        {
            get { return transformMatrix; }
            set { transformMatrix = value; }
        }

        public Transformation()
        {
            translation = new Vector();
            rotation = new Vector();
            scale = new Vector();
            identity = false;
            transformMatrix = IdentityMatrix();
        }

        private double[,] IdentityMatrix()
        {
            double[,] Matrix = new double[4, 4];
           
            Matrix[0, 0] = 1.0;
            Matrix[0, 1] = 0.0;
            Matrix[0, 2] = 0.0;
            Matrix[0, 3] = 0.0;
            Matrix[1, 0] = 0.0;
            Matrix[1, 1] = 1.0;
            Matrix[1, 2] = 0.0;
            Matrix[1, 3] = 0.0;
            Matrix[2, 0] = 0.0;
            Matrix[2, 1] = 0.0;
            Matrix[2, 2] = 1.0;
            Matrix[2, 3] = 0.0;
            Matrix[3, 0] = 0.0;
            Matrix[3, 1] = 0.0;
            Matrix[3, 2] = 0.0;
            Matrix[3, 3] = 1.0;

            return Matrix;
        }

        public void Translate(double x, double y, double z)
        {
            double[,] translate = new double[4, 4];
            translate[0,0] = 1.0;
	        translate[0,1] = 0.0;
	        translate[0,2] = 0.0;
	        translate[0,3] = x;
	        translate[1,0] = 0.0;
	        translate[1,1] = 1.0;
	        translate[1,2] = 0.0;
	        translate[1,3] = y;
	        translate[2,0] = 0.0;
	        translate[2,1] = 0.0;
	        translate[2,2] = 1.0;
	        translate[2,3] = z;
	        translate[3,0] = 0.0;
	        translate[3,1] = 0.0;
	        translate[3,2] = 0.0;
	        translate[3,3] = 1.0;
            Multiply(translate);
        }

        public void Translate()
        {
            double[,] translate = new double[4, 4];
            translate[0, 0] = 1.0;
            translate[0, 1] = 0.0;
            translate[0, 2] = 0.0;
            translate[0, 3] = translation.X;
            translate[1, 0] = 0.0;
            translate[1, 1] = 1.0;
            translate[1, 2] = 0.0;
            translate[1, 3] = translation.Y;
            translate[2, 0] = 0.0;
            translate[2, 1] = 0.0;
            translate[2, 2] = 1.0;
            translate[2, 3] = translation.Z;
            translate[3, 0] = 0.0;
            translate[3, 1] = 0.0;
            translate[3, 2] = 0.0;
            translate[3, 3] = 1.0;
            Multiply(translate);
        }

        public void RotateX(double a)
        {
            double[,] rotateX = new double[4, 4];
            a *= Math.PI / 180.0;
            rotateX[0, 0] = 1.0;
            rotateX[0, 1] = 0.0;
            rotateX[0, 2] = 0.0;
            rotateX[0, 3] = 0.0;
            rotateX[1, 0] = 0.0;
            rotateX[1, 1] = Math.Cos(a);
            rotateX[1, 2] = Math.Sin(a);
            rotateX[1, 3] = 0.0;
            rotateX[2, 0] = 0.0;
            rotateX[2, 1] = -Math.Sin(a);
            rotateX[2, 2] = Math.Cos(a);
            rotateX[2, 3] = 0.0;
            rotateX[3, 0] = 0.0;
            rotateX[3, 1] = 0.0;
            rotateX[3, 2] = 0.0;
            rotateX[3, 3] = 1.0;
            Multiply(rotateX);
        }

        public void RotateX()
        {
            var val = MathHelper.DegreesToRadians(rotation.X);
            double cos = Math.Cos(val);
            double sin = Math.Sin(val);

            double[,] rotateX = new double[4, 4];
            
            rotateX[0, 0] = 1.0;
            rotateX[0, 1] = 0.0;
            rotateX[0, 2] = 0.0;
            rotateX[0, 3] = 0.0;
            rotateX[1, 0] = 0.0;
            rotateX[1, 1] = cos;
            rotateX[1, 2] = sin;
            rotateX[1, 3] = 0.0;
            rotateX[2, 0] = 0.0;
            rotateX[2, 1] = -sin;
            rotateX[2, 2] = cos;
            rotateX[2, 3] = 0.0;
            rotateX[3, 0] = 0.0;
            rotateX[3, 1] = 0.0;
            rotateX[3, 2] = 0.0;
            rotateX[3, 3] = 1.0;
            Multiply(rotateX);
        }

        public void RotateY(double a)
        {
            double[,] rotateY = new double[4, 4];

            a *= Math.PI / 180.0;
            rotateY[0, 0] = Math.Cos(a);
            rotateY[0, 1] = 0.0;
            rotateY[0, 2] = Math.Sin(a);
            rotateY[0, 3] = 0.0;
            rotateY[1, 0] = 0.0;
            rotateY[1, 1] = 1.0;
            rotateY[1, 2] = 0.0;
            rotateY[1, 3] = 0.0;
            rotateY[2, 0] = -Math.Sin(a);
            rotateY[2, 1] = 0.0;
            rotateY[2, 2] = Math.Cos(a);
            rotateY[2, 3] = 0.0;
            rotateY[3, 0] = 0.0;
            rotateY[3, 1] = 0.0;
            rotateY[3, 2] = 0.0;
            rotateY[3, 3] = 1.0;

            Multiply(rotateY);
        }

        public void RotateY()
        {
            var val = MathHelper.DegreesToRadians(rotation.Y);
            double cos = Math.Cos(val);
            double sin = Math.Sin(val);
            double[,] rotateY = new double[4, 4];
           
            rotateY[0, 0] = cos;
            rotateY[0, 1] = 0.0;
            rotateY[0, 2] = sin;
            rotateY[0, 3] = 0.0;
            rotateY[1, 0] = 0.0;
            rotateY[1, 1] = 1.0;
            rotateY[1, 2] = 0.0;
            rotateY[1, 3] = 0.0;
            rotateY[2, 0] = -sin;
            rotateY[2, 1] = 0;
            rotateY[2, 2] = cos;
            rotateY[2, 3] = 0.0;
            rotateY[3, 0] = 0.0;
            rotateY[3, 1] = 0.0;
            rotateY[3, 2] = 0.0;
            rotateY[3, 3] = 1.0;

            Multiply(rotateY);
        }

        public void RotateZ(double a)
        {
            double[,] rotateZ = new double[4, 4];
           
            a *= Math.PI / 180.0;
            rotateZ[0, 0] = Math.Cos(a);
            rotateZ[0, 1] = -Math.Sin(a);
            rotateZ[0, 2] = 0.0;
            rotateZ[0, 3] = 0.0;
            rotateZ[1, 0] = Math.Sin(a);
            rotateZ[1, 1] = Math.Cos(a);
            rotateZ[1, 2] = 0.0;
            rotateZ[1, 3] = 0.0;
            rotateZ[2, 0] = 0.0;
            rotateZ[2, 1] = 0.0;
            rotateZ[2, 2] = 1.0;
            rotateZ[2, 3] = 0.0;
            rotateZ[3, 0] = 0.0;
            rotateZ[3, 1] = 0.0;
            rotateZ[3, 2] = 0.0;
            rotateZ[3, 3] = 1.0;

            Multiply(rotateZ);
        }

        public void RotateZ()
        {
            var val = MathHelper.DegreesToRadians(rotation.Z);
            double cos = Math.Cos(val);
            double sin = Math.Sin(val);
            double[,] rotateZ = new double[4, 4];

            rotateZ[0, 0] = cos;
            rotateZ[0, 1] = -sin;
            rotateZ[0, 2] = 0.0;
            rotateZ[0, 3] = 0.0;
            rotateZ[1, 0] = sin;
            rotateZ[1, 1] = cos;
            rotateZ[1, 2] = 0.0;
            rotateZ[1, 3] = 0.0;
            rotateZ[2, 0] = 0.0;
            rotateZ[2, 1] = 0.0;
            rotateZ[2, 2] = 1.0;
            rotateZ[2, 3] = 0.0;
            rotateZ[3, 0] = 0.0;
            rotateZ[3, 1] = 0.0;
            rotateZ[3, 2] = 0.0;
            rotateZ[3, 3] = 1.0;
            Multiply(rotateZ);
        }

        public void ScaleTrans(double x, double y, double z)
        {

            double[,] scaleMat = new double[4, 4];
            scaleMat[0, 0] = x;
            scaleMat[0, 1] = 0.0;
            scaleMat[0, 2] = 0.0;
            scaleMat[0, 3] = 0.0;
            scaleMat[1, 0] = 0.0;
            scaleMat[1, 1] = y;
            scaleMat[1, 2] = 0.0;
            scaleMat[1, 3] = 0.0;
            scaleMat[2, 0] = 0.0;
            scaleMat[2, 1] = 0.0;
            scaleMat[2, 2] = z;
            scaleMat[2, 3] = 0.0;
            scaleMat[3, 0] = 0.0;
            scaleMat[3, 1] = 0.0;
            scaleMat[3, 2] = 0.0;
            scaleMat[3, 3] = 1.0;

            Multiply(scaleMat);
        }

        public void ScaleTrans()
        {
            double[,] scaleMat = new double[4, 4];
            scaleMat[0, 0] = scale.X;
            scaleMat[0, 1] = 0.0;
            scaleMat[0, 2] = 0.0;
            scaleMat[0, 3] = 0.0;
            scaleMat[1, 0] = 0.0;
            scaleMat[1, 1] = scale.Y;
            scaleMat[1, 2] = 0.0;
            scaleMat[1, 3] = 0.0;
            scaleMat[2, 0] = 0.0;
            scaleMat[2, 1] = 0.0;
            scaleMat[2, 2] = scale.Z;
            scaleMat[2, 3] = 0.0;
            scaleMat[3, 0] = 0.0;
            scaleMat[3, 1] = 0.0;
            scaleMat[3, 2] = 0.0;
            scaleMat[3, 3] = 1.0;

            Multiply(scaleMat);

        }
 
        private void Multiply(double[] pointA, double[] pointB)
        {
            int i, j;

            for (i = 0; i < 4; i++)
                pointB[i] = 0.0;

            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                    pointB[i] += transformMatrix[i, j] * pointA[j];
            }
        }

        private void Multiply(double[,] matrixA)
        {
            int i, j, k;
            double[,] matrixB = new double[4, 4];

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    matrixB[i, j] = transformMatrix[i, j];
                    transformMatrix[i, j] = 0.0;
                }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    for (k = 0; k < 4; k++)
                        transformMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
        }

        public static Transformation Transpose(Transformation transf)
        {
            Transformation transpose = new Transformation();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    transpose.TransformMatrix[i, j] = transf.transformMatrix[j, i];
                }
            }
            return transpose;
        }

        public static Transformation Inverse(Transformation transf)
        {

            Transformation inverse = new Transformation();
            var inver = new double[4, 4];
            MathHelper.Invert(transf.TransformMatrix, out inver);
            inverse.TransformMatrix = inver;

            return inverse;
        }

        public Vector TransformPoint(Vector Point)
        {
            double[] pointA = new double[4];
            double[] pointB = new double[4];

            pointA[0] = Point.X;
            pointA[1] = Point.Y;
            pointA[2] = Point.Z;
            pointA[3] = 1.0;

            Multiply(pointA, pointB);

            return new Vector(pointB[0] / pointB[3], pointB[1] / pointB[3], pointB[2] / pointB[3]);
        }

        public static Transformation operator *(Transformation mat1, Transformation mat2)
        {
            Transformation newMatrix = new Transformation();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    newMatrix.TransformMatrix[i, j] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrix.TransformMatrix[i, j] += mat1.TransformMatrix[i, k] * mat2.TransformMatrix[k, j];
                    }
                }
            }
            return newMatrix;
        }
    }
}
