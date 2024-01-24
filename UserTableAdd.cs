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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MovieDatabase
{
    public partial class UserTableAdd : Form
    {
        public UserTableAdd()
        {
            InitializeComponent();
        }

        SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

        public Boolean checkExistingUser()
        { //Checking an existing username
          //Put this as one line. scn is any name you want to use for SqlConnection.
            SqlCommand scmd = new SqlCommand("select count (*) as cnt from [dbo].[User] where Username=@usr", scn);
            //scmd is any name you want to use for SqlCommand
            scmd.Parameters.Clear();
            //put textBox1.Text to @usr (a variable “ ”)
            scmd.Parameters.AddWithValue("@usr", txtUser.Text);
            scn.Open();
            Boolean myResult;
            if (scmd.ExecuteScalar().ToString() == "1")
            { //MessageBox.Show("Existing UserName");
                myResult = true;
            }
            else
            { //MessageBox.Show("New UserName");
                myResult = false;
            }
            scn.Close();
            return myResult;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!checkExistingUser())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[User] (Username, Password, FavMovie, IsAdmin) "
                + "VALUES (@usr, @pwd, 0, @admin)", scn);
                cmd.Parameters.AddWithValue("@usr", txtUser.Text);
                cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

                //Set IsAdmin field based on chkAdmin check box
                if (chkAdmin.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@admin", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@admin", "N");
                }

                scn.Open();
                cmd.ExecuteNonQuery();

                //Show dialog confirming that the user was added successfully
                MessageBox.Show("The user " + txtUser.Text + " was added to the database successfully!", "User Added");

                //Close SQl connection and new user form
                scn.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("This username is taken. Please enter a different username.", "Username In Use", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to cancel adding this user? Any unsaved changes will be lost!", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
