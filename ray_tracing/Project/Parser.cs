using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COSIG_RAY_TRACING
{
    public class Parser
    {
        private string FileName;
        List<string> values;

        public Parser(string fileName)
        {
            try
            {
                this.FileName = fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public RayTracer ReadFile(PictureBox PictureBoxScene, ProgressBar ProgressBarScene, BackgroundWorker bw, NumericUpDown recursivityLevel)
        {
            try
            {
                RayTracer scene = new RayTracer(PictureBoxScene, ProgressBarScene, bw, recursivityLevel);
                string[] lines = System.IO.File.ReadAllLines(@FileName);

                for (int i = 0; i < lines.Length; i++)
                {
                    switch (lines[i])
                    {
                        case "Camera":

                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            for (int j = 0; j < 4; j++)
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }

                            //// Pass the last useless line
                            //i++;

                            // Parse values into new object
                            {
                                int tIndex = int.Parse(values[0], CultureInfo.InvariantCulture);
                                double distance = double.Parse(values[1], CultureInfo.InvariantCulture);
                                double field_of_view = double.Parse(values[2], CultureInfo.InvariantCulture);
                                scene.Camera = new Camera(scene.Transformations[tIndex], distance, field_of_view);
                            }

                            break;

                        case "Image":

                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            for (int j = 0; j < 2; j++)
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }

                            // Pass the last useless line
                            i++;

                            // Parse values into new object
                            {
                                string[] location = values[0].Split(' ');
                                int horizontal = Convert.ToInt32(location[0]);
                                int vertical = Convert.ToInt32(location[1]);
                                //
                                string[] colorValues = values[1].Split('\t')[1].Split(' ');
                                Color color = new Color(double.Parse(colorValues[0], CultureInfo.InvariantCulture), double.Parse(colorValues[1], CultureInfo.InvariantCulture), double.Parse(colorValues[2], CultureInfo.InvariantCulture));
                                scene.Image = new Image(horizontal, vertical, color);
                            }

                            break;

                        case "Light":

                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            for (int j = 0; j < 2; j++)
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }

                            // Pass the last useless line
                            i++;

                            // Parse values into new object
                            {
                                int lIndex = int.Parse(values[0], CultureInfo.InvariantCulture);
                                var trans = scene.Transformations[lIndex];
                                Vector position = new Vector (trans.Translation);
                                var tcam = scene.Transformations[1];
                                position = tcam.TransformPoint(position);
                                string[] colorValues = values[1].Split(' ');
                                Color color = new Color(double.Parse(colorValues[0], CultureInfo.InvariantCulture), double.Parse(colorValues[1], CultureInfo.InvariantCulture), double.Parse(colorValues[2], CultureInfo.InvariantCulture));


                                scene.Lights.Add(new Light(position, color));
                            }
                            break;
   
                        case "Material":
                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            for (int j = 0; j < 2; j++)
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }
                            // Pass the last useless line
                            i++;
                            // Parse values into new object
                            {
                                string[] colorValues = values[0].Split(' ');
                                string[] status = values[1].Split(' ');
                                double ambient = double.Parse(status[0], CultureInfo.InvariantCulture);
                                double diffuse = double.Parse(status[1], CultureInfo.InvariantCulture);
                                double reflection = double.Parse(status[2], CultureInfo.InvariantCulture);
                                double refraction = double.Parse(status[3], CultureInfo.InvariantCulture);
                                double index_refraction = double.Parse(status[4], CultureInfo.InvariantCulture);
                                Color color = new Color(double.Parse(colorValues[0], CultureInfo.InvariantCulture), double.Parse(colorValues[1], CultureInfo.InvariantCulture), double.Parse(colorValues[2], CultureInfo.InvariantCulture));
                                scene.Materiais.Add(new Material(color, ambient, diffuse, reflection, refraction, index_refraction));
                            }
                            break;

                        case "Triangles":
                            Triangles tri = new Triangles();
                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            while (lines[i] != "}")
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }

                            // Pass the last useless line
                            i++;

                            //get index of transformation
                            tri.Transformation = scene.Transformations[int.Parse(values[0], CultureInfo.InvariantCulture)];
                            values.RemoveAt(0);

                            // Parse values into new object
                            {
                                int numberOfTriangles = values.Count / 4;

                                List<Triangle> listTriangle = new List<Triangle>();

                                for (int k = 0; k < numberOfTriangles; k++)
                                {
                                    int index = 4 * k;
                                    //
                                    int material = Convert.ToInt32(values[index]);
                                    index++;
                                    //
                                    string[] coordinates = values[index].Split(' ');
                                    double x = double.Parse(coordinates[0], CultureInfo.InvariantCulture);
                                    double y = double.Parse(coordinates[1], CultureInfo.InvariantCulture);
                                    double z = double.Parse(coordinates[2], CultureInfo.InvariantCulture);
                                    Vector vec1 = new Vector(x, y, z);
                                    index++;
                                    //
                                    coordinates = values[index].Split(' ');
                                    x = double.Parse(coordinates[0], CultureInfo.InvariantCulture);
                                    y = double.Parse(coordinates[1], CultureInfo.InvariantCulture);
                                    z = double.Parse(coordinates[2], CultureInfo.InvariantCulture);
                                    Vector vec2 = new Vector(x, y, z);
                                    index++;
                                    //
                                    coordinates = values[index].Split(' ');
                                    x = double.Parse(coordinates[0], CultureInfo.InvariantCulture);
                                    y = double.Parse(coordinates[1], CultureInfo.InvariantCulture);
                                    z = double.Parse(coordinates[2], CultureInfo.InvariantCulture);
                                    Vector vec3 = new Vector(x, y, z);

                                    var tCam = scene.Transformations[1];

                                    Triangle triangle = new Triangle(scene.Materiais[material], tCam.TransformPoint(vec1), tCam.TransformPoint(vec2), tCam.TransformPoint(vec3));

                                    scene.Triangles.Add(triangle);
                                    tri.addTriangleToListOfTriangles(triangle);
                                }
                            }
                            break;

                        case "Transformation":
                            // Get the values
                            values = new List<string>();

                            // Get of the label line and pass the first useless line
                            i += 2;

                            // Cycle for the values
                            while (lines[i] != "}")
                            {
                                // Read the values
                                values.Add(lines[i]);
                                i++;
                            }

                            // Pass the last useless line
                            i++;

                            if (values.Count == 0)
                            {
                                scene.Transformations.Add(new Transformation());
                            }
                            else
                            {
                                Transformation trans = new Transformation();
                                bool identity = false;
                                Vector s = null, t = null;
                                double rX = 0, rY = 0, rZ = 0;
                                foreach (string val in values)
                                {
                                    
                                    if (val.Contains("I"))
                                    {
                                        identity = true;
                                        trans.Identity = identity;
                                    }
                                    else if (val.Contains("T"))
                                    {
                                        string[] splitT = val.Split(' ');
                                        t = new Vector(double.Parse(splitT[1], CultureInfo.InvariantCulture), double.Parse(splitT[2], CultureInfo.InvariantCulture), double.Parse(splitT[3], CultureInfo.InvariantCulture));
                                        trans.Translation = t;
                                        if (splitT[1] == "0" && splitT[2] == "0" && splitT[3] == "-74") { identity = true; trans.Identity = identity; }
                                        System.Diagnostics.Debug.WriteLine("i "+identity+" t: x" + splitT[1] + " y " + splitT[2] + " z " + splitT[3]);
                                        trans.Translate();
                                    }
                                    else if (val.Contains("Rx"))
                                    {
                                        string[] splitRx = val.Split(' ');
                                        rX = double.Parse(splitRx[1], CultureInfo.InvariantCulture);
                                        rX = Math.Abs(rX);
                                        System.Diagnostics.Debug.WriteLine("rX: " + rX);
                                        System.Diagnostics.Debug.WriteLine("splitRx[1]: " + splitRx[1]);
                                        trans.Rotation.X = rX;
                                        System.Diagnostics.Debug.WriteLine("trans.Rotation.X: " + trans.Rotation.X);
                                        trans.RotateX();
                                    }
                                    else if (val.Contains("Ry"))
                                    {
                                        string[] splitRy = val.Split(' ');
                                        rY = double.Parse(splitRy[1], CultureInfo.InvariantCulture);
                                        trans.Rotation.Y = rY;
                                        trans.RotateY();
                                    }
                                    else if (val.Contains("Rz"))
                                    {
                                        string[] splitRz = val.Split(' ');
                                        rZ = double.Parse(splitRz[1], CultureInfo.InvariantCulture);
                                        trans.Rotation.Z = rZ;
                                        trans.RotateZ();
                                    }
                                    else if (val.Contains("S"))
                                    {
                                        string[] splitS = val.Split(' ');
                                        s = new Vector(double.Parse(splitS[1], CultureInfo.InvariantCulture), double.Parse(splitS[2], CultureInfo.InvariantCulture), double.Parse(splitS[3], CultureInfo.InvariantCulture));
                                        trans.Scale = s;
                                        trans.ScaleTrans();
                                    }
                                    System.Diagnostics.Debug.WriteLine("transformation- " + val.Length + " val: " + val);
                                }
                                
                                scene.Transformations.Add(trans);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return scene;
            }
            catch (Exception e)
            {
                return new RayTracer();
            }
        }

       

    }
}
