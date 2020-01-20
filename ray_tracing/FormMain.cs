using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COSIG_RAY_TRACING
{
    public partial class FormMain : Form
    {
        private RayTracer scene;
        private Parser openfile;
        int seconds, minutes, hours;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            picScene.Left = (this.Width - scene.Image.Horizontal) / 2;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            var load_File = new System.Windows.Forms.OpenFileDialog();

            if (load_File.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string OpenFile = load_File.FileName;

                if (string.IsNullOrWhiteSpace(OpenFile) || !OpenFile.EndsWith(".txt"))
                {
                    MessageBox.Show("Select file!");
                    return;
                }
                openfile = new Parser(OpenFile);
                //Load file and create the scene
                scene = openfile.ReadFile(picScene, progressBar, bgWorker, recursivityLevelDown);
                MessageBox.Show("Scene loaded!");
                button.Enabled = true;
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.PerformStep();
        }

        private void Clear()
        {           
            picScene.BeginInvoke((MethodInvoker)delegate()
            {
                picScene.Image = new Bitmap(picScene.Width, picScene.Height);
            });
            LB_time.BeginInvoke((MethodInvoker)delegate()
            {
            });
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Limpar componentes
            Clear();

            //Conta so tempo para raytrace da scene
            Stopwatch Elapsedtimer = new Stopwatch();
            Elapsedtimer.Start();
            //Método para construir os raios primários
            scene.tracingRays();
            Elapsedtimer.Stop();
            timer1.Stop();
            //Escreve o tempo na lbl
            LB_time.BeginInvoke((MethodInvoker)delegate()
            {
                TimeSpan ts = Elapsedtimer.Elapsed;
             
            });
        }

        private void start_Click(object sender, EventArgs e)
        {
            timer1.Start();
            resetTime();

            if (openfile != null)
            {
                //A cena e lida no DoWork do bgWorker
                if (bgWorker.IsBusy != true)
                {
                    bgWorker.RunWorkerAsync();
                }
            }
        }

        private void resetTime()
        {
            seconds = 0;
            minutes = 0;
            hours = 0;
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
            string Scene_File = "scene";

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap Image | *.bmp";
            sf.DefaultExt = ".bmp";
            sf.FileName = Scene_File;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (picScene.Image != null && picScene.Image is Bitmap)
                    {
                        //Get Scene PictureBox Scene Image
                        Bitmap PictureBoxSceneImage = (Bitmap)picScene.Image;
                        PictureBoxSceneImage.Save(sf.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        MessageBox.Show("Image saved!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Message = {1}", ex);
                }
            }
        }

        private void ReloadScene_ValueChanged(object sender, EventArgs e)
        {
            if (openfile != null)
            {
                //New Scene
                scene = openfile.ReadFile(picScene, progressBar, bgWorker, recursivityLevelDown);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }

            String sec = seconds + "", min = minutes + "", hour = hours + "";

            if (seconds < 100)
            {
                sec = "" + sec;
            } if (minutes < 100)
            {
                min = "0" + min;
            }
            if (hours < 10)
            {
                hour = "0" + hour;
            }

            LB_time.Text = "Time: " + hour + ":" + min + ":" + sec;

            if (seconds == 100)
            {
                timer1.Stop();
            }
        }

    }
}
