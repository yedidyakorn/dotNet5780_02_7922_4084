using System;

namespace dotNet5780_02_7922_4084
{
    public class Host
    {
        public int _hostKey;
        public int _hostingUnitCollection;

        public Host(int hostKey, int hostingUnitCollection)
        {
            this._hostKey = hostKey;
            this._hostingUnitCollection = hostingUnitCollection;
        }

        internal void AssignRequests(params GuestRequest [] guestRequests)
        {
            throw new NotImplementedException();
        }

        internal void SortUnits()
        {
            throw new NotImplementedException();
        }
    }
}