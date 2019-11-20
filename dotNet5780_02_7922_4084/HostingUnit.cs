using System;
namespace dotNet5780_02_7922_4084

{
    public class HostingUnit: IComparable
    {
        private static int _stSerialKey= 10000000;
        public readonly int _hostingUnitKey;
        public bool[,] _diary;
        HostingUnit ()
        {
            _stSerialKey++;
            _diary = new bool[12, 31];
        }

        public override string ToString()
        {
            string ansewer = "";
            ansewer += string.Format("unit ID number: {0}" , _stSerialKey);
            ansewer += string.Format(showTaken(_diary));
            return ansewer;
        }

        public bool ApproveRequest(GuestRequest guestReq)
        {
            for(int i= guestReq._entryDate.Month-1;i<guestReq._releaseDate.Month-1;i++)
            {
                for(int j=guestReq._entryDate.Day-1;j<guestReq._releaseDate.Day-2;j++)
                {
                    if (_diary[i, j] == true)
                        return false;
                }
            }
            for (int i = guestReq._entryDate.Month - 1; i < guestReq._releaseDate.Month - 1; i++)
            {
                for (int j = guestReq._entryDate.Day - 1; j < guestReq._releaseDate.Day - 2; j++)
                {
                    _diary[i, j] = true;
                    guestReq._isApproved = true;
                }
            }
            return true;
        }

        public int GetAnnualBusyDays()//prints how many days are taken
        {
            int counter = 0;
            for (int i = 0; i < _diary.GetLength(0); i++)
                for (int j = 0; j < _diary.GetLength(1); j++)
                    if (_diary[i, j] == true)
                        counter++;     //Summarize the busy days
            float percentageOfOccupancy = (((float)counter / 372) * 100);   //Percentage calculation
            return counter;
        }

        public float GetAnnualBusyPercentage()
        {
            return (((float)GetAnnualBusyDays() / 372) * 100);
        }

        private static string showTaken(bool[,] arr)
        {

            int day = 1, month = 1;
            string result="";
            //flag for vacation period
            bool isCounting = false;

            while (month < 13)
            {
                //calc current day - Reset to 1 if over 31
                //set counting vaction period = true
                day = day % 31 == 0 ? 31 : day % 31;
                var currentDay = arr[month - 1, day - 1];
                if (currentDay == true && !isCounting)
                {
                    result+=string.Format("Start date : {0} / {1} \n",  day, month);
                    isCounting = true;
                }
                //if vaction ended, isCounting = false
                //or its the last day of the year
                else if ((currentDay == false || (currentDay == true && day == 31 && month == 12)) && isCounting)
                {
                    result += string.Format(" , End date : " + (day - 1 == 0 ? 31 : day - 1) + "/" + ((day - 1 == 0) ? month - 1 : month)+"\n");
                    isCounting = false;
                }
                //increase day 
                day++;
                //calc if month should be increased
                month += day / 32;
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            return GetAnnualBusyDays().CompareTo(((HostingUnit)obj).GetAnnualBusyDays());
        }

    }
}