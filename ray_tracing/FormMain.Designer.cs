namespace COSIG_RAY_TRACING
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picScene = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.LB_time = new System.Windows.Forms.Label();
            this.recursivityLevelDown = new System.Windows.Forms.NumericUpDown();
            this.btn_load = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScene)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recursivityLevelDown)).BeginInit();
            this.SuspendLayout();
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.picScene);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 217);
            this.panel2.TabIndex = 14;
            // 
            // picScene
            // 
            this.picScene.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picScene.Location = new System.Drawing.Point(8, 10);
            this.picScene.Name = "picScene";
            this.picScene.Size = new System.Drawing.Size(200, 200);
            this.picScene.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picScene.TabIndex = 5;
            this.picScene.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.recursivityLevelDown);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(233, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 137);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(408, 220);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(120, 16);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 2;
            // 
            // LB_time
            // 
            this.LB_time.AutoSize = true;
            this.LB_time.Location = new System.Drawing.Point(413, 201);
            this.LB_time.Name = "LB_time";
            this.LB_time.Size = new System.Drawing.Size(0, 13);
            this.LB_time.TabIndex = 7;
            // 
            // recursivityLevelDown
            // 
            this.recursivityLevelDown.Location = new System.Drawing.Point(138, 82);
            this.recursivityLevelDown.Name = "recursivityLevelDown";
            this.recursivityLevelDown.Size = new System.Drawing.Size(34, 20);
            this.recursivityLevelDown.TabIndex = 6;
            this.recursivityLevelDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(17, 24);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(73, 30);
            this.btn_load.TabIndex = 0;
            this.btn_load.Text = "Load Scene";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Recursivity depth:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button
            // 
            this.button.Enabled = false;
            this.button.Location = new System.Drawing.Point(190, 24);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(98, 93);
            this.button.TabIndex = 10;
            this.button.Text = "Start";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.start_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save Preview";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveImage_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(540, 248);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.LB_time);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Ray Tracer";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScene)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recursivityLevelDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picScene;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label LB_time;
        private System.Windows.Forms.NumericUpDown recursivityLevelDown;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}

