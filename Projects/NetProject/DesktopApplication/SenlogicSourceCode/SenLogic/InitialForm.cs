using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace SenLogic
{
    public partial class InitialForm : Form
    {

        public InitialForm()
        {
            InitializeComponent();
        }

        private void IntialForm_Load(object sender, EventArgs e)
        {
           // label3.Text = "Ver – " + System.Configuration.ConfigurationSettings.AppSettings.Get("Version") +"SLA-IMWS";
            label3.Text = "Ver - 4.2.32 SLA-IMWS";
            label4.Text = "All rights reserved to Senlogic Automation (P) Ltd.";
            
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            new Login().Show();
        }
        


      
    }
}
