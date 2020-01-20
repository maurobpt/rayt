using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
namespace COSIG_RAY_TRACING
{
    public class RayTracer
    {
        Camera camera;
        Image image;
        List<Transformation> transformations;
        List<Material> materiais;
        List<Light> lights;
        List<Triangle> triangles;
    
        public RayTracer()
        {
            transformations = new List<Transformation>();
            materiais = new List<Material>();
            lights = new List<Light>();
            triangles = new List<Triangle>();
        }

        PictureBox PicBoxScene;
        ProgressBar ProgressBarScene;
        NumericUpDown recursivityLevelDown;
        BackgroundWorker backgroundWorker;

        public RayTracer(PictureBox PictureBox, ProgressBar ProgressBar, BackgroundWorker bwork, NumericUpDown rec)
        {
            this.PicBoxScene = PictureBox;
            this.ProgressBarScene = ProgressBar;
            this.recursivityLevelDown = rec;
            this.backgroundWorker = bwork;

            transformations = new List<Transformation>();
            materiais = new List<Material>();
            lights = new List<Light>();
            triangles = new List<Triangle>();
        }

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public List<Light> Lights
        {
            get { return lights; }
            set { lights = value; }
        }

        public List<Material> Materiais
        {
            get { return materiais; }
            set { materiais = value; }
        }

        public List<Transformation> Transformations
        {
            get { return transformations; }
            set { transformations = value; }
        }

        public List<Triangle> Triangles
        {
            get { return triangles; }
            set { triangles = value; }
        }

       
        public void tracingRays()
        {

            double [] vec = new double [3];
            vec = MathHelper.GetHeighWidthAndPixelSize(camera, image);
         
            double height = vec[0];
            double width = vec[1];
            double pixelSize = vec[2];

            //raio primario
            Vector Origin = new Vector(0.0d, 0.0d, this.Camera.Distance);
            //rec 
            int rec = Int32.Parse(recursivityLevelDown.Value.ToString());

            initProgressBar();

            drawPicture(rec, height, width, pixelSize, Origin);
        }

         

        private void initProgressBar()
        {
            ProgressBarScene.BeginInvoke((MethodInvoker)delegate()
            {
                ProgressBarScene.Value = 0;
                ProgressBarScene.Maximum = this.Image.Vertical * this.Image.Horizontal;
            });
        }

        public void drawPicture(int rec, double height, double width, double pixelSize, Vector Origin)
        {
            //Coordenadas
            double px = 0, py = 0, pz = 0;

            //Imagem
            var PictureBoxSceneImage = new Bitmap(this.Image.Horizontal, this.Image.Vertical);

            //valor de progresso
            int progressBarValue = 0;
            try
            {
                Parallel.For(0, this.Image.Vertical, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (int j, ParallelLoopState loop) =>
                {
                    int invert_index_j = this.Image.Vertical - j - 1;
                    for (int i = 0; i < this.Image.Horizontal; i++)
                    {
                        px = MathHelper.calc_Pixel(pixelSize, i, width);
                        py = MathHelper.calc_Pixel(pixelSize, j, height);
                        Vector direction = MathHelper.calc_Vector_Direction(px, py, pz, Origin);
                        Ray primary_ray = new Ray(Origin, direction);
                        Color c = RayTrace(primary_ray, rec);
                        if (c.isValidColor())
                        {
                            System.Drawing.Color color = c.getARGB();

                            lock (PictureBoxSceneImage)
                            {
                                PictureBoxSceneImage.SetPixel(i, invert_index_j, color);
                            }
                        }
                        backgroundWorker.ReportProgress(progressBarValue++);
                    }
                });

                setImage(new Bitmap(PictureBoxSceneImage));
            }
            catch (Exception e)
            {
               
            }
        }

        private void setImage(Bitmap PictureBosSceneImage)
        {
            try
            {
                PicBoxScene.BeginInvoke((MethodInvoker)delegate()
                {
                    PicBoxScene.Image = PictureBosSceneImage;
                });
            }
            catch (Exception e)
            {
            }
        }

      

