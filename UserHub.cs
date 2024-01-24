using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieDatabase
{
    public partial class UserHub : Form
    {
        public UserHub()
        {
            InitializeComponent();
        }

        int curUser = 0;
        List<int> idArray = new List<int>();
        public UserHub(string username, int id)
        {
            string _user = username;
            curUser = id;
            MessageBox.Show(curUser.ToString());
            InitializeComponent();
            this.label2.Text = "Logged in as: " + _user;
        }

        
        

        //Used to take the byte array stored in the database and convert it back into an image.
        public Image byteArrayToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image image = Image.FromStream(ms);
            return image;
        }

        private void UserHub_Load(object sender, EventArgs e)
        {
            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");


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

                String newItem = movidID + " " + movidTitle;

                lstMovie.Items.Add(newItem);


            }
            //cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            // cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

            scn.Close();
        }

        /*
        private void btnShowList_Click(object sender, EventArgs e)
        {

            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");


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
            while(reader.Read())
            {
                String movidID = reader[0].ToString();
                String movidTitle = reader[1].ToString();
                String  movieRunTime= reader[2].ToString();

                String newItem = movidID + " " + movidTitle + " " + movieRunTime;

                lstMovie.Items.Add(newItem);


            }
            //cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            // cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

            scn.Close();
     
        }
        */
        private void lstMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            //This will run when the user clicks on a movie from lstMovie
            //Primary function is to change the poster in the picureBox and the details below to match the selection.
            //MessageBox.Show(lstMovie.SelectedIndex.ToString());
            int movId = idArray[lstMovie.SelectedIndex];
            try
            {
                using (SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True"))
                {
                    scn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Poster from Movie where Id=@Id", scn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", movId);
                    byte[] imageArray = (byte[])cmd.ExecuteScalar();
                    Image poster = byteArrayToImage(imageArray);
                    pictureBox1.Image = poster;
                    label3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                pictureBox1.Image = null;
                label3.Visible = true;
            }

            UpdateSelectInfo();
        }

        public void UpdateSelectInfo()
        {
            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");
            //This section will update the info displayed below the movie poster
            SqlCommand cmd = new SqlCommand("select * from Movie where Id=@Id", scn);
            int movId = idArray[lstMovie.SelectedIndex];
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", movId);
            scn.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //int movidID = Convert.ToInt32(reader[0]);
                String movidTitle = reader[1].ToString();
                int movieRunTime = Convert.ToInt32(reader[2]);
                int movieYear = Convert.ToInt32(reader[3]);
                string movieGenre = reader[4].ToString();
                string movieRating = reader[5].ToString();
                int movieLikes = Convert.ToInt32(reader[6]);


                //MessageBox.Show("Title: " + movidTitle + ", Runtime: " + movieRunTime + ", Year: " + movieYear + "" +
                    //", Genre: " + movieGenre + ", Rating: " + movieRating + ", Likes: " + movieLikes, movidTitle + " Details");

                lblGenre.Text = "Genre: " + movieGenre;
                lblRating.Text = "Rating: " + movieRating;
                lblRuntime.Text = "Runtime: " + movieRunTime.ToString();
                lblYear.Text = "Year: " + movieYear.ToString();
                lblLikes.Text = "Likes: " + movieLikes.ToString();
            }
            //cmd.Parameters.AddWithValue("@usr", txtUser.Text);
            // cmd.Parameters.AddWithValue("@pwd", txtPass.Text);

            scn.Close();
        }

        private void btnFav_Click(object sender, EventArgs e)
        {
            //When clicked, this button will set the user's favorite movie to the one selected in the list.
            //This will also adjust like counts in the table.
            int movId = idArray[lstMovie.SelectedIndex];
            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[User] SET FavMovie=@mov where Id=@usr; UPDATE Movie SET Likes = Likes + 1 where Id=@mov", scn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@mov", movId);
            cmd.Parameters.AddWithValue("@usr", curUser);
            //Open connection and execute command
            scn.Open();
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Select Title from Movie where Id=@mov", scn);
            cmd.Parameters.AddWithValue("@mov", movId);

            MessageBox.Show("Your favorite movie is now " + cmd.ExecuteScalar().ToString());
            //Close SQl connection and new user form
            scn.Close();


            
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
    }
}
