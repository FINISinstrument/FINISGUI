namespace FinisGUI
{
    public partial class Form1 // Other part in Form1.cs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.Snap_Button = new System.Windows.Forms.Button();
            this.Record_Button = new System.Windows.Forms.Button();
            this.Timer1 = new System.Timers.Timer();
            this.StillName_textBox = new System.Windows.Forms.TextBox();
            this.StillName_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RecordName_Button = new System.Windows.Forms.Button();
            this.RecordName_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RecordName_Label = new System.Windows.Forms.Label();
            this.StillName_Label = new System.Windows.Forms.Label();
            this.promptBox = new System.Windows.Forms.RichTextBox();
            this.Clear_Button = new System.Windows.Forms.Button();
            this.Live_Button = new System.Windows.Forms.Button();
            this.FPS_Button = new System.Windows.Forms.Button();
            this.FPS_Label = new System.Windows.Forms.Label();
            this.PicCount_TextBox = new System.Windows.Forms.TextBox();
            this.PicCount_Label = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PicCount_Button = new System.Windows.Forms.Button();
            this.Exposure_Label = new System.Windows.Forms.Label();
            this.Exposure_TextBox = new System.Windows.Forms.TextBox();
            this.Exposure_Button = new System.Windows.Forms.Button();
            this.exposureInfo_Label = new System.Windows.Forms.Label();
            this.OpenShutter_Button = new System.Windows.Forms.Button();
            this.Gain_Button = new System.Windows.Forms.Button();
            this.Shutter_Label = new System.Windows.Forms.Label();
            this.Gain_Label = new System.Windows.Forms.Label();
            this.EmptyVideoFolder_Button = new System.Windows.Forms.Button();
            this.EmptyStillFolder_Button = new System.Windows.Forms.Button();
            this.ShutterLock_Button = new System.Windows.Forms.Button();
            this.Bits_Button = new System.Windows.Forms.Button();
            this.Bits_Label = new System.Windows.Forms.Label();
            this.ReinitializeSystem_Button = new System.Windows.Forms.Button();
            this.LockReinitialize_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CloseShutter_Button = new System.Windows.Forms.Button();
            this.InfoFile_Button = new System.Windows.Forms.Button();
            this.AppendImages_Button = new System.Windows.Forms.Button();
            this.ContinuityCheck_Button = new System.Windows.Forms.Button();
            this.Useless_Button = new System.Windows.Forms.Button();
            this.IMUData = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.BackColor = System.Drawing.Color.LightCyan;
            this.ImagePictureBox.BackgroundImage = global::FinisGUI.Properties.Resources.goldeye_g;
            this.ImagePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImagePictureBox.Location = new System.Drawing.Point(-1, 0);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(400, 320);
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            this.ImagePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImagePictureBox_Paint);
            // 
            // Snap_Button
            // 
            this.Snap_Button.BackColor = System.Drawing.Color.White;
            this.Snap_Button.ForeColor = System.Drawing.Color.Black;
            this.Snap_Button.Location = new System.Drawing.Point(12, 424);
            this.Snap_Button.Name = "Snap_Button";
            this.Snap_Button.Size = new System.Drawing.Size(60, 47);
            this.Snap_Button.TabIndex = 2;
            this.Snap_Button.Text = "Snap";
            this.Snap_Button.UseVisualStyleBackColor = false;
            this.Snap_Button.Click += new System.EventHandler(this.Snap_Button_Click);
            // 
            // Record_Button
            // 
            this.Record_Button.BackColor = System.Drawing.Color.White;
            this.Record_Button.ForeColor = System.Drawing.Color.Black;
            this.Record_Button.Location = new System.Drawing.Point(12, 477);
            this.Record_Button.Name = "Record_Button";
            this.Record_Button.Size = new System.Drawing.Size(60, 47);
            this.Record_Button.TabIndex = 3;
            this.Record_Button.Text = "Record";
            this.Record_Button.UseVisualStyleBackColor = false;
            this.Record_Button.Click += new System.EventHandler(this.Record_Button_Click);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.SynchronizingObject = this;
            this.Timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer1_Elapsed);
            // 
            // StillName_textBox
            // 
            this.StillName_textBox.Location = new System.Drawing.Point(549, 404);
            this.StillName_textBox.Name = "StillName_textBox";
            this.StillName_textBox.Size = new System.Drawing.Size(116, 20);
            this.StillName_textBox.TabIndex = 9;
            this.StillName_textBox.TextChanged += new System.EventHandler(this.StillName_textBox_TextChanged);
            // 
            // StillName_Button
            // 
            this.StillName_Button.BackColor = System.Drawing.Color.White;
            this.StillName_Button.ForeColor = System.Drawing.Color.Black;
            this.StillName_Button.Location = new System.Drawing.Point(668, 404);
            this.StillName_Button.Name = "StillName_Button";
            this.StillName_Button.Size = new System.Drawing.Size(75, 21);
            this.StillName_Button.TabIndex = 10;
            this.StillName_Button.Text = "Enter";
            this.StillName_Button.UseVisualStyleBackColor = false;
            this.StillName_Button.Click += new System.EventHandler(this.StillName_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(420, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Still Name:";
            // 
            // RecordName_Button
            // 
            this.RecordName_Button.BackColor = System.Drawing.Color.White;
            this.RecordName_Button.ForeColor = System.Drawing.Color.Black;
            this.RecordName_Button.Location = new System.Drawing.Point(668, 378);
            this.RecordName_Button.Name = "RecordName_Button";
            this.RecordName_Button.Size = new System.Drawing.Size(75, 20);
            this.RecordName_Button.TabIndex = 12;
            this.RecordName_Button.Text = "Enter";
            this.RecordName_Button.UseVisualStyleBackColor = false;
            this.RecordName_Button.Click += new System.EventHandler(this.RecordName_Button_Click);
            // 
            // RecordName_textBox
            // 
            this.RecordName_textBox.Location = new System.Drawing.Point(549, 378);
            this.RecordName_textBox.Name = "RecordName_textBox";
            this.RecordName_textBox.Size = new System.Drawing.Size(116, 20);
            this.RecordName_textBox.TabIndex = 13;
            this.RecordName_textBox.TextChanged += new System.EventHandler(this.RecordName_textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(401, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Record Name:";
            // 
            // RecordName_Label
            // 
            this.RecordName_Label.AutoSize = true;
            this.RecordName_Label.BackColor = System.Drawing.Color.LightCyan;
            this.RecordName_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecordName_Label.Location = new System.Drawing.Point(483, 378);
            this.RecordName_Label.Name = "RecordName_Label";
            this.RecordName_Label.Size = new System.Drawing.Size(64, 15);
            this.RecordName_Label.TabIndex = 15;
            this.RecordName_Label.Text = "videoFrame";
            // 
            // StillName_Label
            // 
            this.StillName_Label.AutoSize = true;
            this.StillName_Label.BackColor = System.Drawing.Color.LightCyan;
            this.StillName_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StillName_Label.Location = new System.Drawing.Point(483, 404);
            this.StillName_Label.Name = "StillName_Label";
            this.StillName_Label.Size = new System.Drawing.Size(45, 15);
            this.StillName_Label.TabIndex = 16;
            this.StillName_Label.Text = "stillShot";
            // 
            // promptBox
            // 
            this.promptBox.Location = new System.Drawing.Point(405, 5);
            this.promptBox.Name = "promptBox";
            this.promptBox.ReadOnly = true;
            this.promptBox.Size = new System.Drawing.Size(345, 315);
            this.promptBox.TabIndex = 18;
            this.promptBox.Text = "";
            // 
            // Clear_Button
            // 
            this.Clear_Button.BackColor = System.Drawing.Color.White;
            this.Clear_Button.ForeColor = System.Drawing.Color.Black;
            this.Clear_Button.Location = new System.Drawing.Point(405, 326);
            this.Clear_Button.Name = "Clear_Button";
            this.Clear_Button.Size = new System.Drawing.Size(62, 25);
            this.Clear_Button.TabIndex = 19;
            this.Clear_Button.Text = "Clear";
            this.Clear_Button.UseVisualStyleBackColor = false;
            this.Clear_Button.Click += new System.EventHandler(this.Clear_Button_Click);
            // 
            // Live_Button
            // 
            this.Live_Button.BackColor = System.Drawing.Color.White;
            this.Live_Button.ForeColor = System.Drawing.Color.Black;
            this.Live_Button.Location = new System.Drawing.Point(12, 371);
            this.Live_Button.Name = "Live_Button";
            this.Live_Button.Size = new System.Drawing.Size(60, 47);
            this.Live_Button.TabIndex = 20;
            this.Live_Button.Text = "Live";
            this.Live_Button.UseVisualStyleBackColor = false;
            this.Live_Button.Click += new System.EventHandler(this.Live_Button_Click);
            // 
            // FPS_Button
            // 
            this.FPS_Button.BackColor = System.Drawing.Color.White;
            this.FPS_Button.ForeColor = System.Drawing.Color.Black;
            this.FPS_Button.Location = new System.Drawing.Point(144, 477);
            this.FPS_Button.Name = "FPS_Button";
            this.FPS_Button.Size = new System.Drawing.Size(60, 47);
            this.FPS_Button.TabIndex = 26;
            this.FPS_Button.Text = "Frame Rate (fps)";
            this.FPS_Button.UseVisualStyleBackColor = false;
            this.FPS_Button.Click += new System.EventHandler(this.FPS_Button_Click);
            // 
            // FPS_Label
            // 
            this.FPS_Label.AutoSize = true;
            this.FPS_Label.BackColor = System.Drawing.Color.LightCyan;
            this.FPS_Label.Location = new System.Drawing.Point(185, 511);
            this.FPS_Label.Name = "FPS_Label";
            this.FPS_Label.Size = new System.Drawing.Size(19, 13);
            this.FPS_Label.TabIndex = 28;
            this.FPS_Label.Text = "30";
            // 
            // PicCount_TextBox
            // 
            this.PicCount_TextBox.Location = new System.Drawing.Point(549, 353);
            this.PicCount_TextBox.Name = "PicCount_TextBox";
            this.PicCount_TextBox.Size = new System.Drawing.Size(116, 20);
            this.PicCount_TextBox.TabIndex = 34;
            this.PicCount_TextBox.TextChanged += new System.EventHandler(this.PicCount_TextBox_TextChanged);
            // 
            // PicCount_Label
            // 
            this.PicCount_Label.AutoSize = true;
            this.PicCount_Label.BackColor = System.Drawing.Color.LightCyan;
            this.PicCount_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicCount_Label.Location = new System.Drawing.Point(483, 353);
            this.PicCount_Label.Name = "PicCount_Label";
            this.PicCount_Label.Size = new System.Drawing.Size(21, 15);
            this.PicCount_Label.TabIndex = 33;
            this.PicCount_Label.Text = "40";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(407, 353);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Frame Count:";
            // 
            // PicCount_Button
            // 
            this.PicCount_Button.BackColor = System.Drawing.Color.White;
            this.PicCount_Button.ForeColor = System.Drawing.Color.Black;
            this.PicCount_Button.Location = new System.Drawing.Point(668, 353);
            this.PicCount_Button.Name = "PicCount_Button";
            this.PicCount_Button.Size = new System.Drawing.Size(75, 20);
            this.PicCount_Button.TabIndex = 31;
            this.PicCount_Button.Text = "Enter";
            this.PicCount_Button.UseVisualStyleBackColor = false;
            this.PicCount_Button.Click += new System.EventHandler(this.PicCount_Button_Click);
            // 
            // Exposure_Label
            // 
            this.Exposure_Label.AutoSize = true;
            this.Exposure_Label.BackColor = System.Drawing.Color.LightCyan;
            this.Exposure_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Exposure_Label.Location = new System.Drawing.Point(483, 431);
            this.Exposure_Label.Name = "Exposure_Label";
            this.Exposure_Label.Size = new System.Drawing.Size(39, 15);
            this.Exposure_Label.TabIndex = 38;
            this.Exposure_Label.Text = "33000";
            // 
            // Exposure_TextBox
            // 
            this.Exposure_TextBox.Location = new System.Drawing.Point(549, 431);
            this.Exposure_TextBox.Name = "Exposure_TextBox";
            this.Exposure_TextBox.Size = new System.Drawing.Size(116, 20);
            this.Exposure_TextBox.TabIndex = 43;
            this.Exposure_TextBox.TextChanged += new System.EventHandler(this.Exposure_TextBox_TextChanged);
            // 
            // Exposure_Button
            // 
            this.Exposure_Button.BackColor = System.Drawing.Color.White;
            this.Exposure_Button.ForeColor = System.Drawing.Color.Black;
            this.Exposure_Button.Location = new System.Drawing.Point(668, 431);
            this.Exposure_Button.Name = "Exposure_Button";
            this.Exposure_Button.Size = new System.Drawing.Size(75, 20);
            this.Exposure_Button.TabIndex = 42;
            this.Exposure_Button.Text = "Enter";
            this.Exposure_Button.UseVisualStyleBackColor = false;
            this.Exposure_Button.Click += new System.EventHandler(this.Exposure_Button_Click);
            // 
            // exposureInfo_Label
            // 
            this.exposureInfo_Label.AutoSize = true;
            this.exposureInfo_Label.BackColor = System.Drawing.Color.Transparent;
            this.exposureInfo_Label.Location = new System.Drawing.Point(346, 431);
            this.exposureInfo_Label.Name = "exposureInfo_Label";
            this.exposureInfo_Label.Size = new System.Drawing.Size(131, 13);
            this.exposureInfo_Label.TabIndex = 44;
            this.exposureInfo_Label.Text = "Exposure (up to 33000μs):";
            // 
            // OpenShutter_Button
            // 
            this.OpenShutter_Button.BackColor = System.Drawing.Color.White;
            this.OpenShutter_Button.ForeColor = System.Drawing.Color.Black;
            this.OpenShutter_Button.Location = new System.Drawing.Point(78, 371);
            this.OpenShutter_Button.Name = "OpenShutter_Button";
            this.OpenShutter_Button.Size = new System.Drawing.Size(60, 47);
            this.OpenShutter_Button.TabIndex = 51;
            this.OpenShutter_Button.Text = "Open Shutter";
            this.OpenShutter_Button.UseVisualStyleBackColor = false;
            this.OpenShutter_Button.Click += new System.EventHandler(this.OpenShutter_Button_Click);
            // 
            // Gain_Button
            // 
            this.Gain_Button.BackColor = System.Drawing.Color.White;
            this.Gain_Button.ForeColor = System.Drawing.Color.Black;
            this.Gain_Button.Location = new System.Drawing.Point(144, 371);
            this.Gain_Button.Name = "Gain_Button";
            this.Gain_Button.Size = new System.Drawing.Size(60, 47);
            this.Gain_Button.TabIndex = 52;
            this.Gain_Button.Text = "Gain";
            this.Gain_Button.UseVisualStyleBackColor = false;
            this.Gain_Button.Click += new System.EventHandler(this.Gain_Button_Click);
            // 
            // Shutter_Label
            // 
            this.Shutter_Label.AutoSize = true;
            this.Shutter_Label.BackColor = System.Drawing.Color.LightCyan;
            this.Shutter_Label.Location = new System.Drawing.Point(99, 405);
            this.Shutter_Label.Name = "Shutter_Label";
            this.Shutter_Label.Size = new System.Drawing.Size(39, 13);
            this.Shutter_Label.TabIndex = 53;
            this.Shutter_Label.Text = "Closed";
            // 
            // Gain_Label
            // 
            this.Gain_Label.AutoSize = true;
            this.Gain_Label.BackColor = System.Drawing.Color.LightCyan;
            this.Gain_Label.Location = new System.Drawing.Point(175, 405);
            this.Gain_Label.Name = "Gain_Label";
            this.Gain_Label.Size = new System.Drawing.Size(29, 13);
            this.Gain_Label.TabIndex = 56;
            this.Gain_Label.Text = "High";
            // 
            // EmptyVideoFolder_Button
            // 
            this.EmptyVideoFolder_Button.BackColor = System.Drawing.Color.White;
            this.EmptyVideoFolder_Button.ForeColor = System.Drawing.Color.Black;
            this.EmptyVideoFolder_Button.Location = new System.Drawing.Point(210, 424);
            this.EmptyVideoFolder_Button.Name = "EmptyVideoFolder_Button";
            this.EmptyVideoFolder_Button.Size = new System.Drawing.Size(86, 47);
            this.EmptyVideoFolder_Button.TabIndex = 57;
            this.EmptyVideoFolder_Button.Text = "Empty Video Folder";
            this.EmptyVideoFolder_Button.UseVisualStyleBackColor = false;
            this.EmptyVideoFolder_Button.Click += new System.EventHandler(this.EmptyVideoFolder_Button_Click);
            // 
            // EmptyStillFolder_Button
            // 
            this.EmptyStillFolder_Button.BackColor = System.Drawing.Color.White;
            this.EmptyStillFolder_Button.ForeColor = System.Drawing.Color.Black;
            this.EmptyStillFolder_Button.Location = new System.Drawing.Point(210, 477);
            this.EmptyStillFolder_Button.Name = "EmptyStillFolder_Button";
            this.EmptyStillFolder_Button.Size = new System.Drawing.Size(86, 46);
            this.EmptyStillFolder_Button.TabIndex = 58;
            this.EmptyStillFolder_Button.Text = "Empty Still Image Folder";
            this.EmptyStillFolder_Button.UseVisualStyleBackColor = false;
            this.EmptyStillFolder_Button.Click += new System.EventHandler(this.EmptyStillFolder_Button_Click);
            // 
            // ShutterLock_Button
            // 
            this.ShutterLock_Button.BackColor = System.Drawing.Color.White;
            this.ShutterLock_Button.ForeColor = System.Drawing.Color.Black;
            this.ShutterLock_Button.Location = new System.Drawing.Point(78, 477);
            this.ShutterLock_Button.Name = "ShutterLock_Button";
            this.ShutterLock_Button.Size = new System.Drawing.Size(60, 47);
            this.ShutterLock_Button.TabIndex = 59;
            this.ShutterLock_Button.Text = "Lock Shutter";
            this.ShutterLock_Button.UseVisualStyleBackColor = false;
            this.ShutterLock_Button.Click += new System.EventHandler(this.ShutterLock_Button_Click);
            // 
            // Bits_Button
            // 
            this.Bits_Button.BackColor = System.Drawing.Color.White;
            this.Bits_Button.ForeColor = System.Drawing.Color.Black;
            this.Bits_Button.Location = new System.Drawing.Point(144, 424);
            this.Bits_Button.Name = "Bits_Button";
            this.Bits_Button.Size = new System.Drawing.Size(60, 47);
            this.Bits_Button.TabIndex = 60;
            this.Bits_Button.Text = "Display Bit-Value";
            this.Bits_Button.UseVisualStyleBackColor = false;
            this.Bits_Button.Click += new System.EventHandler(this.Bits_Button_Click);
            // 
            // Bits_Label
            // 
            this.Bits_Label.AutoSize = true;
            this.Bits_Label.BackColor = System.Drawing.Color.LightCyan;
            this.Bits_Label.Location = new System.Drawing.Point(171, 458);
            this.Bits_Label.Name = "Bits_Label";
            this.Bits_Label.Size = new System.Drawing.Size(33, 13);
            this.Bits_Label.TabIndex = 61;
            this.Bits_Label.Text = "16-bit";
            // 
            // ReinitializeSystem_Button
            // 
            this.ReinitializeSystem_Button.BackColor = System.Drawing.Color.DarkGray;
            this.ReinitializeSystem_Button.Location = new System.Drawing.Point(210, 369);
            this.ReinitializeSystem_Button.Name = "ReinitializeSystem_Button";
            this.ReinitializeSystem_Button.Size = new System.Drawing.Size(86, 47);
            this.ReinitializeSystem_Button.TabIndex = 62;
            this.ReinitializeSystem_Button.Text = "Reinitialize System";
            this.ReinitializeSystem_Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ReinitializeSystem_Button.UseVisualStyleBackColor = false;
            this.ReinitializeSystem_Button.Click += new System.EventHandler(this.ReinitializeSystem_Button_Click);
            // 
            // LockReinitialize_Button
            // 
            this.LockReinitialize_Button.BackColor = System.Drawing.Color.White;
            this.LockReinitialize_Button.Location = new System.Drawing.Point(210, 400);
            this.LockReinitialize_Button.Name = "LockReinitialize_Button";
            this.LockReinitialize_Button.Size = new System.Drawing.Size(70, 23);
            this.LockReinitialize_Button.TabIndex = 63;
            this.LockReinitialize_Button.Text = "Locked";
            this.LockReinitialize_Button.UseVisualStyleBackColor = false;
            this.LockReinitialize_Button.Click += new System.EventHandler(this.LockReinitialize_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(22, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Capture";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(22, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Control";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(87, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Control";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(87, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Shutter";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(231, 353);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 71;
            this.label10.Text = "Control";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(231, 340);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 70;
            this.label11.Text = "System";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(152, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 73;
            this.label8.Text = "Control";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(152, 341);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Camera";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(546, 326);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 74;
            this.label12.Text = "Misc Controls";
            // 
            // CloseShutter_Button
            // 
            this.CloseShutter_Button.BackColor = System.Drawing.Color.White;
            this.CloseShutter_Button.ForeColor = System.Drawing.Color.Black;
            this.CloseShutter_Button.Location = new System.Drawing.Point(78, 425);
            this.CloseShutter_Button.Name = "CloseShutter_Button";
            this.CloseShutter_Button.Size = new System.Drawing.Size(60, 47);
            this.CloseShutter_Button.TabIndex = 75;
            this.CloseShutter_Button.Text = "Close Shutter";
            this.CloseShutter_Button.UseVisualStyleBackColor = false;
            this.CloseShutter_Button.Click += new System.EventHandler(this.CloseShutter_Button_Click);
            // 
            // InfoFile_Button
            // 
            this.InfoFile_Button.BackColor = System.Drawing.Color.White;
            this.InfoFile_Button.ForeColor = System.Drawing.Color.Black;
            this.InfoFile_Button.Location = new System.Drawing.Point(587, 456);
            this.InfoFile_Button.Name = "InfoFile_Button";
            this.InfoFile_Button.Size = new System.Drawing.Size(75, 49);
            this.InfoFile_Button.TabIndex = 76;
            this.InfoFile_Button.Text = "Produce Info File";
            this.InfoFile_Button.UseVisualStyleBackColor = false;
            this.InfoFile_Button.Click += new System.EventHandler(this.InfoFile_Button_Click);
            // 
            // AppendImages_Button
            // 
            this.AppendImages_Button.BackColor = System.Drawing.Color.White;
            this.AppendImages_Button.Location = new System.Drawing.Point(668, 457);
            this.AppendImages_Button.Name = "AppendImages_Button";
            this.AppendImages_Button.Size = new System.Drawing.Size(75, 48);
            this.AppendImages_Button.TabIndex = 77;
            this.AppendImages_Button.Text = "Append Info to Images";
            this.AppendImages_Button.UseVisualStyleBackColor = false;
            this.AppendImages_Button.Click += new System.EventHandler(this.AppendImages_Button_Click);
            // 
            // ContinuityCheck_Button
            // 
            this.ContinuityCheck_Button.Location = new System.Drawing.Point(506, 456);
            this.ContinuityCheck_Button.Name = "ContinuityCheck_Button";
            this.ContinuityCheck_Button.Size = new System.Drawing.Size(75, 49);
            this.ContinuityCheck_Button.TabIndex = 78;
            this.ContinuityCheck_Button.Text = "Continuity Check";
            this.ContinuityCheck_Button.UseVisualStyleBackColor = true;
            this.ContinuityCheck_Button.Click += new System.EventHandler(this.ContinuityCheck_Button_Click);
            // 
            // Useless_Button
            // 
            this.Useless_Button.BackColor = System.Drawing.Color.White;
            this.Useless_Button.Location = new System.Drawing.Point(320, 359);
            this.Useless_Button.Name = "Useless_Button";
            this.Useless_Button.Size = new System.Drawing.Size(75, 52);
            this.Useless_Button.TabIndex = 79;
            this.Useless_Button.Text = "Useless Button";
            this.Useless_Button.UseVisualStyleBackColor = false;
            this.Useless_Button.Click += new System.EventHandler(this.Useless_Button_Click);
            // 
            // IMUData
            // 
            this.IMUData.AutoSize = true;
            this.IMUData.Location = new System.Drawing.Point(22, 323);
            this.IMUData.Name = "IMUData";
            this.IMUData.Size = new System.Drawing.Size(27, 13);
            this.IMUData.TabIndex = 80;
            this.IMUData.Text = "IMU";
            this.IMUData.UseMnemonic = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.PicCount_Button;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(755, 525);
            this.Controls.Add(this.IMUData);
            this.Controls.Add(this.Useless_Button);
            this.Controls.Add(this.ContinuityCheck_Button);
            this.Controls.Add(this.AppendImages_Button);
            this.Controls.Add(this.InfoFile_Button);
            this.Controls.Add(this.Gain_Label);
            this.Controls.Add(this.Shutter_Label);
            this.Controls.Add(this.Gain_Button);
            this.Controls.Add(this.OpenShutter_Button);
            this.Controls.Add(this.CloseShutter_Button);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LockReinitialize_Button);
            this.Controls.Add(this.ReinitializeSystem_Button);
            this.Controls.Add(this.Exposure_Label);
            this.Controls.Add(this.FPS_Label);
            this.Controls.Add(this.Bits_Label);
            this.Controls.Add(this.Bits_Button);
            this.Controls.Add(this.ShutterLock_Button);
            this.Controls.Add(this.EmptyStillFolder_Button);
            this.Controls.Add(this.EmptyVideoFolder_Button);
            this.Controls.Add(this.exposureInfo_Label);
            this.Controls.Add(this.Exposure_TextBox);
            this.Controls.Add(this.Exposure_Button);
            this.Controls.Add(this.PicCount_TextBox);
            this.Controls.Add(this.PicCount_Label);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PicCount_Button);
            this.Controls.Add(this.FPS_Button);
            this.Controls.Add(this.Live_Button);
            this.Controls.Add(this.Clear_Button);
            this.Controls.Add(this.promptBox);
            this.Controls.Add(this.RecordName_textBox);
            this.Controls.Add(this.RecordName_Button);
            this.Controls.Add(this.StillName_Button);
            this.Controls.Add(this.StillName_textBox);
            this.Controls.Add(this.ImagePictureBox);
            this.Controls.Add(this.StillName_Label);
            this.Controls.Add(this.RecordName_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Record_Button);
            this.Controls.Add(this.Snap_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "FINIS";
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Timers.Timer Timer1;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.Button Snap_Button;
        private System.Windows.Forms.Button Record_Button;
        private System.Windows.Forms.TextBox StillName_textBox;
        private System.Windows.Forms.Button StillName_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RecordName_textBox;
        private System.Windows.Forms.Button RecordName_Button;
        private System.Windows.Forms.Label StillName_Label;
        private System.Windows.Forms.Label RecordName_Label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox promptBox;
        private System.Windows.Forms.Button Clear_Button;
        private System.Windows.Forms.Button Live_Button;
        private System.Windows.Forms.Label FPS_Label;
        private System.Windows.Forms.Button FPS_Button;
        private System.Windows.Forms.TextBox PicCount_TextBox;
        private System.Windows.Forms.Label PicCount_Label;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button PicCount_Button;
        private System.Windows.Forms.Label Exposure_Label;
        private System.Windows.Forms.TextBox Exposure_TextBox;
        private System.Windows.Forms.Button Exposure_Button;
        private System.Windows.Forms.Label exposureInfo_Label;
        private System.Windows.Forms.Button OpenShutter_Button;
        private System.Windows.Forms.Button Gain_Button;
        private System.Windows.Forms.Label Gain_Label;
        private System.Windows.Forms.Label Shutter_Label;
        private System.Windows.Forms.Button EmptyVideoFolder_Button;
        private System.Windows.Forms.Button EmptyStillFolder_Button;
        private System.Windows.Forms.Button ShutterLock_Button;
        private System.Windows.Forms.Label Bits_Label;
        private System.Windows.Forms.Button Bits_Button;
        private System.Windows.Forms.Button LockReinitialize_Button;
        private System.Windows.Forms.Button ReinitializeSystem_Button;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseShutter_Button;
        private System.Windows.Forms.Button InfoFile_Button;
        private System.Windows.Forms.Button AppendImages_Button;
        private System.Windows.Forms.Button ContinuityCheck_Button;
        private System.Windows.Forms.Button Useless_Button;
        private System.Windows.Forms.Label IMUData;
    }
}
