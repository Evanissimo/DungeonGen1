namespace DungeonGen1
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
            this.generate = new System.Windows.Forms.Button();
            this.exportText = new System.Windows.Forms.Button();
            this.exportImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(665, 785);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(75, 23);
            this.generate.TabIndex = 0;
            this.generate.Text = "Generate";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // exportText
            // 
            this.exportText.AutoSize = true;
            this.exportText.Location = new System.Drawing.Point(287, 784);
            this.exportText.Name = "exportText";
            this.exportText.Size = new System.Drawing.Size(90, 23);
            this.exportText.TabIndex = 1;
            this.exportText.Text = "Export Text File";
            this.exportText.UseVisualStyleBackColor = true;
            // 
            // exportImage
            // 
            this.exportImage.AutoSize = true;
            this.exportImage.Location = new System.Drawing.Point(1079, 783);
            this.exportImage.Name = "exportImage";
            this.exportImage.Size = new System.Drawing.Size(79, 23);
            this.exportImage.TabIndex = 2;
            this.exportImage.Text = "Export Image";
            this.exportImage.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(98, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1272, 677);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1464, 861);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.exportImage);
            this.Controls.Add(this.exportText);
            this.Controls.Add(this.generate);
            this.Name = "Form1";
            this.Text = "Dungeon Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Button exportText;
        private System.Windows.Forms.Button exportImage;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

