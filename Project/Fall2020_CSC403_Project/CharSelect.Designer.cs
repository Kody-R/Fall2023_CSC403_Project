using System.Resources;
using Fall2020_CSC403_Project.Properties;

namespace Fall2020_CSC403_Project
{
    partial class CharSelect
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
            this.btnChar1 = new System.Windows.Forms.Button();
            this.btnChar2 = new System.Windows.Forms.Button();
            this.btnChar3 = new System.Windows.Forms.Button();
            this.lblCharDesc = new System.Windows.Forms.Label();
            this.pbChar1 = new System.Windows.Forms.PictureBox();
            this.pbChar2 = new System.Windows.Forms.PictureBox();
            this.pbChar3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbChar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChar3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChar1
            // 
            this.btnChar1.Location = new System.Drawing.Point(152, 290);
            this.btnChar1.Name = "btnChar1";
            this.btnChar1.Size = new System.Drawing.Size(109, 50);
            this.btnChar1.TabIndex = 0;
            this.btnChar1.Text = "Character 1";
            this.btnChar1.UseVisualStyleBackColor = true;
            this.btnChar1.Click += new System.EventHandler(this.btnChar1_Click);
            // 
            // btnChar2
            // 
            this.btnChar2.Location = new System.Drawing.Point(371, 288);
            this.btnChar2.Name = "btnChar2";
            this.btnChar2.Size = new System.Drawing.Size(115, 54);
            this.btnChar2.TabIndex = 1;
            this.btnChar2.Text = "Character 2";
            this.btnChar2.UseVisualStyleBackColor = true;
            this.btnChar2.Click += new System.EventHandler(this.btnChar2_Click);
            // 
            // btnChar3
            // 
            this.btnChar3.Location = new System.Drawing.Point(560, 288);
            this.btnChar3.Name = "btnChar3";
            this.btnChar3.Size = new System.Drawing.Size(131, 52);
            this.btnChar3.TabIndex = 2;
            this.btnChar3.Text = "Character 3";
            this.btnChar3.UseVisualStyleBackColor = true;
            this.btnChar3.Click += new System.EventHandler(this.btnChar3_Click);
            // 
            // lblCharDesc
            // 
            this.lblCharDesc.AutoSize = true;
            this.lblCharDesc.Location = new System.Drawing.Point(356, 87);
            this.lblCharDesc.Name = "lblCharDesc";
            this.lblCharDesc.Size = new System.Drawing.Size(146, 16);
            this.lblCharDesc.TabIndex = 3;
            this.lblCharDesc.Text = "Choose Your Character";
            // 
            // pbChar1
            // 
            this.pbChar1.BackColor = System.Drawing.SystemColors.Control;
            this.pbChar1.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.knight;
            this.pbChar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbChar1.Location = new System.Drawing.Point(152, 154);
            this.pbChar1.Name = "pbChar1";
            this.pbChar1.Size = new System.Drawing.Size(100, 93);
            this.pbChar1.TabIndex = 4;
            this.pbChar1.TabStop = false;
            // 
            // pbChar2
            // 
            this.pbChar2.BackColor = System.Drawing.SystemColors.Control;
            this.pbChar2.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.spider;
            this.pbChar2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbChar2.Location = new System.Drawing.Point(371, 154);
            this.pbChar2.Name = "pbChar2";
            this.pbChar2.Size = new System.Drawing.Size(100, 93);
            this.pbChar2.TabIndex = 5;
            this.pbChar2.TabStop = false;
            // 
            // pbChar3
            // 
            this.pbChar3.BackColor = System.Drawing.SystemColors.Control;
            this.pbChar3.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.undead;
            this.pbChar3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbChar3.Location = new System.Drawing.Point(573, 154);
            this.pbChar3.Name = "pbChar3";
            this.pbChar3.Size = new System.Drawing.Size(100, 93);
            this.pbChar3.TabIndex = 6;
            this.pbChar3.TabStop = false;
            // 
            // CharSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbChar3);
            this.Controls.Add(this.pbChar2);
            this.Controls.Add(this.pbChar1);
            this.Controls.Add(this.lblCharDesc);
            this.Controls.Add(this.btnChar3);
            this.Controls.Add(this.btnChar2);
            this.Controls.Add(this.btnChar1);
            this.Name = "CharSelect";
            this.Text = "CharSelect";
            ((System.ComponentModel.ISupportInitialize)(this.pbChar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChar1;
        private System.Windows.Forms.Button btnChar2;
        private System.Windows.Forms.Button btnChar3;
        private System.Windows.Forms.Label lblCharDesc;
        private System.Windows.Forms.PictureBox pbChar1;
        private System.Windows.Forms.PictureBox pbChar2;
        private System.Windows.Forms.PictureBox pbChar3;
    }
}