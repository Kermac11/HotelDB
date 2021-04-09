using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorHotelDB21.Models;

namespace RazorHotelDB21.Interfaces
{
    public interface IHotelService
    {
        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        /// <returns>Liste af hoteller</returns>
        Task<List<Hotel>> GetAllHotelAsync();

        /// <summary>
        /// Henter et specifik hotel fra database 
        /// </summary>
        /// <param name="hotelNr">Udpeger det hotel der ønskes fra databasen</param>
        /// <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        Task<Hotel> GetHotelFromIdAsync(int hotelNr);

        /// <summary>
        /// Indsætter et nyt hotel i databasen
        /// </summary>
        /// <param name="hotel">hotellet der skal indsættes</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateHotelAsync(Hotel hotel);

        /// <summary>
        /// Opdaterer en hotel i databasen
        /// </summary>
        /// <param name="hotel">De nye værdier til hotellet</param>
        /// <param name="hotelNr">Nummer på den hotel der skal opdateres</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr);

        /// <summary>
        /// Sletter et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Nummer på det hotel der skal slettes</param>
        /// <returns>Det hotel der er slettet fra databasen, returnere null hvis hotellet ikke findes</returns>
        Task<Hotel> DeleteHotelAsync(int hotelNr);

        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        /// <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        /// <returns></returns>
        Task<List<Hotel>> GetHotelsByNameAsync(string name);
    }
}
