using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    
    public class Hotel
    {
        public int HotelNr { get; set; }
        public String Navn { get; set; }
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
