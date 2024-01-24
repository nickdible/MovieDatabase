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
    public partial class UserTableEdit : Form
    {
        public UserTableEdit()
        {
            InitializeComponent();
        }

        List<int> idArray = new List<int>();
        SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");


        public void ReloadList()
        {
            lstUser.Items.Clear();
            idArray.Clear();

            SqlCommand cmd = new SqlCommand("Select * from [dbo].[User]", scn);
            cmd.Parameters.Clear();
            scn.Open();


            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userID = Convert.ToInt32(reader[0]);
                String userName = reader[1].ToString();
                string userPass = reader[2].ToString();
                int userFav = Convert.ToInt32(reader[3]);
                string userAdmin = reader[4].ToString();


                //Adding movie IDs to idArray
                idArray.Add(userID);


                String newItem = userID + " " + userName + " " + userAdmin;

                lstUser.Items.Add(newItem);


            }
            scn.Close();
        }

        private void UserTableEdit_Load(object sender, EventArgs e)
        {
            ReloadList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = idArray[lstUser.SelectedIndex];

            UserTableUpdate editWin = new UserTableUpdate(id);
            editWin.Show();
            ReloadList();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //Delete button code
            int id = idArray[lstUser.SelectedIndex];
            MessageBox.Show(id.ToString());

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this user? This change cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                //Add delete code
                SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");


                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[User] where Id=@Id", scn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Id", id);

                //Open db connection and execute command
                scn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("The user with ID " + id + " was deleted successfully.", "Success");
                //Refreshes lstUser to reflect changes.
                ReloadList();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to exit? Any unsaved changes will be lost!", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}