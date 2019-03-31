using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Ionic.Zip;
using System.IO;

namespace SenLogic
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtPassword.Focus();

            //try
            //{

            // string Date=EncryptorDecrypt(File.ReadAllText("lic.txt"),"G0disGre@t");

            // string[] dtt = Date.Split('/');
            // DateTime dt = new DateTime(Convert.ToInt32(dtt[0]), Convert.ToInt32(dtt[1]), Convert.ToInt32(dtt[2]));
            // if (DateTime.Now >= dt)
            // {
            //     MessageBox.Show("Licence Expired Contact support");
            //     Application.Exit();
            // }

            //}
            //catch (Exception ex)
            //{
            //    File.WriteAllText("Error", ex.StackTrace);
            //    Application.Exit();
            //}
            
        }        
      
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //update Login set Pwd='{0}' where LoginType='{1}' and UserName='{2}'
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPassword.Text)) return;
                var dbengine = new DBEngine();
                string pwd = dbengine.ExecuteQuery("select Pwd from Login where LoginType='Login' and UserName='RLYINMWB'");

                if (txtUserName.Text == "RLYINMWB" && txtPassword.Text == pwd)
                {
                    this.Hide();
                    new frmDefaultImage().Show();
                }
                else
                {
                    MessageBox.Show("UserName And Password Is Incorrect");
                    return;
                }
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        public string EncryptorDecrypt(string Text, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < Text.Length; c++)
                result.Append((char)((uint)Text[c] ^ (uint)key[c % key.Length]));
            return result.ToString();

        }
    }
}
