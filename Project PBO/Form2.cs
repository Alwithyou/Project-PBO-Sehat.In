﻿using Project_PBO.App.Context;
using Project_PBO.App.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Project_PBO
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using Npgsql; // Add Npgsql namespace

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = tbemail.Text.Trim();
            string password = tbpass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                // Menyiapkan query dengan parameter
                string query = "SELECT * FROM profil WHERE email = @email AND password = @password";
                NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@email", email),
                new NpgsqlParameter("@password", password)
            };

                // Menggunakan metode queryExecutor untuk mengeksekusi query dan mendapatkan hasil
                DataTable dt = dataconn.queryExecutor(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    // Mendapatkan id_jenis_profil dari hasil query
                    int idJenisProfil = Convert.ToInt32(dt.Rows[0]["id_jenis_profil"]);

                    // Menentukan apakah pengguna adalah admin atau user berdasarkan id_jenis_profil
                    if (idJenisProfil == 1) // Misalkan 1 adalah untuk admin
                    {
                        User newForm = new User();
                        newForm.Show();
                        this.Hide();
                        newForm.FormClosed += (s, args) => this.Close();
                    }
                    else if (idJenisProfil == 2) // Misalkan 2 adalah untuk user
                    {
                        Admin newForm = new Admin();
                        newForm.Show();
                        this.Hide();
                        newForm.FormClosed += (s, args) => this.Close();
                    }
                    else
                    {
                        MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    // Authentication failed
                    MessageBox.Show("Email or password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
