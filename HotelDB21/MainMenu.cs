using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms
        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("1a) List hoteller Async");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("2a) Opret nyt Hotel");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("3a) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("4a) Søg efter hotel udfra hotelnr");
            Console.WriteLine("5) Opdater et hotel");
            Console.WriteLine("6) List alle værelser");
            Console.WriteLine("7) List alle værelser til et bestemt hotel");
            Console.WriteLine("8) Find et bestemt room efter RoomNr og HotelNr");
            Console.WriteLine("9) Skab et nyt rum til et hotel");
            Console.WriteLine("10) Lav ændring i et rum");
            Console.WriteLine("11) Slet et rum");
            Console.WriteLine("12) Find hotel ud fra navn");
            Console.WriteLine("13) List af alle gæster");
            Console.WriteLine("Q) Afslut");
        }

        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "1a":
                    ShowHotelsAsync();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "2a":
                    CreateHotelAsync();
                    return true;
                case "3":
                    DeleteHotel();
                    return true;
                case "3a":
                    DeleteHotelAsync();
                    return true;
                case "4":
                    SearchHotel();
                    return true;
                case "4a":
                    SearchHotelAsync();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "6":
                    GetAllRoom();
                    return true;
                case "7":
                    GetAllRoomFromHotelnr();
                    return true;
                case "8":
                    GetRoomFromRoomNrAndHotelnr();
                    return true;
                case "9":
                    CreateRoom();
                    return true;
                case "10":
                    UpdateRoom();
                    return true;
                case "11":
                    DeleteRoom();
                    return true;
                case "12":
                    GetHotelByName();
                    return true;
                case "13":
                    GetAllGuest();
                    return true;
                case "14":

                case "Q":
                case "q": return false;
                default: return true;
            }
        }

        private static void SearchHotelAsync()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr du leder efter");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            HotelServiceAsync hs = new HotelServiceAsync();
            Hotel search = hs.GetHotelFromIdAsync(hotelnr).Result;
            if (search != null)
            {
                Console.WriteLine($"HotelNr {search.HotelNr} Name {search.Navn} Address {search.Adresse}");
            }
            else
            {
                Console.WriteLine("There was an error in your search");
            }
        }

        private static void DeleteHotelAsync()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr der skal fjernes");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            HotelServiceAsync hs = new HotelServiceAsync();
            Hotel deleted = hs.DeleteHotelAsync(hotelnr).Result;
            if (deleted != null)
            {
                Console.WriteLine("Deletion Succesful");
            }
            else
            {
                Console.WriteLine("Error in Deletion");
            }
        }

        private static void CreateHotelAsync()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæs Hotel navn");
            string hotelnavn = Console.ReadLine();
            Console.WriteLine("Indlæs Hotel adresse");
            string hoteladresse = Console.ReadLine();
            HotelServiceAsync hs = new HotelServiceAsync();
            bool ok = hs.CreateHotelAsync((new Hotel(hotelnr, hotelnavn, hoteladresse))).Result;
            if (ok)
            {
                Console.WriteLine("Hotel Created");
            }
            else
            {
                Console.WriteLine("Error occured in creation");
            }
        }

        private static void ShowHotelsAsync()
        {
            Console.Clear();
            HotelServiceAsync hs = new HotelServiceAsync();
            List<Hotel> hotels = hs.GetAllHotelAsync().Result;
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
        }

        private static void GetAllGuest()
        {
            Console.Clear();
            GuestService gs = new GuestService();
            List<Guest> guests = gs.GetAllGuests();
            foreach (Guest guest in guests)
            {
                Console.WriteLine($"GuestNr {guest.GuestNr} Guest Navn {guest.Name} Guest Adress {guest.Address}");
            }
        }

        private static void GetHotelByName()
        {
            Console.Clear();
            Console.WriteLine("Indlæs navnet op hotellet du vil finde");
            string name = Console.ReadLine();
            HotelService hs = new HotelService();
            List<Hotel> hl = hs.GetHotelsByName(name);
            foreach (Hotel hotel in hl)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
        }

        private static void DeleteRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr hvor rummet skal fjernes");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs det Rumnr der skal fjernes");
            int roomnr = Convert.ToInt32(Console.ReadLine());
            RoomService rs = new RoomService();
            Room deleted = rs.DeleteRoom(roomnr, hotelnr);
            if (deleted != null)
            {
                Console.WriteLine("Deletion Succesful");
            }
            else
            {
                Console.WriteLine("Error in Deletion");
            }
        }

        private static void UpdateRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs HotelNr hvor rummet skal ændres");
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs ets RoomNr");
            int roomNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs den nye rum type, vælg mellem: F Family | D Double | S Single");
            char type = Console.ReadKey().KeyChar;
            switch (type.ToString().ToLower())
            {
                case "f":
                    type = 'F';
                    break;
                case "d":
                    type = 'D';
                    break;
                case "s":
                //type = 'S';
                //break;
                default:
                    type = 'S';
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Indlæs nye pris på rummet");
            double price = Convert.ToDouble(Console.ReadLine());
            Room nrm = new Room(roomNr, type, price, hotelNr);
            RoomService rs = new RoomService();
            bool ok = rs.UpdateRoom(nrm, nrm.RoomNr, nrm.HotelNr);
            if (ok)
            {
                Console.WriteLine("Rummet blev ændret");
            }
            else
            {
                Console.WriteLine("Der var en fejl i ændring af rummet");
            }
        }

        private static void CreateRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs HotelNr hvor rummet skal skabes");
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs ets RoomNr");
            int roomNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs dens rum type, vælg mellem: F Family | D Double | S Single");
            char type = Console.ReadKey().KeyChar;
            switch (type.ToString().ToLower())
            {
                case "f":
                    type = 'F';
                    break;
                case "d":
                    type = 'D';
                    break;
                case "s":
                //type = 'S';
                //break;
                default:
                    type = 'S';
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Indlæs Prisen på rummet");
            double price = Convert.ToDouble(Console.ReadLine());
            Room nrm = new Room(roomNr, type, price, hotelNr);
            RoomService rs = new RoomService();
            bool ok = rs.CreateRoom(nrm.HotelNr, nrm);
            if (ok)
            {
                Console.WriteLine("Rummet blev skabt");
            }
            else
            {
                Console.WriteLine("Der var en fejl i skabelsen af rummet");
            }

        }

        private static void GetRoomFromRoomNrAndHotelnr()
        {
            Console.Clear();
            Console.WriteLine("Indlæs det HotelNr du søger i");
            int searchHt = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs det RoomNr du søger efter");
            int searchRm = Convert.ToInt32(Console.ReadLine());
            RoomService rs = new RoomService();
            Room rm = rs.GetRoomFromId(searchHt, searchRm);
            Console.WriteLine($"HotelNr {rm.HotelNr} RoomNr {rm.RoomNr} Type {rm.Types} Pris {rm.Pris}");
        }

        private static void GetAllRoomFromHotelnr()
        {
            Console.Clear();
            Console.WriteLine("Indlæs det HotelNr rum du vil finde");
            int search = Convert.ToInt32(Console.ReadLine());
            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRoomFromHotel(search);
            foreach (Room room in rooms)
            {
                Console.WriteLine($"HotelNr {room.HotelNr} RoomNr {room.RoomNr} Type {room.Types} Pris {room.Pris}");
            }
        }

        private static void GetAllRoom()
        {
            Console.Clear();
            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRoom();
            foreach (Room room in rooms)
            {
                Console.WriteLine($"HotelNr {room.HotelNr} RoomNr {room.RoomNr} Type {room.Types} Pris {room.Pris}");
            }
        }

        private static void UpdateHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr på det hotel du vil ændre");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs nye Navn på hotellet");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs nye Adresse på hotellet");
            string adresse = Console.ReadLine();
            HotelService hs = new HotelService();
            bool ok = hs.UpdateHotel(new Hotel(hotelnr, navn, adresse), hotelnr);
            if (ok)
            {
                Console.WriteLine("Hotel got updated");
            }
            else
            {
                Console.WriteLine("There was an error in the update");
            }

        }

        private static void SearchHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr du leder efter");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            HotelService hs = new HotelService();
            Hotel search = hs.GetHotelFromId(hotelnr);
            if (search != null)
            {
                Console.WriteLine($"HotelNr {search.HotelNr} Name {search.Navn} Address {search.Adresse}");
            }
            else
            {
                Console.WriteLine("There was an error in your search");
            }
        }

        private static void DeleteHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr der skal fjernes");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            HotelService hs = new HotelService();
            Hotel deleted = hs.DeleteHotel(hotelnr);
            if (deleted != null)
            {
                Console.WriteLine("Deletion Succesful");
            }
            else
            {
                Console.WriteLine("Error in Deletion");
            }
        }

        private static void CreateHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs Hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæs Hotel navn");
            string hotelnavn = Console.ReadLine();
            Console.WriteLine("Indlæs Hotel adresse");
            string hoteladresse = Console.ReadLine();
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel((new Hotel(hotelnr, hotelnavn, hoteladresse)));
            if (ok)
            {
                Console.WriteLine("Hotel Created");
            }
            else
            {
                Console.WriteLine("Error occured in creation");
            }
        }

        private static void ShowHotels()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetAllHotel();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
        }
    }
}
