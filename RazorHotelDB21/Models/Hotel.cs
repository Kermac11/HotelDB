using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDB21.Models
{
    public class Hotel
    {
        /// <summary>
        /// Nummer på hotel for at identifiere dem
        /// </summary>
        public int HotelNr { get; set; }
        /// <summary>
        /// Navnet på hotellet
        /// </summary>
        public String Navn { get; set; }
        /// <summary>
        /// Adresse på hotellet
        /// </summary>
        public String Adresse { get; set; }

        public Hotel()
        {
        }

        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}
