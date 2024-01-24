using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieDatabase
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        //Put this as one line. scn is any name you want to use for SqlConnection.
        SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select count(*) as cnt from [dbo].[User] where Username=@usr and Password=@pwd", scn);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            cmd.Parameters.AddWithValue("@pwd", txtPass.Text);
            scn.Open();

            if (cmd.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("Welcome " + txtUser.Text);
                //admin check based on IsAdmin field
                cmd = new SqlCommand("select IsAdmin from [dbo].[User] where Username=@usr and Password=@pwd", scn);
                cmd.Parameters.AddWithValue("@usr", txtUser.Text);
                cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

                if (cmd.ExecuteScalar().ToString() == "Y")
                {
                    MessageBox.Show("You have administrative privileges. Entering admin mode!");
                    AdminHub adminWin = new AdminHub();
                    adminWin.Show();
                }
                else
                {
                    MessageBox.Show("You do not have admin privileges. Entering normal mode!");

                    //Gets the user ID of the user currently logging in. This will be used for the favorite feature.
                    cmd = new SqlCommand("select Id from [dbo].[User] where Username=@usr and Password=@pwd", scn);
                    cmd.Parameters.AddWithValue("@usr", txtUser.Text);
                    cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

                    UserHub userWin = new UserHub(this.txtUser.Text, Convert.ToInt32(cmd.ExecuteScalar()));
                    userWin.Show();
                }
            }
            else
            {
                MessageBox.Show("User " + txtUser.Text + " does not exist.");
            }
            scn.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            UserSignUp regForm = new UserSignUp();
            regForm.Show();
        }
    }
}
