/*
File: GuestRequest.cs
Description: object that represents a Request of a guest
Course: c# mini project
Exercise 2
Author: Yedidya Korn-203304084 & Dovi Goldberg-301637922
*/

using System;

namespace dotNet5780_02_7922_4084
{
    public class GuestRequest
    {
        public DateTime _entryDate { set; get; }  
        public DateTime _releaseDate { set; get; }  
        public bool _isApproved { set; get; }

        public override string ToString()       //returns a string with the object info
        {
            string ansewer = "";
            ansewer+= string.Format("entry date: {0}\trelease date: {1}\t", _entryDate,_releaseDate );
            ansewer+= string.Format("the requset is "+((_isApproved)? " ":"not ")+"approved\n");
            return ansewer;
        }
    }
}