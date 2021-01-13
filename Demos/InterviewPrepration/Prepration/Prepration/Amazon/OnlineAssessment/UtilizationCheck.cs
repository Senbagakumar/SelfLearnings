using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class UtilizationCheck
    {
        private static int Limit = 2 * 100_100_1000;
        private static int UtilizationChecks(int instances, List<int> utilizationUtil)
        {
            for (int i = 0; i < utilizationUtil.Count; i++)
            {
                int util = utilizationUtil[i];
                if (util < 25 && instances > 1)
                {
                    instances = instances / 2 + (instances & 1);
                    i += 10;
                }
                else if (util > 60)
                {
                    int newInstances = instances * 2;
                    if (newInstances <= Limit)
                    {
                        instances = newInstances;
                        i += 10;
                    }
                }
            }
            return instances;
        }
    }
}
