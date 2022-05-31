namespace BarcodeScanner
{
    partial class barcode_scanner
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.TxtBox_Barcode = new System.Windows.Forms.TextBox();
            this.ComboBox_Camera = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(32, 98);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(659, 370);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(651, 474);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(120, 51);
            this.Btn_Start.TabIndex = 1;
            this.Btn_Start.Text = "start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            // 
            // TxtBox_Barcode
            // 
            this.TxtBox_Barcode.Location = new System.Drawing.Point(127, 486);
            this.TxtBox_Barcode.Name = "TxtBox_Barcode";
            this.TxtBox_Barcode.Size = new System.Drawing.Size(452, 22);
            this.TxtBox_Barcode.TabIndex = 2;
            // 
            // ComboBox_Camera
            // 
            this.ComboBox_Camera.FormattingEnabled = true;
            this.ComboBox_Camera.Location = new System.Drawing.Point(235, 43);
            this.ComboBox_Camera.Name = "ComboBox_Camera";
            this.ComboBox_Camera.Size = new System.Drawing.Size(388, 24);
            this.ComboBox_Camera.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Camera:";
            // 
            // camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 544);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboBox_Camera);
            this.Controls.Add(this.TxtBox_Barcode);
            this.Controls.Add(this.Btn_Start);
            this.Controls.Add(this.pictureBox);
            this.Name = "camera";
            this.Text = "Camera";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.TextBox TxtBox_Barcode;
        private System.Windows.Forms.ComboBox ComboBox_Camera;
        private System.Windows.Forms.Label label1;
    }
}