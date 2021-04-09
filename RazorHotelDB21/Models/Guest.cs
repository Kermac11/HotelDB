using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    public class Guest
    {
        /// <summary>
        /// Nummer for hver gæst til at identificere dem
        /// </summary>
        public int Guest_No { get; set; }
        /// <summary>
        /// Navn på gæst
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Adresse på gæst
        /// </summary>
        public string Address { get; set; }

        public Guest(int guestNo, string name, string address)
        {
            Guest_No = guestNo;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Guest_NO: {Guest_No} | Name: {Name} | Address: {Address}";
        }
    }
}
