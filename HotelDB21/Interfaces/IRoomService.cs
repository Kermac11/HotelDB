using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Models;

namespace HotelDBConsole21.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// henter alle værelser til et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Liste af værelser</returns>
        List<Room> GetAllRoom();

        public List<Room> GetAllRoomFromHotel(int hotelNr);

        /// <summary>
        /// Henter et specifik værelse fra database 
        /// </summary>
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Den fundne værelse eller null hvis værelset ikke findes</returns>
        Room GetRoomFromId(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et ny værelse i databasen
        /// </summary>
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        bool CreateRoom(int hotelNr, Room room);

        /// <summary>
        /// Opdaterer en værelset i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        bool UpdateRoom(Room room, int roomNr, int hotelNr);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Room DeleteRoom(int roomNr, int hotelNr);

    }
}
