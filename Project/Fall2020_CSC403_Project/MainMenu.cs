using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CharSelect charSelect = new CharSelect();
            this.Hide();
            charSelect.Show();
        }

        private void btnControls_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            controller.Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
