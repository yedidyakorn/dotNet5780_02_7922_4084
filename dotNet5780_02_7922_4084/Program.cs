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
            int duration = rand.Next(2, 10);
            int month = rand.Next(1, 13);
            int days = rand.Next(1, 31);
            if (month == 2)
                days = rand.Next(1, 29);
            gs._entryDate = new DateTime(2019, month, days);
            gs._releaseDate = gs._entryDate.AddDays((double)duration);
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
                foreach (var host in lsHosts)
                {
                    GuestRequest gs1 = CreateRandomRequest();
                    GuestRequest gs2 = CreateRandomRequest();
                    GuestRequest gs3 = CreateRandomRequest();
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
            Dictionary<int, float> dict = new Dictionary<int, float>();
            foreach (var host in lsHosts)
            {
                //test Host IEnuramble is ok
                foreach (HostingUnit unit in host)
                {
                    dict[unit._hostingUnitKey] = unit.GetAnnualBusyPrecentege();
                }
            }
            //get max value in dictionary
            float maxVal = dict.Values.Max();
            //get max value key name in dictionary
            int maxKey = dict.FirstOrDefault(x => x.Value == dict.Values.Max()).Key;
            //find the Host that its unit has the maximum occupancy percentage
            foreach (var host in lsHosts)
            {
                //test indexer of Host
                for (int i = 0; i < host._hostingUnitCollection.Count; i++)
                {
                    if (host[i]._hostingUnitKey == maxKey)
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
            Console.ReadKey();
        }
    }
}