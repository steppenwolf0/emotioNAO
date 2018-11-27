namespace CameraCapture
{
    partial class CameraCapture
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
            ReleaseData();
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
            this.captureButton = new System.Windows.Forms.Button();
            this.captureImageBox = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtStatus = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.captureImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(13, 12);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(90, 36);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButtonClick);
            // 
            // captureImageBox
            // 
            this.captureImageBox.Location = new System.Drawing.Point(13, 71);
            this.captureImageBox.Name = "captureImageBox";
            this.captureImageBox.Size = new System.Drawing.Size(520, 362);
            this.captureImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.captureImageBox.TabIndex = 3;
            this.captureImageBox.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(562, 316);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(114, 117);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(562, 77);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(253, 114);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(831, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 55);
            this.button1.TabIndex = 6;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "Controls";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 523);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.captureImageBox);
            this.Controls.Add(this.captureButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CameraCapture";
            this.Text = "Camera Capture";
            this.Load += new System.EventHandler(this.CameraCapture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.captureImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.PictureBox captureImageBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RichTextBox txtStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

