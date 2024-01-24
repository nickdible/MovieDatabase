using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieDatabase
{
    public partial class MovieTableUpdate : Form
    {
        public MovieTableUpdate()
        {
            InitializeComponent();
        }

        SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

        int idToEdit;

        //Used to take the byte array stored in the database and convert it back into an image.
        public Image byteArrayToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image image = Image.FromStream(ms);
            return image;
        }


        public MovieTableUpdate(int id)
        {
            idToEdit = id;
            //MessageBox.Show(idToEdit.ToString());
            InitializeComponent();

            SqlCommand cmd = new SqlCommand("Select * from Movie where Id=@Id", scn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", idToEdit);
            scn.Open();

            //Initializing variables
            int movidID = 0;
            String movidTitle = null;
            int movieRunTime = 0;
            int movieYear = 0;
            string movieGenre = null;
            string movieRating = null;
            int movieLikes = 0;
            Image moviePoster = null;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movidID = Convert.ToInt32(reader[0]);
                movidTitle = reader[1].ToString();
                movieRunTime = Convert.ToInt32(reader[2]);
                movieYear = Convert.ToInt32(reader[3]);
                movieGenre = reader[4].ToString();
                movieRating = reader[5].ToString();
                movieLikes = Convert.ToInt32(reader[6]);
                


                //Adding movie IDs to idArray
                //idArray.Add(movidID);

                String newItem = movidID + " " + movidTitle + " " + movieRunTime + " " + movieYear + " " + 
                    movieGenre + " " + movieRating + " " + movieLikes;

                //lstMovie.Items.Add(newItem);
                //MessageBox.Show(newItem);

            }

            scn.Close();

            //Read byte array from database and convert it to an image to place in picPoster
            try
            {
                scn.Open();
                cmd = new SqlCommand("SELECT Poster from Movie where Id=@Id", scn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Id", idToEdit);
                byte[] imageArray = (byte[])cmd.ExecuteScalar();
                Image poster = byteArrayToImage(imageArray);
                picPoster.Image = poster;
                //label3.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //pictureBox1.Image = null;
                //label3.Visible = true;
            }

            scn.Close();

            //Insert information into text boxes to allow for editing
            txtTitle.Text = movidTitle;
            txtRuntime.Text = movieRunTime.ToString();
            txtYear.Text = movieYear.ToString();
            txtGenre.Text = movieGenre;
            dropRating.Text = movieRating;


        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            //Upload Poster button code
            openFileDialog1.Filter = "Image Files| *.jpg;*.png;*.jpeg";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Image poster = Image.FromFile(openFileDialog1.FileName);
                    picPoster.Image = poster;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Movie SET Title=@Title, Runtime=@Runtime, Year=@Year, Genre=@Genre, Rating=@Rating, Poster=@Img WHERE Id=@Id", scn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", idToEdit);
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
            int.TryParse(txtRuntime.Text, out int runtime);
            cmd.Parameters.AddWithValue("@Runtime", runtime);
            MessageBox.Show(runtime.ToString());
            int.TryParse(txtYear.Text, out int year);
            cmd.Parameters.AddWithValue("@Year", year);
            MessageBox.Show(year.ToString());
            //NOTE: May change Genre text box to combo box later
            cmd.Parameters.AddWithValue("@Genre", txtGenre.Text);
            string rate = dropRating.Text;
            MessageBox.Show(rate);
            cmd.Parameters.AddWithValue("@Rating", rate.Trim());

            //Convert bitmap image from picBox into byte array
            var image = picPoster.Image;
            //Check is user uploaded image
            if (image != null)
            {
                //If yes, convert image to byte array
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    //Take poster image from the poster picture box and add it to the database directly
                    cmd.Parameters.Add("@Img", SqlDbType.VarBinary).Value = ms.ToArray();
                }
            }
            else
            {
                //If no, set poster parameter = 0
                cmd.Parameters.Add("@Img", SqlDbType.VarBinary).Value = BitConverter.GetBytes(0);
            }

            //Open connection and execute command
            scn.Open();
            cmd.ExecuteNonQuery();

            //Show dialog confirming that the movie was added successfully
            MessageBox.Show("The movie " + txtTitle.Text + " was updated successfully!", "Movie Updated");

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
