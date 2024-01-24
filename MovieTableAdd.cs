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
    public partial class MovieTableAdd : Form
    {
        public MovieTableAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            SqlConnection scn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ndibl\OneDrive\Documents\CIT1701 - Intro to Logic and Visual Programming\MovieDatabase\MovieDatabase\User.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Movie] (Title, Runtime, Year, Genre, Rating, Likes, Poster) " +
                "VALUES (@Title, @Runtime, @Year, @Genre, @Rating, 0, @Poster)", scn);

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
                    cmd.Parameters.Add("@Poster", SqlDbType.VarBinary).Value = ms.ToArray();
                }
            }
            else
            {
                //If no, set poster parameter = 0
                cmd.Parameters.Add("@Poster", SqlDbType.VarBinary).Value = BitConverter.GetBytes(0);
            }
            

            //Open connection and execute command
            scn.Open();
            cmd.ExecuteNonQuery();

            //Show dialog confirming that the movie was added successfully
            MessageBox.Show("The movie " + txtTitle.Text + " was added to the database successfully!", "Movie Added");

            //Close SQl connection and new user form
            scn.Close();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Confirm dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to cancel adding a movie? Any unsaved changes will be lost!", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
