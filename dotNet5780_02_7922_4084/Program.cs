using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5780_02_7922_4084
{
    class Program
    {

        static Random rand = new Random(DateTime.Now.Millisecond);
        private static GuestRequest CreateRandomRequest()
        {
            GuestRequest gs = new GuestRequest();
            //Fill randomally the Entry and Release dates of gs
            return gs;
        }
        static void Main(string[] args)
        {
            List<Host> lsHosts;
            lsHosts = new List<Host>()
             {
                 new Host(1, rand.Next(1,5)),
                 new Host(2, rand.Next(1,5)),
                 new Host(3, rand.Next(1,5)),
                 new Host(4, rand.Next(1,5)),
                 new Host(5, rand.Next(1,5))
             };
            for (int i = 0; i < 100; i++)
            {
                GuestRequest gs1 = new GuestRequest();
                GuestRequest gs2 = new GuestRequest();
                GuestRequest gs3 = new GuestRequest();
                foreach (var host in lsHosts)
                {
                    if (!gs1._isApproved)
                        gs1 = CreateRandomRequest();
                    if (!gs2._isApproved)
                        gs2 = CreateRandomRequest();
                    if (!gs3._isApproved)
                        gs3 = CreateRandomRequest();
                    switch (rand.Next(1, 4))
                    {
                        case 1:
                            host.AssignRequests(gs1);
                            break;
                        case 2:
                            host.AssignRequests(gs1, gs2);
                            break;
                        case 3:
                            host.AssignRequests(gs1, gs2, gs3);
                            break;
                        default:
                            break;
                    }
                }
            }
            //Create dictionary for all units <unitkey, occupancy_percentage>
            Dictionary<long, float> dict = new Dictionary<long, float>();
            foreach (var host in lsHosts)
            {
                //test Host IEnuramble is ok
                foreach (HostingUnit unit in host)
                {
                    dict[unit._hostingUnitKey] = unit.GetAnnualBusyPercentage();
                }
            }
            //get max value in dictionary
            float maxVal = dict.Values.Max();
            //get max value key name in dictionary
            long maxKey =
           dict.FirstOrDefault(x => x.Value == dict.Values.Max()).Key;
            //find the Host that its unit has the maximum occupancy percentage
            foreach (var host in lsHosts)
            {
                //test indexer of Host
                for (int i = 0; i < host._hostingUnitCollection; i++)
                {
                    if (host[i].HostingUnitKey == maxKey)
                    {
                        //sort this host by occupancy of its units
                        host.SortUnits();
                        //print this host detailes
                        Console.WriteLine("**** Details of the Host with the most occupied unit:\n");
                        
                         Console.WriteLine(host);
                        break;
                    }
                }
            }
        }
    }
}