using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    /// <summary>
    /// Dette er en Room class, den er tilknyttet et Hotel bagved i databasen.
    /// den indeholder ikke meget andet en nogle properties og 2 constructor
    /// </summary>
    public class Room
    {

        /// <summary>
        /// Nummer på rummet til at identificere dem
        /// </summary>
        public int Room_No { get; set; }
        /// <summary>
        /// Hvilken type rummet er
        /// </summary>
        public char Types { get; set; }
        /// <summary>
        /// Pris per nat
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Foreign key til at referere
        /// </summary>
        public int Hotel_No { get; set; }

        public Room()
        {

        }
        public Room(int nr, char types, double pris)
        {
            Room_No = nr;
            Types = types;
            Price = pris;
        }

        public Room(int nr, char types, double pris, int hotelNr) : this(nr, types, pris)
        {
            Hotel_No = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {Room_No}, Types = {Types}, Pris = {Price}";
        }
    }
}
