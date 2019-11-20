using System;

namespace dotNet5780_02_7922_4084
{
    public class GuestRequest
    {
        public DateTime _entryDate { set; get; }  
        public DateTime _releaseDate { set; get; }  
        public bool _isApproved { set; get; }

        public override string ToString()
        {
            string ansewer = "";
            ansewer+= string.Format("entry date: {0} \trelease date: {1}\t", _entryDate,_releaseDate );
            ansewer+= string.Format("the requset is "+((_isApproved)? " ":"not ")+"approved\n");
            return ansewer;
        }
    }
}