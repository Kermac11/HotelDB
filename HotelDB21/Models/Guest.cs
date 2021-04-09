using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    public class Guest
    {
        public int GuestNr { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        // To be implemented
        public Guest(int guestNo, string name, string address)
        {
            GuestNr = guestNo;
            Name = name;
            Address = address;
        }
    }
}
