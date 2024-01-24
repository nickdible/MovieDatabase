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
    public partial class MovieTableEdit : Form
    {
        public MovieTableEdit()
        {
            InitializeComponent();
        }

        List<int> idArray = new List<int>();

        public void ReloadList()
        {
            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

            lstMovie.Items.Clear();
            idArray.Clear();

            SqlCommand cmd = new SqlCommand("select count(*) as cnt from [dbo].[User] where Username=@usr and Password=@pwd", scn);
            //cmd.Parameters.Clear();

            //cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            // cmd.Parameters.AddWithValue("@pwd", txtPass.Text);



            //MessageBox.Show("Welcome " + txtUser.Text);
            //admin check based on IsAdmin field
            cmd = new SqlCommand("select * from Movie", scn);
            cmd.Parameters.Clear();
            scn.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int movidID = Convert.ToInt32(reader[0]);
                String movidTitle = reader[1].ToString();
                int movieRunTime = Convert.ToInt32(reader[2]);
                int movieYear = Convert.ToInt32(reader[3]);
                string movieGenre = reader[4].ToString();
                string movieRating = reader[5].ToString();
                int movieLikes = Convert.ToInt32(reader[6]);

                //Adding movie IDs to idArray
                idArray.Add(movidID);


                String newItem = movidID + " " + movidTitle + " " + movieRunTime;

                lstMovie.Items.Add(newItem);


            }
            //cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            // cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

            scn.Close();
            //MessageBox.Show(idArray.Count().ToString());
        }

        private void lstMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            int movId = idArray[lstMovie.SelectedIndex];


        }

        private void MovieTableEdit_Load(object sender, EventArgs e)
        {
            ReloadList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Delete button code
            int id = idArray[lstMovie.SelectedIndex];
            //MessageBox.Show(id.ToString());

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this movie? This change cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                //Add delete code
                SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");


                SqlCommand cmd = new SqlCommand("DELETE FROM Movie where Id=@Id", scn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Id", id);

                //Open db connection and execute command
                scn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("The movie with ID " + id + " was deleted successfully.", "Success");
                //Refreshes lstMovie to reflect changes.
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = idArray[lstMovie.SelectedIndex];

            MovieTableUpdate editWin = new MovieTableUpdate(id);
            editWin.Show();
        }
    }
    }
