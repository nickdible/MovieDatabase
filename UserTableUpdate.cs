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
    public partial class UserTableUpdate : Form
    {
        public UserTableUpdate()
        {
            InitializeComponent();
        }

        SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");
        int idToEd = 0;

        

        public UserTableUpdate(int id)
        {
            idToEd = id;

            MessageBox.Show(idToEd.ToString());
            InitializeComponent();

            SqlCommand cmd = new SqlCommand("Select * from [dbo].[User] where Id=@Id", scn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", idToEd);
            scn.Open();

            //Initializing variables
            int userId = 0;
            string userName = null;
            string userPass = null;
            int userFav = 0;
            string userAdmin = null;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userId = Convert.ToInt32(reader[0]);
                userName = reader[1].ToString();
                userPass = reader[2].ToString();
                userFav = Convert.ToInt32(reader[3]);
                userAdmin = reader[4].ToString();
            }

            //MessageBox.Show(userName);
            //MessageBox.Show(userPass);

            //Placing text from table into text boxes to allow for editing
            txtUser.Text = userName;
            txtPass.Text = userPass;

            //Set isAdmin checkbox based on data stored in database
            if (userAdmin == "Y")
            {
                chkAdmin.Checked = true;
            }
            else
            {
                chkAdmin.Checked = false;
            }
            scn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[User] SET Username=@Name, Password=@Pass, IsAdmin=@Admin WHERE Id=@Id", scn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", idToEd);
            cmd.Parameters.AddWithValue("@Name", txtUser.Text);
            cmd.Parameters.AddWithValue("@Pass", txtPass.Text);

            //Update isAdmin based on checkbox
            if (chkAdmin.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Admin", "Y");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Admin", "N");
            }

            //Open connection and execute command
            scn.Open();
            cmd.ExecuteNonQuery();

            //Show dialog confirming that the movie was added successfully
            MessageBox.Show("The user " + txtUser.Text + " was updated successfully!", "User Updated");

            //Close SQl connection and new user form
            scn.Close();
            this.Close();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to cancel this operation? Any unsaved changes will be lost!", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
