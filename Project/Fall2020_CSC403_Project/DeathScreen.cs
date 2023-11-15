using System;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class DeathScreen : Form
    {
        public DeathScreen()
        {
            InitializeComponent();
            SetupButtons();

            // Attach the Load event handler
            this.Load += DeathScreen_Load;
        }

        private void DeathScreen_Load(object sender, EventArgs e)
        {
            // Set the form to open in fullscreen when the form is loaded
            this.WindowState = FormWindowState.Maximized;
        }

        private void SetupButtons()
        {
            // Set button properties
            closeButton.Text = "Close";
            restartButton.Text = "Restart";

            // Set button click event handlers
            closeButton.Click += CloseButton_Click;
            restartButton.Click += RestartButton_Click;

            // Position buttons
            PositionButtons();
        }

        private void PositionButtons()
        {
            // Adjust button positions based on the form size or specific layout preferences
            // For example:
            closeButton.Location = new System.Drawing.Point(350, 550);
            restartButton.Location = new System.Drawing.Point(1000, 550);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the entire application
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Application.Restart(); // Restart the entire application
        }

        private void DeathScreen_Load_1(object sender, EventArgs e)
        {

        }
    }
}
