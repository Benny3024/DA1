namespace Đồ_án
{
    partial class Form1
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbport = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btndisconnect = new System.Windows.Forms.Button();
            this.btnconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbaudrate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtsetpoint = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbcontrolmode = new System.Windows.Forms.ComboBox();
            this.btnreset = new System.Windows.Forms.Button();
            this.btnrun = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.recievedata = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKd = new System.Windows.Forms.TextBox();
            this.txtKi = new System.Windows.Forms.TextBox();
            this.txtKp = new System.Windows.Forms.TextBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbport
            // 
            this.cbport.FormattingEnabled = true;
            this.cbport.Location = new System.Drawing.Point(81, 32);
            this.cbport.Margin = new System.Windows.Forms.Padding(2);
            this.cbport.Name = "cbport";
            this.cbport.Size = new System.Drawing.Size(71, 21);
            this.cbport.TabIndex = 0;
            this.cbport.SelectedIndexChanged += new System.EventHandler(this.cbport_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btndisconnect);
            this.groupBox1.Controls.Add(this.btnconnect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbaudrate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbport);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(184, 185);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 97);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(172, 19);
            this.progressBar1.TabIndex = 6;
            // 
            // btndisconnect
            // 
            this.btndisconnect.BackColor = System.Drawing.Color.Red;
            this.btndisconnect.Location = new System.Drawing.Point(94, 129);
            this.btndisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btndisconnect.Name = "btndisconnect";
            this.btndisconnect.Size = new System.Drawing.Size(85, 41);
            this.btndisconnect.TabIndex = 5;
            this.btndisconnect.Text = "Disconnect";
            this.btndisconnect.UseVisualStyleBackColor = false;
            this.btndisconnect.Click += new System.EventHandler(this.btndisconnect_Click);
            // 
            // btnconnect
            // 
            this.btnconnect.BackColor = System.Drawing.Color.Lime;
            this.btnconnect.Location = new System.Drawing.Point(4, 129);
            this.btnconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(86, 41);
            this.btnconnect.TabIndex = 2;
            this.btnconnect.Text = "Connect";
            this.btnconnect.UseVisualStyleBackColor = false;
            this.btnconnect.Click += new System.EventHandler(this.btnconnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Baudrate";
            // 
            // cbbaudrate
            // 
            this.cbbaudrate.FormattingEnabled = true;
            this.cbbaudrate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.cbbaudrate.Location = new System.Drawing.Point(81, 62);
            this.cbbaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.cbbaudrate.Name = "cbbaudrate";
            this.cbbaudrate.Size = new System.Drawing.Size(71, 21);
            this.cbbaudrate.TabIndex = 3;
            this.cbbaudrate.Text = "9600";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtsetpoint);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbcontrolmode);
            this.groupBox2.Controls.Add(this.btnreset);
            this.groupBox2.Controls.Add(this.btnrun);
            this.groupBox2.Location = new System.Drawing.Point(9, 200);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(184, 210);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motion Control";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Load",
            "No Load"});
            this.comboBox1.Location = new System.Drawing.Point(81, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(98, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "No Load";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Control Load";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 123);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Set Point";
            // 
            // txtsetpoint
            // 
            this.txtsetpoint.Location = new System.Drawing.Point(81, 123);
            this.txtsetpoint.Margin = new System.Windows.Forms.Padding(2);
            this.txtsetpoint.Multiline = true;
            this.txtsetpoint.Name = "txtsetpoint";
            this.txtsetpoint.Size = new System.Drawing.Size(98, 19);
            this.txtsetpoint.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Control Mode";
            // 
            // cbcontrolmode
            // 
            this.cbcontrolmode.FormattingEnabled = true;
            this.cbcontrolmode.Items.AddRange(new object[] {
            "Position",
            "Velocity"});
            this.cbcontrolmode.Location = new System.Drawing.Point(81, 80);
            this.cbcontrolmode.Margin = new System.Windows.Forms.Padding(2);
            this.cbcontrolmode.Name = "cbcontrolmode";
            this.cbcontrolmode.Size = new System.Drawing.Size(98, 21);
            this.cbcontrolmode.TabIndex = 4;
            this.cbcontrolmode.Text = "Position";
            this.cbcontrolmode.SelectedIndexChanged += new System.EventHandler(this.cbcontrolmode_SelectedIndexChanged);
            // 
            // btnreset
            // 
            this.btnreset.Location = new System.Drawing.Point(95, 159);
            this.btnreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(85, 46);
            this.btnreset.TabIndex = 3;
            this.btnreset.Text = "RESET";
            this.btnreset.UseVisualStyleBackColor = true;
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // btnrun
            // 
            this.btnrun.Location = new System.Drawing.Point(8, 159);
            this.btnrun.Margin = new System.Windows.Forms.Padding(2);
            this.btnrun.Name = "btnrun";
            this.btnrun.Size = new System.Drawing.Size(82, 46);
            this.btnrun.TabIndex = 2;
            this.btnrun.Text = "RUN";
            this.btnrun.UseVisualStyleBackColor = true;
            this.btnrun.Click += new System.EventHandler(this.btnrun_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.recievedata);
            this.groupBox3.Location = new System.Drawing.Point(197, 200);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(158, 210);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Receive Data";
            // 
            // recievedata
            // 
            this.recievedata.Location = new System.Drawing.Point(4, 17);
            this.recievedata.Margin = new System.Windows.Forms.Padding(2);
            this.recievedata.Multiline = true;
            this.recievedata.Name = "recievedata";
            this.recievedata.Size = new System.Drawing.Size(150, 188);
            this.recievedata.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtKd);
            this.groupBox4.Controls.Add(this.txtKi);
            this.groupBox4.Controls.Add(this.txtKp);
            this.groupBox4.Location = new System.Drawing.Point(197, 11);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(158, 184);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PID Control";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 93);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ki";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Kd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Kp";
            // 
            // txtKd
            // 
            this.txtKd.Location = new System.Drawing.Point(68, 141);
            this.txtKd.Margin = new System.Windows.Forms.Padding(2);
            this.txtKd.Multiline = true;
            this.txtKd.Name = "txtKd";
            this.txtKd.Size = new System.Drawing.Size(72, 29);
            this.txtKd.TabIndex = 2;
            // 
            // txtKi
            // 
            this.txtKi.Location = new System.Drawing.Point(68, 85);
            this.txtKi.Margin = new System.Windows.Forms.Padding(2);
            this.txtKi.Multiline = true;
            this.txtKi.Name = "txtKi";
            this.txtKi.Size = new System.Drawing.Size(72, 29);
            this.txtKi.TabIndex = 1;
            // 
            // txtKp
            // 
            this.txtKp.Location = new System.Drawing.Point(68, 31);
            this.txtKp.Margin = new System.Windows.Forms.Padding(2);
            this.txtKp.Multiline = true;
            this.txtKp.Name = "txtKp";
            this.txtKp.Size = new System.Drawing.Size(72, 29);
            this.txtKp.TabIndex = 0;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(361, 11);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(669, 398);
            this.zedGraphControl1.TabIndex = 7;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 428);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btndisconnect;
        private System.Windows.Forms.Button btnconnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbaudrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbcontrolmode;
        private System.Windows.Forms.Button btnreset;
        private System.Windows.Forms.Button btnrun;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKd;
        private System.Windows.Forms.TextBox txtKi;
        private System.Windows.Forms.TextBox txtKp;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtsetpoint;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TextBox recievedata;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

