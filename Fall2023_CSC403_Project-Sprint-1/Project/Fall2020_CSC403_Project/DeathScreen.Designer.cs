using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    partial class DeathScreen
    {
        private System.ComponentModel.IContainer components = null;
        private Button closeButton;
        private Button restartButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.closeButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(458, 430);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(151, 90);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(756, 430);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(157, 90);
            this.restartButton.TabIndex = 1;
            this.restartButton.Text = "Restart";
            // 
            // DeathScreen
            // 
            this.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.DeathScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(937, 546);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.restartButton);
            this.DoubleBuffered = true;
            this.Name = "DeathScreen";
            this.Text = "DeathScreen";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
