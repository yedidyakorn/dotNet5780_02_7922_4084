/*
File: HostingUnit.cs
Description: object that represents one Hosting Unit
Course: c# mini project
Exercise 2
Author: Yedidya Korn-203304084 & Dovi Goldberg-301637922
*/


using System;
namespace dotNet5780_02_7922_4084

{
    public class HostingUnit : IComparable
    {
        private static int _stSerialKey = 10000000;
        public readonly int _hostingUnitKey;
        public bool[,] _diary;

        public HostingUnit()
        {
            _hostingUnitKey = _stSerialKey++;
            _diary = new bool[12, 31];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    _diary[i, j] = false;
        }

        public override string ToString()
        {
            string ansewer = "";
            ansewer += string.Format("unit ID number: {0}\n", _hostingUnitKey);
            ansewer += string.Format(showTaken(_diary));
            return ansewer;
        }

        public bool ApproveRequest(GuestRequest guestReq)
        {
            int begD, endD;
            bool[] arr = new bool[372];
            maxToArry(_diary, arr);//converts the calnder to one long array
            begD = (guestReq._entryDate.Month - 1) * 31 + (guestReq._entryDate.Day - 1);//begining day
            endD = (guestReq._releaseDate.Month - 1) * 31 + (guestReq._releaseDate.Day - 1);//ending day
            for (int i = begD + 1; i < endD - 2; i++)//checks if avalible
                if (arr[i] == true)
                    return false;
            for (int i = begD + 1; i < endD - 2; i++)
                arr[i] = true;
            arrayToMax(_diary, arr);//converts back to calnder
            guestReq._isApproved = true;
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

        public float GetAnnualBusyPrecentege()
        {
            return (((float)GetAnnualBusyDays() / 372) * 100);
        }

        private static string showTaken(bool[,] arr)
        {

            int day = 1, month = 1;
            string result = "";
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
                    result += string.Format("Start date: {0}/{1} \t", day, month);
                    isCounting = true;
                }
                //if vaction ended, isCounting = false
                //or its the last day of the year
                else if ((currentDay == false || (currentDay == true && day == 31 && month == 12)) && isCounting)
                {
                    result += string.Format("  End date: " + (day - 1 == 0 ? 31 : day - 1) + "/" + ((day - 1 == 0) ? month - 1 : month) + "\n");
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

        private static void maxToArry(bool[,] max, bool[] arr)//convorts a array to matrix 
        {
            int k = 0;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    arr[k] = max[i, j];
                    k++;
                }
            }
            return;
        }

        private static void arrayToMax(bool[,] max, bool[] arr)//convorts a matrix to array
        {
            int k = 0;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    max[i, j] = arr[k];
                    k++;
                }
            }
            return;
        }
    }
}