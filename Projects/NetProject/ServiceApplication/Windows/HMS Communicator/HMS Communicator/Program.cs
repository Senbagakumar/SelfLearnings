using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Communicator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {            
            #if DEBUG
                        var hms = new HMService();
                        hms.OnExecutor();
            #else
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[]
                        {
                            new HMService()
                        };
                        ServiceBase.Run(ServicesToRun);
      
            #endif
        }
    }
}
