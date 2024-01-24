namespace MovieDatabase
{
    partial class AdminHub
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenUserDB = new System.Windows.Forms.Button();
            this.btnOpenMovieDB = new System.Windows.Forms.Button();
            this.btnEditMovie = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to the Admin Page!";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(664, 383);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(124, 55);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Log Out";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(297, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Administrative Tasks";
            // 
            // btnOpenUserDB
            // 
            this.btnOpenUserDB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenUserDB.Location = new System.Drawing.Point(176, 123);
            this.btnOpenUserDB.Name = "btnOpenUserDB";
            this.btnOpenUserDB.Size = new System.Drawing.Size(177, 59);
            this.btnOpenUserDB.TabIndex = 3;
            this.btnOpenUserDB.Text = "Add New User";
            this.btnOpenUserDB.UseVisualStyleBackColor = true;
            this.btnOpenUserDB.Click += new System.EventHandler(this.btnOpenUserDB_Click);
            // 
            // btnOpenMovieDB
            // 
            this.btnOpenMovieDB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenMovieDB.Location = new System.Drawing.Point(404, 123);
            this.btnOpenMovieDB.Name = "btnOpenMovieDB";
            this.btnOpenMovieDB.Size = new System.Drawing.Size(177, 59);
            this.btnOpenMovieDB.TabIndex = 4;
            this.btnOpenMovieDB.Text = "Add New Movie";
            this.btnOpenMovieDB.UseVisualStyleBackColor = true;
            this.btnOpenMovieDB.Click += new System.EventHandler(this.btnOpenMovieDB_Click);
            // 
            // btnEditMovie
            // 
            this.btnEditMovie.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMovie.Location = new System.Drawing.Point(404, 209);
            this.btnEditMovie.Name = "btnEditMovie";
            this.btnEditMovie.Size = new System.Drawing.Size(177, 59);
            this.btnEditMovie.TabIndex = 6;
            this.btnEditMovie.Text = "View/Edit Movie Table";
            this.btnEditMovie.UseVisualStyleBackColor = true;
            this.btnEditMovie.Click += new System.EventHandler(this.btnEditMovie_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditUser.Location = new System.Drawing.Point(176, 209);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(177, 59);
            this.btnEditUser.TabIndex = 5;
            this.btnEditUser.Text = "View/Edit User Table";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEditMovie);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnOpenMovieDB);
            this.Controls.Add(this.btnOpenUserDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Movie Database - Admin Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenUserDB;
        private System.Windows.Forms.Button btnOpenMovieDB;
        private System.Windows.Forms.Button btnEditMovie;
        private System.Windows.Forms.Button btnEditUser;
    }
}