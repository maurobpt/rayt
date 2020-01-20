using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSIG_RAY_TRACING
{
    //Calculo auxiliar
    public class MathHelper
    {

        //Raios primarios
        public static double calc_Height(Camera Camera)
        {
            double DistanceofCamera = Camera.Distance;
            double degreesToRadians = MathHelper.DegreesToRadians(Camera.Field_of_view / 2);
            return 2 * DistanceofCamera * Math.Tan(degreesToRadians);
        }

        public static double calc_Width(Image Image, double height)
        {
            double imageHorizontal = Image.Horizontal;
            double imageVertical = Image.Vertical;
            return (imageHorizontal / imageVertical) * height;
        }

        public static double Pixel_Size(Image Image, double height)
        {
            double res = Image.Vertical;
            return  height / res;
           ;
        }

        public static double calc_Pixel(double pixelSize, int index, double axis)
        {
            return pixelSize * (index + 0.5) - (axis / 2);          
        }

        public static Vector calc_Vector_Direction(double px, double py, double pz, Vector Origin)
        {
            Vector Pixel = new Vector(px, py, pz);
            return Vector.Normalize(Pixel - Origin);
        }

        //Calcular intersecao
        public static double calc_Determinant_A(Ray ray, Triangle triangle)
        {
            double[,] MatrixAlpha = new double[3, 3] { 
                                    { triangle.Point1.X - triangle.Point2.X,  triangle.Point1.X - triangle.Point3.X, ray.Direction.X }, 
                                    { triangle.Point1.Y - triangle.Point2.Y,  triangle.Point1.Y - triangle.Point3.Y, ray.Direction.Y }, 
                                    { triangle.Point1.Z - triangle.Point2.Z,  triangle.Point1.Z - triangle.Point3.Z, ray.Direction.Z } };
            return MathHelper.Determinant(MatrixAlpha);
        }

        public static double calc_Beta(Ray ray, Triangle triangle, double det_A)
        {
            double[,] matrixBeta = new double[3, 3] { { triangle.Point1.X - ray.Origin.X, triangle.Point1.X - triangle.Point3.X, ray.Direction.X },   
                                                {  triangle.Point1.Y - ray.Origin.Y, triangle.Point1.Y - triangle.Point3.Y, ray.Direction.Y }, 
                                                {  triangle.Point1.Z - ray.Origin.Z, triangle.Point1.Z - triangle.Point3.Z, ray.Direction.Z } };
            double detMatrixBeta = MathHelper.Determinant(matrixBeta);
            return detMatrixBeta / det_A;
        }

        public static double calc_Gamma(Ray ray, Triangle triangle, double det_A)
        {
            double[,] matrixGama = new double[3, 3] { {  triangle.Point1.X - triangle.Point2.X, triangle.Point1.X - ray.Origin.X, ray.Direction.X },   
                                                { triangle.Point1.Y - triangle.Point2.Y, triangle.Point1.Y - ray.Origin.Y, ray.Direction.Y }, 
                                                { triangle.Point1.Z- triangle.Point2.Z, triangle.Point1.Z - ray.Origin.Z, ray.Direction.Z } };
            double detMatrixGamma = MathHelper.Determinant(matrixGama);
            return detMatrixGamma / det_A;
        }

        public static double calculateT(Ray ray, Triangle triangle, double det_A)
        {
            double[,] matrixT = new double[3, 3] { { triangle.Point1.X - triangle.Point2.X, triangle.Point1.X - triangle.Point3.X, triangle.Point1.X - ray.Origin.X},   
                                                { triangle.Point1.Y - triangle.Point2.Y, triangle.Point1.Y - triangle.Point3.Y, triangle.Point1.Y - ray.Origin.Y }, 
                                                { triangle.Point1.Z- triangle.Point2.Z, triangle.Point1.Z - triangle.Point3.Z, triangle.Point1.Z - ray.Origin.Z } };
            double detMatrixT = MathHelper.Determinant(matrixT);
            return detMatrixT / det_A;
        }

        //Reflexao
        public static Vector calc_Ray_Refractor(Vector Normal, Vector Direction)
        {
            double ray1 = -Vector.DotProduct(Normal, Direction);
            return Vector.Normalize((Direction + (Normal * 2.0 * ray1)));
        }

        //Refracaao
        public static Vector calc_Ray_Refractor(double n1, double n2, Vector Direction, Vector N1)
        {
            double ray1 = -Vector.DotProduct(N1, Direction);
            double n = n1 / n2;
            double ray2 = Math.Sqrt((1 - Math.Pow(n, 2) * (1 - Math.Pow(ray1, 2))));
            return Vector.Normalize((Direction * n) + N1 * (n * ray1 - ray2));

        }

        /* O valor para o qual todos os números absolutos menores que são considerados iguais a zero.*/
        public const double ZeroTolerance = 1e-6; // Value a 8x higher than 1.19209290E-07F

        /* Valor da aproximacao de π - 180 graus.*/
        public const double Pi = Math.PI;

        /*Valor epislon usado no intersection*/
        public const double Epislon = 1.0E-6;

        /*Determinar se um valor especifico esta proxima de 0 (0.0f)*/

        public static bool IsZero(double a)
        {
            return Math.Abs(a) < ZeroTolerance;
        }

        /* Determinar um valor especifico esta proximo de 1 (1.0f).*/
        public static bool IsOne(double a)
        {
            return IsZero(a - 1.0);
        }

        /* Converter graus para radianos.*/
        public static double DegreesToRadians(double degree)
        {
            return degree * (Pi / 180.0);
        }

        /* Converter radianos para graus*/
        public static double RadiansToDegrees(double radian)
        {
            return radian * (180.0 / Pi);
        }

        /*Calcular Determinat*/
        public static double Determinant(double[,] matrix)
        {
            double det;
            det = 0;
            det = ((double)(matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) + (matrix[0, 1] * matrix[1, 2] *
            matrix[2, 0]) + (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) -
            (matrix[2, 0] * matrix[1, 1] * matrix[0, 2]) - (matrix[2, 1] * matrix[1, 2] * matrix[0, 0]) -
            (matrix[2, 2] * matrix[1, 0] * matrix[0, 1]));
            return det;
        }
   
        public static bool Invert(double[,] matrix, out double[,] result)
        {
            double a = matrix[0, 0], b = matrix[0, 1], c = matrix[0, 2], d = matrix[0, 3];
            double e = matrix[1, 0], f = matrix[1, 1], g = matrix[1, 2], h = matrix[1, 3];
            double i = matrix[2, 0], j = matrix[2, 1], k = matrix[2, 2], l = matrix[2, 3];
            double m = matrix[3, 0], n = matrix[3, 1], o = matrix[3, 2], p = matrix[3, 3];

            double kp_lo = k * p - l * o;
            double jp_ln = j * p - l * n;
            double jo_kn = j * o - k * n;
            double ip_lm = i * p - l * m;
            double io_km = i * o - k * m;
            double in_jm = i * n - j * m;

            double a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            double a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            double a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            double a14 = -(e * jo_kn - f * io_km + g * in_jm);

            double det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < double.Epsilon)
            {
                result = new double[4, 4];
                return false;
            } result = new double[4, 4];

            double invDet = 1.0f / det;

            result[0, 0] = a11 * invDet;
            result[1, 0] = a12 * invDet;
            result[2, 0] = a13 * invDet;
            result[3, 0] = a14 * invDet;

            result[0, 1] = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result[1, 1] = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result[2, 1] = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result[3, 1] = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            double gp_ho = g * p - h * o;
            double fp_hn = f * p - h * n;
            double fo_gn = f * o - g * n;
            double ep_hm = e * p - h * m;
            double eo_gm = e * o - g * m;
            double en_fm = e * n - f * m;

            result[0, 2] = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result[1, 2] = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result[2, 2] = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result[3, 2] = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            double gl_hk = g * l - h * k;
            double fl_hj = f * l - h * j;
            double fk_gj = f * k - g * j;
            double el_hi = e * l - h * i;
            double ek_gi = e * k - g * i;
            double ej_fi = e * j - f * i;

            result[0, 3] = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result[1, 3] = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result[2, 3] = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result[3, 3] = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return true;
        }

        public static double RayIntersects(Triangle tri, Ray ray)
        {
            double detA = MathHelper.calc_Determinant_A(ray, tri);

            double beta = MathHelper.calc_Beta(ray, tri, detA);
            if (beta < 0)
            {
                return -1;
            }

            double gamma = MathHelper.calc_Gamma(ray, tri, detA);
            if (gamma < 0)
            {
                return -1;
            }

            if (beta + gamma >= 1)
            {
                return -1;
            }

            double t = MathHelper.calculateT(ray, tri, detA);
            if (t < MathHelper.Epislon)
            {
                return -1;
            }

            return t;
        }

        public static double[] GetHeighWidthAndPixelSize(Camera camera, Image image)
        {
            double[] vec = new double[3];
            //height
            double height = MathHelper.calc_Height(camera);
            //width
            double width = MathHelper.calc_Width(image, height);
            //pixel size
            double pixelSize = MathHelper.Pixel_Size(image, height);

            vec[0] = height;
            vec[1] = width;
            vec[2] = pixelSize;

            return vec;
        }
    }
}
