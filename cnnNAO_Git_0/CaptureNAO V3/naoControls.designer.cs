namespace CameraCapture
{
    partial class naoControls
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
            this.headUp = new System.Windows.Forms.Button();
            this.headDown = new System.Windows.Forms.Button();
            this.headRight = new System.Windows.Forms.Button();
            this.headLeft = new System.Windows.Forms.Button();
            this.headReset = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headUp
            // 
            this.headUp.Location = new System.Drawing.Point(126, 26);
            this.headUp.Name = "headUp";
            this.headUp.Size = new System.Drawing.Size(53, 47);
            this.headUp.TabIndex = 2;
            this.headUp.Text = "Up";
            this.headUp.UseVisualStyleBackColor = true;
            this.headUp.Click += new System.EventHandler(this.headUp_Click);
            // 
            // headDown
            // 
            this.headDown.Location = new System.Drawing.Point(126, 154);
            this.headDown.Name = "headDown";
            this.headDown.Size = new System.Drawing.Size(53, 47);
            this.headDown.TabIndex = 3;
            this.headDown.Text = "Down";
            this.headDown.UseVisualStyleBackColor = true;
            this.headDown.Click += new System.EventHandler(this.headDown_Click);
            // 
            // headRight
            // 
            this.headRight.Location = new System.Drawing.Point(202, 89);
            this.headRight.Name = "headRight";
            this.headRight.Size = new System.Drawing.Size(53, 47);
            this.headRight.TabIndex = 4;
            this.headRight.Text = "Right";
            this.headRight.UseVisualStyleBackColor = true;
            this.headRight.Click += new System.EventHandler(this.headRight_Click);
            // 
            // headLeft
            // 
            this.headLeft.Location = new System.Drawing.Point(49, 89);
            this.headLeft.Name = "headLeft";
            this.headLeft.Size = new System.Drawing.Size(53, 47);
            this.headLeft.TabIndex = 5;
            this.headLeft.Text = "Left";
            this.headLeft.UseVisualStyleBackColor = true;
            this.headLeft.Click += new System.EventHandler(this.headLeft_Click);
            // 
            // headReset
            // 
            this.headReset.Location = new System.Drawing.Point(118, 89);
            this.headReset.Name = "headReset";
            this.headReset.Size = new System.Drawing.Size(67, 47);
            this.headReset.TabIndex = 6;
            this.headReset.Text = "Center";
            this.headReset.UseVisualStyleBackColor = true;
            this.headReset.Click += new System.EventHandler(this.headReset_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 46);
            this.button2.TabIndex = 7;
            this.button2.Text = "Example Move";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // naoControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 360);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.headReset);
            this.Controls.Add(this.headLeft);
            this.Controls.Add(this.headRight);
            this.Controls.Add(this.headDown);
            this.Controls.Add(this.headUp);
            this.Name = "naoControls";
            this.Text = "naoControls";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button headUp;
        private System.Windows.Forms.Button headDown;
        private System.Windows.Forms.Button headRight;
        private System.Windows.Forms.Button headLeft;
        private System.Windows.Forms.Button headReset;
        private System.Windows.Forms.Button button2;
    }
}