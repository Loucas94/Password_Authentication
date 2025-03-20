using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Authentication
{
    public partial class Form1 : Form
    { 
        private Authentication auth = new Authentication();
    
        public Form1()
        {
            InitializeComponent();
        }

        private bool HasNumber(string input)
        {
            return input.Any(char.IsDigit); // Checks if the string contains at least one number
        }

        private bool HasUpperCase(string input)
        {
            return input.Any(char.IsUpper); // Checks if the string contains at least one uppercase letter
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (password.Length < 12 || !HasNumber(password) || !HasUpperCase(password))
            {
                label2.Text = "Password must be at least 12 characters long, contain a number and an uppercase letter.";
                return;
            }

            if (auth.RegisterUser(username, password))
            {
                label2.Text = "User registered successfully!";
            }
            else
            {
                label2.Text = "Username already exists!";
            
        }
    }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            string result = auth.AuthenticateUser(username, password);
            label2.Text = result;
            
                
                 
        }

     
}
    }

