using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class CharSelect : Form
    {
        public CharSelect()
        {
            InitializeComponent();
        }

        private void btnChar1_Click(object sender, EventArgs e)
        {
            FrmLevel frmLevel = new FrmLevel(1);
            this.Hide();
            frmLevel.Show();

        }

        private void btnChar2_Click(object sender, EventArgs e)
        {
            FrmLevel frmLevel = new FrmLevel(2);
            this.Hide();
            frmLevel.Show();
        }

        private void btnChar3_Click(object sender, EventArgs e)
        {
            FrmLevel frmLevel = new FrmLevel(3);
            this.Hide();
            frmLevel.Show();
        }

    }
}
