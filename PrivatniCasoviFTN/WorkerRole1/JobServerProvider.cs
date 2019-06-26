using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    class JobServerProvider : IContract
    {
        TableHelper tableHelper = new TableHelper("Cas");
        public void test()
        {
            Trace.WriteLine("USPEO");
        }
    }
}
