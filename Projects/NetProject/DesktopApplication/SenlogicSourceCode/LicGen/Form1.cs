using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LicGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "GannetTech@9876")
            {
                File.WriteAllText("lic.txt", EncryptorDecrypt(textBox2.Text, "G0disGre@t"));
                MessageBox.Show("Licence Successfully Updated");

            }
            else
            {
                MessageBox.Show("Enter The Password Correltly");
            }
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
