using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieDatabase
{
    public partial class AdminHub : Form
    {
        public AdminHub()
        {
            InitializeComponent();
        }

        private void btnOpenUserDB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening User database!");
            UserTableAdd userDbWin = new UserTableAdd();
            userDbWin.Show();
        }

        private void btnOpenMovieDB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening Movie database!");
            MovieTableAdd movieDbWin = new MovieTableAdd();
            movieDbWin.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to log out? Any unsaved changes will be lost!", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            UserTableEdit userEd = new UserTableEdit();
            userEd.Show();
        }

        private void btnEditMovie_Click(object sender, EventArgs e)
        {
            MovieTableEdit movieEd = new MovieTableEdit();
            movieEd.Show();
        }
    }
}
