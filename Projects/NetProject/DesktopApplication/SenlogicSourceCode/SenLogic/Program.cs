using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SenLogic
{
    static class Program
    {
        /// <summary>5
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new StaticWeighment());
            //Application.Run(new Report());
            //Application.Run(new frmDefaultImage());
            //Application.Run(new ManualTareEntry());
            //Application.Run(new View());
            Application.Run(new InitialForm());
        }
    }
}
