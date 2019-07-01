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
        TableHelper tableHelper = new TableHelper(KLASE.PREDMET.ToString());
        public string test(string id)
        {
            List<CustomEntity> predmeti = new List<CustomEntity>()
            {
                new Predmet("PJISP"),
                new Predmet("SCADA"),
                new Predmet("RVA"),
                new Predmet("MISS"),
                new Predmet("HCI"),
            };

            tableHelper.InitTable(predmeti);

            Trace.WriteLine("USPEO");

            return ((Predmet)tableHelper.GetOne(id)).Naziv;
        }
    }
}