        private Color RayTrace(Ray ray, int rec)
        {
            Intersection firstIntersecion = Intersection.GetFirstIntersection(ray, Triangles);

            Vector intersectionPoint = firstIntersecion.intersectionPoint;
            Triangle firstTriangle = firstIntersecion.FirstTriangle;

            if (firstIntersecion.Distance < 0)
            {
                return image.Background;
            }
            else
            {
                Material m = firstTriangle.Material;
                //Luz ambiente
                Color color = new Color(0, 0, 0);
                color = AmbientLight(color, m);

                //Luz ambiente + difusa
                Vector Normal = firstTriangle.getNormal(intersectionPoint);
                color = DiffuseLight(color, m, intersectionPoint, Normal);

                if (rec > 0)
                {
                    //Refleccao
                    color = Reflection(color, m, intersectionPoint, Normal, ray.Direction, rec);
                    //Refracao
                    color = Refraction(color, m, ray.Direction, Normal, intersectionPoint, rec);
                }

                return color;
            }
        }

        //---- Luz ambiente
        private Color AmbientLight(Color colour, Material material)
        {
            foreach (Light light in lights)
            {
                colour += (light.Color * material.Color * material.AmbientCoe);
            }

            return colour;
        }

        //Luz difusa
        private Color DiffuseLight(Color color, Material m, Vector hitPoint, Vector Normal)
        {
            // Variables
            Ray ray;
            foreach (Light light in lights)
            {
                Vector L = light.Position/*Transformation.Translation*/ - hitPoint;
                double distance = L.Magnitude();
                L.Normalize();
                double cos_theta = Vector.DotProduct(Normal, L);
                ray = new Ray(hitPoint, L);
                if (!Shadows(ray, distance) && cos_theta > 0.0d)
                {
                    color += (light.Color * m.Color * m.DiffuseCoe * cos_theta);
                }
            }
            return color;
        }

        //Calcular as sombras para cada triangulo do solido
        private bool Shadows(Ray ray, double distance)
        {
            foreach (Triangle triangle in Triangles)
            {
                double t = triangle.RayIntersects(ray);

                if (t > 0.0d && t < distance)
                {
                    return true;
                }
            }
            return false;
        }

        private Color Reflection(Color color, Material m, Vector hitPoint, Vector Normal, Vector Direction, int nRecursive)
        {
            //Se tivermos niveis de recusividade
            if (m.ReflectionCoe > 0.0d)
            {

                Vector vectorReflection = MathHelper.calc_Ray_Refractor(Normal, Direction);
                Ray reflection = new Ray(hitPoint, vectorReflection);
                color += RayTrace(reflection, (nRecursive - 1)) * m.Color * m.ReflectionCoe;
            }

            return color;
        }

        private Color Refraction(Color color, Material material, Vector Direction, Vector Normal, Vector hitPoint, int nRecursive)
        {
            Vector N1 = new Vector();
            double n1 = 0d;
            double n2 = 0d;
            if (material.RefractionCoe > 0.0d)
            {
                // Raio a entrar
                if (RayIntersects(Direction, Normal))
                {
                    N1 = Normal;
                    n1 = 1.0d;
                    n2 = material.IndexRefraction;
                }
                else
                {
                    //Raio a sair
                    N1 = Vector.Neg(Normal);
                    n1 = material.IndexRefraction;
                    n2 = 1.0d;
                }
                Vector Rr = MathHelper.calc_Ray_Refractor(n1, n2, Direction, N1);
                Ray rayRefraction = new Ray(hitPoint, Rr);
                color += RayTrace(rayRefraction, nRecursive - 1) * material.Color * material.RefractionCoe;
            }
            return color;
        }

        private bool RayIntersects(Vector Direction, Vector Normal)
        {
            double cos_fi = Vector.DotProduct(Normal, Direction);
            if (cos_fi > 0.0d)
            {
                return false;
            }
            return true;
        }
    }
}
    
