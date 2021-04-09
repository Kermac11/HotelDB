using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        Task<List<Room>> GetAllRoomAsync();

        public Task<List<Room>> GetAllRoomFromHotelAsync(int hotelNr);

        /// <summary>
        /// Henter et specifik værelse fra database 
        /// </summary>
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Den fundne værelse eller null hvis værelset ikke findes</returns>
        Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et ny værelse i databasen
        /// </summary>
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        /// <summary>
        /// Opdaterer en værelset i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr);

        Task<List<Room>> SearchForRoomFilterAsync(char type, int pris, int hotelnumber);
        /// <summary>
        /// Denne metode laver en sql command man kan bruge til at hente en liste udfor forskellige kritrier
        /// </summary>
        /// <param name="searchlist"> Denne Parameter modtager en liste af keyvaluepairs der bruges til at sammesætte objecterne så de passer med properties i en Room class </param>
        /// <returns>Returnere en liste af Room klassen</returns>
        Task<List<Room>> TestSearchRoomsAsync(List<KeyValuePair<string,object>> searchlist);
    }
}
