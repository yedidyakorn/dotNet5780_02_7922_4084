using System;
using System.Collections;
using System.Collections.Generic;

namespace dotNet5780_02_7922_4084
{
    public class Host:IEnumerable<HostingUnit>
    {
        public int _hostKey;
        public readonly List<HostingUnit> _hostingUnitCollection;

        public Host(int hostKey, int hostingUnitCollection)
        {
            this._hostKey = hostKey;
            _hostingUnitCollection = new List<HostingUnit>();
            for (int i=0; i< hostingUnitCollection;i++)
            {
                _hostingUnitCollection.Add(new HostingUnit());
            }
        }

        public override string ToString()
        {
            string ansewer = "";
            foreach (HostingUnit item in _hostingUnitCollection)
                ansewer += item.ToString()+"\n";
            return ansewer;
        }

        private int SubmitRequest(GuestRequest guestReq)
        {
            foreach (HostingUnit item in _hostingUnitCollection)
                if (item.ApproveRequest(guestReq))
                    return item._hostingUnitKey;
            return -1;
        }

        public int GetHostAnnualBusyDays()
        {
            int sum=0;
            foreach (HostingUnit item in _hostingUnitCollection)
                sum += item.GetAnnualBusyDays();
            return sum;
        }

        public void SortUnits()
        {
            _hostingUnitCollection.Sort();
        }

        public bool AssignRequests(params GuestRequest[] req)
        {
            bool result=true;
            for(int i=0;i<req.Length;i++)
            {
                foreach (HostingUnit item in _hostingUnitCollection)
                {
                    if (SubmitRequest(req[i]) > 0)
                        break;
                }
                if (!req[i]._isApproved)
                    result=false;
            }
            return result;
        }

        public IEnumerator<HostingUnit> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public HostingUnit this[int serialNb]
        {
            get
            {
                HostingUnit result = null;
                foreach (HostingUnit item in _hostingUnitCollection)
                {
                    if (item._hostingUnitKey == serialNb)
                    {
                        result = item;
                        break;
                    }
                }
                return result;
            }
        }

    }
}